using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Threading.Tasks;
using InventoryManagementSystem.Models;

namespace InventoryManagementSystem.Repositories
{
    public class DashboardRepository
    {
        private readonly string connectionString = "Server=.;Database=IMSDB;Trusted_Connection=True;TrustServerCertificate=True;";

        // 1. Get Metric Card Summaries
        public async Task<DashboardSummary> GetDashboardSummaryAsync()
        {
            string query = @"
                SELECT 
                    (SELECT COUNT(*) FROM Products) AS TotalProducts,
                    (SELECT COUNT(*) FROM Categories) AS TotalCategories,
                    (SELECT COUNT(*) FROM Customers) AS TotalCustomers,
                    (SELECT COUNT(*) FROM Suppliers) AS TotalSuppliers;";

            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                await conn.OpenAsync();
                using (SqlCommand cmd = new SqlCommand(query, conn))
                {
                    using (SqlDataReader reader = await cmd.ExecuteReaderAsync())
                    {
                        if (await reader.ReadAsync())
                        {
                            return new DashboardSummary
                            {
                                TotalProducts = Convert.ToInt32(reader["TotalProducts"]),
                                TotalCategories = Convert.ToInt32(reader["TotalCategories"]),
                                TotalCustomers = Convert.ToInt32(reader["TotalCustomers"]),
                                TotalSuppliers = Convert.ToInt32(reader["TotalSuppliers"])
                            };
                        }
                    }
                }
            }
            return new DashboardSummary();
        }

        // 2. Get Monthly Sales Overview for Bar Chart
        public async Task<List<MonthlySales>> GetMonthlySalesOverviewAsync()
        {
            var salesList = new List<MonthlySales>();

            string query = @"
                SELECT 
                    FORMAT(OrderDate, 'MMM') AS MonthName,
                    SUM(TotalAmount) AS TotalSales,
                    MONTH(OrderDate) AS MonthNum
                FROM Orders
                WHERE OrderDate >= DATEADD(MONTH, -6, GETDATE())
                GROUP BY FORMAT(OrderDate, 'MMM'), MONTH(OrderDate)
                ORDER BY MonthNum;";

            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                await conn.OpenAsync();
                using (SqlCommand cmd = new SqlCommand(query, conn))
                {
                    using (SqlDataReader reader = await cmd.ExecuteReaderAsync())
                    {
                        while (await reader.ReadAsync())
                        {
                            salesList.Add(new MonthlySales
                            {
                                MonthName = reader["MonthName"].ToString(),
                                TotalSales = Convert.ToDecimal(reader["TotalSales"])
                            });
                        }
                    }
                }
            }
            return salesList;
        }

        // 3. Get Stock Status Counts for Doughnut Chart
        public async Task<StockStatusMetrics> GetStockStatusMetricsAsync()
        {
            string query = @"
                SELECT 
                    SUM(CASE WHEN Stock > 10 THEN 1 ELSE 0 END) AS InStockCount,
                    SUM(CASE WHEN Stock > 0 AND Stock <= 10 THEN 1 ELSE 0 END) AS LowStockCount,
                    SUM(CASE WHEN Stock = 0 THEN 1 ELSE 0 END) AS OutOfStockCount
                FROM Products;";

            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                await conn.OpenAsync();
                using (SqlCommand cmd = new SqlCommand(query, conn))
                {
                    using (SqlDataReader reader = await cmd.ExecuteReaderAsync())
                    {
                        if (await reader.ReadAsync())
                        {
                            return new StockStatusMetrics
                            {
                                InStockCount = reader["InStockCount"] != DBNull.Value ? Convert.ToInt32(reader["InStockCount"]) : 0,
                                LowStockCount = reader["LowStockCount"] != DBNull.Value ? Convert.ToInt32(reader["LowStockCount"]) : 0,
                                OutOfStockCount = reader["OutOfStockCount"] != DBNull.Value ? Convert.ToInt32(reader["OutOfStockCount"]) : 0
                            };
                        }
                    }
                }
            }
            return new StockStatusMetrics();
        }

        // 4. Get Top Recent Products for DataGridView
        // 4. Get Top Recent Products for DataGridView
        // 4. Get Top Recent Products for DataGridView
        public async Task<List<RecentProduct>> GetRecentProductsAsync(int topCount = 5)
        {
            var productList = new List<RecentProduct>();

            string query = $@"
        SELECT TOP ({topCount}) 
            p.ProductID AS ID, 
            p.ProductName AS Name, 
            p.Price, 
            p.Stock, 
            c.CategoryName AS Category,
            CASE 
                WHEN p.Stock > 5 THEN 'In Stock'
                WHEN p.Stock > 0 AND p.Stock <= 5 THEN 'Low Stock'
                ELSE 'Out of Stock'
            END AS Status,
            p.CreatedAt
        FROM Products p
        LEFT JOIN Categories c ON p.CategoryID = c.CategoryID
        ORDER BY p.CreatedAt DESC;";

            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                await conn.OpenAsync();
                using (SqlCommand cmd = new SqlCommand(query, conn))
                {
                    using (SqlDataReader reader = await cmd.ExecuteReaderAsync())
                    {
                        while (await reader.ReadAsync())
                        {
                            productList.Add(new RecentProduct
                            {
                                ID = Convert.ToInt32(reader["ID"]),
                                Name = reader["Name"].ToString(),
                                Price = Convert.ToDecimal(reader["Price"]),
                                Stock = Convert.ToInt32(reader["Stock"]),
                                Category = reader["Category"] != DBNull.Value ? reader["Category"].ToString() : "N/A",
                                Status = reader["Status"].ToString(),
                                CreatedAt = Convert.ToDateTime(reader["CreatedAt"])
                            });
                        }
                    }
                }
            }
            return productList;
        }
    }
}