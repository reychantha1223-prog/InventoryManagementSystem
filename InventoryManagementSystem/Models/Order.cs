using System;

namespace InventoryManagementSystem.Models
{
    public class Order
    {
        public int OrderID { get; set; }
        public string OrderNumber { get; set; }
        public int CustomerID { get; set; }
        public DateTime OrderDate { get; set; } = DateTime.Now;
        public decimal TotalAmount { get; set; }
        public string Status { get; set; }
        public string Description { get; set; } 

        public string CustomerName { get; set; }
    }
}