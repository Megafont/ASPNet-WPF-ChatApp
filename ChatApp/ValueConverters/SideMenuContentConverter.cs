using System.Globalization;
using System.Windows;
using ASPNet_WPF_ChatApp.Controls.Chat.ChatList;
using ASPNet_WPF_ChatApp.Core.DataModels;

namespace ASPNet_WPF_ChatApp.ValueConverters
{
    /// <summary>
    /// A converter that takes a <see cref="SideMenuContent"/> value and converts it to the
    /// correct UI element
    /// </summary>
    public class SideMenuContentConverter : BaseValueConverter<SideMenuContentConverter>
    {
        #region Protected Memebers

        /// <summary>
        /// An instance of the current chat list control
        /// </summary>
        protected ChatListControl _ChatListControl = new ChatListControl();

        #endregion

        public override object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            // Get the side menu content type
            var sideMenuType = (SideMenuContent) value;

            // Switch based on type
            switch (sideMenuType)
            {
                // Chat
                case SideMenuContent.Chat:
                    return _ChatListControl;

                // Unknown type
                default:
                    return "No UI here yet, sorry.";
            }
        }

        public override object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
