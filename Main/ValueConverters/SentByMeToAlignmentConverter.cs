using System.Globalization;
using System.Windows;

namespace ASPNet_WPF_ChatApp.ValueConverters
{
    /// <summary>
    /// A converter that takes in a boolean. If the message was sent by me, it aligns it to the right
    /// If not sent by me, it aligns it to the left
    /// </summary>
    public class SentByMeToAlignmentConverter : BaseValueConverter<SentByMeToAlignmentConverter>
    {
        public override object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return (bool) value ? HorizontalAlignment.Right : HorizontalAlignment.Left;
        }

        public override object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
