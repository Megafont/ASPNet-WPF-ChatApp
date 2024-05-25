using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Dna;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;

using ASPNet_WPF_ChatApp.Core.InversionOfControl.Interfaces;
using System.Reflection;


namespace APSNet_WPF_ChatApp.RelationalDB
{
    /// <summary>
    /// Extension methods for the <see cref="FrameworkConstruction"/>
    /// </summary>
    public static class FrameworkConstructionExtensions
    {

        public static FrameworkConstruction UseClientDataStore(this FrameworkConstruction construction)
        {
            // Inject our SQLite EntityFramework data store
            construction.Services.AddDbContext<ClientDataStoreDbContext>(options =>
            {
                // Setup connection string
                // This one line determines which database backend we use. In the video, he says this is the beauty of EntityFramework Core, as it is doing most of the work for us.
                options.UseSqlite(construction.Configuration.GetConnectionString("ClientDataStoreConnection"));
            });


            // Add client data store for easy access/use of the backing data store
            // Make it scoped so we can inject the scoped DbContext
            construction.Services.AddScoped<IClientDataStore>(provider =>
            {
                return new ClientDataStore(provider.GetService<ClientDataStoreDbContext>());
            });


            // Return framework for chaining
            return construction;
        }
  
    }
}
