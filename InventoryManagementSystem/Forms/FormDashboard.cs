using Guna.Charts.WinForms;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;
using System.Xml.Linq;

namespace InventoryManagementSystem.Forms
{
    public partial class FormDashboard : Form
    {
        public class Product
        {
            public int ID { get; set; }
            public string Name { get; set; }
            public decimal Price { get; set; }
            public int Stock { get; set; }
            public string Category { get; set; }
            public string Status { get; set; }
            public DateTime CreatedAt { get; set; }
        }

        public FormDashboard()
        {
            InitializeComponent();
        }

        private void FormProduct_Load(object sender, EventArgs e)
        {
            ConfigureBarChart();
            ConfigurePieChart();
            ConfigureRecentProductView();
        }

        private void ConfigureBarChart()
        {
        }

        private void ConfigurePieChart()
        {
        }

        private void ConfigureRecentProductView()
        {
            recentProductView.ThemeStyle.HeaderStyle.BackColor =
                Color.FromArgb(15, 23, 42);

            recentProductView.ThemeStyle.HeaderStyle.Font =
                new Font("Segoe UI", 10F, FontStyle.Bold);

            recentProductView.ThemeStyle.RowsStyle.Font =
                new Font("Segoe UI", 10.5F);

            recentProductView.AllowUserToResizeColumns = false;
            recentProductView.AllowUserToResizeRows = false;
            recentProductView.ColumnHeadersHeightSizeMode =
                DataGridViewColumnHeadersHeightSizeMode.DisableResizing;
            recentProductView.ColumnHeadersHeight = 38;
            recentProductView.RowTemplate.Height = 32;

            List<Product> products = new List<Product>
            {
                new Product { ID = 1, Name = "Laptop", Price = 999.99m, Stock = 12, Category = "Electronics", Status = "In Stock", CreatedAt = DateTime.Now },
                new Product { ID = 2, Name = "Mouse", Price = 25.50m, Stock = 3, Category = "Accessories", Status = "Low Stock", CreatedAt = DateTime.Now },
                new Product { ID = 3, Name = "Keyboard", Price = 45.00m, Stock = 8, Category = "Accessories", Status = "In Stock", CreatedAt = DateTime.Now },
                new Product { ID = 4, Name = "Monitor", Price = 249.99m, Stock = 5, Category = "Electronics", Status = "Low Stock", CreatedAt = DateTime.Now },
                new Product { ID = 5, Name = "Printer", Price = 180.00m, Stock = 0, Category = "Office", Status = "Out of Stock", CreatedAt = DateTime.Now }
            };

            recentProductView.DataSource = null;
            recentProductView.DataSource = products;
        }

        private void guna2Chart1_Load(object sender, EventArgs e)
        {
            guna2Chart1.Datasets.Clear();

            GunaBarDataset dataset = new GunaBarDataset
            {
                Label = "Total Sales",
                CornerRadius = 10
            };

            dataset.FillColors.Add(Color.FromArgb(54, 140, 246));

            dataset.DataPoints.Add("Jan", 73);
            dataset.DataPoints.Add("Feb", 83);
            dataset.DataPoints.Add("Mar", 72);
            dataset.DataPoints.Add("Apr", 22);
            dataset.DataPoints.Add("May", 81);
            dataset.DataPoints.Add("Jun", 39);

            guna2Chart1.Datasets.Add(dataset);
            guna2Chart1.Update();
        }

        private void gunaChart1_Load(object sender, EventArgs e)
        {
            GunaDoughnutDataset stockDataset = new GunaDoughnutDataset
            {
                Label = "Stock Overview"
            };

            stockDataset.FillColors.Add(Color.FromArgb(46, 204, 113));
            stockDataset.FillColors.Add(Color.FromArgb(243, 156, 18));
            stockDataset.FillColors.Add(Color.FromArgb(231, 76, 60));

            stockDataset.DataPoints.Add("In Stock", 120);
            stockDataset.DataPoints.Add("Low Stock", 40);
            stockDataset.DataPoints.Add("Out of Stock", 15);

            chartStockOverview.Datasets.Clear();
            chartStockOverview.Datasets.Add(stockDataset);
            chartStockOverview.Update();
        }
    }
}
