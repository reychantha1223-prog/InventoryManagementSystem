using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace InventoryManagementSystem.Forms
{
    public partial class FormCategory : Form
    {
        public class Category
        {
            public int ID { get; set; }
            public string Name { get; set; }
            public string Description { get; set; }
            [DisplayName("Total Products")]
            public int TotalProducts { get; set; }
            public string Status { get; set; }
            public DateTime CreatedAt { get; set; }
        }
        public FormCategory()
        {
            InitializeComponent();
        }
        private void FormCategory_Load(object sender, EventArgs e)
        {
            ConfigureCategoryView();
        }
        private void ConfigureCategoryView()
        {
            // 1. Force Guna to use Custom Theme Preset
            CategoryView.Theme = Guna.UI2.WinForms.Enums.DataGridViewPresetThemes.Default;

            // Define Custom Colors (High-Contrast Ice Blue)
            Color headerBg = ColorTranslator.FromHtml("#1E90FF");
            Color headerFg = Color.White;

            // 2. Apply Header Styling
            CategoryView.ThemeStyle.HeaderStyle.BackColor = headerBg;
            CategoryView.ThemeStyle.HeaderStyle.ForeColor = headerFg;
            CategoryView.ThemeStyle.HeaderStyle.Font = new Font("Segoe UI", 11F, FontStyle.Bold);
            CategoryView.ThemeStyle.HeaderStyle.BorderStyle = DataGridViewHeaderBorderStyle.None;

            // 3. Prevent Header Selection Blue Highlight (Force colors)
            CategoryView.EnableHeadersVisualStyles = false;
            CategoryView.ColumnHeadersDefaultCellStyle.SelectionBackColor = headerBg;
            CategoryView.ColumnHeadersDefaultCellStyle.SelectionForeColor = headerFg;

            // 4. Apply Row Styling
            CategoryView.ThemeStyle.RowsStyle.Font = new Font("Segoe UI", 12F);
            CategoryView.ThemeStyle.RowsStyle.ForeColor = Color.FromArgb(51, 65, 85);
            CategoryView.ThemeStyle.RowsStyle.SelectionBackColor = Color.FromArgb(240, 246, 255); // Clean light-blue row focus
            CategoryView.ThemeStyle.RowsStyle.SelectionForeColor = Color.Black;

            // 5. Resizing and Layout Setup
            CategoryView.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            CategoryView.AllowUserToAddRows = false;
            CategoryView.AllowUserToResizeColumns = false;
            CategoryView.AllowUserToResizeRows = false;
            CategoryView.RowHeadersVisible = false;
            CategoryView.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.DisableResizing;
            CategoryView.ColumnHeadersHeight = 40;
            CategoryView.RowTemplate.Height = 42;

            // 6. Clear existing columns before binding
            CategoryView.Columns.Clear();

            // Load Sample Category Data
            List<Category> categories = new List<Category>
            {
                new Category { ID = 1, Name = "Electronics", Description = "Devices, gadgets, and components", TotalProducts = 45, Status = "Active", CreatedAt = DateTime.Now },
                new Category { ID = 2, Name = "Accessories", Description = "Peripherals and add-ons", TotalProducts = 30, Status = "Active", CreatedAt = DateTime.Now },
                new Category { ID = 3, Name = "Clothing", Description = "Apparel and footwear", TotalProducts = 85, Status = "Active", CreatedAt = DateTime.Now },
                new Category { ID = 4, Name = "Food & Beverages", Description = "Packaged food and drink items", TotalProducts = 120, Status = "Active", CreatedAt = DateTime.Now },
                new Category { ID = 5, Name = "Office", Description = "Office supplies and stationery", TotalProducts = 18, Status = "Inactive", CreatedAt = DateTime.Now }
            };

            // 7. Bind Data and Remove Initial Blue Box Highlight
            CategoryView.DataSource = null;
            CategoryView.DataSource = categories;
            CategoryView.ClearSelection();
        }
        private void btnAddCategory_Click(object sender, EventArgs e)
        {
            Form mainForm = this.TopLevelControl as Form ?? Form.ActiveForm ?? this;

            using (Form overlay = new Form())
            {
                overlay.StartPosition = FormStartPosition.Manual;
                overlay.FormBorderStyle = FormBorderStyle.None;
                overlay.Opacity = 0.50d; // Controls dark overlay intensity
                overlay.BackColor = Color.Black;
                overlay.ShowInTaskbar = false;

                // Cover the exact client area of the entire dashboard window
                overlay.Location = mainForm.PointToScreen(Point.Empty);
                overlay.Size = mainForm.ClientSize;

                // Display overlay over main form
                overlay.Show(mainForm);

                // Open FormAddCategory popup centered on top of the overlay
                using (FormAddCategory addCategoryForm = new FormAddCategory())
                {
                    addCategoryForm.StartPosition = FormStartPosition.CenterParent;
                    addCategoryForm.ShowDialog(overlay);
                }
            }
        }
    }
}