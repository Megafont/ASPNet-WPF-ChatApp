using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using ASPNet_WPF_ChatApp.Core.InversionOfControl.Base;
using ASPNet_WPF_ChatApp.Core.ViewModels.Base;
using ASPNet_WPF_ChatApp.Core.ViewModels.PopupMenus;

namespace ASPNet_WPF_ChatApp.Core.ViewModels.Chat.ChatMessage
{
    /// <summary>
    /// A view model for a chat message thread list
    /// </summary>
    public class ChatMessageListViewModel : BaseViewModel
    {
        #region Protected Members

        /// <summary>
        /// The last searched text in this list
        /// </summary>
        protected string _LastSearchText;

        /// <summary>
        /// The text to search for in the search command
        /// </summary>
        protected string _SearchText;

        /// <summary>
        /// The chat thread items for the list
        /// </summary>
        protected ObservableCollection<ChatMessageListItemViewModel> _Items;

        /// <summary>
        /// A flag indicating if the search dialog is open
        /// </summary>
        protected bool _SearchIsOpen;

        #endregion

        #region Public Properties

        /// <summary>
        /// The chat thread items for the list
        /// NOTE: Do not call Items.Add to add messages to this list
        ///       as it will make the FilteredItems out of sync
        /// </summary>
        public ObservableCollection<ChatMessageListItemViewModel> Items 
        { 
            get => _Items;
            set
            {
                // Make sure list has changed
                if (_Items == value)
                    return;

                // Update value
                _Items = value;

                // Update filtered list to match
                FilteredItems = new ObservableCollection<ChatMessageListItemViewModel>(_Items);
            }
        }

        /// <summary>
        /// The chat thread items from the list that passed through any search filtering
        /// </summary>
        public ObservableCollection<ChatMessageListItemViewModel> FilteredItems { get; set; }

        /// <summary>
        /// The title of this chat list
        /// </summary>
        public string DisplayTitle { get; set; }

        /// <summary>
        /// True to show the attachment menu, false to hide it
        /// </summary>
        public bool AttachmentMenuVisible { get; set; }

        /// <summary>
        /// True if any pop-up menus are visible
        /// </summary>
        public bool AnyPopupVisible => AttachmentMenuVisible;

        /// <summary>
        /// The view model for the attachment menu
        /// </summary>
        public ChatAttachmentPopupMenuViewModel AttachmentMenu { get; set; }

        /// <summary>
        /// The text for the current message being written
        /// </summary>
        public string PendingMessageText { get; set; }

        /// <summary>
        /// The text to search for when we do a search
        /// </summary>
        public string SearchText
        {
            get => _SearchText;
            set
            {
                // Check value is different
                if (_SearchText == value)
                    return;

                // Update value
                _SearchText = value;

                // If search text is empty
                if (string.IsNullOrWhiteSpace(SearchText))
                {
                    // Search to restore messages
                    Search();
                }
            }
        }

        /// <summary>
        /// A flag indicating if the search dialog is open
        /// </summary>
        public bool SearchIsOpen
        {
            get => _SearchIsOpen;
            set
            {
                // Check value has changed
                if (_SearchIsOpen == value)
                    return;

                // Update value
                _SearchIsOpen = value;

                // If dialog closes
                if (!_SearchIsOpen)
                {
                    // Clear search text
                    SearchText = string.Empty;
                }
            }
        }
        #endregion

        #region Public Commands

        /// <summary>
        /// The command for when the attachment button is clicked
        /// </summary>
        public ICommand AttachmentButtonCommand { get; set; }

        /// <summary>
        /// The command for when the area outside of any pop-up is clicked
        /// </summary>
        public ICommand PopupClickAwayCommand { get; set; }

        /// <summary>
        /// The command for when the send button is clicked
        /// </summary>
        public ICommand SendCommand { get; set; }

        /// <summary>
        /// The command for when the user wants to search
        /// </summary>
        public ICommand SearchCommand { get; set; }

        /// <summary>
        /// The command for when the user wants to open the search dialog
        /// </summary>
        public ICommand CloseSearchCommand { get; set; }

        /// <summary>
        /// The command for when the user wants to close the search dialog
        /// </summary>
        public ICommand OpenSearchCommand { get; set; }

        /// <summary>
        /// The command for when the user wants to clear the search text
        /// </summary>
        public ICommand ClearSearchCommand { get; set; }

        #endregion

        #region Constructors

        /// <summary>
        /// Default constructor
        /// </summary>
        public ChatMessageListViewModel()
        {
            // Create commands
            AttachmentButtonCommand = new RelayCommand(AttachmentButtonClicked);
            PopupClickAwayCommand = new RelayCommand(PopupClickAway);
            SendCommand = new RelayCommand(SendButtonClicked);
            SearchCommand = new RelayCommand(Search);
            OpenSearchCommand = new RelayCommand(OpenSearch);
            CloseSearchCommand = new RelayCommand(CloseSearch);
            ClearSearchCommand = new RelayCommand(ClearSearch);


            // Make a default menu
            AttachmentMenu = new ChatAttachmentPopupMenuViewModel();
        }

        #endregion

        #region Command Methods

        /// <summary>
        /// When the attachment button is clicked, show/hide the attachment pop-up
        /// </summary>
        public void AttachmentButtonClicked()
        {
            // Toggle menu visibility
            AttachmentMenuVisible ^= true;
        }

        /// <summary>
        /// When the when the area outside of any popup is clicked, hide all pop-ups
        /// </summary>
        public void PopupClickAway()
        {
            // Hide attachment menu
            AttachmentMenuVisible = false;
        }

        /// <summary>
        /// Sends the message when the user clicks the send button
        /// </summary>
        public async void SendButtonClicked()
        {
            // Do not allow user to send a blank message
            if (string.IsNullOrWhiteSpace(PendingMessageText))
            {
                return;
            }


            // Ensure lists are not null
            if (Items == null)
                Items = new ObservableCollection<ChatMessageListItemViewModel>();
            if (FilteredItems == null)
                FilteredItems = new ObservableCollection<ChatMessageListItemViewModel>();

            // Fake send a new message
            var message = new ChatMessageListItemViewModel
            {
                Initials = "MF",
                Message = PendingMessageText,
                MessageSentTime = DateTime.UtcNow,
                SentByMe = true,
                SenderName = "Michael Fontanini",
                NewItem = true,
            };

            // Add message to both lists
            Items.Add(message);
            FilteredItems.Add(message);

            // Clear the pending message text
            PendingMessageText = string.Empty;
        }

        /// <summary>
        /// Searches the current message list and filters the view
        /// </summary>
        public void Search()
        {
            // Make sure we don't re-search the same text
            if ((string.IsNullOrWhiteSpace(_LastSearchText) && string.IsNullOrWhiteSpace(SearchText)) ||
                string.Equals(_LastSearchText, SearchText))
            {
                return;
            }

            // If we have no search text, or no items
            if (string.IsNullOrWhiteSpace(SearchText) || Items == null || Items.Count <= 0)
            {
                // Make filtered list the same
                FilteredItems = new ObservableCollection<ChatMessageListItemViewModel>(Items);

                // Set last search string
                _LastSearchText = SearchText;

                return;
            }

            // Find all items that contain the given text
            // TODO: Make more efficient search
            FilteredItems = new ObservableCollection<ChatMessageListItemViewModel>(
                Items.Where(item => item.Message.ToLower().Contains(SearchText.ToLower())));

            // Set last search string
            _LastSearchText = SearchText;
        }

        /// <summary>
        /// Clears the search text
        /// </summary>
        public void ClearSearch()
        {
            // If there is some search text
            if (!string.IsNullOrWhiteSpace(SearchText))
            {
                // Clear the text
                SearchText = string.Empty;
            }
            else
            {
                // Close search dialog
                SearchIsOpen = false;
            }

        }

        /// <summary>
        /// Opens the search dialog
        /// </summary>
        public void OpenSearch() => SearchIsOpen = true;

        /// <summary>
        /// Closes the search dialog
        /// </summary>
        public void CloseSearch() => SearchIsOpen = false;

        #endregion

    }
}
