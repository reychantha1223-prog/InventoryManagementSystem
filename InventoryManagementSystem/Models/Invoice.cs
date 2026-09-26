using System;
using System.Collections.Generic;

namespace InventoryManagementSystem.Models
{
    public class InvoiceHeader
    {
        public int InvoiceID { get; set; }
        public string InvoiceCode => $"INV-{InvoiceID:D4}"; // Formats as INV-0001
        public DateTime Date { get; set; }
        public string CustomerName { get; set; }
        public decimal TotalAmount { get; set; }
        public string Status { get; set; } // Paid, Pending, Cancelled
        public string PaymentMethod { get; set; } // Cash, Credit Card, Bank Transfer
        public decimal DiscountPercent { get; set; }
        public string Notes { get; set; }
        public string CreatedBy { get; set; }
        public DateTime CreatedAt { get; set; }

        public List<InvoiceItem> Items { get; set; } = new List<InvoiceItem>();
    }

    public class InvoiceItem
    {
        public int ItemNo { get; set; }
        public string ProductName { get; set; }
        public int Quantity { get; set; }
        public decimal UnitPrice { get; set; }
        public decimal Total => Quantity * UnitPrice;
    }
}