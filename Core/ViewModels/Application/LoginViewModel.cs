using System;
using System.Collections.Generic;
using System.Security;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

using ASPNet_WPF_ChatApp.Core;
using ASPNet_WPF_ChatApp.Core.DataModels;
using ASPNet_WPF_ChatApp.Core.InversionOfControl.Base;
using ASPNet_WPF_ChatApp.Core.Security;
using ASPNet_WPF_ChatApp.Core.ViewModels.Base;
using ASPNet_WPF_ChatApp.Core.ViewModels.Input;

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
                // TODO: Fake a login...
                await Task.Delay(1000);

                // Ok, successfully logged in... Now get the user's data
                // TODO: Ask server for user's info

                // TODO: In the future, replace this with real information pulled from our database
                IoC.SettingsViewModel.Name = new TextEntryViewModel { Label = "Name", OriginalText = $"Michael Fontanini {DateTime.Now.ToLocalTime()}" };
                IoC.SettingsViewModel.Username = new TextEntryViewModel { Label = "Username", OriginalText = "Megafont" };
                IoC.SettingsViewModel.Password = new PasswordEntryViewModel { Label = "Password", FakePassword = "********" };
                IoC.SettingsViewModel.Email = new TextEntryViewModel { Label = "Email", OriginalText = "megafont@gmail.com" };


                // Go to chat page
                IoC.ApplicationViewModel.GoToPage(ApplicationPages.Chat);
                //var email = Email;

                // IMPORTANT: Never store an unsecure password in a variable like this!!! This is just temporary debug code.
                //var pass = (parameter as IHavePassword).SecurePassword.Unsecure();
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

