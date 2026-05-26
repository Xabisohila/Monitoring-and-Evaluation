using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Configuration;
using System.Data.SqlClient;

namespace MnE2.DAL
{
    /// <summary>
    /// Centralized connection factory for the DAL. 
    /// - Reads the connection string from Web.config / App.config
    /// - Throws a clear exception if the connection string is missing
    /// - Exposes optional diagnostics helpers
    /// </summary>
    public static class Database
    {
        /// <summary>
        /// Default connection string name expected in Web.config/App.config.
        /// </summary>
        public const string DefaultConnectionName = "ConnectionString";

        /// <summary>
        /// Returns a new SqlConnection using the default connection string ("DefaultConnection").
        /// Usage:
        ///     using (SqlConnection con = Database.GetConnection())
        ///     { ... con.Open(); ... }
        /// </summary>
        public static SqlConnection GetConnection()
        {
            return GetConnection(DefaultConnectionName);
        }

        /// <summary>
        /// Returns a new SqlConnection using a specific named connection string.
        /// </summary>
        public static SqlConnection GetConnection(string connectionName)
        {
            if (string.IsNullOrWhiteSpace(connectionName))
                throw new ArgumentException("Connection name must be provided.", "connectionName");

            var settings = ConfigurationManager.ConnectionStrings[connectionName];
            if (settings == null || string.IsNullOrWhiteSpace(settings.ConnectionString))
            {
                throw new ConfigurationErrorsException(
                    string.Format("Connection string '{0}' was not found or is empty in <connectionStrings>.", connectionName));
            }

            return new SqlConnection(settings.ConnectionString);
        }

        /// <summary>
        /// Quick diagnostics for deployment: opens and closes a connection to verify configuration.
        /// Returns true when a connection can be opened; false otherwise (and outputs the exception).
        /// </summary>
        public static bool TryOpen(out Exception error, string connectionName = DefaultConnectionName)
        {
            error = null;
            try
            {
                using (var con = GetConnection(connectionName))
                {
                    con.Open();
                    con.Close();
                }
                return true;
            }
            catch (Exception ex)
            {
                error = ex;
                return false;
            }
        }
    }
}