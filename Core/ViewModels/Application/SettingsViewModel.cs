using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

using ASPNet_WPF_ChatApp.Core.DataModels;
using ASPNet_WPF_ChatApp.Core.InversionOfControl.Base;
using ASPNet_WPF_ChatApp.Core.ViewModels.Base;
using ASPNet_WPF_ChatApp.Core.ViewModels.Input;

namespace ASPNet_WPF_ChatApp.Core.ViewModels.Application
{
    /// <summary>
    /// The settings state as a view model
    /// </summary>
    public class SettingsViewModel : BaseViewModel
    {
        #region Public Properties

        /// <summary>
        /// The current user's name
        /// </summary>
        public TextEntryViewModel Name { get; set; }

        /// <summary>
        /// The current user's username
        /// </summary>
        public TextEntryViewModel Username { get; set; }

        /// <summary>
        /// The current user's password
        /// </summary>
        public PasswordEntryViewModel Password { get; set; }

        /// <summary>
        /// The current user's email
        /// </summary>
        public TextEntryViewModel Email { get; set; }

        /// <summary>
        /// The text for the logout button
        /// </summary>
        public string LogoutButtonText { get; set; }

        #endregion

        #region Public Commands

        /// <summary>
        /// The command to open the settings menu
        /// </summary>
        public ICommand OpenCommand { get; set; }

        /// <summary>
        /// The command to close the settings menu
        /// </summary>
        public ICommand CloseCommand { get; set; }

        /// <summary>
        /// The command to log out of the application
        /// </summary>
        public ICommand LogoutCommand { get; set; }

        /// <summary>
        /// The command to clear the user's data from this view model
        /// </summary>
        public ICommand ClearCommand { get; set; }

        #endregion

        #region Constructors

        /// <summary>
        /// Default constructor
        /// </summary>
        public SettingsViewModel()
        {
            // Create commands
            CloseCommand = new RelayCommand(Close);
            OpenCommand = new RelayCommand(Open);
            LogoutCommand = new RelayCommand(Logout);
            ClearCommand = new RelayCommand(ClearUserData);

            // TODO: Remove this once the real back-end is ready
            Name = new TextEntryViewModel { Label = "Name", OriginalText = $"Michael Fontanini {DateTime.Now.ToLocalTime()}" };
            Username = new TextEntryViewModel { Label = "Username", OriginalText = "Megafont" };
            Password = new PasswordEntryViewModel { Label = "Password", FakePassword = "********" };
            Email = new TextEntryViewModel { Label = "Email", OriginalText = "megafont@gmail.com" };


            // TODO: Get from localization
            LogoutButtonText = "Logout";
        }

        #endregion

        /// <summary>
        /// Closes the settings menu
        /// </summary>
        public void Close()
        {
            // Close the settings menu
            IoC.ApplicationViewModel.SettingsMenuVisible = false;
        }

        /// <summary>
        /// Opens the settings menu
        /// </summary>
        public void Open()
        {
            // Open the settings menu
            IoC.ApplicationViewModel.SettingsMenuVisible = true;
        }

        /// <summary>
        /// Logs the user out of the application
        /// </summary>
        public void Logout()
        {
            // TODO: Confirm the user wants to logout

            // TODO: Clear any user data/cache

            // Clean all application level view models that contain
            // any information about the current user
            ClearUserData();

            // Go to login page
            IoC.ApplicationViewModel.GoToPage(ApplicationPages.Login);
        }

        /// <summary>
        /// Clears any data specific to the current user
        /// </summary>
        public void ClearUserData()
        {
            // Clear all view models containing the user's info
            Name = null;
            Username = null;
            Password = null;
            Email = null;
        }
    }
}
