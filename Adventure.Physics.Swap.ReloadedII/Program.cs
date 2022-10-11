using Adventure.Physics.Swap.ReloadedII.Configuration;
using Adventure.Physics.Swap.ReloadedII.Enums;
using Adventure.Physics.Swap.Shared.Configs;
using Reloaded.Mod.Interfaces;
using Reloaded.Mod.Interfaces.Internal;

namespace Adventure.Physics.Swap.ReloadedII;

public class Program : IMod
{
    private const string ThisModId = "sonicadventureengine.physicsswap";
    private IModLoader _modLoader = null!;
    private ILogger _logger = null!;
    private string _modDirectory = null!;

    private Config _config = null!;
    private PhysicsMod _physicsMod = null!;
    private FileSystemWatcher _watcher = null!;
    private Task _setupPhysicsSwap = null!;

    public void Start(IModLoaderV1 loader)
    {
        _modLoader = (IModLoader)loader;
        _logger = (ILogger) _modLoader.GetLogger();

        /* Your mod code starts here. */
        // Load mod asynchronously during the same time other mods load. (Newtonsoft.Json is slow on first run)
        // Then wait if necessary if mod loader is done with other mods.
        _modLoader.OnModLoaderInitialized += OnModLoaderInitialized;
        _setupPhysicsSwap = Task.Run(() =>
        {
            _modDirectory = _modLoader.GetDirectoryForModId(ThisModId);
            _config = Config.FromJson(_modDirectory);
            _physicsMod = new PhysicsMod();
            _physicsMod.ApplyFromConfig(_config);

            string filePath = Config.GetFilePath(_modDirectory);
            _watcher = new FileSystemWatcher(Path.GetDirectoryName(filePath)!, Path.GetFileName(filePath));
            _watcher.EnableRaisingEvents = true;
            _watcher.Changed += WatcherOnChanged;
        });
    }

    private void OnModLoaderInitialized()
    {
        _setupPhysicsSwap.Wait();
        _modLoader.OnModLoaderInitialized -= OnModLoaderInitialized;
    }

    private void WatcherOnChanged(object sender, FileSystemEventArgs e)
    {
        string note = " ";
        switch (_physicsMod.Game)
        {
            case Game.Sadx:
            case Game.Sa2b:
                note += "SADX/SA2: Re-launch stage to take effect.";
                break;
            case Game.Heroes:
                note += "Heroes: Switch characters to take effect.";
                break;
        }

        _logger.WriteLine($"[AdventurePhysicsSwap] Config file changed. Reapplying config. {note}", _logger.ColorGreen);
        _config = Utilities.TryGetValue(() => Config.FromJson(_modDirectory), 250, 2);
        _physicsMod.ApplyFromConfig(_config);
    }

    /* Mod loader actions. */
    public void Suspend() => _physicsMod.RestorePhysics();
    public void Resume()  => _physicsMod.ApplyFromConfig(_config);
    public void Unload()  => Suspend();

    public bool CanUnload()  => true;
    public bool CanSuspend() => true;

    /* Automatically called by the mod loader when the mod is about to be unloaded. */
    public Action Disposing { get; } = () => { };
}