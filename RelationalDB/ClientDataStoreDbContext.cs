using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Microsoft.EntityFrameworkCore;

using ASPNet_WPF_ChatApp.Core.DataModels;


namespace APSNet_WPF_ChatApp.RelationalDB
{
    /// <summary>
    /// A database context for the client data store
    /// </summary>
    public class ClientDataStoreDbContext : DbContext
    {
        #region DbSets

        // DbSets are essentially tables

        /// <summary>
        /// The clients login credentials
        /// </summary>
        public DbSet<LoginCredentialsDataModel> LoginCredentials { get; set; }

        #endregion

        #region Constructors

        /// <summary>
        /// Default constructor
        /// </summary>
        public ClientDataStoreDbContext(DbContextOptions<ClientDataStoreDbContext> options)
            : base(options)
        {

        }

        #endregion

        #region Model Creation

        /// <summary>
        /// Configures the database structure and relationships
        /// </summary>
        /// <param name="modelBuilder"></param>
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Fluent API

            // Configure LoginCredentials
            // --------------------------
            //
            // Set Id as primary key
            modelBuilder.Entity<LoginCredentialsDataModel>().HasKey(a => a.Id);

            // TODO: Set up limits
            //modelBuilder.Entity<LoginCredentialsDataModel>().Property(a => a.FirstName).HasMaxLength(50);

        }

        #endregion
    }
}
