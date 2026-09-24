using System;
using System.Data;
using System.Data.SqlClient;
using System.Threading.Tasks;
using InventoryManagementSystem.Models;
using InventoryManagementSystem.Database;

namespace InventoryManagementSystem.Repositories
{
    public class CategoryRepository : ICategoryRepository
    {
        public async Task<DataTable> GetAllCategoriesDataTableAsync()
        {
            string query = @"
                SELECT 
                    c.CategoryID AS ID,
                    c.CategoryName AS Name,
                    COUNT(p.ProductID) AS [Total Products],
                    ISNULL(NULLIF(LTRIM(RTRIM(c.Description)), ''), 'N/A') AS [Descriptions],
                    c.Status,
                    c.CreatedAt AS [Create_At]
                FROM Categories c
                LEFT JOIN Products p ON c.CategoryID = p.CategoryID
                GROUP BY c.CategoryID, c.CategoryName, c.Description, c.Status, c.CreatedAt
                ORDER BY c.CategoryID DESC";

            var dt = new DataTable();

            using (var conn = DbConnection.Create())
            using (var cmd = new SqlCommand(query, conn))
            {
                await conn.OpenAsync();

                using (var reader = await cmd.ExecuteReaderAsync())
                {
                    dt.Load(reader);
                }
            }

            return dt;
        }

        public async Task<CategoryMetrics> GetCategoryMetricsAsync()
        {
            string query = @"
                SELECT 
                    COUNT(*) AS TotalCategories,
                    ISNULL(SUM(CASE WHEN Status = 'Active' THEN 1 ELSE 0 END), 0) AS ActiveCategories,
                    ISNULL(SUM(CASE WHEN Status = 'Inactive' THEN 1 ELSE 0 END), 0) AS InactiveCategories,
                    COUNT(CASE WHEN ProductCount = 0 THEN 1 END) AS EmptyCategories
                FROM (
                    SELECT c.CategoryID, c.Status, COUNT(p.ProductID) AS ProductCount
                    FROM Categories c
                    LEFT JOIN Products p ON c.CategoryID = p.CategoryID
                    GROUP BY c.CategoryID, c.Status
                ) AS SubQuery";

            var metrics = new CategoryMetrics();

            using (var conn = DbConnection.Create())
            using (var cmd = new SqlCommand(query, conn))
            {
                await conn.OpenAsync();

                using (var reader = await cmd.ExecuteReaderAsync())
                {
                    if (await reader.ReadAsync())
                    {
                        metrics.TotalCategories =
                            Convert.ToInt32(reader["TotalCategories"]);

                        metrics.ActiveCategories =
                            Convert.ToInt32(reader["ActiveCategories"]);

                        metrics.InactiveCategories =
                            Convert.ToInt32(reader["InactiveCategories"]);

                        metrics.EmptyCategories =
                            Convert.ToInt32(reader["EmptyCategories"]);
                    }
                }
            }

            return metrics;
        }

        public async Task SaveAsync(Category category)
        {
            if (category == null)
                throw new ArgumentNullException(nameof(category));

            string query = category.ID == 0
                ? @"INSERT INTO Categories 
                    (CategoryName, Description, Status, CreatedAt) 
                    VALUES 
                    (@CategoryName, @Description, @Status, GETDATE())"

                : @"UPDATE Categories 
                    SET CategoryName = @CategoryName, 
                        Description = @Description, 
                        Status = @Status 
                    WHERE CategoryID = @CategoryID";

            using (var conn = DbConnection.Create())
            using (var cmd = new SqlCommand(query, conn))
            {
                cmd.Parameters.AddWithValue(
                    "@CategoryName",
                    category.Name);

                cmd.Parameters.AddWithValue(
                    "@Description",
                    string.IsNullOrWhiteSpace(category.Description)
                        ? DBNull.Value
                        : (object)category.Description);

                cmd.Parameters.AddWithValue(
                    "@Status",
                    category.Status);

                if (category.ID > 0)
                {
                    cmd.Parameters.AddWithValue(
                        "@CategoryID",
                        category.ID);
                }

                await conn.OpenAsync();
                await cmd.ExecuteNonQueryAsync();
            }
        }

        public async Task DeleteAsync(int categoryId)
        {
            string query =
                "DELETE FROM Categories WHERE CategoryID = @CategoryID";

            using (var conn = DbConnection.Create())
            using (var cmd = new SqlCommand(query, conn))
            {
                cmd.Parameters.AddWithValue(
                    "@CategoryID",
                    categoryId);

                await conn.OpenAsync();
                await cmd.ExecuteNonQueryAsync();
            }
        }
    }
}