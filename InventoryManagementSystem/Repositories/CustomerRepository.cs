using InventoryManagementSystem.Database;
using InventoryManagementSystem.Models;
using System;
using System.Data;
using System.Data.SqlClient;
using System.Threading.Tasks;

namespace InventoryManagementSystem.Repositories
{
    public class CustomerRepository
    {
        public async Task<DataTable> GetAllCustomersAsync()
        {
            DataTable dt = new DataTable();
            string query = @"
                SELECT 
                    CustomerID AS ID,
                    CustomerName AS Name,
                    ISNULL(Phone, 'N/A') AS Phone,
                    ISNULL(Email, 'N/A') AS Email,
                    ISNULL(Address, 'N/A') AS Address
                FROM Customers
                ORDER BY CustomerID DESC";

            using (SqlConnection conn = DbConnection.Create())
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

        public async Task AddCustomerAsync(Customer customer)
        {
            string query = @"
                INSERT INTO Customers (CustomerName, Phone, Email, Address) 
                VALUES (@Name, @Phone, @Email, @Address)";

            using (SqlConnection conn = DbConnection.Create())
            {
                await conn.OpenAsync();
                using (SqlCommand cmd = new SqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@Name", customer.Name);
                    cmd.Parameters.AddWithValue("@Phone", customer.Phone);
                    cmd.Parameters.AddWithValue("@Email", string.IsNullOrWhiteSpace(customer.Email) ? (object)DBNull.Value : customer.Email);
                    cmd.Parameters.AddWithValue("@Address", customer.Address);

                    await cmd.ExecuteNonQueryAsync();
                }
            }
        }

        public async Task UpdateCustomerAsync(Customer customer)
        {
            string query = @"
                UPDATE Customers 
                SET CustomerName = @Name, 
                    Phone = @Phone, 
                    Email = @Email, 
                    Address = @Address 
                WHERE CustomerID = @ID";

            using (SqlConnection conn = DbConnection.Create())
            {
                await conn.OpenAsync();
                using (SqlCommand cmd = new SqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@ID", customer.ID);
                    cmd.Parameters.AddWithValue("@Name", customer.Name);
                    cmd.Parameters.AddWithValue("@Phone", customer.Phone);
                    cmd.Parameters.AddWithValue("@Email", string.IsNullOrWhiteSpace(customer.Email) ? (object)DBNull.Value : customer.Email);
                    cmd.Parameters.AddWithValue("@Address", customer.Address);

                    await cmd.ExecuteNonQueryAsync();
                }
            }
        }
    }
}