using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

using ASPNet_WPF_ChatApp.ViewModels.Base;
using ASPNet_WPF_ChatApp.ViewModels.Dialogs;
using ASPNet_WPF_ChatApp.ViewModels;
using ASPNet_WPF_ChatApp.WPFViewModels;

namespace ASPNet_WPF_ChatApp.Dialogs
{
    /// <summary>
    public class BaseDialogUserControl : UserControl
    {
        #region Private Members

        /// <summary>
        /// The dialog window we will be contained within
        /// </summary>
        private DialogWindow _DialogWindow;

        #endregion

        #region Public Commands

        /// <summary>
        /// Closes this dialog
        /// </summary>
        public ICommand CloseCommand { get; private set; }

        #endregion

        #region Public Properties

        /// <summary>
        /// The minimum width of this dialog
        /// </summary>
        public int WindowMinimumWidth { get; set; } = 250;

        /// <summary>
        /// The minimum height of this dialog
        /// </summary>
        public int WindowMinimumHeight { get; set; } = 100;

        /// <summary>
        /// The height of the title bar
        /// </summary>
        public int TitleHeight { get; set; } = 30;

        /// <summary>
        /// The title for this dialog
        /// </summary>
        public string Title { get; set; }

        #endregion

        #region Constructors

        public BaseDialogUserControl()
        {
            // Create a new dialog window
            _DialogWindow = new DialogWindow();
            _DialogWindow.ViewModel = new DialogWindowViewModel(_DialogWindow);

            // Create commands
            CloseCommand = new RelayCommand(() => _DialogWindow.Close());
        }

        #endregion

        #region Public Dialog Show Methods

        /// <summary>
        /// Displays a single message box to the user
        /// </summary>
        /// <param name="viewModel">The view model</param>
        /// <typeparam name="T">The view model type for this control</typeparam>
        /// <returns></returns>
        public Task ShowDialog<T>(T viewModel)
            where T : BaseDialogViewModel
        {
            // Create a task to wait the dialog closing
            var tcs = new TaskCompletionSource<bool>();

            // Run on UI thread
            Application.Current.Dispatcher.Invoke(() =>
            {
                try
                {
                    // Match controls expected sizes to the dialog window's view model
                    _DialogWindow.ViewModel.WindowMinimumWidth = WindowMinimumWidth;
                    _DialogWindow.ViewModel.WindowMinimumHeight = WindowMinimumHeight;
                    _DialogWindow.ViewModel.TitleHeight = TitleHeight;
                    _DialogWindow.ViewModel.Title = string.IsNullOrWhiteSpace(viewModel.Title) ? Title : viewModel.Title;

                    // Set this control to the dialog window content
                    _DialogWindow.ViewModel.Content = this;

                    // Set up this control's data context binding to the view model
                    DataContext = viewModel;

                    // Show the dialog in the center of the parent window
                    _DialogWindow.Owner = Application.Current.MainWindow;
                    _DialogWindow.WindowStartupLocation = WindowStartupLocation.CenterOwner;

                    // Show dialog
                    _DialogWindow.ShowDialog();
                }
                finally
                {
                    // Let caller know we finished
                    tcs.TrySetResult(true);
                }
            });

            return tcs.Task;
        }

        #endregion

    }
}
