using System.Configuration;
using System.Data.SqlClient;

namespace InventoryManagementSystem.Database
{
    public static class DbConnection
    {
        public static string ConnectionString =>
            ConfigurationManager
                .ConnectionStrings["IMSDB"]
                .ConnectionString;

        public static SqlConnection Create()
        {
            return new SqlConnection(ConnectionString);
        }
    }
}