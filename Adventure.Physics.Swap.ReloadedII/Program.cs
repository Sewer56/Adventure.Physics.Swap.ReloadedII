﻿using System;
using System.Diagnostics;
using System.IO;
using System.Threading.Tasks;
using Adventure.Physics.Swap.ReloadedII.Configuration;
using Adventure.Physics.Swap.ReloadedII.Enums;
using Adventure.Physics.Swap.Shared.Configs;
using Reloaded.Mod.Interfaces;
using Reloaded.Mod.Interfaces.Internal;

namespace Adventure.Physics.Swap.ReloadedII
{
    public class Program : IMod
    {
        private const string ThisModId = "sonicadventureengine.physicsswap";
        private IModLoader      _modLoader;
        private ILogger         _logger;
        private string          _modDirectory;

        private Config          _config;
        private PhysicsMod      _physicsMod;
        private FileSystemWatcher _watcher;

        private Task _setupPhysicsSwap;

        public static void Main() { }
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
                _watcher = new FileSystemWatcher(Path.GetDirectoryName(filePath), Path.GetFileName(filePath));
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
            if (_physicsMod.Game == Game.Sadx || _physicsMod.Game == Game.Sa2b)
                note += "SADX/SA2: Re-launch stage to take effect.";
            else if (_physicsMod.Game == Game.Heroes)
                note += "Heroes: Switch characters to take effect.";

            _logger.WriteLine($"[AdventurePhysicsSwap] Config file changed. Reapplying config.{note}", _logger.ColorGreen);
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
        public Action Disposing { get; }
    }
}
