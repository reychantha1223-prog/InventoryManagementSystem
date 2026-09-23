namespace InventoryManagementSystem.Models
{
    public class Product
    {
        private string _name;

        public int ID { get; set; } = 0;

        public string Name
        {
            get => _name;
            set => _name = value?.Trim();
        }

        public int CategoryID { get; set; }
        public int? SupplierID { get; set; }
        public decimal Price { get; set; }
        public int Stock { get; set; }
        public string Description { get; set; }
    }
}