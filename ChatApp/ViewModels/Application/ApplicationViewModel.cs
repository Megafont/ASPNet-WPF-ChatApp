using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using ASPNet_WPF_ChatApp.Core.ApiModels;
using ASPNet_WPF_ChatApp.Core.DataModels;
using ASPNet_WPF_ChatApp.Core.DependencyInjection;
using ASPNet_WPF_ChatApp.Core.Tasks;
using ASPNet_WPF_ChatApp.ViewModels.Base;

// This makes it so we can access members on this static class without needing to write "ChatAppDI." first.
using static ASPNet_WPF_ChatApp.DependencyInjection.ChatAppDI;


namespace ASPNet_WPF_ChatApp.ViewModels.Application
{
    /// <summary>
    /// The application state as a view model
    /// </summary>
    public class ApplicationViewModel : BaseViewModel
    {
        #region Private Members

        private bool _SettingsMenuVisible;

        #endregion

        #region Public Properties

        /// <summary>
        /// The current page of the application
        /// </summary>
        public ApplicationPages CurrentPage { get; private set; } = ApplicationPages.Login;

        /// <summary>
        /// The view model to use for the current page when the current page changes
        /// NOTE: This is not a live up-to-date view model of the current page;
        ///       it is simply used to set the view model of the current page
        ///       at the time it changes
        /// </summary>
        public BaseViewModel CurrentPageViewModel { get; set; }

        /// <summary>
        /// True if the settings menu should be shown
        /// </summary>
        public bool SideMenuVisible { get; set; } = false;

        /// <summary>
        /// True if the settings menu should be shown
        /// </summary>
        public bool SettingsMenuVisible 
        { 
            get => _SettingsMenuVisible; 
            set
            {
                // If property has not changed, then do nothing.
                if (_SettingsMenuVisible == value)
                    return;

                // Set the backing field
                _SettingsMenuVisible = value;

                // If the settings menu is now visible...
                if (value)
                {
                    // Reload settings values
                    CoreDI.TaskManager.RunAndForget(ViewModel_Settings.LoadSettingsAsync);
                }

            }
        }

        /// <summary>
        /// Determines the currently visible side menu content
        /// </summary>
        public SideMenuContent CurrentSideMenuContent { get; set; } = SideMenuContent.Chat;

        #endregion

        #region Public Commands

        /// <summary>
        /// The command to change the side menu to Chat
        /// </summary>
        public ICommand OpenChatCommand { get; set; }

        /// <summary>
        /// The command to change the side menu to Contacts
        /// </summary>
        public ICommand OpenContactsCommand { get; set; }

        /// <summary>
        /// The command to change the side menu to Media
        /// </summary>
        public ICommand OpenMediaCommand { get; set; }

        #endregion

        #region Constructors

        /// <summary>
        /// The default constructor
        /// </summary>
        public ApplicationViewModel()
        {
            // Create the commands
            OpenChatCommand = new RelayCommand(OpenChat);
            OpenContactsCommand = new RelayCommand(OpenContacts);
            OpenMediaCommand = new RelayCommand(OpenMedia);
        }

        #endregion

        #region Command Methods

        /// <summary>
        /// Changes the current side menu content to Chat
        /// </summary>
        public void OpenChat()
        {
            // Set the current side menu content to Chat
            CurrentSideMenuContent = SideMenuContent.Chat;
        }

        /// <summary>
        /// Changes the current side menu content to Contacts
        /// </summary>
        public void OpenContacts()
        {
            // Set the current side menu content to Contacts
            CurrentSideMenuContent = SideMenuContent.Contacts;
        }

        /// <summary>
        /// Changes the current side menu content to Media
        /// </summary>
        public void OpenMedia()
        {
            // Set the current side menu content to Media
            CurrentSideMenuContent = SideMenuContent.Media;
        }

        #endregion

        #region Public Helper Methods

        /// <summary>
        /// Navigates to the specified page
        /// </summary>
        /// <param name="page">The page to go to</param>
        /// <param name="viewModel">The view model, if any, to set explicitly to the new page</param>
        public void GoToPage(ApplicationPages page, BaseViewModel viewModel = null)
        {
            // Always hide the settings page if we are changing pages
            SettingsMenuVisible = false;

            // Set the view model
            CurrentPageViewModel = viewModel;

            // Set the current page
            CurrentPage = page;

            // Fire off a CurrentPage changed event
            OnPropertyChanged(nameof(CurrentPage));

            // Show side menu or not
            SideMenuVisible = page == ApplicationPages.Chat;
        }

        /// <summary>
        /// Handles what happens when we have successfully logged in
        /// </summary>
        /// <param name="loginResult">The results from the successful login</param>
        /// <returns></returns>     
        public async Task HandleSuccessfulLoginAsync(UserProfileDetailsApiModel loginResult)
        {
            // Convert the login result to a LoginCredentialsDataModel
            LoginCredentialsDataModel dataModel = loginResult.ToLoginCredentialsDataModel();

            // Store this in the client data store
            await ClientDataStore.SaveLoginCredentialsAsync(dataModel);

            // Load new settings
            await ViewModel_Settings.LoadSettingsAsync();

            // Go to chat page
            ViewModel_Application.GoToPage(ApplicationPages.Chat);
        }

        #endregion
    }
}
