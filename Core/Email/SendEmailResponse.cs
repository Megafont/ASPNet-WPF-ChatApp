using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ASPNet_WPF_ChatApp.Core.Email
{
    /// <summary>
    /// A response from any SendMailAsync() call for any <see cref="IEmailSender"/> implementation
    /// </summary>
    public class SendEmailResponse
    {
        /// <summary>
        /// True if the email was sent successfully
        /// </summary>
        public bool Successful => !(Errors?.Count > 0); // Returns true if Errors is null or empty.

        /// <summary>
        /// The error message(s) if the sending failed
        /// </summary>
        public List<string> Errors { get; set; }
    }
}
