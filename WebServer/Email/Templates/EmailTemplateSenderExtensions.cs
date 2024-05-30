using ASPNet_WPF_ChatApp.Core.DependencyInjection.Interfaces;
using ASPNet_WPF_ChatApp.WebServer.Email.SendGrid;

namespace ASPNet_WPF_ChatApp.WebServer.Email.Templates
{
    /// <summary>
    /// Extension methods for any EmailTemplateSender classes
    /// </summary>
    public static class EmailTemplateSenderExtensions
    {
        /// <summary>
        /// Injects the <see cref="EmailTemplateSender"/> into the services to handle the
        /// <see cref="IEmailTemplateSender"/> service
        /// </summary>
        /// <param name="services"></param>
        /// <returns></returns>
        public static IServiceCollection AddEmailTemplateSender(this IServiceCollection services)
        {
            // Inject the SendTemplateEmailSender. Transient means a new instance is returned
            // each time this service is requested.
            services.AddTransient<IEmailTemplateSender, EmailTemplateSender>();

            // Return collection for chaining
            return services;
        }
    }
}
