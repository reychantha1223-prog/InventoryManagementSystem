using System;

namespace InventoryManagementSystem.Models
{
    public class InvoiceDetailsModel
    {
        public int InvoiceID { get; set; }
        public DateTime Date { get; set; }
        public string CustomerName { get; set; }
        public string PaymentMethod { get; set; }
        public decimal TotalAmount { get; set; }
        public decimal DiscountPercent { get; set; }
        public string Notes { get; set; }
        public string CreatedBy { get; set; }
        public DateTime CreatedAt { get; set; }
    }
}