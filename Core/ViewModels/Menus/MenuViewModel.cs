using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using ASPNet_WPF_ChatApp.Core.DataModels;
using ASPNet_WPF_ChatApp.Core.ViewModels.Base;

namespace ASPNet_WPF_ChatApp.Core.ViewModels.Menus
{
    /// <summary>
    /// A view model for a menu item
    /// </summary>
    public class MenuViewModel : BaseViewModel
    {
        #region Public Properties

        /// <summary>
        /// The items in this menu
        /// </summary>
        public List<MenuItemViewModel> MenuItems { get; set; }

        #endregion
    }
}
