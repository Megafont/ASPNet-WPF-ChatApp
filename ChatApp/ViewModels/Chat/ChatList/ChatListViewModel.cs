using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using ASPNet_WPF_ChatApp.ViewModels.Base;

namespace ASPNet_WPF_ChatApp.ViewModels.Chat.ChatList
{
    /// <summary>
    /// A view model for the overview chat list
    /// </summary>
    public class ChatListViewModel: BaseViewModel
    {
        /// <summary>
        /// The chat list items for the list
        /// </summary>
        public List<ChatListItemViewModel> Items { get; set; }
    }
}
