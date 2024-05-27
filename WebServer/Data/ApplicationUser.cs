using Microsoft.AspNetCore.Identity;

namespace WebServer.Data
{
    /// <summary>
    /// The user data and profile for our application
    /// </summary>
    public class ApplicationUser : IdentityUser
    {
        #region Public Properties

        // NOTE: I had to make some fields below nullable by adding the nullable operator (?).
        //       Otherwise, these fields were causing errors because they wouldn't allow null values.

        /// <summary>
        /// The user's first name
        /// </summary>        
        public string? FirstName { get; set; }

        /// <summary>
        /// The user's last name
        /// </summary>
        public string? LastName { get; set; }

        #endregion
    }
}
