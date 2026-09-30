using System;
using System.Configuration;
using System.Data.SqlClient;
using System.Threading.Tasks;

namespace InventoryManagementSystem.Database
{
    public static class DbConnection
    {
        private static string _cachedConnectionString = null;

        public static string ConnectionString
        {
            get
            {
                if (string.IsNullOrEmpty(_cachedConnectionString))
                {
                    _cachedConnectionString = ResolveWorkingConnectionString();
                }
                return _cachedConnectionString;
            }
        }

        public static SqlConnection Create()
        {
            return new SqlConnection(ConnectionString);
        }

        public static string BuildConnectionString(string serverType)
        {
            string dataSource = serverType.Equals("SQLExpress", StringComparison.OrdinalIgnoreCase) ? @".\SQLEXPRESS" : "localhost";
            SqlConnectionStringBuilder builder = new SqlConnectionStringBuilder
            {
                DataSource = dataSource,
                InitialCatalog = "IMSDB",
                IntegratedSecurity = true,
                TrustServerCertificate = true,
                ConnectTimeout = 2
            };
            return builder.ConnectionString;
        }

        private static string ResolveWorkingConnectionString()
        {
            string savedType = GetSavedServerType();
            string primaryConnStr = BuildConnectionString(savedType);
            if (CanConnect(primaryConnStr)) return primaryConnStr;

            string fallbackType = savedType.Equals("Localhost", StringComparison.OrdinalIgnoreCase) ? "SQLExpress" : "Localhost";
            string fallbackConnStr = BuildConnectionString(fallbackType);
            if (CanConnect(fallbackConnStr))
            {
                SaveServerType(fallbackType);
                return fallbackConnStr;
            }
            return primaryConnStr;
        }

        private static bool CanConnect(string connStr)
        {
            try
            {
                using (SqlConnection conn = new SqlConnection(connStr))
                {
                    conn.Open();
                    return true;
                }
            }
            catch
            {
                return false;
            }
        }

        public static async Task<bool> TestConnectionAsync(string connectionString)
        {
            try
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    await conn.OpenAsync();
                    return true;
                }
            }
            catch
            {
                return false;
            }
        }

        public static string GetSavedServerType()
        {
            try
            {
                string savedType = Properties.Settings.Default.ServerType;
                if (!string.IsNullOrEmpty(savedType)) return savedType;
            }
            catch { }
            return "Localhost";
        }

        public static void SaveServerType(string serverType)
        {
            try
            {
                Properties.Settings.Default.ServerType = serverType;
                Properties.Settings.Default.Save();
                _cachedConnectionString = BuildConnectionString(serverType);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error saving local server settings: {ex.Message}");
            }
        }

        public static void SaveConnectionString(string newConnectionString, string serverType = "Localhost")
        {
            var config = ConfigurationManager.OpenExeConfiguration(ConfigurationUserLevel.None);
            if (config.ConnectionStrings.ConnectionStrings["IMSDB"] != null)
            {
                config.ConnectionStrings.ConnectionStrings["IMSDB"].ConnectionString = newConnectionString;
            }
            else
            {
                config.ConnectionStrings.ConnectionStrings.Add(
                    new ConnectionStringSettings("IMSDB", newConnectionString, "System.Data.SqlClient"));
            }
            config.Save(ConfigurationSaveMode.Modified);
            ConfigurationManager.RefreshSection("connectionStrings");
            if (!string.IsNullOrEmpty(serverType)) SaveServerType(serverType);
        }
    }
}