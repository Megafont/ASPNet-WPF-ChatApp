using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using ASPNet_WPF_ChatApp.Core.ViewModels.Base;

namespace ASPNet_WPF_ChatApp.Core.ViewModels.Chat.ChatMessage
{
    /// <summary>
    /// A view model for each chat message item's attachment in a chat thread
    /// </summary>
    public class ChatMessageListItemImageAttachmentViewModel: BaseViewModel
    {
        #region Private Members

        /// <summary>
        /// The thumbnail URL of this attachment
        /// </summary>
        private string _ThumbnailURL;

        #endregion

        #region Public Properties

        /// <summary>
        /// The title of this attachment
        /// </summary>
        public string Title { get; set; }

        /// <summary>
        /// The original file name of the attachment
        /// </summary>
        public string FileName { get; set; }

        /// <summary>
        /// The file size of this attachment in bytes
        /// </summary>
        public long FileSize { get; set; }

        /// <summary>
        /// The thumbnail URL of this attachment
        /// </summary>
        public string ThumbnailURL
        {
            get => _ThumbnailURL;
            set
            {
                // If the value hasn't changed, return
                if (value == _ThumbnailURL)
                    return;

                // Update value
                _ThumbnailURL = value;

                // TODO: Download image from website
                //       Save file to local storage/cache
                //       Set LocalFilePath
                //
                //       For now, just set the file path directly
                Task.Delay(2000).ContinueWith(t => LocalFilePath = "/Images/Samples/Willow.png");
            }
        }

        /// <summary>
        /// The local file path on this machine to the downloaded thumbnail
        /// </summary>
        public string LocalFilePath { get; set; }

        /// <summary>
        /// Indicates if an image has loaded
        /// </summary>
        public bool ImageLoaded => LocalFilePath != null;

        #endregion
    }
}
