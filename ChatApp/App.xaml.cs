using System.Configuration;
using System.Data;
using System.Windows;

using Dna;

using APSNet_WPF_ChatApp.RelationalDB;
using ASPNet_WPF_ChatApp.Core.DataModels;
using ASPNet_WPF_ChatApp.Core.FileSystem;
using ASPNet_WPF_ChatApp.Core.InversionOfControl.Base;
using ASPNet_WPF_ChatApp.Core.InversionOfControl.Interfaces;
using ASPNet_WPF_ChatApp.Core.Logging;
using ASPNet_WPF_ChatApp.Core.Tasks;
using ASPNet_WPF_ChatApp.InversionOfControl;

using FileLogger = ASPNet_WPF_ChatApp.Core.Logging.FileLogger;


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
            IoC.Logger.Log("Application starting...", LogLevel.Debug);

            
            // Setup the application view model based on if we are logged in
            IoC.ApplicationViewModel.GoToPage(
                // If we are logged in...
                await IoC.ClientDataStore.HasCredentialsAsync() 
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
            // Setup the Dna Framework
            // I had to change the first line here from "new DefaultFrameworkConstruction()" to what it is now to fix a null reference exception.
            Framework.Construct<DefaultFrameworkConstruction>()
                .AddFileLogger()
                .UseClientDataStore()
                .Build();

            // Setup IoC container
            IoC.Setup();
            

            // Bind a logger
            IoC.Kernel.Bind<ILogFactory>().ToConstant(new BaseLogFactory(new[]
            {
                // TODO: Add ApplicationSettings so we can set/edit a log location.
                //       For now, just log to the path where this application is running.
                new FileLogger("log.txt"),
            }));

            // Bind a task manager
            IoC.Kernel.Bind<ITaskManager>().ToConstant(new TaskManager());

            // Bind a file manager
            IoC.Kernel.Bind<IFileManager>().ToConstant(new FileManager());

            // Bind a UI manager
            IoC.Kernel.Bind<IUIManager>().ToConstant(new UIManager());

            
            // Ensure the client data store
            await IoC.ClientDataStore.EnsureDataStoreAsync();

            // Load new settings
            await IoC.SettingsViewModel.LoadSettingsAsync();
        }
    }

}
