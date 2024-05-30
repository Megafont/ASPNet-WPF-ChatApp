using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

using ASPNet_WPF_ChatApp.Core.DataModels;
using ASPNet_WPF_ChatApp.Core.DependencyInjection;
using ASPNet_WPF_ChatApp.ViewModels.Base;
using ASPNet_WPF_ChatApp.ViewModels.Input;

// This makes it so we can access members on this static class without needing to write "ChatAppDI." first.
using static ASPNet_WPF_ChatApp.DependencyInjection.ChatAppDI;


namespace ASPNet_WPF_ChatApp.ViewModels.Application
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

        /// <summary>
        /// The command to load the settings data from the client data store
        /// </summary>
        public ICommand LoadCommand { get; set; }

        /// <summary>
        /// Saves the current name to the server
        /// </summary>
        public ICommand SaveNameCommand { get; set; }

        /// <summary>
        /// Saves the current user name to the server
        /// </summary>
        public ICommand SaveUserNameCommand { get; set; }

        /// <summary>
        /// Saves the current email to the server
        /// </summary>
        public ICommand SaveEmailCommand { get; set; }

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
            LoadCommand = new RelayCommand(async () => await LoadSettingsAsync());            
            SaveNameCommand = new RelayCommand(async () => await SaveNameAsync());
            SaveUserNameCommand = new RelayCommand(async () => await SaveUserNameAsync());
            SaveEmailCommand = new RelayCommand(async () => await SaveEmailAsync());


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
            ViewModel_Application.SettingsMenuVisible = false;
        }

        /// <summary>
        /// Opens the settings menu
        /// </summary>
        public void Open()
        {
            // Open the settings menu
            ViewModel_Application.SettingsMenuVisible = true;
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
            ViewModel_Application.GoToPage(ApplicationPages.Login);
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

        /// <summary>
        /// Sets the settings view model properties based on the data in the client data store
        /// </summary>
        public async Task LoadSettingsAsync()
        {
            // Get the stored credentials
            var storedCredentials = await ClientDataStore.GetLoginCredentialsAsync();

            Name = new TextEntryViewModel 
            { 
                Label = "Name", 
                OriginalText = $"{storedCredentials?.FirstName} {storedCredentials?.LastName}",
                CommitAction = SaveNameAsync
            };

            Username = new TextEntryViewModel
            {
                Label = "Username",
                OriginalText = storedCredentials?.UserName,
                CommitAction = SaveUserNameAsync
            };

            Password = new PasswordEntryViewModel 
            { 
                Label = "Password", 
                FakePassword = "********",
                CommitAction = SavePasswordAsync
            };

            Email = new TextEntryViewModel 
            { 
                Label = "Email", 
                OriginalText = storedCredentials?.Email,
                CommitAction = SaveEmailAsync
            };
        }


        /// <summary>
        /// Saves the new name to the server
        /// </summary>
        /// <param name="self">The view model of the field where the name was edited</param>
        /// <returns>True if successful, or false otherwise</returns>
        public async Task<bool> SaveNameAsync()
        {
            // TODO: Update with server
            await Task.Delay(3000);

            // Return fail
            return false;
        }

        /// <summary>
        /// Saves the new user name to the server
        /// </summary>
        /// <param name="self">The view model of the field where the user name was edited</param>
        /// <returns>True if successful, or false otherwise</returns>
        public async Task<bool> SaveUserNameAsync()
        {
            // TODO: Update with server
            await Task.Delay(3000);

            // Return success
            return true;
        }

        /// <summary>
        /// Saves the new email to the server
        /// </summary>
        /// <param name="self">The view model of the field where the email was edited</param>
        /// <returns>True if successful, or false otherwise</returns>
        public async Task<bool> SaveEmailAsync()
        {
            // TODO: Update with server
            await Task.Delay(3000);

            // Return fail
            return false;
        }

        /// <summary>
        /// Saves the new password to the server
        /// </summary>
        /// <param name="self">The view model of the field where the password was edited</param>
        /// <returns>True if successful, or false otherwise</returns>
        public async Task<bool> SavePasswordAsync()
        {
            // TODO: Update with server
            await Task.Delay(3000);

            // Return fail
            return false;
        }

    }
}
