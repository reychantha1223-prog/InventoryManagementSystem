using InventoryManagementSystem.Models;
using System;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Threading.Tasks;

namespace InventoryManagementSystem.Repositories
{
    public class ProductRepository
    {
        private readonly string connectionString =
            ConfigurationManager.ConnectionStrings["IMSDB"].ConnectionString;

        public async Task<DataTable> GetProductsDataTableAsync()
        {
            string query = @"
        SELECT
            p.ProductID AS ID,
            p.ProductName AS Name,
            c.CategoryID,
            c.CategoryName AS Category,
            s.SupplierID,
            ISNULL(s.SupplierName, 'N/A') AS Supplier,
            p.Price,
            p.Stock,
            CASE 
                WHEN p.Stock <= 0 THEN 'Out of Stock'
                WHEN p.Stock <= 5 THEN 'Low Stock'
                ELSE 'In Stock'
            END AS Status,
            ISNULL(NULLIF(LTRIM(RTRIM(p.Descriptions)), ''), 'N/A') AS Description,
            p.CreatedAt
        FROM Products p
        INNER JOIN Categories c ON p.CategoryID = c.CategoryID
        LEFT JOIN Suppliers s ON p.SupplierID = s.SupplierID
        ORDER BY p.ProductID DESC";

            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                await conn.OpenAsync();
                using (SqlCommand cmd = new SqlCommand(query, conn))
                using (SqlDataReader reader = await cmd.ExecuteReaderAsync())
                {
                    DataTable dt = new DataTable();
                    dt.Load(reader);
                    return dt;
                }
            }
        }

        public async Task<DataTable> GetCategoriesAsync()
        {
            string query = "SELECT CategoryID, CategoryName FROM Categories ORDER BY CategoryName";
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                await conn.OpenAsync();
                using (SqlCommand cmd = new SqlCommand(query, conn))
                using (SqlDataReader reader = await cmd.ExecuteReaderAsync())
                {
                    DataTable dt = new DataTable();
                    dt.Load(reader);
                    return dt;
                }
            }
        }

        public async Task<DataTable> GetSuppliersAsync()
        {
            string query = "SELECT SupplierID, SupplierName FROM Suppliers ORDER BY SupplierName";
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                await conn.OpenAsync();
                using (SqlCommand cmd = new SqlCommand(query, conn))
                using (SqlDataReader reader = await cmd.ExecuteReaderAsync())
                {
                    DataTable dt = new DataTable();
                    dt.Load(reader);
                    return dt; 
                }
            }
        }

        public async Task SaveProductAsync(Product prod)
        {
            string query = prod.ID == 0
                ? @"INSERT INTO Products (ProductName, CategoryID, SupplierID, Price, Stock, Descriptions, CreatedAt) 
                    VALUES (@ProductName, @CategoryID, @SupplierID, @Price, @Stock, @Descriptions, GETDATE())"
                : @"UPDATE Products 
                    SET ProductName = @ProductName, CategoryID = @CategoryID, SupplierID = @SupplierID, 
                        Price = @Price, Stock = @Stock, Descriptions = @Descriptions 
                    WHERE ProductID = @ProductID";

            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                await conn.OpenAsync();
                using (SqlCommand cmd = new SqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@ProductName", prod.Name);
                    cmd.Parameters.AddWithValue("@CategoryID", prod.CategoryID);
                    cmd.Parameters.AddWithValue("@SupplierID", (object)prod.SupplierID ?? DBNull.Value);
                    cmd.Parameters.AddWithValue("@Price", prod.Price);
                    cmd.Parameters.AddWithValue("@Stock", prod.Stock);
                    cmd.Parameters.AddWithValue("@Descriptions", string.IsNullOrWhiteSpace(prod.Description) ? (object)DBNull.Value : prod.Description.Trim());

                    if (prod.ID > 0)
                    {
                        cmd.Parameters.AddWithValue("@ProductID", prod.ID);
                    }

                    await cmd.ExecuteNonQueryAsync();
                }
            }
        }
        // Method to adjust product stock dynamically (for sales orders or adjustments)
        public async Task<bool> IsProductNameExistsAsync(string name, int excludeProductId = 0)
        {
            string query = @"SELECT COUNT(1) 
                    FROM Products 
                    WHERE LOWER(LTRIM(RTRIM(ProductName))) = LOWER(LTRIM(RTRIM(@Name))) 
                    AND ProductID <> @ExcludeID";

            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                await conn.OpenAsync();
                using (SqlCommand cmd = new SqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@Name", name ?? string.Empty);
                    cmd.Parameters.AddWithValue("@ExcludeID", excludeProductId);

                    int count = Convert.ToInt32(await cmd.ExecuteScalarAsync());
                    return count > 0;
                }
            }
        }
        public async Task DeductStockAsync(int productId, int quantityToDeduct)
        {
            string query = @"
                UPDATE Products 
                SET Stock = Stock - @Quantity 
                WHERE ProductID = @ProductID AND Stock >= @Quantity";

            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                await conn.OpenAsync();
                using (SqlCommand cmd = new SqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@Quantity", quantityToDeduct);
                    cmd.Parameters.AddWithValue("@ProductID", productId);

                    int rowsAffected = await cmd.ExecuteNonQueryAsync();
                    if (rowsAffected == 0)
                    {
                        throw new InvalidOperationException("Insufficient stock available or product not found.");
                    }
                }
            }
        }

        public async Task<(int Total, int InStock, int LowStock, int OutOfStock)> GetSummaryCardsDataAsync()
        {
            string query = @"
                SELECT 
                    COUNT(*) AS TotalProducts,
                    SUM(CASE WHEN Stock > 5 THEN 1 ELSE 0 END) AS InStock,
                    SUM(CASE WHEN Stock > 0 AND Stock <= 5 THEN 1 ELSE 0 END) AS LowStock,
                    SUM(CASE WHEN Stock <= 0 THEN 1 ELSE 0 END) AS OutOfStock
                FROM Products";

            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                await conn.OpenAsync();
                using (SqlCommand cmd = new SqlCommand(query, conn))
                using (SqlDataReader reader = await cmd.ExecuteReaderAsync())
                {
                    if (await reader.ReadAsync())
                    {
                        int total = reader["TotalProducts"] != DBNull.Value ? Convert.ToInt32(reader["TotalProducts"]) : 0;
                        int inStock = reader["InStock"] != DBNull.Value ? Convert.ToInt32(reader["InStock"]) : 0;
                        int lowStock = reader["LowStock"] != DBNull.Value ? Convert.ToInt32(reader["LowStock"]) : 0;
                        int outOfStock = reader["OutOfStock"] != DBNull.Value ? Convert.ToInt32(reader["OutOfStock"]) : 0;

                        return (total, inStock, lowStock, outOfStock);
                    }
                }
            }

            return (0, 0, 0, 0);
        }
    }
}