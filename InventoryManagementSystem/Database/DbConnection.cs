using System;
using System.Configuration;
using System.Data.SqlClient;
using System.Threading.Tasks;

namespace InventoryManagementSystem.Database
{
    public static class DbConnection
    {
        // Dynamic property: Always resolves a working connection string automatically
        public static string ConnectionString => GetOrAutoDetectConnectionString();

        public static SqlConnection Create()
        {
            return new SqlConnection(ConnectionString);
        }

        public static string BuildConnectionString(string serverType)
        {
            string dataSource = serverType.Equals("SQLExpress", StringComparison.OrdinalIgnoreCase)
                ? @".\SQLEXPRESS"
                : ".";

            SqlConnectionStringBuilder builder = new SqlConnectionStringBuilder
            {
                DataSource = dataSource,
                InitialCatalog = "IMSDB",
                IntegratedSecurity = true,
                TrustServerCertificate = true,
                ConnectTimeout = 3 // Fast timeout to quickly switch if instance doesn't exist
            };

            return builder.ConnectionString;
        }

        private static string GetOrAutoDetectConnectionString()
        {
            string savedType = GetSavedServerType();
            string connStr = BuildConnectionString(savedType);

            // Test if the saved preference connects
            if (CanConnect(connStr))
            {
                return connStr;
            }

            // Fallback: If saved preference fails, test the alternative server instance
            string fallbackType = savedType.Equals("SQLExpress", StringComparison.OrdinalIgnoreCase) ? "Localhost" : "SQLExpress";
            string fallbackConnStr = BuildConnectionString(fallbackType);

            if (CanConnect(fallbackConnStr))
            {
                // Auto-save the working instance so future queries connect instantly
                SaveConnectionString(fallbackConnStr, fallbackType);
                return fallbackConnStr;
            }

            // Return primary string if both fail (allows error to surface cleanly)
            return connStr;
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
                if (!string.IsNullOrEmpty(savedType))
                {
                    return savedType;
                }
            }
            catch { }

            return "SQLExpress"; 
        }

        public static void SaveConnectionString(string connectionString, string serverType)
        {
            try
            {
                // 1. Update Properties.Settings (User scope)
                Properties.Settings.Default.ServerType = serverType;
                Properties.Settings.Default.Save();

                // 2. Update ConnectionStrings in memory / App.config
                Configuration config = ConfigurationManager.OpenExeConfiguration(ConfigurationUserLevel.None);
                if (config.ConnectionStrings.ConnectionStrings["IMSDB"] != null)
                {
                    config.ConnectionStrings.ConnectionStrings["IMSDB"].ConnectionString = connectionString;
                    config.Save(ConfigurationSaveMode.Modified);
                    ConfigurationManager.RefreshSection("connectionStrings");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error saving connection settings: {ex.Message}");
            }
        }
    }
}