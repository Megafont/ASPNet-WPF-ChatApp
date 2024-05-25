using System.Globalization;
using System.Windows;

using ASPNet_WPF_ChatApp.Core.ViewModels.PopupMenus;
using ASPNet_WPF_ChatApp.Controls.Menus;

namespace ASPNet_WPF_ChatApp.ValueConverters
{
    /// <summary>
    /// A converter that takes in a <see cref="BaseViewModel"/> and returns the specific UI control
    /// that should bind to tha ttype of ViewModel
    /// </summary>
    public class PopupContentConverter : BaseValueConverter<PopupContentConverter>
    {
        public override object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value is ChatAttachmentPopupMenuViewModel basePopup)
                return new VerticalMenu { DataContext = basePopup.Content };

            return null;
        }

        public override object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
