using ASPNet_WPF_ChatApp.Core.DataModels;
using ASPNet_WPF_ChatApp.Core.InversionOfControl.Interfaces;
using System.Diagnostics;

namespace APSNet_WPF_ChatApp.RelationalDB
{
    /// <summary>
    /// Store and retreives information about the client application
    /// such as login credentials, messages, settings, and so on
    /// </summary>
    public class ClientDataStore : IClientDataStore
    {
        #region Protected Members

        /// <summary>
        /// The database context for the client data store
        /// </summary>
        protected ClientDataStoreDbContext _DbContext;

        #endregion

        #region Public Properties

        #endregion

        #region Constructors

        /// <summary>
        /// Default constructor
        /// </summary>
        /// <param name="dbContext">The database to use</param>
        public ClientDataStore(ClientDataStoreDbContext dbContext)
        {
            _DbContext = dbContext;
        }

        #endregion

        #region Interface Implementation

        /// <summary>
        /// Determines if the current user has logged in credentials
        /// </summary>
        public async Task<bool> HasCredentialsAsync()
        {
            return await GetLoginCredentialsAsync() != null;
        }

        /// <summary>
        /// Make sure the client data store is correctly set up
        /// </summary>
        /// <returns>Returns a task that will finish once setup is complete</returns>
        public async Task EnsureDataStoreAsync()
        {
            // Make sure the database exists and is created
            await _DbContext.Database.EnsureCreatedAsync();
        }

        /// <summary>
        /// Gets the stored login credentials for this client
        /// </summary>
        /// <returns>The login credentials if they exist, or null if none exist</returns>
        public Task<LoginCredentialsDataModel> GetLoginCredentialsAsync()
        {
            // Get the first column in the login credentials table, or null if none exist
            return Task.FromResult(_DbContext.LoginCredentials.FirstOrDefault());
        }

        /// <summary>
        /// Stores the given login crdentials to the backing data store
        /// </summary>
        /// <param name="loginCredentials">The login credentials to save</param>
        /// <returns>A task that will finish once the save is complete</returns>
        public async Task SaveLoginCredentialsAsync(LoginCredentialsDataModel loginCredentials)
        {
            // Clear all entries from the database table
            _DbContext.LoginCredentials.RemoveRange(_DbContext.LoginCredentials);

            // Add the new one
            _DbContext.LoginCredentials.Add(loginCredentials);

            // Save changes
            await _DbContext.SaveChangesAsync();
        }

        #endregion

    }
}
