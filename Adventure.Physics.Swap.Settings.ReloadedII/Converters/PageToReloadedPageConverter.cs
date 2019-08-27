using System;
using System.Diagnostics;
using System.Globalization;
using System.Windows.Data;
using Adventure.Physics.Swap.Settings.ReloadedII.Pages;
using Reloaded.WPF.Theme.Default;

namespace Adventure.Physics.Swap.Settings.ReloadedII.Converters
{
    [ValueConversion(typeof(ApplicationPage), typeof(ReloadedPage))]
    public class PageToReloadedPageConverter : IValueConverter
    {
        public static PageToReloadedPageConverter Instance = new PageToReloadedPageConverter();

        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            switch ((ApplicationPage)value)
            {
                case ApplicationPage.PhysicsEditor:
                    return IoC.GetConstant<PhysicsEditor>();

                case ApplicationPage.SadxMappingEditor:
                    return IoC.Get<SadxMappingEditor>();

                case ApplicationPage.Sa2bMappingEditor:
                    return IoC.Get<Sa2bMappingEditor>();

                case ApplicationPage.HeroesMappingEditor:
                    return IoC.Get<HeroesMappingEditor>();

                default:
                    Debugger.Break();
                    return null;
            }
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
