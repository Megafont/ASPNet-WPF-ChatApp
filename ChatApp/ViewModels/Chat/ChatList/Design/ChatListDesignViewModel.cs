using System;
using System.CodeDom;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ASPNet_WPF_ChatApp.ViewModels.Chat.ChatList.Design
{
    /// <summary>
    /// The design-time data for a <see cref="ChatListViewModel"/>
    /// </summary>
    public class ChatListDesignViewModel : ChatListViewModel
    {
        #region Singleton

        /// <summary>
        /// A single instance of this design model
        /// </summary>
        public static ChatListDesignViewModel Instance => new ChatListDesignViewModel();

        #endregion

        #region Constructors

        /// <summary>
        /// Default constructor
        /// </summary>
        public ChatListDesignViewModel()
        {
            Items = new List<ChatListItemViewModel>
            {
                new ChatListItemViewModel
                {
                    Name = "Michael",
                    Initials = "MF",
                    Message = "This new chat app is awesome!",
                    ProfilePictureRGB = "3099c5",
                    NewContentAvailable = true,
                },
                new ChatListItemViewModel
                {
                    Name = "Johnny",
                    Initials = "JA",
                    Message = "Hey dude, here are the new icons",
                    ProfilePictureRGB = "fe4503",
                },
                new ChatListItemViewModel
                {
                    Name = "Pangolin35",
                    Initials = "PL",
                    Message = "The new server is up, got 192.168.1.1",
                    ProfilePictureRGB = "00d405",
                    IsSelected = true
                },
                new ChatListItemViewModel
                {
                    Name = "TechnoGeek32",
                    Initials = "TG",
                    Message = "Hey dude, here are the new icons?",
                    ProfilePictureRGB = "e033cc",
                },
                new ChatListItemViewModel
                {
                    Name = "HeroOfTime",
                    Initials = "HT",
                    Message = "Hey, are you there?",
                    ProfilePictureRGB = "fe9903",
                },
            };

        }

        #endregion
    }
}
