using System;
using System.Data.SqlClient;
using System.Threading.Tasks;
using InventoryManagementSystem.Database;
using InventoryManagementSystem.Models;

namespace InventoryManagementSystem.Repositories
{
    public class AuthenticationRepository
    {
        // Holds global session state for logged-in user
        public static Authentication CurrentUser { get; private set; }

        public async Task<Authentication> ValidateLoginAsync(string username, string password)
        {
            if (string.IsNullOrWhiteSpace(username) || string.IsNullOrWhiteSpace(password))
            {
                return null;
            }

            string query = @"
                SELECT UserID, Username, FullName, Role 
                FROM Users 
                WHERE Username = @Username 
                  AND PasswordHash = @Password 
                  AND IsActive = 1;";

            using (SqlConnection conn = DbConnection.Create())
            {
                await conn.OpenAsync();
                using (SqlCommand cmd = new SqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@Username", username.Trim());
                    cmd.Parameters.AddWithValue("@Password", password);

                    using (SqlDataReader reader = await cmd.ExecuteReaderAsync())
                    {
                        if (await reader.ReadAsync())
                        {
                            Authentication user = new Authentication
                            {
                                UserID = Convert.ToInt32(reader["UserID"]),
                                Username = reader["Username"].ToString(),
                                FullName = reader["FullName"]?.ToString(),
                                Role = reader["Role"]?.ToString()
                            };

                            // Save current user session
                            CurrentUser = user;
                            return user;
                        }
                    }
                }
            }

            CurrentUser = null;
            return null;
        }

        public static void Logout()
        {
            CurrentUser = null;
        }
    }
}