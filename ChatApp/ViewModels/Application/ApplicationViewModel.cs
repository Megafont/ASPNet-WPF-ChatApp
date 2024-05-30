using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using ASPNet_WPF_ChatApp.Core.ApiModels;
using ASPNet_WPF_ChatApp.Core.DataModels;
using ASPNet_WPF_ChatApp.Core.DependencyInjection;
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
        public bool SettingsMenuVisible { get; set; }



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
            // Store it in the client data store
            await ClientDataStore.SaveLoginCredentialsAsync(new LoginCredentialsDataModel
            {
                Id = Guid.NewGuid().ToString("N"), // I had to add this line to stop it complaining that this field was null
                Email = loginResult.Email,
                FirstName = loginResult.FirstName,
                LastName = loginResult.LastName,
                UserName = loginResult.UserName,
                Token = loginResult.Token
            });


            // Load new settings
            await ViewModel_Settings.LoadSettingsAsync();

            // Go to chat page
            ViewModel_Application.GoToPage(ApplicationPages.Chat);
        }
    }
}
