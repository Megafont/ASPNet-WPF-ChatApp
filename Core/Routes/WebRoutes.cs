using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ASPNet_WPF_ChatApp.Core.Routes
{
    /// <summary>
    /// The relative routes to all normal (non-API) calls in the server
    /// </summary>
    public static class WebRoutes
    {
        /// <summary>
        /// The address of the webserver
        /// </summary>
        public const string ServerAddress = "http://localhost:5289/";

        /// <summary>
        /// The route to the CreateUser() method
        /// </summary>
        public const string CreateUser = "/user/create";


        /// <summary>
        /// The route to the Private() method
        /// This is just a sample of how to make a private area on the website that requires a user be logged in to enter it
        /// </summary>
        public const string Private = "/private";


        /// <summary>
        /// The route to the LogOut() method
        /// </summary>
        public const string LogOut = "/logout";


        /// <summary>
        /// The route to the Login() method
        /// </summary>
        public const string Login = "/login";

    }
}
