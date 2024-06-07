using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

using ASPNet_WPF_ChatApp.Core.DependencyInjection;
using ASPNet_WPF_ChatApp.ViewModels.Application;


// This makes it so we can access members on this static class without needing to write "ChatAppDI." first.
using static ASPNet_WPF_ChatApp.DependencyInjection.ChatAppDI;

namespace ASPNet_WPF_ChatApp.Controls
{
    /// <summary>
    /// Interaction logic for SettingsControl.xaml
    /// </summary>
    public partial class SettingsControl : UserControl
    {
        public SettingsControl()
        {
            InitializeComponent();

            // If are in design mode...
            if (DesignerProperties.GetIsInDesignMode(this))
            {
                // Create new instance of settings view model
                DataContext = new SettingsViewModel();
            }
            else
            {
                // Set data context to settings view model
                DataContext = ViewModel_Settings;
            }
        }
    }
}
