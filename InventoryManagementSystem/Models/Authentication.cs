namespace InventoryManagementSystem.Models
{
    public class Authentication
    {
        public int UserID { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
        public string FullName { get; set; }
        public string Email { get; set; }
        public string Role { get; set; }
        public bool IsActive { get; set; }
        public byte[] ProfileImageBytes { get; set; }

        public Authentication() { }

        public Authentication(string username, string password)
        {
            Username = username;
            Password = password;
        }
    }
}