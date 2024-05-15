using WebServer.Data;

namespace WebServer.InversionOfControl
{
    /// <summary>
    /// The dependency injection container making use of the built-in .Net Core service provider
    /// </summary>
    public static class IoC
    {
        /// <summary>
        /// The scoped instance of the <see cref="ApplicationDbContext"/>
        /// </summary>
        public static ApplicationDbContext ApplicationDbContext => Provider.GetService<ApplicationDbContext>();

        /// <summary>
        /// The service provider for this application
        /// </summary>
        public static IServiceProvider Provider { get; set; }
    }
}
