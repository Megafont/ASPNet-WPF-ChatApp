using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ASPNet_WPF_ChatApp.Core.ApiModels
{
    /// <summary>
    /// The details to change for a User Password via an API client call
    /// </summary>
    public class UpdateUserPasswordDetailsApiModel
    {
        #region Public Properties

        /// <summary>
        /// TThe user's current password
        /// </summary>
        /// <remarks>The token is only provided when this class is returned from the Login methods</remarks>
        public string CurrentPassword { get; set; }

        /// <summary>
        /// The user's new password
        /// </summary>
        public string NewPassword { get; set; }

        #endregion

    }
}
