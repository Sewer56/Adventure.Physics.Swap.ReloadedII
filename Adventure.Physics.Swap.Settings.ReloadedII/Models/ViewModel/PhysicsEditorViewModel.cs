using System;
using System.Collections.ObjectModel;
using System.Linq;
using Adventure.Physics.Swap.Settings.ReloadedII.Models.Model;
using Adventure.Physics.Swap.Shared.Configs;
using Adventure.Physics.Swap.Shared.Structs;
using Reloaded.WPF.MVVM;

namespace Adventure.Physics.Swap.Settings.ReloadedII.Models.ViewModel
{
    public class PhysicsEditorViewModel : ObservableObject
    {
        public CharacterAdventurePhysicsPair CurrentPhysics { get; set; }
        public ObservableCollection<CharacterAdventurePhysicsPair> Physics  { get; set; } = new ObservableCollection<CharacterAdventurePhysicsPair>();
        public Config Config { get; private set; }

        public PhysicsEditorViewModel(Config config)
        {
            Config = config;
            foreach (var phys in config.Physics)
                Physics.Add(new CharacterAdventurePhysicsPair(phys.Key, phys.Value));
            
            CurrentPhysics = Physics.First();
        }

        public void Save()
        {
            foreach (var phys in Physics)
            {
                Config.Physics[phys.Character] = phys.Physics;
            }

            Config.ToJson(Environment.CurrentDirectory);
        }
    }
}
