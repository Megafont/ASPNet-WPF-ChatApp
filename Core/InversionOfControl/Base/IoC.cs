using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Dna;
using Ninject;

using ASPNet_WPF_ChatApp.Core.DataModels;
using ASPNet_WPF_ChatApp.Core.ViewModels;
using ASPNet_WPF_ChatApp.Core.ViewModels.Application;
using ASPNet_WPF_ChatApp.Core.InversionOfControl.Interfaces;
using System.Diagnostics;


namespace ASPNet_WPF_ChatApp.Core.InversionOfControl.Base
{
    /// <summary>
    /// The Inversion of Control container for our application
    /// </summary>
    public static class IoC
    {
        #region Public Properties

        /// <summary>
        /// The kernel for our IoC container
        /// </summary>
        public static IKernel Kernel { get; private set; } = new StandardKernel();

        /// <summary>
        /// A shortcut to access the <see cref="IUIManager"/>
        /// </summary>
        public static IUIManager UI => Get<IUIManager>();

        /// <summary>
        /// A shortcut to access the <see cref="ILogFactory"/>
        /// </summary>
        public static ILogFactory Logger => Get<ILogFactory>();

        /// <summary>
        /// A shortcut to access the <see cref="IFileManager"/>
        /// </summary>
        public static IFileManager FileManager => Get<IFileManager>();

        /// <summary>
        /// A shortcut to access the <see cref="ITaskManager"/>
        /// </summary>
        public static ITaskManager TaskManager => Get<ITaskManager>();

        /// <summary>
        /// A shortcut to access the <see cref="ApplicationViewModel"/>
        /// </summary>
        public static ApplicationViewModel ApplicationViewModel => Get<ApplicationViewModel>();

        /// <summary>
        /// A shortcut to access the <see cref="SettingsViewModel"/>
        /// </summary>
        public static SettingsViewModel SettingsViewModel => Get<SettingsViewModel>();

        /// <summary>
        /// A shortcut access to the <see cref="IClientDataStore"/>
        /// </summary>
        public static IClientDataStore ClientDataStore => (IClientDataStore) Framework.Provider.GetService(typeof(IClientDataStore));

        #endregion

        #region Construction

        /// <summary>
        /// Sets up the IoC container, binds all information required and is ready for use
        /// NOTE: Must be called as soon as your application starts up to ensure all
        ///       services can be found
        /// </summary>
        public static void Setup()
        {
            /// Bind all required view models
            BindViewModels();
        }

        /// <summary>
        /// Binds all singleton view models
        /// </summary>
        private static void BindViewModels()
        {
            // Bind to a single instance of ApplicationViewModel
            Kernel.Bind<ApplicationViewModel>().ToConstant(new ApplicationViewModel());

            // Bind to a single instance of SettingsViewModel
            Kernel.Bind<SettingsViewModel>().ToConstant(new SettingsViewModel());

        }

        #endregion


        /// <summary>
        /// Gets a service from the IoC container of the specified type
        /// </summary>
        /// <typeparam name="T">The type of service to get</typeparam>
        /// <returns></returns>
        public static T Get<T>()
        {
            return (T)Kernel.GetService(typeof(T));
        }
    }
}
