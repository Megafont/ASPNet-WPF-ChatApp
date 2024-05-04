using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ASPNet_WPF_ChatApp.Core.DataModels
{
    public enum MenuItemTypes
    {
        /// <summary>
        /// Shows menu text and an icon
        /// </summary>
        TextAndIcon = 0,
        /// <summary>
        /// Shows a simple divider between menu items
        /// </summary>
        Divider,
        /// <summary>
        /// Shows the menu text as a header
        /// </summary>
        Header,
    }
}
