using InventoryManagementSystem.Models;
using System;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Threading.Tasks;

namespace InventoryManagementSystem.Repositories
{
    public class OrderRepository : IOrderRepository
    {
        private readonly string connectionString =
            ConfigurationManager.ConnectionStrings["IMSDB"].ConnectionString;

        public async Task<DataTable> GetCustomersLookupAsync()
        {
            DataTable dt = new DataTable();
            string query = "SELECT CustomerID, CustomerName FROM Customers ORDER BY CustomerName ASC";

            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                await conn.OpenAsync();
                using (SqlCommand cmd = new SqlCommand(query, conn))
                using (SqlDataReader reader = await cmd.ExecuteReaderAsync())
                {
                    dt.Load(reader);
                }
            }
            return dt;
        }

        public async Task<DataTable> GetProductsLookupAsync()
        {
            DataTable dt = new DataTable();
            string query = "SELECT ProductID, ProductName, Price, Stock FROM Products ORDER BY ProductName ASC";

            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                await conn.OpenAsync();
                using (SqlCommand cmd = new SqlCommand(query, conn))
                using (SqlDataReader reader = await cmd.ExecuteReaderAsync())
                {
                    dt.Load(reader);
                }
            }
            return dt;
        }

        public async Task<int> GetOrCreateCustomerByNameAsync(string customerName)
        {
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                await conn.OpenAsync();

                string selectQuery = "SELECT CustomerID FROM Customers WHERE CustomerName = @CustomerName;";
                using (SqlCommand selectCmd = new SqlCommand(selectQuery, conn))
                {
                    selectCmd.Parameters.AddWithValue("@CustomerName", customerName);
                    object result = await selectCmd.ExecuteScalarAsync();

                    if (result != null && result != DBNull.Value)
                    {
                        return Convert.ToInt32(result);
                    }
                }

                string insertQuery = @"
                    INSERT INTO Customers (CustomerName) 
                    OUTPUT INSERTED.CustomerID 
                    VALUES (@CustomerName);";

                using (SqlCommand insertCmd = new SqlCommand(insertQuery, conn))
                {
                    insertCmd.Parameters.AddWithValue("@CustomerName", customerName);
                    return Convert.ToInt32(await insertCmd.ExecuteScalarAsync());
                }
            }
        }

        public async Task<DataTable> GetAllOrdersAsync(string searchQuery = "", string statusFilter = "All")
        {
            DataTable dt = new DataTable();
            string query = @"
                SELECT 
                    o.OrderID AS [Order ID],
                    o.OrderDate AS [Date],
                    c.CustomerName AS [Customer],
                    o.Discount AS [Discount],
                    o.TotalAmount AS [Total Amount],
                    o.Status AS [Status]
                FROM Orders o
                LEFT JOIN Customers c ON o.CustomerID = c.CustomerID
                WHERE (@Search = '' 
                       OR o.OrderID LIKE '%' + @Search + '%' 
                       OR c.CustomerName LIKE '%' + @Search + '%')
                  AND (@Status = 'All' OR o.Status = @Status)
                ORDER BY o.OrderID DESC;";

            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                await conn.OpenAsync();
                using (SqlCommand cmd = new SqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@Search", searchQuery?.Trim() ?? string.Empty);
                    cmd.Parameters.AddWithValue("@Status", string.IsNullOrWhiteSpace(statusFilter) ? "All" : statusFilter.Trim());

                    using (SqlDataReader reader = await cmd.ExecuteReaderAsync())
                    {
                        dt.Load(reader);
                    }
                }
            }
            return dt;
        }

        public async Task<DataTable> GetOrderByIdAsync(int orderId)
        {
            DataTable dt = new DataTable();
            string query = @"
                SELECT 
                    o.OrderID,
                    o.CustomerID,
                    c.CustomerName,
                    o.OrderDate,
                    o.Status,
                    o.Discount,
                    o.TotalAmount,
                    o.Description,
                    od.ProductID,
                    p.ProductName,
                    od.Quantity,
                    od.UnitPrice
                FROM Orders o
                LEFT JOIN Customers c ON o.CustomerID = c.CustomerID
                LEFT JOIN OrderDetails od ON o.OrderID = od.OrderID
                LEFT JOIN Products p ON od.ProductID = p.ProductID
                WHERE o.OrderID = @OrderID;";

            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                await conn.OpenAsync();
                using (SqlCommand cmd = new SqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@OrderID", orderId);
                    using (SqlDataReader reader = await cmd.ExecuteReaderAsync())
                    {
                        dt.Load(reader);
                    }
                }
            }
            return dt;
        }

        public async Task<bool> CreateOrderAndReduceStockAsync(Order order, OrderDetail detail)
        {
            if (order == null) throw new ArgumentNullException(nameof(order));
            if (detail == null) throw new ArgumentNullException(nameof(detail));

            bool requiresStockDeduction = IsStockDeductibleStatus(order.Status);

            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                await conn.OpenAsync();
                using (SqlTransaction transaction = conn.BeginTransaction())
                {
                    try
                    {
                        if (requiresStockDeduction)
                        {
                            // 1. Check stock
                            string checkStockQuery = "SELECT Stock FROM Products WHERE ProductID = @ProductID";
                            using (SqlCommand checkCmd = new SqlCommand(checkStockQuery, conn, transaction))
                            {
                                checkCmd.Parameters.AddWithValue("@ProductID", detail.ProductID);
                                object result = await checkCmd.ExecuteScalarAsync();

                                if (result == null)
                                    throw new InvalidOperationException($"Product ID {detail.ProductID} not found.");

                                int currentStock = Convert.ToInt32(result);
                                if (currentStock < detail.Quantity)
                                {
                                    throw new InvalidOperationException($"Not enough stock! Available: {currentStock}, Requested: {detail.Quantity}");
                                }
                            }

                            // 2. Deduct stock
                            string reduceStockQuery = "UPDATE Products SET Stock = Stock - @Quantity WHERE ProductID = @ProductID";
                            using (SqlCommand reduceCmd = new SqlCommand(reduceStockQuery, conn, transaction))
                            {
                                reduceCmd.Parameters.AddWithValue("@Quantity", detail.Quantity);
                                reduceCmd.Parameters.AddWithValue("@ProductID", detail.ProductID);
                                await reduceCmd.ExecuteNonQueryAsync();
                            }
                        }

                        // 3. Insert Order with Discount
                        string insertOrderQuery = @"
                            INSERT INTO Orders (CustomerID, OrderDate, Discount, TotalAmount, Status, Description)
                            OUTPUT INSERTED.OrderID
                            VALUES (@CustomerID, @OrderDate, @Discount, @TotalAmount, @Status, @Description);";

                        int newOrderId;
                        using (SqlCommand cmdOrder = new SqlCommand(insertOrderQuery, conn, transaction))
                        {
                            cmdOrder.Parameters.AddWithValue("@CustomerID", order.CustomerID);
                            cmdOrder.Parameters.AddWithValue("@OrderDate", order.OrderDate);
                            cmdOrder.Parameters.AddWithValue("@Discount", order.Discount);
                            cmdOrder.Parameters.AddWithValue("@TotalAmount", order.TotalAmount);
                            cmdOrder.Parameters.AddWithValue("@Status", string.IsNullOrWhiteSpace(order.Status) ? "Pending" : order.Status);
                            cmdOrder.Parameters.AddWithValue("@Description", string.IsNullOrWhiteSpace(order.Description) ? (object)DBNull.Value : order.Description);

                            newOrderId = Convert.ToInt32(await cmdOrder.ExecuteScalarAsync());
                        }

                        // 4. Insert OrderDetail
                        string insertDetailQuery = @"
                            INSERT INTO OrderDetails (OrderID, ProductID, Quantity, UnitPrice)
                            VALUES (@OrderID, @ProductID, @Quantity, @UnitPrice);";

                        using (SqlCommand cmdDetail = new SqlCommand(insertDetailQuery, conn, transaction))
                        {
                            cmdDetail.Parameters.AddWithValue("@OrderID", newOrderId);
                            cmdDetail.Parameters.AddWithValue("@ProductID", detail.ProductID);
                            cmdDetail.Parameters.AddWithValue("@Quantity", detail.Quantity);
                            cmdDetail.Parameters.AddWithValue("@UnitPrice", detail.UnitPrice);

                            await cmdDetail.ExecuteNonQueryAsync();
                        }

                        transaction.Commit();
                        return true;
                    }
                    catch
                    {
                        transaction.Rollback();
                        throw;
                    }
                }
            }
        }

        public async Task<bool> UpdateOrderAsync(Order order, OrderDetail detail)
        {
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                await conn.OpenAsync();
                using (SqlTransaction transaction = conn.BeginTransaction())
                {
                    try
                    {
                        // 1. Fetch current order details to calculate stock adjustments
                        string oldDetailQuery = @"
                            SELECT o.Status, od.ProductID, od.Quantity 
                            FROM Orders o 
                            LEFT JOIN OrderDetails od ON o.OrderID = od.OrderID 
                            WHERE o.OrderID = @OrderID;";

                        string oldStatus = string.Empty;
                        int oldProductID = 0;
                        int oldQuantity = 0;

                        using (SqlCommand oldCmd = new SqlCommand(oldDetailQuery, conn, transaction))
                        {
                            oldCmd.Parameters.AddWithValue("@OrderID", order.OrderID);
                            using (SqlDataReader reader = await oldCmd.ExecuteReaderAsync())
                            {
                                if (reader.Read())
                                {
                                    oldStatus = reader["Status"]?.ToString() ?? string.Empty;
                                    oldProductID = reader["ProductID"] != DBNull.Value ? Convert.ToInt32(reader["ProductID"]) : 0;
                                    oldQuantity = reader["Quantity"] != DBNull.Value ? Convert.ToInt32(reader["Quantity"]) : 0;
                                }
                            }
                        }

                        bool wasDeducted = IsStockDeductibleStatus(oldStatus);
                        bool isDeductedNow = IsStockDeductibleStatus(order.Status);

                        // Restore old stock if previously deducted
                        if (wasDeducted && oldProductID > 0)
                        {
                            string restoreQuery = "UPDATE Products SET Stock = Stock + @Quantity WHERE ProductID = @ProductID";
                            using (SqlCommand cmdRestore = new SqlCommand(restoreQuery, conn, transaction))
                            {
                                cmdRestore.Parameters.AddWithValue("@Quantity", oldQuantity);
                                cmdRestore.Parameters.AddWithValue("@ProductID", oldProductID);
                                await cmdRestore.ExecuteNonQueryAsync();
                            }
                        }

                        // Deduct new stock if currently required
                        if (isDeductedNow)
                        {
                            string checkStockQuery = "SELECT Stock FROM Products WHERE ProductID = @ProductID";
                            using (SqlCommand checkCmd = new SqlCommand(checkStockQuery, conn, transaction))
                            {
                                checkCmd.Parameters.AddWithValue("@ProductID", detail.ProductID);
                                object result = await checkCmd.ExecuteScalarAsync();

                                int currentStock = result != null ? Convert.ToInt32(result) : 0;
                                if (currentStock < detail.Quantity)
                                {
                                    throw new InvalidOperationException($"Not enough stock! Available: {currentStock}, Requested: {detail.Quantity}");
                                }
                            }

                            string deductQuery = "UPDATE Products SET Stock = Stock - @Quantity WHERE ProductID = @ProductID";
                            using (SqlCommand cmdDeduct = new SqlCommand(deductQuery, conn, transaction))
                            {
                                cmdDeduct.Parameters.AddWithValue("@Quantity", detail.Quantity);
                                cmdDeduct.Parameters.AddWithValue("@ProductID", detail.ProductID);
                                await cmdDeduct.ExecuteNonQueryAsync();
                            }
                        }

                        // 2. Update Order with Discount
                        string updateOrderQuery = @"
                            UPDATE Orders
                            SET CustomerID = @CustomerID,
                                OrderDate = @OrderDate,
                                Discount = @Discount,
                                TotalAmount = @TotalAmount,
                                Status = @Status,
                                Description = @Description
                            WHERE OrderID = @OrderID;";

                        using (SqlCommand cmd = new SqlCommand(updateOrderQuery, conn, transaction))
                        {
                            cmd.Parameters.AddWithValue("@OrderID", order.OrderID);
                            cmd.Parameters.AddWithValue("@CustomerID", order.CustomerID);
                            cmd.Parameters.AddWithValue("@OrderDate", order.OrderDate);
                            cmd.Parameters.AddWithValue("@Discount", order.Discount);
                            cmd.Parameters.AddWithValue("@TotalAmount", order.TotalAmount);
                            cmd.Parameters.AddWithValue("@Status", order.Status);
                            cmd.Parameters.AddWithValue("@Description", string.IsNullOrWhiteSpace(order.Description) ? (object)DBNull.Value : order.Description);

                            await cmd.ExecuteNonQueryAsync();
                        }

                        // 3. Update OrderDetails
                        string deleteDetailsQuery = "DELETE FROM OrderDetails WHERE OrderID = @OrderID;";
                        using (SqlCommand cmdDelete = new SqlCommand(deleteDetailsQuery, conn, transaction))
                        {
                            cmdDelete.Parameters.AddWithValue("@OrderID", order.OrderID);
                            await cmdDelete.ExecuteNonQueryAsync();
                        }

                        string insertDetailQuery = @"
                            INSERT INTO OrderDetails (OrderID, ProductID, Quantity, UnitPrice)
                            VALUES (@OrderID, @ProductID, @Quantity, @UnitPrice);";

                        using (SqlCommand cmdDetail = new SqlCommand(insertDetailQuery, conn, transaction))
                        {
                            cmdDetail.Parameters.AddWithValue("@OrderID", order.OrderID);
                            cmdDetail.Parameters.AddWithValue("@ProductID", detail.ProductID);
                            cmdDetail.Parameters.AddWithValue("@Quantity", detail.Quantity);
                            cmdDetail.Parameters.AddWithValue("@UnitPrice", detail.UnitPrice);

                            await cmdDetail.ExecuteNonQueryAsync();
                        }

                        transaction.Commit();
                        return true;
                    }
                    catch
                    {
                        transaction.Rollback();
                        throw;
                    }
                }
            }
        }

        public async Task<bool> DeleteOrderAndRestoreStockAsync(int orderId)
        {
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                await conn.OpenAsync();
                using (SqlTransaction transaction = conn.BeginTransaction())
                {
                    try
                    {
                        string checkStatusQuery = "SELECT Status FROM Orders WHERE OrderID = @OrderID;";
                        string status = string.Empty;

                        using (SqlCommand cmdCheck = new SqlCommand(checkStatusQuery, conn, transaction))
                        {
                            cmdCheck.Parameters.AddWithValue("@OrderID", orderId);
                            object result = await cmdCheck.ExecuteScalarAsync();
                            status = result?.ToString() ?? string.Empty;
                        }

                        if (IsStockDeductibleStatus(status))
                        {
                            string restoreStockQuery = @"
                                UPDATE Products 
                                SET Stock = Stock + od.Quantity
                                FROM Products p
                                INNER JOIN OrderDetails od ON p.ProductID = od.ProductID
                                WHERE od.OrderID = @OrderID;";

                            using (SqlCommand cmdRestore = new SqlCommand(restoreStockQuery, conn, transaction))
                            {
                                cmdRestore.Parameters.AddWithValue("@OrderID", orderId);
                                await cmdRestore.ExecuteNonQueryAsync();
                            }
                        }

                        string deleteDetailsQuery = "DELETE FROM OrderDetails WHERE OrderID = @OrderID;";
                        using (SqlCommand cmdDetail = new SqlCommand(deleteDetailsQuery, conn, transaction))
                        {
                            cmdDetail.Parameters.AddWithValue("@OrderID", orderId);
                            await cmdDetail.ExecuteNonQueryAsync();
                        }

                        string deleteOrderQuery = "DELETE FROM Orders WHERE OrderID = @OrderID;";
                        using (SqlCommand cmdOrder = new SqlCommand(deleteOrderQuery, conn, transaction))
                        {
                            cmdOrder.Parameters.AddWithValue("@OrderID", orderId);
                            await cmdOrder.ExecuteNonQueryAsync();
                        }

                        transaction.Commit();
                        return true;
                    }
                    catch
                    {
                        transaction.Rollback();
                        throw;
                    }
                }
            }
        }

        private bool IsStockDeductibleStatus(string status)
        {
            if (string.IsNullOrWhiteSpace(status)) return false;

            return status.Equals("Processing", StringComparison.OrdinalIgnoreCase) ||
                   status.Equals("Completed", StringComparison.OrdinalIgnoreCase);
        }
    }
}