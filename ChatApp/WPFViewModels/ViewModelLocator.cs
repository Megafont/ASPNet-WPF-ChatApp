using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using ASPNet_WPF_ChatApp.Core.DependencyInjection;
using ASPNet_WPF_ChatApp.ViewModels;
using ASPNet_WPF_ChatApp.ViewModels.Application;

// This makes it so we can access members on this static class without needing to write "CoreDI." first.
using static ASPNet_WPF_ChatApp.DependencyInjection.ChatAppDI;

namespace ASPNet_WPF_ChatApp.WPFViewModels
{
    /// <summary>
    /// Locates view models from the IoC container for use in binding in XAML files.
    /// </summary>
    public class ViewModelLocator
    {
        #region Public Properties

        /// <summary>
        /// Singleton instance of the locator
        /// </summary>
        public static ViewModelLocator Instance { get; private set; } = new ViewModelLocator();

        /// <summary>
        /// The application view model
        /// </summary>
        public static ApplicationViewModel ApplicationViewModel => ViewModel_Application;

        /// <summary>
        /// The settings view model
        /// </summary>
        public static SettingsViewModel SettingsViewModel => ViewModel_Settings;

        #endregion

    }
}
