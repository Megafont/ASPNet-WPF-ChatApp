using ASPNet_WPF_ChatApp.Core.DataModels;
using System.Globalization;
using System.Windows;

namespace ASPNet_WPF_ChatApp.ValueConverters
{
    /// <summary>
    /// A converter that takes in a <see cref="MenuItemTypes"/> value and returns a <see cref="Visibility"/>
    /// based on the given parameter value being the same as the menu item type
    /// </summary>
    public class MenuItemTypeToVisibilityConverter : BaseValueConverter<MenuItemTypeToVisibilityConverter>
    {
        public override object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            // If we have no parameter, then return invisible
            if (parameter == null)
                return Visibility.Collapsed;

            // Try and convert parameter string to enum
            if (!Enum.TryParse(parameter as string, out MenuItemTypes type))
                return Visibility.Collapsed;

            // Return visible if the parameter matches the type
            return (MenuItemTypes) value == type ? Visibility.Visible : Visibility.Collapsed;
        }

        public override object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
