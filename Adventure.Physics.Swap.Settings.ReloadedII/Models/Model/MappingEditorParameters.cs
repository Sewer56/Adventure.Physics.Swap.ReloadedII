using System;
using System.Collections.Generic;

namespace Adventure.Physics.Swap.Settings.ReloadedII.Models.Model
{
    public class MappingEditorParameters<T, U>
    {
        public Dictionary<T, U> Mappings              { get; set; }
        public Action<Dictionary<T, U>> ApplyToConfig { get; set; }

        public MappingEditorParameters(Dictionary<T, U> mappings, Action<Dictionary<T, U>> applyToConfig)
        {
            Mappings = mappings;
            ApplyToConfig = applyToConfig;
        }
    }
}
