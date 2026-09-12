using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;
using static InventoryManagementSystem.Forms.FormDashboard;

namespace InventoryManagementSystem.Forms
{
    public partial class FormProducts : Form
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

        private bool isFirstClick = true;

        public FormProducts()
        {
            InitializeComponent();
        }

        private void FormProducts_Load(object sender, EventArgs e)
        {
            SetupComboBoxes();
            LoadSampleData();
        }

        private void SetupComboBoxes()
        {
            FilterCategory.Items.AddRange(new object[]
            {
                "All Category", "Electronics", "Clothing", "Food & Beverages"
            });
            FilterCategory.SelectedIndex = 0;

            FilterStatus.Items.AddRange(new object[]
            {
                "All Stock", "Low Stock", "Out of Stock"
            });
            FilterStatus.SelectedIndex = 0;
        }

        private void LoadSampleData()
        {
            ConfigureProductView();
        }

        private void ConfigureProductView()
        {
            // 1. Force Guna to use Custom Theme Preset
            ProductView.Theme = Guna.UI2.WinForms.Enums.DataGridViewPresetThemes.Default;

            // Define Custom Colors (High-Contrast Ice Blue)
            Color headerBg = ColorTranslator.FromHtml("#1E90FF");
            Color headerFg = Color.White;

            // 2. Apply Header Styling
            ProductView.ThemeStyle.HeaderStyle.BackColor = headerBg;
            ProductView.ThemeStyle.HeaderStyle.ForeColor = headerFg;
            ProductView.ThemeStyle.HeaderStyle.Font = new Font("Segoe UI", 11F, FontStyle.Bold);
            ProductView.ThemeStyle.HeaderStyle.BorderStyle = DataGridViewHeaderBorderStyle.None;

            // 3. Prevent Header Selection Blue Highlight (Force colors)
            ProductView.EnableHeadersVisualStyles = false;
            ProductView.ColumnHeadersDefaultCellStyle.SelectionBackColor = headerBg;
            ProductView.ColumnHeadersDefaultCellStyle.SelectionForeColor = headerFg;

            // 4. Apply Row Styling
            ProductView.ThemeStyle.RowsStyle.Font = new Font("Segoe UI", 12F);
            ProductView.ThemeStyle.RowsStyle.ForeColor = Color.FromArgb(51, 65, 85);
            ProductView.ThemeStyle.RowsStyle.SelectionBackColor = Color.FromArgb(240, 246, 255); // Clean light-blue row focus
            ProductView.ThemeStyle.RowsStyle.SelectionForeColor = Color.Black;

            // 5. Resizing and Layout Setup
            ProductView.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            ProductView.AllowUserToAddRows = false;
            ProductView.AllowUserToResizeColumns = false;
            ProductView.AllowUserToResizeRows = false;
            ProductView.RowHeadersVisible = false;
            ProductView.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.DisableResizing;
            ProductView.ColumnHeadersHeight = 40;
            ProductView.RowTemplate.Height = 42;

            // 6. Clear existing columns before binding
            ProductView.Columns.Clear();

            // Load Sample Product Data
            List<Product> products = new List<Product>
        {
            new Product { ID = 1, Name = "Laptop", Price = 999.99m, Stock = 12, Category = "Electronics", Status = "In Stock", CreatedAt = DateTime.Now },
            new Product { ID = 2, Name = "Mouse", Price = 25.50m, Stock = 3, Category = "Accessories", Status = "Low Stock", CreatedAt = DateTime.Now },
            new Product { ID = 3, Name = "Keyboard", Price = 45.00m, Stock = 8, Category = "Accessories", Status = "In Stock", CreatedAt = DateTime.Now },
            new Product { ID = 4, Name = "Monitor", Price = 249.99m, Stock = 5, Category = "Electronics", Status = "Low Stock", CreatedAt = DateTime.Now },
            new Product { ID = 5, Name = "Printer", Price = 180.00m, Stock = 0, Category = "Office", Status = "Out of Stock", CreatedAt = DateTime.Now }
        };

            // 7. Bind Data and Remove Initial Blue Box Highlight
            ProductView.DataSource = null;
            ProductView.DataSource = products;
            ProductView.ClearSelection();
        }

        private void txtProduct_Click(object sender, EventArgs e)
        {
            if (isFirstClick)
            {
                txtProducts.ForeColor = Color.Black;
                txtProducts.Clear();
                isFirstClick = false;
            }
        }

        private void btnAddProduct_Click(object sender, EventArgs e)
        {
            Form mainForm = this.TopLevelControl as Form ?? Form.ActiveForm ?? this;

            using (Form overlay = new Form())
            {
                overlay.StartPosition = FormStartPosition.Manual;
                overlay.FormBorderStyle = FormBorderStyle.None;
                overlay.Opacity = 0.50d;
                overlay.BackColor = Color.Black;
                overlay.ShowInTaskbar = false;

                overlay.Location = mainForm.PointToScreen(Point.Empty);
                overlay.Size = mainForm.ClientSize;
                overlay.Show(mainForm);

                using (FormAddProduct addProductForm = new FormAddProduct())
                {
                    addProductForm.StartPosition = FormStartPosition.CenterParent;
                    addProductForm.ShowDialog(overlay);
                }
            }
        }
    }
}