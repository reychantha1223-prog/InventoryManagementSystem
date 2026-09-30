using System;
using System.Configuration;
using System.Data.SqlClient;
using System.Threading.Tasks;
using System.Xml;

namespace InventoryManagementSystem.Database
{
    public static class DbConnection
    {
        public static string ConnectionString =>
            ConfigurationManager.ConnectionStrings["IMSDB"]?.ConnectionString;

        public static SqlConnection Create()
        {
            return new SqlConnection(ConnectionString);
        }

        public static string BuildConnectionString(string serverType)
        {
            // Use "." internally for fast local shared memory connection
            string dataSource = serverType.Equals("SQLExpress", StringComparison.OrdinalIgnoreCase)
                ? @".\SQLEXPRESS"
                : ".";

            SqlConnectionStringBuilder builder = new SqlConnectionStringBuilder
            {
                DataSource = dataSource,
                InitialCatalog = "IMSDB",
                IntegratedSecurity = true,
                TrustServerCertificate = true,
                ConnectTimeout = 5 // Responds in milliseconds via shared memory
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
                Configuration config = ConfigurationManager.OpenExeConfiguration(ConfigurationUserLevel.None);
                ClientSettingsSection section = config.GetSection("userSettings/InventoryManagementSystem.Properties.Settings") as ClientSettingsSection;

                if (section != null)
                {
                    SettingElement setting = section.Settings.Get("ServerType");
                    if (setting != null)
                    {
                        return setting.Value.ValueXml.InnerText;
                    }
                }
            }
            catch
            {
                // Default fallback
            }

            return "Localhost";
        }

        public static void SaveConnectionString(string connectionString, string serverType)
        {
            Configuration config = ConfigurationManager.OpenExeConfiguration(ConfigurationUserLevel.None);

            // 1. Update connectionString in App.config
            if (config.ConnectionStrings.ConnectionStrings["IMSDB"] != null)
            {
                config.ConnectionStrings.ConnectionStrings["IMSDB"].ConnectionString = connectionString;
            }

            // 2. Update ServerType inside userSettings in App.config
            ClientSettingsSection section = config.GetSection("userSettings/InventoryManagementSystem.Properties.Settings") as ClientSettingsSection;
            if (section != null)
            {
                SettingElement setting = section.Settings.Get("ServerType");
                if (setting == null)
                {
                    setting = new SettingElement("ServerType", SettingsSerializeAs.String);
                    section.Settings.Add(setting);
                }

                XmlDocument doc = new XmlDocument();
                XmlElement valueElement = doc.CreateElement("value");
                valueElement.InnerText = serverType;
                setting.Value.ValueXml = valueElement;
            }

            config.Save(ConfigurationSaveMode.Modified);
            ConfigurationManager.RefreshSection("connectionStrings");
            ConfigurationManager.RefreshSection("userSettings");
        }
    }
}