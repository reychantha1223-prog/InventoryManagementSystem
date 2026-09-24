using InventoryManagementSystem.Models;
using System;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Threading.Tasks;

namespace InventoryManagementSystem.Repositories
{
    public class SupplierRepository
    {
        private readonly string connectionString =
            ConfigurationManager.ConnectionStrings["IMSDB"].ConnectionString;

        public async Task<DataTable> GetAllSuppliersAsync()
        {
            DataTable dt = new DataTable();
            string query = @"
                SELECT 
                    SupplierID AS ID,
                    SupplierName AS Name,
                    ISNULL(ContactPerson, 'N/A') AS ContactPerson,
                    ISNULL(Phone, 'N/A') AS Phone,
                    ISNULL(Email, 'N/A') AS Email,
                    ISNULL(Address, 'N/A') AS Address
                FROM Suppliers
                ORDER BY SupplierID DESC";

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

        public async Task AddSupplierAsync(Supplier supplier)
        {
            string query = @"
                INSERT INTO Suppliers (SupplierName, ContactPerson, Phone, Email, Address) 
                VALUES (@Name, @ContactPerson, @Phone, @Email, @Address)";

            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                await conn.OpenAsync();
                using (SqlCommand cmd = new SqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@Name", supplier.Name);
                    cmd.Parameters.AddWithValue("@ContactPerson", string.IsNullOrWhiteSpace(supplier.ContactPerson) ? (object)DBNull.Value : supplier.ContactPerson);
                    cmd.Parameters.AddWithValue("@Phone", supplier.Phone);
                    cmd.Parameters.AddWithValue("@Email", string.IsNullOrWhiteSpace(supplier.Email) ? (object)DBNull.Value : supplier.Email);
                    cmd.Parameters.AddWithValue("@Address", supplier.Address);

                    await cmd.ExecuteNonQueryAsync();
                }
            }
        }

        public async Task<bool> UpdateSupplierAsync(Supplier supplier)
        {
            string query = @"UPDATE Suppliers 
                    SET SupplierName = @Name, 
                        ContactPerson = @ContactPerson, 
                        Phone = @Phone, 
                        Email = @Email, 
                        Address = @Address 
                    WHERE SupplierID = @ID;"; // Fixed: SupplierName and SupplierID

            using (SqlConnection conn = new SqlConnection(connectionString))
            using (SqlCommand cmd = new SqlCommand(query, conn))
            {
                cmd.Parameters.AddWithValue("@ID", supplier.ID);
                cmd.Parameters.AddWithValue("@Name", supplier.Name ?? (object)DBNull.Value);
                cmd.Parameters.AddWithValue("@ContactPerson", supplier.ContactPerson ?? (object)DBNull.Value);
                cmd.Parameters.AddWithValue("@Phone", supplier.Phone ?? (object)DBNull.Value);
                cmd.Parameters.AddWithValue("@Email", supplier.Email ?? (object)DBNull.Value);
                cmd.Parameters.AddWithValue("@Address", supplier.Address ?? (object)DBNull.Value);

                await conn.OpenAsync();
                int rowsAffected = await cmd.ExecuteNonQueryAsync();
                return rowsAffected > 0;
            }
        }

        public async Task<bool> DeleteSupplierAsync(int supplierId)
        {
            string query = "DELETE FROM Suppliers WHERE SupplierID = @ID;"; // Fixed: SupplierID

            using (SqlConnection conn = new SqlConnection(connectionString))
            using (SqlCommand cmd = new SqlCommand(query, conn))
            {
                cmd.Parameters.AddWithValue("@ID", supplierId);

                await conn.OpenAsync();
                int rowsAffected = await cmd.ExecuteNonQueryAsync();
                return rowsAffected > 0;
            }
        }
    }
}