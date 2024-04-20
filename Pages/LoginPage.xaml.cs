using System.Windows;
using System.Windows.Input;

using ASPNet_WPF_ChatApp.Pages;

namespace ASPNet_WPF_ChatApp
{
    /// <summary>
    /// Interaction logic for LoginPage.xaml
    /// </summary>
    public partial class LoginPage : BasePage
    {
        public LoginPage()
        {
            InitializeComponent();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            this.AnimateOut();
        }
    }
}
