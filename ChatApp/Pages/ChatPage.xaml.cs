using System.Security;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media.Animation;

using ASPNet_WPF_ChatApp.Animations;
using ASPNet_WPF_ChatApp.ViewModels;
using ASPNet_WPF_ChatApp.ViewModels.Base;
using ASPNet_WPF_ChatApp.ViewModels.Chat.ChatMessage;


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

            // Make the text box focused
            MessageText.Focus();
        }

        #endregion

        /// <summary>
        /// Preview the input into the text box and respond as required
        /// </summary>
        public void MessageText_PreviewKeyDown(object sender, KeyEventArgs e)
        {
            // Get the text box
            var textBox = sender as TextBox;

            // Check if the user is pressing Enter
            if (e.Key == Key.Enter) 
            {
                // Check if the user is also holding down the control key
                if (Keyboard.Modifiers.HasFlag(ModifierKeys.Control))
                { 
                    // Add a new line at the point where the cursor is
                    var index = textBox.CaretIndex;

                    // Insert the new line
                    textBox.Text = textBox.Text.Insert(index, Environment.NewLine);

                    // Shift the caret forward to the new line
                    textBox.CaretIndex = index + Environment.NewLine.Length;

                    // Mark this key press as handled by us
                    e.Handled = true;
                }
                else
                {
                    // The user is not holding down the Ctrl key with this Enter key press, so send the message
                    ViewModel.SendButtonClicked();

                    // Mark this key press as handled by us
                    e.Handled = true;
                }
            }
        }

    }
}
