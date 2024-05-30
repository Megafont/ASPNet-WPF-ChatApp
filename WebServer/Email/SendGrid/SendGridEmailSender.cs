using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

using SendGrid;
using SendGrid.Helpers.Mail;

using ASPNet_WPF_ChatApp.Core.Email;
using ASPNet_WPF_ChatApp.WebServer.InversionOfControl;
using ASPNet_WPF_ChatApp.Core.DependencyInjection.Interfaces;
using System.Diagnostics;
using Newtonsoft.Json;

namespace ASPNet_WPF_ChatApp.WebServer.Email.SendGrid
{
    /// <summary>
    /// Sends emails using the SendGrid.com service.
    /// </summary>
    public class SendGridEmailSender : IEmailSender
    {
        public async Task<SendEmailResponse> SendEmailAsync(SendEmailDetails details)
        {
            // Get the SendGrid key from our appsettings.json file
            var apiKey = IoC.Configuration["SendGridKey"];
            if (string.IsNullOrWhiteSpace(apiKey))
                throw new Exception("The SendGrid API Key string is null or empty. Fix this by entering it into appsettings.json!");


            // Create a new SendGrid client
            var client = new SendGridClient(apiKey);

            // From
            var from = new EmailAddress(details.FromEmail, details.FromName);

            // To
            var to = new EmailAddress(details.ToEmail, details.ToName);

            // Subject
            var subject = details.Subject;

            // Content
            //var htmlContent = details.Content;
            //var plainTextContent = "and easy to do anywhere, even with C#";
            var msg = MailHelper.CreateSingleEmail(from,
                                                    to,
                                                    subject,
                                                    // Plain text content
                                                    details.IsHTML ? null : details.Content,
                                                    // or HTML content
                                                    details.IsHTML ? details.Content : null);

            // Finally, send the email...
            var response = await client.SendEmailAsync(msg);

            // If we succeeded...
            if (response.StatusCode == HttpStatusCode.Accepted)
            {
                // Return successful response
                return new SendEmailResponse();
            }

            // Otherwise, it failed...
            try
            {
                // Get the result in the body
                var bodyResult = await response.Body.ReadAsStringAsync();

                // Deserialize the response
                var sendGridResponse = JsonConvert.DeserializeObject<SendGridResponse>(bodyResult);

                // Add any errors to the response
                var errorResponse = new SendEmailResponse
                {
                    Errors = sendGridResponse?.Errors.Select(f => f.Message).ToList()
                };

                // Make sure we have at least one error
                if (errorResponse.Errors == null || errorResponse.Errors.Count == 0)
                {
                    // TODO: Localization

                    // If we don't, add an unknown error
                    errorResponse.Errors = new List<string> { "Unknown error from email sending service occurred." };
                }

                // Return the response
                return errorResponse;
            }
            catch (Exception ex)
            {
                // TODO: Localization

                // Break if we are debugging
                if (Debugger.IsAttached)
                {
                    Debugger.Break();
                }

                // If something unexpected happened, return error message
                return new SendEmailResponse
                {
                    Errors = new List<string> { "Unknown error occurred." }
                };
            }

        }
    }
}
