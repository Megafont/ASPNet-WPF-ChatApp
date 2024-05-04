using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using ASPNet_WPF_ChatApp.Core.DataModels;
using ASPNet_WPF_ChatApp.Core.ViewModels.Chat.ChatList.Design;

namespace ASPNet_WPF_ChatApp.Core.ViewModels.Menus.Design
{
    /// <summary>
    /// The design-time data for a <see cref="MenuViewModel"/>
    /// </summary>
    public class MenuDesignViewModel : MenuViewModel
    {
        #region Singleton

        /// <summary>
        /// A single instance of this design model
        /// </summary>
        public static MenuDesignViewModel Instance => new MenuDesignViewModel();

        #endregion

        #region Constructors

        public MenuDesignViewModel()
        {
            MenuItems = new List<MenuItemViewModel>(new[]
            {
                new MenuItemViewModel { Type = MenuItemTypes.Header, Text = "Design time header..."},
                new MenuItemViewModel { Text = "Menu item 1", Icon = IconTypes.File},
                new MenuItemViewModel { Text = "Menu item 2", Icon = IconTypes.Picture},
            });
        }

        #endregion

    }
}
