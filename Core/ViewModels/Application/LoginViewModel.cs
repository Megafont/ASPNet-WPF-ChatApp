using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Security;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

using ASPNet_WPF_ChatApp.Core;
using ASPNet_WPF_ChatApp.Core.ApiModels;
using ASPNet_WPF_ChatApp.Core.DataModels;
using ASPNet_WPF_ChatApp.Core.InversionOfControl.Base;
using ASPNet_WPF_ChatApp.Core.Security;
using ASPNet_WPF_ChatApp.Core.ViewModels.Base;
using ASPNet_WPF_ChatApp.Core.ViewModels.Input;
using Dna;

namespace ASPNet_WPF_ChatApp.Core.ViewModels.Application
{
    /// <summary>
    /// The View Model for a login screen
    /// </summary>
    public class LoginViewModel : BaseViewModel
    {
        #region Public Properties

        /// <summary>
        /// The email of the user
        /// </summary>
        public string Email { get; set; }

        /// <summary>
        /// A flag indicating if the login command is running
        /// </summary>
        public bool LoginIsRunning { get; set; }

        #endregion

        #region Commands

        /// <summary>
        /// The command to login
        /// </summary>
        public ICommand LoginCommand { get; set; }

        /// <summary>
        /// The command to register for a new account
        /// </summary>
        public ICommand RegisterCommand { get; set; }

        #endregion

        #region Constructors        

        /// <summary>
        /// Default constructor
        /// </summary>
        public LoginViewModel()
        {
            // Create commands
            LoginCommand = new RelayParameterizedCommand(async (parameter) => await LoginAsync(parameter));
            RegisterCommand = new RelayCommand(async () => await RegisterAsync());
        }

        #endregion

        /// <summary>
        /// Attemps to log the user in
        /// </summary>
        /// <param name="parameter">The <see cref="SecureString"/> passed in from the view for the user's password</param>
        /// <returns></returns>
        public async Task LoginAsync(object parameter)
        {
            if (LoginIsRunning)
                return;



            await RunCommandAsync(() => LoginIsRunning, async () =>
            {
                // Call the server and attempt to login with credentials
                // TODO: Move all URLs and API routes to static class in Core
                var result = await WebRequests.PostAsync<ApiResponseModel<LoginResultApiModel>>(
                    "http://localhost:5289/api/login",
                    new LoginCredentialsApiModel
                    {
                        UsernameOrEmail = Email,
                        Password = (parameter as IHavePassword).SecurePassword.Unsecure()
                    });

                // If there was no response, bad data, or a response with an error message...
                if (result == null || result.ServerResponse == null || !result.ServerResponse.Successful)
                {
                    // Default error message
                    // TODO: Localize strings
                    var message = "Unknown error occurred.";

                    // If we got a response from the server...
                    if (result?.ServerResponse != null)
                    {
                        // Set message to the server's response
                        message = result.ServerResponse.ErrorMessage;
                    }
                    
                    // If we have a result, but deserialization failed...
                    else if (!string.IsNullOrWhiteSpace(result?.RawServerResponse))
                    {
                        // Set message to the raw server response
                        message = $"Unexpected response from server. \"{result.RawServerResponse}\"";
                    }

                    // If we have a result, but no server response details at all...
                    else if (result != null)
                    {
                        // Set message to standard HTTP server response details
                        message = $"Failed to communicate with server. Status code {result.StatusCode}. \"{result.StatusDescription}\"";
                    }


                    // Display error
                    await IoC.UI.ShowMessage(new Dialogs.MessageBoxDialogViewModel
                    {
                        // TODO: Localize strings
                        Title = "Login Failed",
                        Message = message
                    });

                    // We are done
                    return;
                }


                // Ok, successfully logged in... Now get the user's dat              
                var userData = result.ServerResponse.Response;

                // Store it in the client data store
                await IoC.ClientDataStore.SaveLoginCredentialsAsync(new LoginCredentialsDataModel
                {
                    Id = Guid.NewGuid().ToString("N"), // I had to add this line to stop it complaining that this field was null
                    Email = userData.Email,
                    FirstName = userData.FirstName,
                    LastName = userData.LastName,
                    UserName = userData.UserName,
                    Token = userData.Token
                });


                // Load new settings
                await IoC.SettingsViewModel.LoadSettingsAsync();
                
                // Go to chat page
                IoC.ApplicationViewModel.GoToPage(ApplicationPages.Chat);
            });

        }

        /// <summary>
        /// Takes the user to the register page
        /// </summary>
        /// <returns></returns>
        public async Task RegisterAsync()
        {
            // Go to registration page
            IoC.ApplicationViewModel.GoToPage(ApplicationPages.Register);

            await Task.Delay(1);

        }
    }
}

