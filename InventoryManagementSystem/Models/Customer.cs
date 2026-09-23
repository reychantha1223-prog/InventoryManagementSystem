namespace InventoryManagementSystem.Models
{
    public class Customer
    {
        public int ID { get; set; }
        public string Name { get; set; }
        public string Phone { get; set; }
        public string Email { get; set; }
        public string Address { get; set; }

        // Parameterless constructor
        public Customer() { }

        // Parameterized constructor
        public Customer(int id, string name, string phone, string email, string address)
        {
            ID = id;
            Name = name;
            Phone = phone;
            Email = email;
            Address = address;
        }
    }
}