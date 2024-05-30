using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using ASPNet_WPF_ChatApp.Core.Email;

namespace ASPNet_WPF_ChatApp.Core.DependencyInjection.Interfaces
{
    /// <summary>
    /// A service that handles sending emails on behalf of the caller
    /// </summary>
    public interface IEmailSender
    {
        /// <summary>
        /// Sends an email message with the given information
        /// </summary>
        /// <param name="details">The details about the email to send</param>
        /// <returns></returns>
        Task<SendEmailResponse> SendEmailAsync(SendEmailDetails details);
    }
}
