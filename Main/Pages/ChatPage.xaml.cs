using System.Security;
using System.Windows;
using System.Windows.Input;
using System.Windows.Media.Animation;
using ASPNet_WPF_ChatApp.Animations;
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
        #region Constructors

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

        #endregion

        #region Override Methods

        /// <summary>
        /// Fired when the view model changes
        /// </summary>
        protected override void OnViewModelChanged()
        {
            // Make sure UI exists first
            if (ChatMessageList == null)
                return;

            // Fade in chat message list
            var storyboard = new Storyboard();
            storyboard.AddFadeIn(1);
            storyboard.Begin(ChatMessageList);
        }

        #endregion
    }
}
