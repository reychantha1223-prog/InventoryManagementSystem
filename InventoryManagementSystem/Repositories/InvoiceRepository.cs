using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Threading.Tasks;
using InventoryManagementSystem.Database;
using InventoryManagementSystem.Models;

namespace InventoryManagementSystem.Repositories
{
    public class InvoiceRepository
    {
        public async Task<List<InvoiceHeader>> GetInvoicesAsync(string customerName = "", DateTime? filterDate = null)
        {
            var list = new List<InvoiceHeader>();

            using (SqlConnection conn = DbConnection.Create())
            {
                string query = @"
            SELECT 
                o.OrderID AS InvoiceID,
                o.OrderDate AS [Date],
                ISNULL(c.CustomerName, 'Walk-in Customer') AS CustomerName,
                o.TotalAmount,
                'Paid' AS Status
            FROM Orders o
            LEFT JOIN Customers c ON o.CustomerID = c.CustomerID
            WHERE ISNULL(o.Status, '') NOT IN ('Cancelled', 'Canceled', 'Pending')
              AND (@Search = '' OR c.CustomerName LIKE '%' + @Search + '%')
              AND (@FilterDate IS NULL OR CAST(o.OrderDate AS DATE) = CAST(@FilterDate AS DATE))
            ORDER BY o.OrderID DESC";

                using (SqlCommand cmd = new SqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@Search", customerName ?? string.Empty);
                    cmd.Parameters.AddWithValue("@FilterDate", (object)filterDate ?? DBNull.Value);

                    await conn.OpenAsync();
                    using (SqlDataReader reader = await cmd.ExecuteReaderAsync())
                    {
                        while (await reader.ReadAsync())
                        {
                            list.Add(new InvoiceHeader
                            {
                                InvoiceID = Convert.ToInt32(reader["InvoiceID"]),
                                Date = Convert.ToDateTime(reader["Date"]),
                                CustomerName = reader["CustomerName"].ToString(),
                                TotalAmount = Convert.ToDecimal(reader["TotalAmount"]),
                                Status = "Paid"
                            });
                        }
                    }
                }
            }
            return list;
        }

        public async Task<List<InvoiceItem>> GetInvoiceItemsAsync(int invoiceId)
        {
            var items = new List<InvoiceItem>();

            using (SqlConnection conn = DbConnection.Create())
            {
                string query = @"
            SELECT 
                ROW_NUMBER() OVER(ORDER BY od.OrderDetailID) AS ItemNo,
                p.ProductName,
                od.Quantity,
                od.UnitPrice,
                (od.Quantity * od.UnitPrice) AS Total
            FROM OrderDetails od
            INNER JOIN Products p ON od.ProductID = p.ProductID
            WHERE od.OrderID = @InvoiceID";

                using (SqlCommand cmd = new SqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@InvoiceID", invoiceId);

                    await conn.OpenAsync();
                    using (SqlDataReader reader = await cmd.ExecuteReaderAsync())
                    {
                        while (await reader.ReadAsync())
                        {
                            items.Add(new InvoiceItem
                            {
                                ItemNo = Convert.ToInt32(reader["ItemNo"]),
                                ProductName = reader["ProductName"].ToString(),
                                Quantity = Convert.ToInt32(reader["Quantity"]),
                                UnitPrice = Convert.ToDecimal(reader["UnitPrice"])
                            });
                        }
                    }
                }
            }
            return items;
        }

        public async Task<InvoiceDetailsModel> GetInvoiceDetailsAsync(int orderId)
        {
            using (SqlConnection conn = DbConnection.Create())
            {
                // Calculates DiscountPercent based on Items Subtotal vs Order TotalAmount
                string query = @"
            WITH OrderSubTotals AS (
                SELECT 
                    OrderID, 
                    SUM(Quantity * UnitPrice) AS SubTotal 
                FROM OrderDetails 
                WHERE OrderID = @OrderId
                GROUP BY OrderID
            )
            SELECT 
                o.OrderID AS InvoiceID,
                o.OrderDate AS [Date],
                ISNULL(c.CustomerName, 'Walk-in Customer') AS CustomerName,
                o.TotalAmount,
                'Cash' AS PaymentMethod,
                CASE 
                    WHEN st.SubTotal IS NOT NULL AND st.SubTotal > o.TotalAmount 
                    THEN ((st.SubTotal - o.TotalAmount) / NULLIF(st.SubTotal, 0)) * 100.0
                    ELSE 0.0 
                END AS DiscountPercent,
                '' AS Notes,
                'Admin' AS CreatedBy,
                o.OrderDate AS CreatedAt
            FROM Orders o
            LEFT JOIN Customers c ON o.CustomerID = c.CustomerID
            LEFT JOIN OrderSubTotals st ON o.OrderID = st.OrderID
            WHERE o.OrderID = @OrderId";

                using (SqlCommand cmd = new SqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@OrderId", orderId);

                    await conn.OpenAsync();
                    using (SqlDataReader reader = await cmd.ExecuteReaderAsync())
                    {
                        if (await reader.ReadAsync())
                        {
                            return new InvoiceDetailsModel
                            {
                                InvoiceID = Convert.ToInt32(reader["InvoiceID"]),
                                Date = Convert.ToDateTime(reader["Date"]),
                                CustomerName = reader["CustomerName"].ToString(),
                                TotalAmount = Convert.ToDecimal(reader["TotalAmount"]),
                                PaymentMethod = reader["PaymentMethod"].ToString(),
                                DiscountPercent = Convert.ToDecimal(reader["DiscountPercent"]),
                                Notes = reader["Notes"].ToString(),
                                CreatedBy = reader["CreatedBy"].ToString(),
                                CreatedAt = Convert.ToDateTime(reader["CreatedAt"])
                            };
                        }
                    }
                }
            }
            return null;
        }
    }
}