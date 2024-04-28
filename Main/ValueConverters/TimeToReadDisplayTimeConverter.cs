using System.Globalization;
using System.Windows;

namespace ASPNet_WPF_ChatApp.ValueConverters
{
    /// <summary>
    /// A converter that takes in a <see cref="DateTimeOffset"/> and converts it to a message read time display time
    /// </summary>
    public class TimeToReadDisplayTimeConverter : BaseValueConverter<TimeToReadDisplayTimeConverter>
    {
        public override object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            DateTimeOffset time = (DateTimeOffset)value;


            // If it is not read...
            if (time == DateTimeOffset.MinValue)
            {
                // Show nothing
                return string.Empty;
            }


            // If it is today...
            if (time.Date == DateTimeOffset.UtcNow.Date)
            {
                // Return just the time
                return $"Read at {time.ToLocalTime().ToString("hh:mm tt")}";
            }


            // Otherwise, return a full date
            return $"Read at {time.ToLocalTime().ToString("hh:mm tt, MMM yyyy")}";
        }

        public override object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
