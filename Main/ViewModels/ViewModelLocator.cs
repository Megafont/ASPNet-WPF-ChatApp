using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using ASPNet_WPF_ChatApp.Core.InversionOfControl.Base;
using ASPNet_WPF_ChatApp.Core.ViewModels;

namespace ASPNet_WPF_ChatApp.ViewModels
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
        public static ApplicationViewModel ApplicationViewModel => IoC.ApplicationViewModel;

        /// <summary>
        /// The settings view model
        /// </summary>
        public static SettingsViewModel SettingsViewModel => IoC.SettingsViewModel;

        #endregion

    }
}
