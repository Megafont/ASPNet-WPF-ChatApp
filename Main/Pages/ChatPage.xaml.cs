using System.Security;
using System.Windows;
using System.Windows.Input;

using ASPNet_WPF_ChatApp.Core.ViewModels;
using ASPNet_WPF_ChatApp.Core.ViewModels.Base;
using ASPNet_WPF_ChatApp.Core.ViewModels.Chat.ChatMessage;


namespace ASPNet_WPF_ChatApp.Pages
{
    /// <summary>
    /// Interaction logic for LoginPage.xaml
    /// </summary>
    public partial class ChatPage : BasePage<ChatMessageListViewModel>
    {
        /// <summary>
        /// Default constructor
        /// </summary>
        public ChatPage() : base()
        {

        }

        /// <summary>
        /// Constructor that takes a view model
        /// </summary>
        /// <param name="specificViewModel">The specific view model to use for this page</param>
        public ChatPage(ChatMessageListViewModel specificViewModel)
            : base(specificViewModel)
        {
            InitializeComponent();
        }

    }
}
