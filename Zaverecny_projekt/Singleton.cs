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
        /// Metoda, která umožňuje připojení k db.
        /// </summary>
        public static SqlConnection GetInstance()
        {
            if (conn == null)
            {
                SqlConnectionStringBuilder consStringBuilder = new SqlConnectionStringBuilder();
                consStringBuilder.UserID = ReadSetting("Name");// Načtení už. jména z konfigurace.
                consStringBuilder.Password = ReadSetting("Password");// Načtení hesla z konfigurace.
                consStringBuilder.InitialCatalog = ReadSetting("Database");// Načtení jména db z konfigurace.
                consStringBuilder.DataSource = ReadSetting("DataSource");// Načtení adresy z konfigurace.
                consStringBuilder.TrustServerCertificate = true;// Bez tohoto mi nefungovalo připojení k db z důvodů důvěryhodnosti.
                consStringBuilder.ConnectTimeout = 30;// Časpvý limit 30 sekund pro připojení.
                conn = new SqlConnection(consStringBuilder.ConnectionString);
                conn.Open(); // Otevření připojení s databází.
            }
            return conn;
        }
        /// <summary>
        /// Uzavře připojení s databází, pokud je otevřené.
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
        /// Načte hodnotu konfiguračního klíče.
        /// </summary>
        /// <param name="key">Klíč, pod kterým je uložena hodnota v konfiguračním souboru.</param>
        /// <returns>Hodnota odpovídající zadanému klíči, nebo zpráva "Not Found", pokud klíč není nalezen.</returns>
        private static string ReadSetting(string key)
        {

            var appSettings = ConfigurationManager.AppSettings;
            string result = appSettings[key] ?? "Not Found";
            return result;
        }
    }
}

