using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Dna;

using ASPNet_WPF_ChatApp.Core.DataModels;
using ASPNet_WPF_ChatApp.Core.DependencyInjection.Interfaces;


namespace ASPNet_WPF_ChatApp.Core.DependencyInjection
{
    /// <summary>
    /// The Inversion of Control container for our application
    /// </summary>
    public static class CoreDI
    {
        /// <summary>
        /// A shortcut to access the <see cref="IFileManager"/>
        /// </summary>
        public static IFileManager FileManager => (IFileManager)Framework.Provider.GetService(typeof(IFileManager));

        /// <summary>
        /// A shortcut to access the <see cref="ITaskManager"/>
        /// </summary>
        public static ITaskManager TaskManager => (ITaskManager)Framework.Provider.GetService(typeof(ITaskManager));

    }
}
