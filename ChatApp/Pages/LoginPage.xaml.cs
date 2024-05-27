using System.Security;
using System.Windows;
using System.Windows.Input;

using ASPNet_WPF_ChatApp.ViewModels.Application;
using ASPNet_WPF_ChatApp.ViewModels.Base;

namespace ASPNet_WPF_ChatApp.Pages
{
    /// <summary>
    /// Interaction logic for LoginPage.xaml
    /// </summary>
    public partial class LoginPage : BasePage<LoginViewModel>, IHavePassword
    {
        #region Constructors

        /// <summary>
        /// Default constructor
        /// </summary>
        public LoginPage()
            : base()
        {
            InitializeComponent();
        }

        /// <summary>
        /// A constructor that takes a view model
        /// </summary>
        public LoginPage(LoginViewModel specificViewModel)
            : base(specificViewModel)
        {
            InitializeComponent();
        }

        #endregion

        #region Public Properties

        /// <summary>
        /// The secure password for this login page
        /// </summary>
        public SecureString SecurePassword => PasswordText.SecurePassword; 

        #endregion
    }
}
