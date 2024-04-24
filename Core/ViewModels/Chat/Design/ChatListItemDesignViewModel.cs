using System;
using System.CodeDom;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ASPNet_WPF_ChatApp.Core.ViewModels.Chat.Design
{
    /// <summary>
    /// The design-time data for a <see cref="ChatListItemViewModel"/>
    /// </summary>
    public class ChatListItemDesignViewModel : ChatListItemViewModel
    {
        #region Singleton

        /// <summary>
        /// A single instance of this design model
        /// </summary>
        public static ChatListItemDesignViewModel Instance => new ChatListItemDesignViewModel();

        #endregion

        #region Constructors

        /// <summary>
        /// Default constructor
        /// </summary>
        public ChatListItemDesignViewModel()
        {
            if (string.IsNullOrWhiteSpace(Name))
            {
                Initials = "LM";
                Name = "Luke";
                Message = "This new chat app is awesome! I bet it will be fast too!";
                ProfilePictureRGB = "3099c5";
            }
        }

        #endregion
    }
}
