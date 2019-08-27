using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Markup;

namespace Adventure.Physics.Swap.Settings.ReloadedII.Extensions
{
    public class EnumBindingSourceExtension : MarkupExtension
    {
        private Type _enumType;
        public Type EnumType
        {
            get => this._enumType;
            set
            {
                if (value == this._enumType)
                    return;

                if (value != null)
                {
                    Type enumType = Nullable.GetUnderlyingType(value) ?? value;
                    if (!enumType.IsEnum)
                        throw new ArgumentException("Not an enumerable type.");
                }

                this._enumType = value;
            }
        }

        public EnumBindingSourceExtension() { }
        public EnumBindingSourceExtension(Type enumType)
        {
            this.EnumType = enumType;
        }

        public override object ProvideValue(IServiceProvider serviceProvider)
        {
            if (null == this._enumType)
                throw new InvalidOperationException($"The {nameof(EnumType)} must be specified.");

            Type actualEnumType = Nullable.GetUnderlyingType(this._enumType) ?? this._enumType;
            Array enumValues    = Enum.GetValues(actualEnumType);

            if (actualEnumType == this._enumType)
                return enumValues;

            Array tempArray = Array.CreateInstance(actualEnumType, enumValues.Length + 1);
            enumValues.CopyTo(tempArray, 1);
            return tempArray;
        }
    }
}
