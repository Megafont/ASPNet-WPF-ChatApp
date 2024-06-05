using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Security;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

using Dna;

using ASPNet_WPF_ChatApp.Core.ApiModels;
using ASPNet_WPF_ChatApp.Core.DataModels;
using ASPNet_WPF_ChatApp.Core.DependencyInjection;
using ASPNet_WPF_ChatApp.Core.Security;
using ASPNet_WPF_ChatApp.ViewModels.Base;
using ASPNet_WPF_ChatApp.WebRequestUtils;

// This makes it so we can access members on this static class without needing to write "ChatAppDI." first.
using static ASPNet_WPF_ChatApp.DependencyInjection.ChatAppDI;
using ASPNet_WPF_ChatApp.Core.Routes;


namespace ASPNet_WPF_ChatApp.ViewModels.Application
{
    /// <summary>
    /// The View Model for a register screen
    /// </summary>
    public class RegisterViewModel : BaseViewModel
    {
        #region Public Properties

        /// <summary>
        /// The user name of the user
        /// </summary>
        public string UserName { get; set; }

        /// <summary>
        /// The email of the user
        /// </summary>
        public string Email { get; set; }

        /// <summary>
        /// A flag indicating if the register command is running
        /// </summary>
        public bool RegisterIsRunning { get; set; }

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
        public RegisterViewModel()
        {
            // Create commands
            RegisterCommand = new RelayParameterizedCommand(async (parameter) => await RegisterAsync(parameter));
            LoginCommand = new RelayCommand(async () => await LoginAsync());
        }

        #endregion

        /// <summary>
        /// Attemps to register a new user
        /// </summary>
        /// <param name="parameter">The <see cref="SecureString"/> passed in from the view for the user's password</param>
        /// <returns></returns>
        public async Task RegisterAsync(object parameter)
        {
            if (RegisterIsRunning)
                return;


            await RunCommandAsync(() => RegisterIsRunning, async () =>
            {
                // Call the server and attempt to register an account with the provided credentials
                // TODO: Move all URLs and API routes to static class in Core
                var result = await WebRequests.PostAsync<ApiResponseModel<RegisterResultApiModel>>(
                    WebRoutes.ServerAddress + ApiRoutes.Register,
                    new RegisterCredentialsApiModel
                    {
                        UserName = this.UserName,
                        Email = this.Email,
                        Password = (parameter as IHavePassword).SecurePassword.Unsecure()
                    });

                // If the response has an error...
                if (await result.DisplayErrorIfFailedAsync("Registration Failed"))
                {
                    // We are done
                    return;
                }

                // Ok, successfully registered (and logged in)... Now get the user's dat              
                var registrationResult = result.ServerResponse.Response;

                // Let the application view model handle what happens
                // with the ssuccessful login
                await ViewModel_Application.HandleSuccessfulLoginAsync(registrationResult);
            });

        }

        /// <summary>
        /// Takes the user to the login page
        /// </summary>
        /// <returns></returns>
        public async Task LoginAsync()
        {
            // Go to login page
            ViewModel_Application.GoToPage(ApplicationPages.Login);

            await Task.Delay(1);

        }
    }
}

