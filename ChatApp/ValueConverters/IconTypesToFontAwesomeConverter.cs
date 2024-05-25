using ASPNet_WPF_ChatApp.Core.DataModels;
using ASPNet_WPF_ChatApp.Core.StyleConverters;
using System.Globalization;
using System.Windows;

namespace ASPNet_WPF_ChatApp.ValueConverters
{
    /// <summary>
    /// A converter that takes in a <see cref="IconTypes"/> value and returns
    /// the FontAwesome string for that icon
    /// </summary>
    public class IconTypesToFontAwesomeConverter : BaseValueConverter<IconTypesToFontAwesomeConverter>
    {
        public override object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return ((IconTypes)value).ToFontAwesome();
        }

        public override object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
