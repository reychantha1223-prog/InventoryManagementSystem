using InventoryManagementSystem.Models;
using System;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;
using System.Threading.Tasks;

namespace InventoryManagementSystem.Repositories
{
    public class AccountRepository
    {
        private readonly string connectionString = ConfigurationManager.ConnectionStrings["IMSDB"].ConnectionString;

        // Get user profile details by UserID using the Authentication model
        public async Task<Authentication> GetUserProfileAsync(int userId)
        {
            string query = @"
                SELECT UserID, Username, FullName, Email, Role, IsActive, ProfileImage 
                FROM Users 
                WHERE UserID = @UserID;";

            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                await conn.OpenAsync();
                using (SqlCommand cmd = new SqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@UserID", userId);

                    using (SqlDataReader reader = await cmd.ExecuteReaderAsync())
                    {
                        if (await reader.ReadAsync())
                        {
                            return new Authentication
                            {
                                UserID = Convert.ToInt32(reader["UserID"]),
                                Username = reader["Username"].ToString(),
                                FullName = reader["FullName"]?.ToString(),
                                Email = reader["Email"]?.ToString(),
                                Role = reader["Role"]?.ToString(),
                                IsActive = reader["IsActive"] != DBNull.Value && Convert.ToBoolean(reader["IsActive"]),
                                ProfileImageBytes = reader["ProfileImage"] as byte[]
                            };
                        }
                    }
                }
            }
            return null;
        }

        // Update Full Name, Email, and Profile Picture
        public async Task<bool> UpdateProfileInfoAsync(int userId, string fullName, string email, byte[] imageBytes)
        {
            // If new imageBytes are provided, update ProfileImage; otherwise, keep existing DB value
            string query = @"
                UPDATE Users 
                SET FullName = @FullName, 
                    Email = @Email, 
                    ProfileImage = CASE WHEN @HasNewImage = 1 THEN @ProfileImage ELSE ProfileImage END
                WHERE UserID = @UserID;";

            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                await conn.OpenAsync();
                using (SqlCommand cmd = new SqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@UserID", userId);
                    cmd.Parameters.AddWithValue("@FullName", fullName.Trim());
                    cmd.Parameters.AddWithValue("@Email", string.IsNullOrWhiteSpace(email) ? (object)DBNull.Value : email.Trim());

                    // Flag to tell SQL if a new image was uploaded
                    cmd.Parameters.AddWithValue("@HasNewImage", imageBytes != null && imageBytes.Length > 0 ? 1 : 0);

                    // Explicitly declare VARBINARY(MAX) parameter
                    SqlParameter imgParam = new SqlParameter("@ProfileImage", SqlDbType.VarBinary, -1);
                    imgParam.Value = (object)imageBytes ?? DBNull.Value;
                    cmd.Parameters.Add(imgParam);

                    int rows = await cmd.ExecuteNonQueryAsync();
                    return rows > 0;
                }
            }
        }
        public async Task<bool> ChangePasswordAsync(int userId, string currentPassword, string newPassword)
        {
            string query = @"
        UPDATE Users 
        SET PasswordHash = @NewPassword 
        WHERE UserID = @UserID AND PasswordHash = @CurrentPassword;";

            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                await conn.OpenAsync();
                using (SqlCommand cmd = new SqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@UserID", userId);
                    cmd.Parameters.AddWithValue("@CurrentPassword", currentPassword);
                    cmd.Parameters.AddWithValue("@NewPassword", newPassword);

                    int rowsAffected = await cmd.ExecuteNonQueryAsync();
                    return rowsAffected > 0; // Returns true if current password matched and update succeeded
                }
            }
        }
    }
}