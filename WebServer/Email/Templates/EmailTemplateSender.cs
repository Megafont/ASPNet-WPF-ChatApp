using System.Diagnostics;
using System.Reflection;
using System.Text;

using ASPNet_WPF_ChatApp.Core.DependencyInjection.Interfaces;
using ASPNet_WPF_ChatApp.Core.Email;
using ASPNet_WPF_ChatApp.WebServer.InversionOfControl;

namespace ASPNet_WPF_ChatApp.WebServer.Email.Templates
{
    /// <summary>
    /// Handles sending templated emails
    /// </summary>
    public class EmailTemplateSender : IEmailTemplateSender
    {
        public async Task<SendEmailResponse> SendGeneralEmailAsync(SendEmailDetails details, string title, string content1, string content2, string buttonText, string buttonUrl)
        {
            var templateText = default(string);

            // Read the general template from file
            // TODO: Replace with IoC Flat data provider
            using (var reader = new StreamReader(Assembly.GetEntryAssembly().GetManifestResourceStream("ASPNet_WPF_ChatApp.WebServer.Email.Templates.GeneralTemplate.html"), Encoding.UTF8))
            {
                // Read file contents
                templateText = await reader.ReadToEndAsync();
            }

            // Replace special values with those inside the template
            templateText = templateText
                .Replace("--Title--", title)
                .Replace("--Content1--", content1)
                .Replace("--Content2--", content2)
                .Replace("--ButtonText--", buttonText)
                .Replace("--ButtonUrl--", buttonUrl);

            // Set the details content to this templated content.
            details.Content = templateText;

            // Send the email
            return await IoC.EmailSender.SendEmailAsync(details);
        }
    }
}
