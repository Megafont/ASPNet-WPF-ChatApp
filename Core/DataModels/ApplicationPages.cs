using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace ASPNet_WPF_ChatApp.Core.DataModels
{
    /// <summary>
    /// The page types in the application
    /// </summary>
    public enum ApplicationPages
    {
        None = -1,

        /// <summary>
        /// The initial login page
        /// </summary>
        Login = 0,

        /// <summary>
        /// The registration page
        /// </summary>
        Register = 1,

        /// <summary>
        /// The main chat page
        /// </summary>
        Chat = 2,

    }
}
