using System.Globalization;
using System.Windows;

namespace ASPNet_WPF_ChatApp.ValueConverters
{
    /// <summary>
    /// A converter that takes in a boolean and returns a <see cref="Visibility"/>
    /// where false is <see cref="Visibility.Collapsed"/>
    /// </summary>
    public class BooleanToVisibilityGoneConverter : BaseValueConverter<BooleanToVisibilityGoneConverter>
    {
        public override object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (parameter == null)
                return (bool) value ? Visibility.Visible : Visibility.Collapsed;
            else
                return (bool) value ? Visibility.Collapsed : Visibility.Visible;
        }

        public override object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
