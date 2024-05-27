using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using ASPNet_WPF_ChatApp.Pages;
using ASPNet_WPF_ChatApp.Core.DataModels;
using ASPNet_WPF_ChatApp.ViewModels.Chat.ChatMessage;
using ASPNet_WPF_ChatApp.ViewModels.Application;


namespace ASPNet_WPF_ChatApp.ValueConverters
{
    /// <summary>
    /// Converts a <see cref="ApplicationPages"/> value to an actual view/page
    /// </summary>
    public static class ApplicationPageHelpers
    {
        /// <summary>
        /// Takes a <see cref="ApplicationPages"/> value and a view model, if any, and creates the desired page
        /// </summary>
        /// <param name="page"></param>
        /// <param name="viewModel"></param>
        /// <returns></returns>
        public static BasePage ToBasePage(this ApplicationPages page, object viewModel = null)
        {
            // Find the appropriate page
            switch (page)
            {
                case ApplicationPages.Login:
                    return new LoginPage(viewModel as LoginViewModel);

                case ApplicationPages.Register:
                    return new RegisterPage(viewModel as RegisterViewModel);

                case ApplicationPages.Chat:
                    return new ChatPage(viewModel as ChatMessageListViewModel);

                default:
                    Debugger.Break();
                    return null;
            }
        }

        /// <summary>
        /// Takes a <see cref="BasePage"/> and returns its type as a <see cref="ApplicationPages"/> value
        /// </summary>
        /// <param name="page"></param>
        /// <returns></returns>
        public static ApplicationPages ToApplicationPage(this BasePage page)
        {
            switch (page)
            {
                case LoginPage:
                    return ApplicationPages.Login;
                case RegisterPage:
                    return ApplicationPages.Register;
                case ChatPage:
                    return ApplicationPages.Chat;

                default:
                    Console.WriteLine($"ERROR: Could not get the type for the passed in page (Type=\"{page.GetType().Name}\")! Returning default page type.");
                    Debugger.Break();
                    return default(ApplicationPages);
            }
        }
    }
}
