using System;

namespace InventoryManagementSystem.Models
{
    // 1. Metric Cards Summary
    public class DashboardSummary
    {
        public int TotalProducts { get; set; }
        public int TotalCategories { get; set; }
        public int TotalCustomers { get; set; }
        public int TotalSuppliers { get; set; }
    }

    // 2. Sales Bar Chart Data
    public class MonthlySales
    {
        public string MonthName { get; set; }
        public decimal TotalSales { get; set; }
    }

    // 3. Stock Status Doughnut Chart Data
    public class StockStatusMetrics
    {
        public int InStockCount { get; set; }
        public int LowStockCount { get; set; }
        public int OutOfStockCount { get; set; }
        public int TotalProducts => InStockCount + LowStockCount + OutOfStockCount;
    }

    // 4. Recent Products Table Data
    public class RecentProduct
    {
        public int ID { get; set; }
        public string Name { get; set; }
        public decimal Price { get; set; }
        public int Stock { get; set; }
        public string Category { get; set; }
        public string Status { get; set; }
        public DateTime CreatedAt { get; set; }
    }
}