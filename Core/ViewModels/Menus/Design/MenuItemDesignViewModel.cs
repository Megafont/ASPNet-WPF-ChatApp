using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using ASPNet_WPF_ChatApp.Core.DataModels;

namespace ASPNet_WPF_ChatApp.Core.ViewModels.Menus.Design
{
    /// <summary>
    /// The design-time data for a <see cref="MenuItemViewModel"/>
    /// </summary>
    public class MenuItemDesignViewModel : MenuItemViewModel
    {
        #region Singleton

        /// <summary>
        /// A single instance of this design model
        /// </summary>
        public static MenuItemDesignViewModel Instance => new MenuItemDesignViewModel();

        #endregion

        #region Constructors

        public MenuItemDesignViewModel()
        {
            Text = "Hello World!";
            Icon = IconTypes.File;
        }

        #endregion

    }
}
