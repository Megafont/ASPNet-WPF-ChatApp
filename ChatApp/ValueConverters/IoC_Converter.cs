using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Globalization;

using ASPNet_WPF_ChatApp.DependencyInjection;
using ASPNet_WPF_ChatApp.ViewModels;
using ASPNet_WPF_ChatApp.ViewModels.Application;
using ASPNet_WPF_ChatApp.Pages;

// This makes it so we can access members on this static class without needing to write "CoreDI." first.
using static ASPNet_WPF_ChatApp.DependencyInjection.ChatAppDI;


namespace ASPNet_WPF_ChatApp.ValueConverters
{
    /// <summary>
    /// Converts a string name to a service pulled from the IoC container
    /// </summary>
    public class IoC_Converter : BaseValueConverter<IoC_Converter>
    {
        public override object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            // Find the appropriate page
            switch ((string) parameter)
            {
                case nameof(ApplicationViewModel):
                    return ViewModel_Application;

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
