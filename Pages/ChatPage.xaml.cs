using System.Security;
using System.Windows;
using System.Windows.Input;

using ASPNet_WPF_ChatApp.ViewModels;
using ASPNet_WPF_ChatApp.ViewModels.Base;

namespace ASPNet_WPF_ChatApp.Pages
{
    /// <summary>
    /// Interaction logic for LoginPage.xaml
    /// </summary>
    public partial class ChatPage : BasePage<LoginViewModel>
    {
        public ChatPage()
        {
            InitializeComponent();
        }

    }
}
