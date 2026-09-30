using System;
using System.Configuration;
using System.Data.SqlClient;
using System.Threading.Tasks;

namespace InventoryManagementSystem.Database
{
    public static class DbConnection
    {
        public static string ConnectionString
        {
            get
            {
                string serverType = GetSavedServerType();
                return BuildConnectionString(serverType);
            }
        }

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
                ConnectTimeout = 5
            };

            return builder.ConnectionString;
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

            return "Localhost";
        }

        public static void SaveConnectionString(string connectionString, string serverType)
        {
            try
            {
                Properties.Settings.Default.ServerType = serverType;
                Properties.Settings.Default.Save();

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