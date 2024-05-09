using System;
using System.Collections.Generic;
using System.Linq;
using System.Security;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using ASPNet_WPF_ChatApp.Core.InversionOfControl.Base;
using ASPNet_WPF_ChatApp.Core.Security;
using ASPNet_WPF_ChatApp.Core.ViewModels.Base;

namespace ASPNet_WPF_ChatApp.Core.ViewModels.Input
{
    /// <summary>
    /// The view model for a password entry field to edit a password value
    /// </summary>
    public class PasswordEntryViewModel : BaseViewModel
    {
        #region Public Properties

        /// <summary>
        /// The label to identity what this value is for
        /// </summary>
        public string Label { get; set; }

        /// <summary>
        /// The fake password display string
        /// </summary>
        public string FakePassword { get; set; }

        /// <summary>
        /// The current password hint text
        /// </summary>
        public string CurrentPasswordHintText { get; set; }

        /// <summary>
        /// The new password hint text
        /// </summary>
        public string NewPasswordHintText { get; set; }

        /// <summary>
        /// The confirm password hint text
        /// </summary>
        public string ConfirmPasswordHintText { get; set; }

        /// <summary>
        /// The current saved password
        /// </summary>
        public SecureString CurrentPassword { get; set; }

        /// <summary>
        /// The currrent edited password that has not been submited yet.
        /// </summary>
        public SecureString NewPassword { get; set; }

        /// <summary>
        /// The currrent confirmed password that has not been submited yet.
        /// </summary>
        public SecureString ConfirmPassword { get; set; }

        /// <summary>
        /// Indicates if the current password is in edit mode
        /// </summary>
        public bool Editing { get; set; }

        #endregion

        #region Public Commands

        /// <summary>
        /// Puts the control into edit mode
        /// </summary>
        public ICommand EditCommand { get; set; }

        /// <summary>
        /// Cancels out of edit mode
        /// </summary>
        public ICommand CancelCommand { get; set; }

        /// <summary>
        /// Commits the changes and saves the value
        /// as well as goes back to non-edit mode
        /// </summary>
        public ICommand SaveCommand { get; set; }

        #endregion

        #region Constructors

        /// <summary>
        /// Default constructor
        /// </summary>
        public PasswordEntryViewModel()
        {
            // Create commands
            EditCommand = new RelayCommand(Edit);
            CancelCommand = new RelayCommand(Cancel);
            SaveCommand = new RelayCommand(Save);

            // Set the default hints
            // TODO: Replace with localization text
            CurrentPasswordHintText = "Current Password";
            NewPasswordHintText = "New Password";
            ConfirmPasswordHintText = "Confirm Password";
        }

        #endregion

        #region Command Methods

        /// <summary>
        /// Puts the control into edit mode
        /// </summary>
        private void Edit()
        {

            // Go into edit mode
            Editing = true;
        }

        /// <summary>
        /// Cancels out of edit mode
        /// </summary>
        private void Cancel()
        {
            // Clearr all paswords
            NewPassword = new SecureString();
            ConfirmPassword = new SecureString();

            Editing = false;
        }

        /// <summary>
        /// Commits the changes and saves the value
        /// as well as goes back to non-edit mode
        /// </summary>
        private void Save()
        {
            // Make sure current password is correct
            // TODO: This will come from the real back-end store of this user's password
            //       or via asking the web  server to confirm it
            var storedPassword = "Testing";

            // Confirm current password is a match
            // NOTE: Typically this isn't done here, it's done on the server
            if (storedPassword != CurrentPassword.Unsecure())
            {
                // Let the user know
                IoC.UI.ShowMessage(new Dialogs.MessageBoxDialogViewModel
                {
                    Title = "Wrong Password",
                    Message = "The current password is invalid!"
                });

                return;
            }

            // Now check that the new and confirm password values match
            if (NewPassword.Unsecure() != ConfirmPassword.Unsecure())
            {
                // Let the user know
                IoC.UI.ShowMessage(new Dialogs.MessageBoxDialogViewModel
                {
                    Title = "Password Mismatch",
                    Message = "The new and confirm password values do not match!"
                });

                return;
            }

            // Check that we actually have a password
            if (NewPassword.Unsecure().Length == 0)
            {
                // Let the user know
                IoC.UI.ShowMessage(new Dialogs.MessageBoxDialogViewModel
                {
                    Title = "Password Too Short",
                    Message = "You must enter a password!"
                });

                return;
            }

            // Set the edited password to the current value
            CurrentPassword = new SecureString();
            foreach (var c in NewPassword.Unsecure().ToCharArray())
                CurrentPassword.AppendChar(c);


            Editing = false;
        }

        #endregion

    }
}
