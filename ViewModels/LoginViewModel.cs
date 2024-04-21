using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.InteropServices;
using System.Security;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using System.Windows.Shell;

using ASPNet_WPF_ChatApp.DataModels;
using ASPNet_WPF_ChatApp.Security;
using ASPNet_WPF_ChatApp.ViewModels.Base;
using ASPNet_WPF_ChatApp.Window;


namespace ASPNet_WPF_ChatApp.ViewModels
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
        /// The command to open the system menu
        /// </summary>
        public ICommand LoginCommand { get; set; }

        #endregion

        #region Constructors        

        /// <summary>
        /// Default constructor
        /// </summary>
        public LoginViewModel()
        {
            // Create commands
            LoginCommand = new RelayParameterizedCommand(async (parameter) => await Login(parameter));
        }

        #endregion

        /// <summary>
        /// Attemps to log the user in
        /// </summary>
        /// <param name="parameter">The <see cref="SecureString"/> passed in from the view for the user's password</param>
        /// <returns></returns>
        /// <exception cref="NotImplementedException"></exception>
        public async Task Login(object parameter)
        {
            if (LoginIsRunning)
                return;



            await RunCommand(() => this.LoginIsRunning, async () =>
            {
                await Task.Delay(5000);

                var email = this.Email;

                // IMPORTANT: Never store an unsecure password in a variable like this!!! This is just temporary debug code.
                var pass = (parameter as IHavePassword).SecurePassword.Unsecure();
            });

        }

    }
}

