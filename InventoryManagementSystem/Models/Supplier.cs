namespace InventoryManagementSystem.Models
{
    public class Supplier
    {
        public int ID { get; set; }
        public string Name { get; set; }
        public string ContactPerson { get; set; }
        public string Phone { get; set; }
        public string Email { get; set; }
        public string Address { get; set; }

        public Supplier() { }

        public Supplier(int id, string name, string contactPerson, string phone, string email, string address)
        {
            ID = id;
            Name = name;
            ContactPerson = contactPerson;
            Phone = phone;
            Email = email;
            Address = address;
        }
    }
}