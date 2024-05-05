using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

using ASPNet_WPF_ChatApp.Core.InversionOfControl.Interfaces;
using ASPNet_WPF_ChatApp.Core.ViewModels.Dialogs;
using ASPNet_WPF_ChatApp.Dialogs;


namespace ASPNet_WPF_ChatApp.InversionOfControl
{
    /// <summary>
    /// The application's implementation of the <see cref="IUIManager"/>
    /// </summary>
    public class UIManager : IUIManager
    {
        /// <summary>
        /// Displays a single message box to the user
        /// </summary>
        /// <param name="viewModel">The view model</param>
        /// <returns></returns>
        public Task ShowMessage(MessageBoxDialogViewModel viewModel)
        {
            return new DialogMessageBox().ShowDialog(viewModel);
        }
    }
}
