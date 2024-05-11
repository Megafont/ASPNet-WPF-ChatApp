using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using ASPNet_WPF_ChatApp.Core.ViewModels.Base;

namespace ASPNet_WPF_ChatApp.Core.ViewModels.Chat.ChatMessage
{
    /// <summary>
    /// A view model for each chat message item in a chat thread
    /// </summary>
    public class ChatMessageListItemViewModel: BaseViewModel
    {
        #region Public Properties

        /// <summary>
        /// The display name of the sender of the message
        /// </summary>
        public string SenderName { get; set; }

        /// <summary>
        /// The latest message from this chat
        /// </summary>
        public string Message { get; set; }

        /// <summary>
        /// The initials to show for the profile picture background
        /// </summary>
        public string Initials { get; set; }

        /// <summary>
        /// The RGB calues (in hex) for the background color of the profile picture
        /// Fore example FF00FF for Red and Blue mixed
        /// </summary>
        public string ProfilePictureRGB { get; set; }

        /// <summary>
        /// True if this item is currently selected
        /// </summary>
        public bool IsSelected { get; set; }

        /// <summary>
        /// True if this message was sent by the signed in user
        /// </summary>
        public bool SentByMe { get; set; }

        /// <summary>
        /// The time the message was read, or <see cref="DateTimeOffset.MinValue"/> if not read
        /// </summary>
        public DateTimeOffset MessageReadTime { get; set; }

        /// <summary>
        /// True if this message has been read
        /// </summary>
        public bool MessageRead => MessageReadTime > DateTimeOffset.MinValue;

        /// <summary>
        /// The time the message was sent
        /// </summary>
        public DateTimeOffset MessageSentTime { get; set; }

        /// <summary>
        /// A flag indicating if this item was added since the first main list of items was createdf
        /// Used as a flag for animating new messages in
        /// </summary>
        public bool NewItem { get; set; }

        /// <summary>
        /// The attachment to the message, if it is of an image type
        /// </summary>
        public ChatMessageListItemImageAttachmentViewModel ImageAttachment { get; set; }

        /// <summary>
        /// A flag indicating if we have a message or not
        /// </summary>
        public bool HasMessage => Message != null;


        /// <summary>
        /// A flag indicating if we have an image attached to this message
        /// </summary>
        public bool HasImageAttachment => ImageAttachment != null;

        #endregion
    }
}
