using System;
using Adventure.Physics.Swap.Shared.Enums;
using Adventure.Physics.Swap.Shared.Structs;
using Reloaded.WPF.MVVM;

namespace Adventure.Physics.Swap.Settings.ReloadedII.Models.Model
{
    public class CharacterAdventurePhysicsPair : ObservableObject
    {
        public AllCharacters Character  { get; set; }
        public AdventurePhysics Physics { get; set; }

        public CharacterAdventurePhysicsPair(AllCharacters character, AdventurePhysics physics)
        {
            Character = character;
            Physics = physics;
        }


        public override string ToString()
        {
            return Enum.GetName(typeof(AllCharacters), Character);
        }
    }
}
