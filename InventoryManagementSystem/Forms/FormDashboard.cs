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
            // 1. Set Guna Theme to Custom
            recentProductView.Theme = Guna.UI2.WinForms.Enums.DataGridViewPresetThemes.Default; // or Light / Custom depending on Guna version

            // 2. Define Header Colors
            Color headerBg = Color.FromArgb(200, 220, 248);
            Color headerFg = Color.FromArgb(25, 50, 90);

            // Apply Header Colors
            recentProductView.ThemeStyle.HeaderStyle.BackColor = headerBg;
            recentProductView.ThemeStyle.HeaderStyle.ForeColor = headerFg;

            // 3. Configure Row Selection Colors
            recentProductView.ThemeStyle.RowsStyle.SelectionBackColor = Color.FromArgb(240, 246, 255);
            recentProductView.ThemeStyle.RowsStyle.SelectionForeColor = Color.Black;

            // 4. Set Selection Mode & Disable Visual Styles (This removes the blue header highlight!)
            recentProductView.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            recentProductView.EnableHeadersVisualStyles = false;
            recentProductView.ColumnHeadersDefaultCellStyle.SelectionBackColor = headerBg;
            recentProductView.ColumnHeadersDefaultCellStyle.SelectionForeColor = headerFg;
            // 1. Header Styling (Light Ice Blue background with Navy text)
            recentProductView.EnableHeadersVisualStyles = false;
            recentProductView.ThemeStyle.HeaderStyle.BackColor = Color.FromArgb(200, 220, 248);
            recentProductView.ThemeStyle.HeaderStyle.ForeColor = Color.FromArgb(25, 50, 90);
            recentProductView.ThemeStyle.HeaderStyle.Font = new Font("Segoe UI", 10F, FontStyle.Bold);

            // 2. Row Styling
            recentProductView.ThemeStyle.RowsStyle.Font = new Font("Segoe UI", 10.5F);
            recentProductView.RowTemplate.Height = 32;

            // 3. Sizing & Resizing Rules
            recentProductView.AllowUserToResizeColumns = false;
            recentProductView.AllowUserToResizeRows = false;
            recentProductView.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.DisableResizing;
            recentProductView.ColumnHeadersHeight = 38;

            // 4. Clean Header Look (Removes thick borders & selection highlights on headers)
            recentProductView.ThemeStyle.HeaderStyle.BorderStyle = DataGridViewHeaderBorderStyle.None;
            // Match header selection colors to the normal header colors

            List<Product> products = new List<Product>
            {
                new Product { ID = 1, Name = "Laptop", Price = 999.99m, Stock = 12, Category = "Electronics", Status = "In Stock", CreatedAt = DateTime.Now },
                new Product { ID = 2, Name = "Mouse", Price = 25.50m, Stock = 3, Category = "Accessories", Status = "Low Stock", CreatedAt = DateTime.Now },
                new Product { ID = 3, Name = "Keyboard", Price = 45.00m, Stock = 8, Category = "Accessories", Status = "In Stock", CreatedAt = DateTime.Now },
                new Product { ID = 4, Name = "Monitor", Price = 249.99m, Stock = 5, Category = "Electronics", Status = "Low Stock", CreatedAt = DateTime.Now },
                new Product { ID = 5, Name = "Printer", Price = 180.00m, Stock = 0, Category = "Office", Status = "Out of Stock", CreatedAt = DateTime.Now }
            };
            // Deselect any automatically selected cell/column header
            recentProductView.ClearSelection();
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
            stockDataset.FillColors.Clear();
            stockDataset.DataPoints.Clear();
            stockDataset.FillColors.Add(Color.FromArgb(34, 197, 94));
            stockDataset.DataPoints.Add("In Stock", 120);
            stockDataset.FillColors.Add(Color.FromArgb(245, 158, 11));
            stockDataset.DataPoints.Add("Low Stock", 40);
            stockDataset.FillColors.Add(Color.Red);
            stockDataset.DataPoints.Add("Out of Stock", 15);
            chartStockOverview.Datasets.Clear();
            chartStockOverview.Datasets.Add(stockDataset);
            chartStockOverview.Update();
        }

        private void chartStockOverview_Paint(object sender, PaintEventArgs e)
        {

        }
    }
}
