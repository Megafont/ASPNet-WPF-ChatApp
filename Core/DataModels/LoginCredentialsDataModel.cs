using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ASPNet_WPF_ChatApp.Core.DataModels
{
    /// <summary>
    /// The data model for the login credentials of a client
    /// </summary>
    public class LoginCredentialsDataModel
    {
        /// <summary>
        /// The unique Id for this data model. This field is needed so this data is indexed in the database
        /// </summary>
        public string Id { get; set; }

        /// <summary>
        /// The user's username
        /// </summary>
        public string UserName { get; set; }

        /// <summary>
        /// The user's first name
        /// </summary>
        public string FirstName { get; set; }

        /// <summary>
        /// The user's last name
        /// </summary>
        public string LastName { get; set; }

        /// <summary>
        /// The user's email
        /// </summary>
        public string Email { get; set; }

        /// <summary>
        /// The user's login token
        /// </summary>
        public string Token { get; set; }
    }

}
