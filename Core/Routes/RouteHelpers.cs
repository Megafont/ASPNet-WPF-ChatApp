using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using static Dna.FrameworkDI;


namespace ASPNet_WPF_ChatApp.Core.Routes
{
    /// <summary>
    /// Helper methods for getting and working with Routes
    /// </summary>
    public static class RouteHelpers
    {
        /// <summary>
        /// Converts a relative route URL into an absolute route URL
        /// </summary>
        /// <param name="relativeURL">The relative URL of the route to get the absolute route URL for</param>
        /// <returns>The absolute route URL including the Host URL</returns>
        public static string GetAbsoluteRoute(string relativeURL)
        {
            // Get the host
            var host = Configuration["ChatAppServer:HostURL"];

            // If they are not passing in any URL...
            if (string.IsNullOrWhiteSpace(relativeURL))
                return host;

            // If the relative URL does not start with a / and the host URL does not end with a /...
            if (!relativeURL.StartsWith("/") && !host.EndsWith("/"))
            {
                // Add the / that will separate the two pieces of the absolute route URL
                relativeURL = $"/{relativeURL}";
            }

            // Return combined route URL
            return host + relativeURL;
        }
    }
}
