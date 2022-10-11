using System.Collections.ObjectModel;
using Adventure.Physics.Swap.Settings.ReloadedII.Models.Model;
using Adventure.Physics.Swap.Shared.Configs;
using Reloaded.WPF.MVVM;
using ObservableObject = Reloaded.WPF.MVVM.ObservableObject;

namespace Adventure.Physics.Swap.Settings.ReloadedII.Models.ViewModel;

public class PhysicsEditorViewModel : ObservableObject
{
    public CharacterAdventurePhysicsPair CurrentPhysics { get; set; } = null!;
    public ObservableCollection<CharacterAdventurePhysicsPair> Physics  { get; set; } = new ObservableCollection<CharacterAdventurePhysicsPair>();
    public Config Config { get; private set; }

    public PhysicsEditorViewModel(Config config)
    {
        Config = config;
        GetPhysicsFromConfig();
    }

    public void Save()
    {
        foreach (var phys in Physics) 
            Config.Physics[phys.Character] = phys.Physics;

        Config.ToJson(Environment.CurrentDirectory);
    }

    public byte[] ToBytes() => Config.ToBytes();
    public void Load(string filePath)
    {
        Config.ImportFile(filePath);
        GetPhysicsFromConfig();
    }

    private void GetPhysicsFromConfig()
    {
        Physics.Clear();

        foreach (var phys in Config.Physics)
            Physics.Add(new CharacterAdventurePhysicsPair(phys.Key, phys.Value));

        CurrentPhysics = Physics.First();
    }
}