using System.Diagnostics;

using ASPNet_WPF_ChatApp.Core.Email;
using ASPNet_WPF_ChatApp.WebServer.InversionOfControl;


namespace ASPNet_WPF_ChatApp.WebServer.Email
{
    /// <summary>
    /// Handles sending emails specific to this chat app web server.
    /// </summary>
    public static class WebServerEmailSender
    {
        /// <summary>
        /// Sends a verification email to the specified user
        /// </summary>
        /// <param name="displayName">The user's display name (typically first name)</param>
        /// <param name="email">The user's email address to be verified</param>
        /// <param name="verificationUrl">The URL the user needs to click to verify their email address</param>
        /// <returns></returns>
        public static async Task<SendEmailResponse> SendUserVerificationEmailAsync(string displayName, string email, string verificationUrl)
        {
            return await IoC.EmailTemplateSender.SendGeneralEmailAsync(new SendEmailDetails
            {
                IsHTML = true,
                FromEmail = IoC.Configuration["ChatAppServerSettings:SendEmailFromEmail"],
                FromName = IoC.Configuration["ChatAppServerSettings:SendEmailFromName"],
                ToEmail = email,
                ToName = displayName,
                Subject = "Verify Your Email - Chat App"
            },
            "Verify Email",
            $"Hi {displayName ?? "stranger"},",
            "Thanks for creating an account with us.</br>To continue, please verify your email address.",
            "Verify Email",
            verificationUrl);
        }
    }
}
