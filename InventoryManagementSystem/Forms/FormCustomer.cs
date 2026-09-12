using Guna.UI2.WinForms;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;

namespace InventoryManagementSystem.Forms
{
    public partial class FormCustomer : Form
    {
        public class Customer
        {
            public int ID { get; set; }
            public string Name { get; set; }
            public string Phone { get; set; }
            public string Email { get; set; }
            public string Address { get; set; }
        }
        private void ConfigureCustomerView()
        {
            // 1. Force Guna to use Custom Theme Preset
            CustomerView.Theme = Guna.UI2.WinForms.Enums.DataGridViewPresetThemes.Default;

            // Define Custom Colors (High-Contrast Ice Blue)
            Color headerBg = ColorTranslator.FromHtml("#1E90FF");
            Color headerFg = Color.White;

            // 2. Apply Header Styling
            CustomerView.ThemeStyle.HeaderStyle.BackColor = headerBg;
            CustomerView.ThemeStyle.HeaderStyle.ForeColor = headerFg;
            CustomerView.ThemeStyle.HeaderStyle.Font = new Font("Segoe UI", 11F, FontStyle.Bold);
            CustomerView.ThemeStyle.HeaderStyle.BorderStyle = DataGridViewHeaderBorderStyle.None;

            // 3. Prevent Header Selection Blue Highlight (Force colors)
            CustomerView.EnableHeadersVisualStyles = false;
            CustomerView.ColumnHeadersDefaultCellStyle.SelectionBackColor = headerBg;
            CustomerView.ColumnHeadersDefaultCellStyle.SelectionForeColor = headerFg;

            // 4. Apply Row Styling
            CustomerView.ThemeStyle.RowsStyle.Font = new Font("Segoe UI", 12F);
            CustomerView.ThemeStyle.RowsStyle.ForeColor = Color.FromArgb(51, 65, 85);
            CustomerView.ThemeStyle.RowsStyle.SelectionBackColor = Color.FromArgb(240, 246, 255); // Clean light-blue row focus
            CustomerView.ThemeStyle.RowsStyle.SelectionForeColor = Color.Black;

            // 5. Resizing and Layout Setup
            CustomerView.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            CustomerView.AllowUserToAddRows = false;
            CustomerView.AllowUserToResizeColumns = false;
            CustomerView.AllowUserToResizeRows = false;
            CustomerView.RowHeadersVisible = false;
            CustomerView.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.DisableResizing;
            CustomerView.ColumnHeadersHeight = 40;
            CustomerView.RowTemplate.Height = 42;

            // 6. Clear existing columns before binding
            CustomerView.Columns.Clear();

            // Load Sample Customer Data
            List<Customer> customers = new List<Customer>
            {
                new Customer { ID = 1, Name = "John Doe", Phone = "+1 555-0192", Email = "john.doe@example.com", Address = "123 Main St, New York" },
                new Customer { ID = 2, Name = "Jane Smith", Phone = "+1 555-0143", Email = "jane.smith@example.com", Address = "456 Market St, San Francisco" },
                new Customer { ID = 3, Name = "Robert Johnson", Phone = "+1 555-0178", Email = "robert.j@example.com", Address = "789 Pine Rd, Chicago" },
                new Customer { ID = 4, Name = "Emily Davis", Phone = "+1 555-0122", Email = "emily.d@example.com", Address = "321 Oak Ave, Seattle" },
                new Customer { ID = 5, Name = "Michael Brown", Phone = "+1 555-0155", Email = "m.brown@example.com", Address = "654 Elm St, Austin" }
            };

            // 7. Bind Data and Remove Initial Blue Box Highlight
            CustomerView.DataSource = null;
            CustomerView.DataSource = customers;
            CustomerView.ClearSelection();
        }
        public FormCustomer()
        {
            InitializeComponent();
        }

        private void FormCustomer_Load(object sender, EventArgs e)
        {
            ConfigureCustomerView();
        }
        private void guna2ComboBox1_SelectedIndexChanged(
            object sender,
            EventArgs e)
        {
        }

        private void guna2ComboBox2_SelectedIndexChanged(
            object sender,
            EventArgs e)
        {
        }

        private void btnAddCustomer_Click(object sender, EventArgs e)
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
                using (FormAddCustomer addCustomerForm = new FormAddCustomer())
                {
                    addCustomerForm.StartPosition = FormStartPosition.CenterParent;
                    addCustomerForm.ShowDialog(overlay);
                }
            }
        }
    }
}