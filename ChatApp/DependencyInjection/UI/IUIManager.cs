
using System.Threading.Tasks;

using ASPNet_WPF_ChatApp.ViewModels.Dialogs;

namespace ASPNet_WPF_ChatApp.DependencyInjection.UI
{
    /// <summary>
    /// The UI manager that handles any UI interaction in the application
    /// </summary>
    public interface IUIManager
    {
        /// <summary>
        /// Displays a single message box to the user
        /// </summary>
        /// <param name="viewModel">The view model</param>
        /// <returns></returns>
        Task ShowMessage(MessageBoxDialogViewModel viewModel);
    }
}
