using System;

namespace InventoryManagementSystem.Models
{
    public class Category
    {
        private string _name;
        private string _description;

        public int ID { get; set; } = 0;

        public string Name
        {
            get => _name;
            set => _name = value?.Trim();
        }

        public string Description
        {
            get => _description;
            set => _description = value?.Trim();
        }

        public string Status { get; set; } = "Active";
    }
}