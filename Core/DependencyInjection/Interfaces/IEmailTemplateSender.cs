using ASPNet_WPF_ChatApp.Core.Email;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace ASPNet_WPF_ChatApp.Core.DependencyInjection.Interfaces
{
    /// <summary>
    /// Sends emails using the <see cref="IEmailSender"/> and recating the HTML
    /// email from specific templates
    /// </summary>
    public interface IEmailTemplateSender
    {
        /// <summary>
        /// Sends an email with the given details using the General template
        /// </summary>
        /// <param name="details">The email message details. Note: the content property is ignored and replaced with the template</param>
        /// <param name="title">The content title</param>
        /// <param name="content1">The first line contents</param>
        /// <param name="content2">The second line contents</param>
        /// <param name="buttonText">The button text</param>
        /// <param name="buttonUrl">The button URL</param>
        /// <returns></returns>
        Task<SendEmailResponse> SendGeneralEmailAsync(SendEmailDetails details, string title, string content1, string content2, string buttonText, string buttonUrl);
    }
}
