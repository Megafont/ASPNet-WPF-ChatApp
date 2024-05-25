using System.Globalization;
using System.Windows;

namespace ASPNet_WPF_ChatApp.ValueConverters
{
    /// <summary>
    /// A converter that takes in a core <see cref="ElementHorizontalAlignment"/> enum value and converts it to a WPF HorizontalAlignment value.
    /// </summary>
    public class HorizontalAlignmentConverter : BaseValueConverter<HorizontalAlignmentConverter>
    {
        public override object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return (HorizontalAlignment)value;
        }

        public override object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
