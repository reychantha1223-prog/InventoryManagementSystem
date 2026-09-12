using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;

namespace InventoryManagementSystem.Forms
{
    public partial class FormSuppliers : Form
    {
        public class Supplier
        {
            public int ID { get; set; }
            public string Name { get; set; }
            [DisplayName("Contact Person")]
            public string ContactPerson { get; set; }
            public string Phone { get; set; }
            public string Email { get; set; }
            public string Address { get; set; }
        }

        public FormSuppliers()
        {
            InitializeComponent();

        }
        private void ConfigureSupplierView()
        {
            // 1. Force Guna to use Custom Theme Preset
            SupplierView.Theme = Guna.UI2.WinForms.Enums.DataGridViewPresetThemes.Default;

            // Define Custom Colors (High-Contrast Ice Blue)
            Color headerBg = ColorTranslator.FromHtml("#1E90FF");
            Color headerFg = Color.White;

            // 2. Apply Header Styling
            SupplierView.ThemeStyle.HeaderStyle.BackColor = headerBg;
            SupplierView.ThemeStyle.HeaderStyle.ForeColor = headerFg;
            SupplierView.ThemeStyle.HeaderStyle.Font = new Font("Segoe UI", 11F, FontStyle.Bold);
            SupplierView.ThemeStyle.HeaderStyle.BorderStyle = DataGridViewHeaderBorderStyle.None;

            // 3. Prevent Header Selection Blue Highlight (Force colors)
            SupplierView.EnableHeadersVisualStyles = false;
            SupplierView.ColumnHeadersDefaultCellStyle.SelectionBackColor = headerBg;
            SupplierView.ColumnHeadersDefaultCellStyle.SelectionForeColor = headerFg;

            // 4. Apply Row Styling (Bigger Text)
            SupplierView.ThemeStyle.RowsStyle.Font = new Font("Segoe UI", 12F);
            SupplierView.ThemeStyle.RowsStyle.ForeColor = Color.FromArgb(51, 65, 85);
            SupplierView.ThemeStyle.RowsStyle.SelectionBackColor = Color.FromArgb(240, 246, 255);
            SupplierView.ThemeStyle.RowsStyle.SelectionForeColor = Color.Black;

            // 5. Resizing and Layout Setup
            SupplierView.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            SupplierView.AllowUserToAddRows = false;
            SupplierView.AllowUserToResizeColumns = false;
            SupplierView.AllowUserToResizeRows = false;
            SupplierView.RowHeadersVisible = false;
            SupplierView.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.DisableResizing;
            SupplierView.ColumnHeadersHeight = 40;
            SupplierView.RowTemplate.Height = 42;

            // 6. Clear existing columns before binding
            SupplierView.Columns.Clear();

            // Load Sample Supplier Data
            List<Supplier> suppliers = new List<Supplier>
            {
                new Supplier { ID = 1, Name = "TechCorp Solutions", ContactPerson = "Alice Johnson", Phone = "+1 555-0199", Email = "supply@techcorp.com", Address = "100 Innovation Way, Tech City" },
                new Supplier { ID = 2, Name = "Global Logistics Ltd", ContactPerson = "Mark Wilson", Phone = "+1 555-0144", Email = "contact@globallogistics.com", Address = "250 Freight Ave, Port Harbor" },
                new Supplier { ID = 3, Name = "Apex Electronics", ContactPerson = "Sarah Connor", Phone = "+1 555-0177", Email = "orders@apexelectronics.com", Address = "78 Industrial Blvd, Silicon Valley" },
                new Supplier { ID = 4, Name = "Prime Office Supplies", ContactPerson = "David Lee", Phone = "+1 555-0133", Email = "sales@primeoffice.com", Address = "42 Commerce St, Metro City" },
                new Supplier { ID = 5, Name = "Omni Distribution", ContactPerson = "Elena Rostova", Phone = "+1 555-0188", Email = "info@omnidist.com", Address = "900 Warehouse Rd, Logistics Hub" }
            };

            // 7. Bind Data and Remove Initial Blue Box Highlight
            SupplierView.DataSource = null;
            SupplierView.DataSource = suppliers;
            SupplierView.ClearSelection();
        }
        private void FormSuppliers_Load(object sender, EventArgs e)
        {
            ConfigureSupplierView();
        }
        private void btnAddSupplier_Click(object sender, EventArgs e)
        {
            Form mainForm =
                TopLevelControl as Form
                ?? Form.ActiveForm
                ?? this;

            using (Form overlay = new Form())
            {
                overlay.StartPosition =
                    FormStartPosition.Manual;

                overlay.FormBorderStyle =
                    FormBorderStyle.None;

                overlay.Opacity = 0.50d;
                overlay.BackColor = Color.Black;
                overlay.ShowInTaskbar = false;

                overlay.Location =
                    mainForm.PointToScreen(Point.Empty);

                overlay.Size =
                    mainForm.ClientSize;

                overlay.Show(mainForm);

                using (FormAddSupplier supplierForm =
                       new FormAddSupplier())
                {
                    supplierForm.StartPosition =
                        FormStartPosition.CenterParent;

                    supplierForm.ShowDialog(overlay);
                }
            }
        }
    }
}