using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using ASPNet_WPF_ChatApp.Pages;
using ASPNet_WPF_ChatApp.Core.DataModels;


namespace ASPNet_WPF_ChatApp.ValueConverters
{
    /// <summary>
    /// Converts a <see cref="ApplicationPages"/> value to an actual view/page
    /// </summary>
    public class ApplicationPageValueConverter : BaseValueConverter<ApplicationPageValueConverter>
    {
        public override object Convert(object value, Type targetType = null, object parameter = null, CultureInfo culture = null)
        {
            // Find the appropriate page
            switch ((ApplicationPages) value)
            {
                case ApplicationPages.Login:
                    return new LoginPage();

                case ApplicationPages.Chat:
                    return new ChatPage();

                case ApplicationPages.Register:
                    return new RegisterPage();

                default:
                    Debugger.Break();
                    return null;
            }
        }

        public override object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
