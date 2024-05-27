using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Dna;

using ASPNet_WPF_ChatApp.Core.DependencyInjection.Interfaces;
using ASPNet_WPF_ChatApp.DependencyInjection.UI;
using ASPNet_WPF_ChatApp.ViewModels.Application;

namespace ASPNet_WPF_ChatApp.DependencyInjection
{
    /// <summary>
    /// A shorthand access class to get DI services with nice clean short code
    /// </summary>
    public static class ChatAppDI
    {
        /// <summary>
        /// A shortcut to access the <see cref="IUIManager"/>
        /// </summary>
        public static IUIManager UI => (IUIManager) Framework.Provider.GetService(typeof(IUIManager));

        /// <summary>
        /// A shortcut to access the <see cref="ViewModel_Application"/>
        /// </summary>
        public static ApplicationViewModel ViewModel_Application => (ApplicationViewModel)Framework.Provider.GetService(typeof(ApplicationViewModel));

        /// <summary>
        /// A shortcut to access the <see cref="ViewModel_Settings"/>
        /// </summary>
        public static SettingsViewModel ViewModel_Settings => (SettingsViewModel)Framework.Provider.GetService(typeof(SettingsViewModel));

        /// <summary>
        /// A shortcut access to the <see cref="IClientDataStore"/>
        /// </summary>
        public static IClientDataStore ClientDataStore => (IClientDataStore)Framework.Provider.GetService(typeof(IClientDataStore));
    }
}
