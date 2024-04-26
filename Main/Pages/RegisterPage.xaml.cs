using System.Security;
using System.Windows;
using System.Windows.Input;

using ASPNet_WPF_ChatApp.Core.ViewModels;
using ASPNet_WPF_ChatApp.Core.ViewModels.Base;

namespace ASPNet_WPF_ChatApp.Pages
{
    /// <summary>
    /// Interaction logic for RegisterPage.xaml
    /// </summary>
    public partial class RegisterPage : BasePage<RegisterViewModel>, IHavePassword
    {
        public RegisterPage()
        {
            InitializeComponent();
        }

        /// <summary>
        /// The secure password for this login page
        /// </summary>
        public SecureString SecurePassword => PasswordText.SecurePassword;
    }
}
