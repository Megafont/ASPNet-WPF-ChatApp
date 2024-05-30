using Dna;

using ASPNet_WPF_ChatApp.Core.DependencyInjection.Interfaces;
using ASPNet_WPF_ChatApp.WebServer.Data;

namespace ASPNet_WPF_ChatApp.WebServer.DependencyInjection
{
    /// <summary>
    /// The dependency injection container making use of the built-in .Net Core service provider
    /// </summary>
    public static class WebServerDI
    {
        /// <summary>
        /// The scoped instance of the <see cref="ApplicationDbContext"/>
        /// </summary>
        public static ApplicationDbContext ApplicationDbContext => Framework.Provider.GetService<ApplicationDbContext>();

        /// <summary>
        /// The transient instance of the <see cref="IEmailSender"/>
        /// </summary>
        public static IEmailSender EmailSender => Framework.Provider.GetService<IEmailSender>();

        /// <summary>
        /// The transient instance of the <see cref="IEmailTemplateSender"/>
        /// </summary>
        public static IEmailTemplateSender EmailTemplateSender => Framework.Provider.GetService<IEmailTemplateSender>();

    }
}
