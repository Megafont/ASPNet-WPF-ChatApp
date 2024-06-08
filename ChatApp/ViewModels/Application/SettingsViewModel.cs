using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection.Metadata;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

using Dna;

using ASPNet_WPF_ChatApp.WebRequestUtils;
using ASPNet_WPF_ChatApp.Core.ApiModels;
using ASPNet_WPF_ChatApp.Core.DataModels;
using ASPNet_WPF_ChatApp.Core.DependencyInjection;
using ASPNet_WPF_ChatApp.Core.Expressions;
using ASPNet_WPF_ChatApp.Core.Routes;
using ASPNet_WPF_ChatApp.DependencyInjection;
using ASPNet_WPF_ChatApp.ViewModels.Base;
using ASPNet_WPF_ChatApp.ViewModels.Input;

// This makes it so we can access members on this static class without needing to write "ChatAppDI." first.
using static ASPNet_WPF_ChatApp.DependencyInjection.ChatAppDI;
using ASPNet_WPF_ChatApp.Core.Security;
using ASPNet_WPF_ChatApp.Core.DependencyInjection.Interfaces;


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
        /// The current user's first name
        /// </summary>
        public TextEntryViewModel FirstName { get; set; }

        /// <summary>
        /// The current user's last name
        /// </summary>
        public TextEntryViewModel LastName { get; set; }

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


        #region TransactionalProperties

        /// <summary>
        /// Indicates if the first name is currently being saved
        /// </summary>
        public bool FirstNameIsSaving { get; set; }

        /// <summary>
        /// Indicates if the last name is currently being saved
        /// </summary>
        public bool LastNameIsSaving { get; set; }

        /// <summary>
        /// Indicates if the username is currently being saved
        /// </summary>
        public bool UserNameIsSaving { get; set; }

        /// <summary>
        /// Indicates if the email is currently being saved
        /// </summary>
        public bool EmailIsSaving { get; set; }
        
        /// <summary>
        /// Indicates if the password is currently being changed
        /// </summary>
        public bool PasswordIsChanging { get; set; }

        /// <summary>
        /// Indicates if the settings details are currently being loaded
        /// </summary>
        public bool SettingsAreLoading { get; set; }

        /// <summary>
        /// Indicates if the user is currently logging out
        /// </summary>
        public bool LoggingOut { get; set; }

        #endregion

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
        /// Saves the current first name to the server
        /// </summary>
        public ICommand SaveFirstNameCommand { get; set; }

        /// <summary>
        /// Saves the current last name to the server
        /// </summary>
        public ICommand SaveLastNameCommand { get; set; }

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
            // Create First Name
            FirstName = new TextEntryViewModel
            {
                Label = "First Name",
                OriginalText = _LoadingText,
                CommitAction = SaveFirstNameAsync
            };

            // Create Last Name
            LastName = new TextEntryViewModel
            {
                Label = "Last Name",
                OriginalText = _LoadingText,
                CommitAction = SaveLastNameAsync
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
            SaveFirstNameCommand = new RelayCommand(async () => await SaveFirstNameAsync());
            SaveLastNameCommand = new RelayCommand(async () => await SaveLastNameAsync());
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
            // Lock this command to ignore any other requests while processing. The lock block is inside the 2nd version of RunCommandAsync().
            await RunCommandAsync(() => LoggingOut, async () =>
            {

                // TODO: Confirm the user wants to logout

                // Clear any user data/cache
                await ClientDataStore.ClearAllLoginCredentialsAsync();

                // Clean all application level view models that contain
                // any information about the current user
                ClearUserData();

                // Go to login page
                ViewModel_Application.GoToPage(ApplicationPages.Login);
            });
        }

        /// <summary>
        /// Clears any data specific to the current user
        /// </summary>
        public void ClearUserData()
        {
            // Clear all view models containing the user's info
            FirstName.OriginalText = _LoadingText;
            LastName.OriginalText = _LoadingText;
            UserName.OriginalText = _LoadingText;
            Email.OriginalText = _LoadingText;
        }

        /// <summary>
        /// Sets the settings view model properties based on the data in the client data store
        /// </summary>
        public async Task LoadSettingsAsync()
        {
            // Lock this command to ignore any other requests while processing. The lock block is inside the 2nd version of RunCommandAsync().
            await RunCommandAsync(() => SettingsAreLoading, async () =>
            {
                // Store a single transient instance of the client data store
                var scopedClientDataStore = ClientDataStore;

                // Update values from the local cache
                await UpdateValuesFromLocalDataStoreAsync(scopedClientDataStore);

                // Get the user token
                LoginCredentialsDataModel loginDataModel = (await scopedClientDataStore.GetLoginCredentialsAsync());
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
                    // Get URL
                    RouteHelpers.GetAbsoluteRoute(ApiRoutes.GetUserProfile),
                    // Pass in the JWT (JSON web token) user Token
                    bearerToken: token
                    // The following line was the original way we did the above "bearerToken:" line
                    //configureRequest: (request) => request.Headers.Add("Authorization", "Bearer " + token)
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
                    await scopedClientDataStore.SaveLoginCredentialsAsync(dataModel);

                    // Update values from the local cache
                    await UpdateValuesFromLocalDataStoreAsync(scopedClientDataStore);
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
            });
        }

        /// <summary>
        /// Saves the new first name to the server
        /// </summary>
        /// <param name="self">The view model of the field where the first name was edited</param>
        /// <returns>True if successful, or false otherwise</returns>
        public async Task<bool> SaveFirstNameAsync()
        {
            // Lock this command to ignore any other requests while processing. The lock block is inside the 2nd version of RunCommandAsync().
            return await RunCommandAsync(() => FirstNameIsSaving, async () =>
            {
                // Update the First Name value on the server...
                return await UpdateUserCredentialsValueAsync(
                    // Display name
                    "First Name",
                    // Update the First Name
                    (credentials) => credentials.FirstName,
                    // To new value
                    FirstName.OriginalText,
                    // Set Api model value
                    (apiModel, value) => apiModel.FirstName = value
                    );                    
            });
        }

        /// <summary>
        /// Saves the new last name to the server
        /// </summary>
        /// <param name="self">The view model of the field where the last name was edited</param>
        /// <returns>True if successful, or false otherwise</returns>
        public async Task<bool> SaveLastNameAsync()
        {
            // Lock this command to ignore any other requests while processing. The lock block is inside the 2nd version of RunCommandAsync().
            return await RunCommandAsync(() => LastNameIsSaving, async () =>
            {
                // Update the Last Name value on the server...
                return await UpdateUserCredentialsValueAsync(
                    // Display name
                    "Last Name",
                    // Update the Last Name
                    (credentials) => credentials.LastName,
                    // To new value
                    LastName.OriginalText,
                    // Set Api model value
                    (apiModel, value) => apiModel.LastName = value
                    );
            });
        }

        /// <summary>
        /// Saves the new user name to the server
        /// </summary>
        /// <param name="self">The view model of the field where the user name was edited</param>
        /// <returns>True if successful, or false otherwise</returns>
        public async Task<bool> SaveUserNameAsync()
        {
            // Lock this command to ignore any other requests while processing. The lock block is inside the 2nd version of RunCommandAsync().
            return await RunCommandAsync(() => UserNameIsSaving, async () =>
            {
                // Update the User Name value on the server...
                return await UpdateUserCredentialsValueAsync(
                    // Display name
                    "Username",
                    // Update the UserName
                    (credentials) => credentials.UserName,
                    // To new value
                    UserName.OriginalText,
                    // Set Api model value
                    (apiModel, value) => apiModel.UserName = value
                    );
            });
        }

        /// <summary>
        /// Saves the new email to the server
        /// </summary>
        /// <param name="self">The view model of the field where the email was edited</param>
        /// <returns>True if successful, or false otherwise</returns>
        public async Task<bool> SaveEmailAsync()
        {
            // Lock this command to ignore any other requests while processing. The lock block is inside the 2nd version of RunCommandAsync().
            return await RunCommandAsync(() => EmailIsSaving, async () =>
            {
                // Update the Email value on the server...
                return await UpdateUserCredentialsValueAsync(
                    // Display name
                    "Email",
                    // Update the Email
                    (credentials) => credentials.Email,
                    // To new value
                    Email.OriginalText,
                    // Set Api model value
                    (apiModel, value) => apiModel.Email = value
                    );
            });
        }

        /// <summary>
        /// Saves the new password to the server
        /// </summary>
        /// <param name="self">The view model of the field where the password was edited</param>
        /// <returns>True if successful, or false otherwise</returns>
        public async Task<bool> SavePasswordAsync()
        {
            // Lock this command to ignore any other requests while processing. The lock block is inside the 2nd version of RunCommandAsync().
            return await RunCommandAsync(() => PasswordIsChanging, async () =>
            {
                // Log it
                FrameworkDI.Logger.LogDebugSource("Chaning password...");

                // Get the current known credentials
                var credentials = await ClientDataStore.GetLoginCredentialsAsync();

                // Make sure the user has entered the same password
                if (Password.NewPassword.Unsecure() != Password.ConfirmPassword.Unsecure())
                {
                    // Display error
                    await UI.ShowMessage(new Dialogs.MessageBoxDialogViewModel
                    {
                        Title = "Password Mismatch",
                        Message = "New password and confirm password must match!"
                    });

                    // Return fail
                    return false;
                }

                // Update the server with the new password
                var result = await WebRequests.PostAsync<ApiResponseModel>(
                    // Set URL
                    RouteHelpers.GetAbsoluteRoute(ApiRoutes.UpdateUserPassword),
                    // Create our API model
                    new UpdateUserPasswordApiModel
                    {
                        CurrentPassword = Password.CurrentPassword.Unsecure(),
                        NewPassword = Password.NewPassword.Unsecure()
                    },
                    // Pass in the JWT (JSON web token) user Token
                    bearerToken: credentials.Token);

                // If the response has an error...
                if (await result.HandleErrorIfFailedAsync($"Change Password"))
                {
                    // Log it
                    FrameworkDI.Logger.LogDebugSource($"Failed to change password: \"{result.ErrorMessage}\"");

                    return false;
                }

                // Otherwise, we succeeded.

                // Log it
                FrameworkDI.Logger.LogDebugSource($"Successfully updated password.");

                // Return successful
                return true;
            });
        }

        #endregion

        #region Private Helper Methods

        /// <summary>
        /// Loads the settings from the local data store and binds them
        /// to this view model
        /// </summary>
        /// <returns></returns>
        private async Task UpdateValuesFromLocalDataStoreAsync(IClientDataStore clientDataStore)
        {
            // Get the stored credentials
            var storedCredentials = await clientDataStore.GetLoginCredentialsAsync();

            // Set first name
            FirstName.OriginalText = storedCredentials?.FirstName;

            // Set last name
            LastName.OriginalText = storedCredentials?.LastName;

            // Set user name
            UserName.OriginalText = storedCredentials?.UserName;

            // Set email
            Email.OriginalText = storedCredentials?.Email;
        }

        /// <summary>
        /// Updates a specific value from the client data store for the user profile details
        /// and attempts to update the server to match those details.
        /// For example, updating the first name of the user.
        /// </summary>
        /// <param name="displayName">The display name for logging and display purposes of the property we are updating</param>
        /// <param name="propertyToUpdate">The property from the <see cref="LoginCredentialsDataModel"/> to be updated</param>
        /// <param name="newValue">The new value to update it to</param>
        /// <param name="setApiModel">Sets the correct property in the <see cref="UpdateUserProfileDetailsApiModel"/> model that this property maps to</param>
        /// <returns></returns>
        private async Task<bool> UpdateUserCredentialsValueAsync(string displayName, Expression<Func<LoginCredentialsDataModel, string>> propertyToUpdate, string newValue, Action<UpdateUserProfileDetailsApiModel, string> setApiModel)
        {
            // Log it
            FrameworkDI.Logger.LogDebugSource($"Saving {displayName}...");

            // Get the current known credentials
            var credentials = await ClientDataStore.GetLoginCredentialsAsync();

            // Get the property to update from the credentials
            var toUpdate = propertyToUpdate.GetPropertyValue(credentials);

            // Check if the value is the same, and if so return true
            if (toUpdate == newValue)
            {
                // Log it
                FrameworkDI.Logger.LogDebugSource($"{displayName} is unchanged, no need to save...");

                return true;
            }


            // Log it
            FrameworkDI.Logger.LogDebugSource($"{displayName} currently updating from \"{toUpdate}\" to \"{newValue}\".");

            // Set the new first name
            propertyToUpdate.SetPropertyValue(newValue, credentials);

            // Create update details
            var updateApiModel = new UpdateUserProfileDetailsApiModel();

            // Ask the caller to set the appropriate value
            setApiModel(updateApiModel, newValue);


            // Update the server with the new details
            var result = await WebRequests.PostAsync<ApiResponseModel>(
                // Set URL
                RouteHelpers.GetAbsoluteRoute(ApiRoutes.UpdateUserProfile),
                // Pass in the Api model
                updateApiModel,
                // Pass in the JWT (JSON web token) user token
                bearerToken: credentials.Token);

            // If the response has an error...
            if (await result.HandleErrorIfFailedAsync($"Update {displayName}"))
            {
                // Log it
                FrameworkDI.Logger.LogDebugSource($"Failed to update {displayName}: \"{result.ErrorMessage}\"");

                return false;
            }

            // Log it
            FrameworkDI.Logger.LogDebugSource($"Successfully updated {displayName}. Saving to local data store...");

            // Store the new user credentials in the client data store
            await ClientDataStore.SaveLoginCredentialsAsync(credentials);

            // Return successful
            return true;
        }

        #endregion
    }
}
