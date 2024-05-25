using System.Security;
using System.Windows;
using System.Windows.Input;

using ASPNet_WPF_ChatApp.Core.ViewModels.Application;
using ASPNet_WPF_ChatApp.Core.ViewModels.Base;

namespace ASPNet_WPF_ChatApp.Pages
{
    /// <summary>
    /// Interaction logic for RegisterPage.xaml
    /// </summary>
    public partial class RegisterPage : BasePage<RegisterViewModel>, IHavePassword
    {
        #region Constructors
        
        /// <summary>
        /// Default constructor
        /// </summary>
        public RegisterPage()
            : base()
        {
            InitializeComponent();
        }

        /// <summary>
        /// A constructor that takes a view model
        /// </summary>
        public RegisterPage(RegisterViewModel specificViewModel)
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
