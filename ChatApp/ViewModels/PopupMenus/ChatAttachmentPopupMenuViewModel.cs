using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

using ASPNet_WPF_ChatApp.Core.DataModels;
using ASPNet_WPF_ChatApp.ViewModels.Menus;

namespace ASPNet_WPF_ChatApp.ViewModels.PopupMenus
{
    /// <summary>
    /// A view model for any popup menus
    /// </summary>
    public class ChatAttachmentPopupMenuViewModel : BasePopupViewModel
    {

        #region Public Properties
        
        #endregion

        #region Constructor

        public ChatAttachmentPopupMenuViewModel()
        {
            Content = new MenuViewModel
            {
                MenuItems = new List<MenuItemViewModel>(new[]
                {
                    new MenuItemViewModel { Text = "Attach a file...", Type = MenuItemTypes.Header },
                    new MenuItemViewModel { Text = "From Computer", Icon = IconTypes.File},
                    new MenuItemViewModel { Text = "From Pictures", Icon = IconTypes.Picture},
                })
            };

        }

        #endregion

    }    
}
