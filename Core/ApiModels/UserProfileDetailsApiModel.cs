using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ASPNet_WPF_ChatApp.Core.ApiModels
{
    /// <summary>
    /// The result of a successful login or get user profile details request via the API
    /// </summary>
    public class UserProfileDetailsApiModel
    {
        #region Public Properties

        /// <summary>
        /// The authentication token useed to stay authenticated through future requests
        /// </summary>
        /// <remarks>The token is only provided when this class is returned from the Login methods</remarks>
        public string Token { get; set; }

        /// <summary>
        /// The user's first name
        /// </summary>
        public string FirstName { get; set; }

        /// <summary>
        /// The user's last name
        /// </summary>
        public string LastName { get; set; }

        /// <summary>
        /// The user's username
        /// </summary>
        public string UserName { get; set; }

        /// <summary>
        /// The user's email
        /// </summary>
        public string Email { get; set; }

        #endregion

        #region Constructors

        public UserProfileDetailsApiModel()
        {
            
        }

        #endregion
    }
}
