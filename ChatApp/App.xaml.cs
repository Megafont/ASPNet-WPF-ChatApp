using System.Configuration;
using System.Data;
using System.Windows;

using Dna;

using APSNet_WPF_ChatApp.RelationalDB;
using ASPNet_WPF_ChatApp.Core.DataModels;
using ASPNet_WPF_ChatApp.Core.FileSystem;
using ASPNet_WPF_ChatApp.Core.DependencyInjection;
using ASPNet_WPF_ChatApp.Core.DependencyInjection.Interfaces;
using ASPNet_WPF_ChatApp.Core.Logging;
using ASPNet_WPF_ChatApp.Core.Tasks;
using ASPNet_WPF_ChatApp.DependencyInjection;

using FileLogger = ASPNet_WPF_ChatApp.Core.Logging.FileLogger;

// This makes it so we can access members on this static class without needing to write "ChatAppDI." first.
using static ASPNet_WPF_ChatApp.DependencyInjection.ChatAppDI;
// This makes it so we can access members on this static class without needing to write "FrameworkDI." first.
using static Dna.FrameworkDI;

namespace ASPNet_WPF_ChatApp
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        /// <summary>
        /// Custom startup so we load our Inversion of Control container before anything else
        /// </summary>
        /// <param name="e"></param>
        protected override async void OnStartup(StartupEventArgs e)
        {
            // Let the base application do what it needs to
            base.OnStartup(e);

            // Setup the main application
            await ApplicationSetupAsync();

            // Log it
            Logger.LogDebugSource("Application starting...");

            
            // Setup the application view model based on if we are logged in
            ViewModel_Application.GoToPage(
                // If we are logged in...
                await ClientDataStore.HasCredentialsAsync() 
                // Go to chat page
                ? ApplicationPages.Chat 
                // Otherwise, go to login page
                : ApplicationPages.Login);
            

            // Show the main window
            Current.MainWindow = new MainWindow();
            Current.MainWindow.Show();
        }

        /// <summary>
        /// Configures our application ready for use
        /// </summary>
        private async Task ApplicationSetupAsync()
        {
            // Setup the Dna Framework (dependency injection)
            // I had to change the first line here from "new DefaultFrameworkConstruction()" to what it is now to fix a null reference exception.
            Framework.Construct<DefaultFrameworkConstruction>()
                .AddFileLogger()
                .UseClientDataStore()
                .AddChatAppViewModels()
                .AddChatAppClientServices()
                .Build();

            
            // Ensure the client data store
            await ClientDataStore.EnsureDataStoreAsync();

            // Load new settings (this is actually CoreDI.SettingsViewModel, but the static using at the top of this file let's us write it without qualifying it with "CoreDI.").
            await ViewModel_Settings.LoadSettingsAsync();
        }
    }

}
