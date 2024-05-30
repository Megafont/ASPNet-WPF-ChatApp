using Microsoft.AspNetCore.Identity;

namespace ASPNet_WPF_ChatApp.WebServer.Identity
{
    /// <summary>
    /// Extension methods for <see cref="IdentityError"/> class
    /// </summary>
    public static class IdentityErrorExtensions
    {
        /// <summary>
        /// Combines all errors into a single string
        /// </summary>
        /// <param name="errors">The errors to aggregate</param>
        /// <returns>A single string with each error separated by a new line</returns>
        public static string AggregateErrors(this IEnumerable<IdentityError> errors)
        {
            // Get all errors into a list
            return errors?.ToList()
                // Grab the error message for each
                .Select(f => f.Description)
                // And combine with them with a new line separator
                .Aggregate((a, b) => $"{a}{Environment.NewLine}{b}");
        }

    }
}
