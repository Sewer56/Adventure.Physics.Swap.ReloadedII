using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Reloaded.WPF.MVVM;

namespace Adventure.Physics.Swap.Settings.ReloadedII.Models.Model
{
    public class KeyValuePair<TKey, TValue> : ObservableObject
    {
        public TKey Key { get; set; }
        public TValue Value { get; set; }

        public KeyValuePair(TKey key, TValue value)
        {
            Key = key;
            Value = value;
        }
    }
}
