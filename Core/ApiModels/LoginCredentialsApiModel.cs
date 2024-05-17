using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ASPNet_WPF_ChatApp.Core.ApiModels
{
    /// <summary>
    /// The credentials for an API client to log into the server and receive a token back
    /// </summary>
    public class LoginCredentialsApiModel
    {

        #region Public Properties

        /// <summary>
        /// The user's username or email
        /// </summary>
        public string UsernameOrEmail { get; set; }

        /// <summary>
        /// The user's password
        /// </summary>
        public string Password { get; set; }

        #endregion

        #region Constructors

        /// <summary>
        /// Default constructor
        /// </summary>
        public LoginCredentialsApiModel()
        {

        }

        #endregion
    }
}
