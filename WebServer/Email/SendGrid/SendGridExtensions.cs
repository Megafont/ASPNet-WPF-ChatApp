﻿using Microsoft.Extensions.DependencyInjection;

using ASPNet_WPF_ChatApp.Core.DependencyInjection.Interfaces;

namespace ASPNet_WPF_ChatApp.WebServer.Email.SendGrid
{
    /// <summary>
    /// Extension methods for any SendGrid classes
    /// </summary>
    public static class SendGridExtensions
    {
        /// <summary>
        /// Injects the <see cref="SendGridEmailSender"/> into the services to handle the
        /// <see cref="IEmailSender"/> service
        /// </summary>
        /// <param name="services"></param>
        /// <returns></returns>
        public static IServiceCollection AddSendGridEmailSender(this IServiceCollection services)
        {
            // Inject the SendGridEmailSender. Transient means a new instance is returned
            // each time this service is requested.
            services.AddTransient<IEmailSender, SendGridEmailSender>();

            // Return collection for chaining
            return services;
        }

    }
}
