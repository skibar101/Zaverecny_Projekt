using Microsoft.Data.SqlClient;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Zaverecny_projekt
{
    internal class Singleton
    {
        private static SqlConnection conn = null;

        private Singleton()
        {
        }
        /// <summary>
        /// Method that enables connection to the database.
        /// </summary>
        public static SqlConnection GetInstance()
        {
            if (conn == null)
            {
                SqlConnectionStringBuilder consStringBuilder = new SqlConnectionStringBuilder();
                consStringBuilder.UserID = ReadSetting("Name");// Load username from configuration.
                consStringBuilder.Password = ReadSetting("Password");// Load password from configuration.
                consStringBuilder.InitialCatalog = ReadSetting("Database"); ;// Load database name from configuration.
                consStringBuilder.DataSource = ReadSetting("DataSource");// Load server address from configuration.
                consStringBuilder.TrustServerCertificate = true;// Without this, the connection to the db did not work due to trust issues.
                consStringBuilder.ConnectTimeout = 30;// Connection timeout set to 30 seconds.
                conn = new SqlConnection(consStringBuilder.ConnectionString);
                conn.Open(); // Open the connection to the database.
            }
            return conn;
        }
        /// <summary>
        /// Closes the connection to the database if it is open.
        /// </summary>
        public static void CloseConnection()
        {
            if (conn != null)
            {
                conn.Close();
                conn.Dispose();
            }
        }
        /// <summary>
        /// Reads the value of a configuration key.
        /// </summary>
        /// <param name="key">The key under which the value is stored in the configuration file.</param>
        /// <returns>The value corresponding to the specified key, or the message "Not Found" if the key is not found.</returns>
        private static string ReadSetting(string key)
        {

            var appSettings = ConfigurationManager.AppSettings;
            string result = appSettings[key] ?? "Not Found";
            return result;
        }
    }
}

