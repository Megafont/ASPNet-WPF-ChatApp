using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ASPNet_WPF_ChatApp.Core.DataModels;
using ASPNet_WPF_ChatApp.ViewModels.Base;

namespace ASPNet_WPF_ChatApp.ViewModels.Menus
{
    /// <summary>
    /// A view model for a menu item
    /// </summary>
    public class MenuItemViewModel : BaseViewModel
    {
        #region Public Properties

        /// <summary>
        /// The text to display for the menu item
        /// </summary>
        public string Text { get; set; }

        /// <summary>
        /// The icon for this menu item
        /// </summary>
        public IconTypes Icon { get; set; }

        /// <summary>
        /// The type of this menu item
        /// </summary>
        public MenuItemTypes Type { get; set; }

        #endregion
    }
}
