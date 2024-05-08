using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using ASPNet_WPF_ChatApp.Core.ViewModels.Base;
using ASPNet_WPF_ChatApp.Core.ViewModels.Chat.ChatList.Design;

namespace ASPNet_WPF_ChatApp.Core.ViewModels.Dialogs.Design
{
    /// <summary>
    /// Design data for the <see cref="MessageBoxDialogViewModel"/>
    /// </summary>
    public class MessageBoxDialogDesignViewModel : MessageBoxDialogViewModel
    {
        #region Singleton

        /// <summary>
        /// A single instance of this design model
        /// </summary>
        public static MessageBoxDialogDesignViewModel Instance => new MessageBoxDialogDesignViewModel();

        #endregion

        #region Constructors

        public MessageBoxDialogDesignViewModel()
        {
            Message = "Design time messages are fun :)";
            OkText = "OK";
        }

        #endregion

    }
}
