using System.Configuration;
using System.Data;
using System.Windows;

using ASPNet_WPF_ChatApp.Core.InversionOfControl.Base;
using ASPNet_WPF_ChatApp.Core.InversionOfControl.Interfaces;
using ASPNet_WPF_ChatApp.Core.Logging;
using ASPNet_WPF_ChatApp.InversionOfControl;


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
        protected override void OnStartup(StartupEventArgs e)
        {
            // Let the base application do what it needs to
            base.OnStartup(e);

            // Setup the main application
            ApplicationSetup();

            // Log it
            IoC.Logger.Log("This is Debug", LogLevel.Debug);
            IoC.Logger.Log("This is Verbose", LogLevel.Verbose);
            IoC.Logger.Log("This is Informative", LogLevel.Informative);
            IoC.Logger.Log("This is Warning", LogLevel.Warning);
            IoC.Logger.Log("This is Error", LogLevel.Error);
            IoC.Logger.Log("This is Success", LogLevel.Success);

            // Show the main window
            Current.MainWindow = new MainWindow();
            Current.MainWindow.Show();
        }

        /// <summary>
        /// Configures our application ready for use
        /// </summary>
        private void ApplicationSetup()
        {
            // Setup IoC container
            IoC.Setup();

            // Bind a UI manager
            IoC.Kernel.Bind<IUIManager>().ToConstant(new UIManager());

            // Bind a logger
            IoC.Kernel.Bind<ILogFactory>().ToConstant(new BaseLogFactory());

        }
    }

}
