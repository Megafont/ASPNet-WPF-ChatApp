using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

using Dna;
using Microsoft.Extensions.DependencyInjection; // Include the Dna Framework.

using ASPNet_WPF_ChatApp.ViewModels.Application;
using ASPNet_WPF_ChatApp.Core.DependencyInjection.Interfaces;
using ASPNet_WPF_ChatApp.Core.Tasks;
using ASPNet_WPF_ChatApp.Core.FileSystem;
using ASPNet_WPF_ChatApp.DependencyInjection.UI;


namespace ASPNet_WPF_ChatApp.DependencyInjection
{
    /// <summary>
    /// Extension methods for the <see cref="FrameworkConstruction"/>
    /// </summary>
    public static class FrameworkConstructionExtensions
    {
        /// <summary>
        /// Injects the view models needed for the chat application
        /// </summary>
        /// <param name="construction"></param>
        /// <returns></returns>
        public static FrameworkConstruction AddChatAppViewModels(this FrameworkConstruction construction)
        {
            // Bind to a single instance of ApplicationViewModel
            construction.Services.AddSingleton<ApplicationViewModel>();

            // Bind to a single instance of SettingsViewModel
            construction.Services.AddSingleton<SettingsViewModel>();

            // Return construction for chaining (aka Fluent API).
            return construction;
        }

        /// <summary>
        /// Injects the chat application client services needed for the chat application
        /// </summary>
        /// <param name="construction"></param>
        /// <returns></returns>
        public static FrameworkConstruction AddChatAppClientServices(this FrameworkConstruction construction)
        {
            // Bind a task manager. Transient means a new instance is created every time the service is requested.
            construction.Services.AddTransient<ITaskManager, BaseTaskManager>();

            // Bind a file manager
            construction.Services.AddTransient<IFileManager, BaseFileManager>();

            // Bind a UI manager
            construction.Services.AddSingleton<IUIManager, UIManager>();

            // Return construction for chaining (aka Fluent API).
            return construction;
        }
    }
}
