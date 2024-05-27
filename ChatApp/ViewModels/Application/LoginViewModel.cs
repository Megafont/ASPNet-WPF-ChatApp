using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Security;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

using Dna;

using ASPNet_WPF_ChatApp.Core;
using ASPNet_WPF_ChatApp.Core.ApiModels;
using ASPNet_WPF_ChatApp.Core.DataModels;
using ASPNet_WPF_ChatApp.Core.DependencyInjection;
using ASPNet_WPF_ChatApp.Core.Security;
using ASPNet_WPF_ChatApp.ViewModels.Base;
using ASPNet_WPF_ChatApp.ViewModels.Input;
using ASPNet_WPF_ChatApp.WebRequestUtils;

// This makes it so we can access members on this static class without needing to write "ChatAppDI." first.
using static ASPNet_WPF_ChatApp.DependencyInjection.ChatAppDI;

namespace ASPNet_WPF_ChatApp.ViewModels.Application
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

                // If the response has an error...
                if (await result.DisplayErrorIfFailedAsync("Login Failed"))
                {
                    // We are done
                    return;
                }

                // Ok, successfully logged in... Now get the user's dat              
                var loginResult = result.ServerResponse.Response;

                // Let the application view model handle what happens
                // with the ssuccessful login
                await ViewModel_Application.HandleSuccessfulLoginAsync(loginResult);
            });

        }

        /// <summary>
        /// Takes the user to the register page
        /// </summary>
        /// <returns></returns>
        public async Task RegisterAsync()
        {
            // Go to registration page
            ViewModel_Application.GoToPage(ApplicationPages.Register);

            await Task.Delay(1);

        }
    }
}

