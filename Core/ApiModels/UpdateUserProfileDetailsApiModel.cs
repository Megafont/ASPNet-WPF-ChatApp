using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ASPNet_WPF_ChatApp.Core.ApiModels
{
    /// <summary>
    /// The details to change for a User Profile via an API client call
    /// </summary>
    public class UpdateUserProfileDetailsApiModel
    {
        /// <summary>
        /// The user's new first name, or null to leave unchanged
        /// </summary>
        public string FirstName { get; set; }

        /// <summary>
        /// The user's new last name, or null to leave unchanged
        /// </summary>
        public string LastName { get; set; }

        /// <summary>
        /// The user's new email, or null to leave unchanged
        /// </summary>
        public string Email { get; set; }

        /// <summary>
        /// The user's new user name, or null to leave unchanged
        /// </summary>
        public string UserName { get; set; }
    }
}
