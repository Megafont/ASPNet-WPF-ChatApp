using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ASPNet_WPF_ChatApp.Core.Routes
{
    /// <summary>
    /// The relative routes to all API calls in the server
    /// </summary>
    public static class ApiRoutes
    {
        /// <summary>
        /// The route to the Register() API method
        /// </summary>
        public const string Register = "api/register";

        /// <summary>
        /// The route to the Login() API method
        /// </summary>
        public const string Login = "api/login";

        /// <summary>
        /// The route to the VerifyEmail() API method
        /// </summary>
        public const string VerifyEmail = "api/verify/email/{userId}/{emailToken}";

        /// <summary>
        /// The route to the GetUserProfile() API method
        /// </summary>
        public const string GetUserProfile = "api/user/profile";

        /// <summary>
        /// The route to the UpdateUserProfile() API method
        /// </summary>
        public const string UpdateUserProfile = "api/user/profile/update";

        /// <summary>
        /// The route to the UpdatePassword() API method
        /// </summary>
        public const string UpdatePassword = "api/user/password/update";

        /// <summary>
        /// The route to the Private() API method
        /// This is just a sample for now of how to create a private area on the website that requires a user to be logged in to enter it.
        /// </summary>
        public const string Private = "api/private";
    }
}
