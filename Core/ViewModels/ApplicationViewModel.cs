using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using ASPNet_WPF_ChatApp.Core.DataModels;
using ASPNet_WPF_ChatApp.Core.ViewModels.Base;

namespace ASPNet_WPF_ChatApp.Core.ViewModels
{
    /// <summary>
    /// The application state as a view model
    /// </summary>
    public class ApplicationViewModel : BaseViewModel
    {
        /// <summary>
        /// The current page of the application
        /// </summary>
        public ApplicationPages CurrentPage { get; private set; } = ApplicationPages.Chat;

        /// <summary>
        /// True if the settings menu should be shown
        /// </summary>
        public bool SideMenuVisible { get; set; } = true;

        public bool SettingsMenuVisible { get; set; }

        /// <summary>
        /// Navigates to the specified page
        /// </summary>
        /// <param name="page">The page to go to</param>
        public void GoToPage(ApplicationPages page)
        {
            // Set the current page
            CurrentPage = page;

            // Show side menu or not
            SideMenuVisible = page == ApplicationPages.Chat;
        }
    }
}
