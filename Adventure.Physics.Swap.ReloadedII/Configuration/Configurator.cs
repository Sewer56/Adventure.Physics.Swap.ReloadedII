using System.Diagnostics;
using Reloaded.Mod.Interfaces;

namespace Adventure.Physics.Swap.ReloadedII.Configuration;

public class Configurator : IConfiguratorV1
{
    public string ModFolder { get; private set; } = null!;

    public Configurator() { }
    public Configurator(string modDirectory) : this()
    {
        ModFolder = modDirectory;
    }

    /* IConfigurator. */
    public void SetModDirectory(string modDirectory) => ModFolder = modDirectory;
    public IConfigurable[] GetConfigurations() => null!;

    /// <summary>
    /// Allows for custom launcher/configurator implementation.
    /// If you have your own configuration program/code, run that code here and return true, else return false.
    /// </summary>
    public bool TryRunCustomConfiguration()
    {
        var executable = Path.Combine(ModFolder, "Config.dll");
        var processStartInfo = new ProcessStartInfo()
        {
            FileName = "dotnet",
            Arguments = $"\"{executable}\"",
            WorkingDirectory = ModFolder,
            CreateNoWindow = true
        };

        Process.Start(processStartInfo);
        return true;
    }
}