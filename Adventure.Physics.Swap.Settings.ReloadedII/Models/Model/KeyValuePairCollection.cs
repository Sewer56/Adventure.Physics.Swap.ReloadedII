using System.Collections.Generic;
using System.Collections.ObjectModel;
using Reloaded.WPF.MVVM;

namespace Adventure.Physics.Swap.Settings.ReloadedII.Models.Model
{
    /// <summary>
    /// Deconstructs a Key/Value observable concurrent dictionary into a bindable set of Key-Value pairs.
    /// </summary>
    public class KeyValuePairCollection<TSourceEnum, TTargetEnum> : ObservableObject
    {
        public ObservableCollection<KeyValuePair<TSourceEnum, TTargetEnum>> Pairs { get; set; } = new ObservableCollection<KeyValuePair<TSourceEnum, TTargetEnum>>();
        
        public KeyValuePairCollection(Dictionary<TSourceEnum, TTargetEnum> dictionary)
        {
            foreach (var pair in dictionary)
            {
                Pairs.Add(new KeyValuePair<TSourceEnum, TTargetEnum>(pair.Key, pair.Value));
            }
        }

        /// <summary>
        /// Converts the set of key-value pairs back into a dictionary.
        /// </summary>
        public Dictionary<TSourceEnum, TTargetEnum> AsDictionary()
        {
            var dictionary = new Dictionary<TSourceEnum, TTargetEnum>();
            foreach (var pair in Pairs)
            {
                dictionary[pair.Key] = pair.Value;
            }

            return dictionary;
        }
    }
}
