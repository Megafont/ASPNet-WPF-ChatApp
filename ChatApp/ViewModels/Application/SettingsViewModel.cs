using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Reflection.Metadata;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

using Dna;

using ASPNet_WPF_ChatApp.Core.ApiModels;
using ASPNet_WPF_ChatApp.Core.DataModels;
using ASPNet_WPF_ChatApp.Core.DependencyInjection;
using ASPNet_WPF_ChatApp.Core.Routes;
using ASPNet_WPF_ChatApp.DependencyInjection;
using ASPNet_WPF_ChatApp.ViewModels.Base;
using ASPNet_WPF_ChatApp.ViewModels.Input;

// This makes it so we can access members on this static class without needing to write "ChatAppDI." first.
using static ASPNet_WPF_ChatApp.DependencyInjection.ChatAppDI;
using ASPNet_WPF_ChatApp.WebRequestUtils;


namespace ASPNet_WPF_ChatApp.ViewModels.Application
{
    /// <summary>
    /// The settings state as a view model
    /// </summary>
    public class SettingsViewModel : BaseViewModel
    {
        #region Private Members
        
        /// <summary>
        /// The text to show while loading
        /// </summary>
        private string _LoadingText = "...";

        #endregion

        #region Public Properties

        /// <summary>
        /// The current user's name
        /// </summary>
        public TextEntryViewModel Name { get; set; }

        /// <summary>
        /// The current user's username
        /// </summary>
        public TextEntryViewModel UserName { get; set; }

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
            // Create Name
            Name = new TextEntryViewModel
            {
                Label = "Name",
                OriginalText = _LoadingText,
                CommitAction = SaveNameAsync
            };

            // Create UserName
            UserName = new TextEntryViewModel
            {
                Label = "Username",
                OriginalText = _LoadingText,
                CommitAction = SaveUserNameAsync
            };

            // Create Password
            Password = new PasswordEntryViewModel
            {
                Label = "Password",
                FakePassword = "********",
                CommitAction = SavePasswordAsync
            };

            // Create Email
            Email = new TextEntryViewModel
            {
                Label = "Email",
                OriginalText = _LoadingText,
                CommitAction = SaveEmailAsync
            };


            // Create commands
            CloseCommand = new RelayCommand(Close);
            OpenCommand = new RelayCommand(Open);
            LogoutCommand = new RelayCommand(async () => await LogoutAsync());
            ClearCommand = new RelayCommand(ClearUserData);
            LoadCommand = new RelayCommand(async () => await LoadSettingsAsync());            
            SaveNameCommand = new RelayCommand(async () => await SaveNameAsync());
            SaveUserNameCommand = new RelayCommand(async () => await SaveUserNameAsync());
            SaveEmailCommand = new RelayCommand(async () => await SaveEmailAsync());

            // TODO: Get string from localization
            LogoutButtonText = "Logout";
        }

        #endregion

        #region Command Methods

        /// <summary>
        /// Opens the settings menu
        /// </summary>
        public void Open()
        {
            // Open the settings menu
            ViewModel_Application.SettingsMenuVisible = true;
        }

        /// <summary>
        /// Closes the settings menu
        /// </summary>
        public void Close()
        {
            // Close the settings menu
            ViewModel_Application.SettingsMenuVisible = false;
        }

        /// <summary>
        /// Logs the user out of the application
        /// </summary>
        public async Task LogoutAsync()
        {
            // TODO: Confirm the user wants to logout

            // Clear any user data/cache
            await ClientDataStore.ClearAllLoginCredentialsAsync();

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
            Name.OriginalText = _LoadingText;
            UserName.OriginalText = _LoadingText;
            Email.OriginalText = _LoadingText;
        }

        /// <summary>
        /// Sets the settings view model properties based on the data in the client data store
        /// </summary>
        public async Task LoadSettingsAsync()
        {
            // Update values from the local cache
            await UpdateValuesFromLocalDataStoreAsync();

            // Get the user token
            LoginCredentialsDataModel loginDataModel = (await ClientDataStore.GetLoginCredentialsAsync());
            string token = loginDataModel != null ? loginDataModel.Token
                                                  : null;

            // If we don't have a token (we are not logged in...)
            if (token == null || string.IsNullOrWhiteSpace(token))
            {
                // Then simply return.
                return;
            }


            // Load user profile details from server
            var result = await WebRequests.PostAsync<ApiResponseModel<UserProfileDetailsApiModel>>(
                WebRoutes.ServerAddress + ApiRoutes.GetUserProfile,
                //bearerToken: token
                // The following line was the original way we did the above "bearerToken:" line
                configureRequest: (request) => request.Headers.Add("Authorization", "Bearer " + token)
                );


            //Debug.WriteLine($"SERVER RESPONSE: \"{result.RawServerResponse}\"", "Warning");

            // If the response has an error...
            if (!string.IsNullOrWhiteSpace(result.ErrorMessage))
            {
                System.Windows.Application.Current.Dispatcher.Invoke(() =>
                {
                    UI.ShowMessage(new Dialogs.MessageBoxDialogViewModel
                    {
                        Title = "Error",
                        Message = result.ErrorMessage
                    });
                });
                // We are done
                return;
            }

            // If it was successful...
            if (result.Successful)
            {
                // TODO: Check if values are different before saving.

                // Create data model from the response
                var dataModel = result.ServerResponse.Response.ToLoginCredentialsDataModel();

                // Re-add our known token
                dataModel.Token = token;

                // Save the new information into the local data store
                await ClientDataStore.SaveLoginCredentialsAsync(dataModel);

                // Update values from the local cache
                await UpdateValuesFromLocalDataStoreAsync();
            }
            else
            {
                // Run the ShowMessage() call on the UI thread. Otherwise it would throw an exception
                System.Windows.Application.Current.Dispatcher.Invoke(() =>
                {
                    UI.ShowMessage(new Dialogs.MessageBoxDialogViewModel
                    {
                        Title = "Error",
                        Message = result.ErrorMessage
                    });
                });
            }
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

        #endregion

        #region Private Helper Methods

        /// <summary>
        /// Loads the settings from the local data store and binds them
        /// to this view model
        /// </summary>
        /// <returns></returns>
        private async Task UpdateValuesFromLocalDataStoreAsync()
        {
            // Get the stored credentials
            var storedCredentials = await ClientDataStore.GetLoginCredentialsAsync();

            // Set name
            Name.OriginalText = $"{storedCredentials?.FirstName} {storedCredentials?.LastName}";

            // Set user name
            UserName.OriginalText = storedCredentials?.UserName;

            // Set email
            Email.OriginalText = storedCredentials?.Email;
        }

        #endregion
    }
}
