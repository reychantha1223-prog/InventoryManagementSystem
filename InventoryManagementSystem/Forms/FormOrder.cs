using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Windows.Forms;

namespace InventoryManagementSystem.Forms
{
    public partial class FormOrder : Form
    {
        public class Order
        {
            public int ID { get; set; }
            [DisplayName("Order Number")]
            public string OrderNumber { get; set; }
            [DisplayName("Customer Name")]
            public string CustomerName { get; set; }
            [DisplayName("Total Amount")]
            public decimal TotalAmount { get; set; }
            public string Status { get; set; }
            public DateTime OrderDate { get; set; }
        }
        private void ConfigureOrderView()
        {
            // 1. Force Guna to use Custom Theme Preset
            OrderView.Theme = Guna.UI2.WinForms.Enums.DataGridViewPresetThemes.Default;

            // Define Custom Colors (Dodger Blue Header Theme: #1E90FF)
            Color headerBg = ColorTranslator.FromHtml("#1E90FF"); // Dodger Blue
            Color headerFg = Color.White;                        // White text for high contrast

            // 2. Apply Header Styling
            OrderView.ThemeStyle.HeaderStyle.BackColor = headerBg;
            OrderView.ThemeStyle.HeaderStyle.ForeColor = headerFg;
            OrderView.ThemeStyle.HeaderStyle.Font = new Font("Segoe UI", 11F, FontStyle.Bold);
            OrderView.ThemeStyle.HeaderStyle.BorderStyle = DataGridViewHeaderBorderStyle.None;

            // 3. Prevent Header Selection Highlight (Force Dodger Blue)
            OrderView.EnableHeadersVisualStyles = false;
            OrderView.ColumnHeadersDefaultCellStyle.SelectionBackColor = headerBg;
            OrderView.ColumnHeadersDefaultCellStyle.SelectionForeColor = headerFg;

            // 4. Apply Row Styling (12F Text Size)
            OrderView.ThemeStyle.RowsStyle.Font = new Font("Segoe UI", 12F);
            OrderView.ThemeStyle.RowsStyle.ForeColor = Color.FromArgb(51, 65, 85);
            OrderView.ThemeStyle.RowsStyle.SelectionBackColor = Color.FromArgb(240, 246, 255); // Ice blue row selection
            OrderView.ThemeStyle.RowsStyle.SelectionForeColor = Color.Black;

            // 5. Resizing and Layout Setup
            OrderView.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            OrderView.AllowUserToAddRows = false;
            OrderView.AllowUserToResizeColumns = false;
            OrderView.AllowUserToResizeRows = false;
            OrderView.RowHeadersVisible = false;
            OrderView.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.DisableResizing;
            OrderView.ColumnHeadersHeight = 40;
            OrderView.RowTemplate.Height = 42;

            // 6. Clear existing columns before binding
            OrderView.Columns.Clear();

            // Load Sample Order Data
            List<Order> orders = new List<Order>
            {
                new Order { ID = 1, OrderNumber = "ORD-2026-001", CustomerName = "John Doe", TotalAmount = 1025.49m, Status = "Completed", OrderDate = DateTime.Now.AddDays(-2) },
                new Order { ID = 2, OrderNumber = "ORD-2026-002", CustomerName = "Jane Smith", TotalAmount = 275.50m, Status = "Pending", OrderDate = DateTime.Now.AddDays(-1) },
                new Order { ID = 3, OrderNumber = "ORD-2026-003", CustomerName = "Robert Johnson", TotalAmount = 45.00m, Status = "Processing", OrderDate = DateTime.Now },
                new Order { ID = 4, OrderNumber = "ORD-2026-004", CustomerName = "Emily Davis", TotalAmount = 499.98m, Status = "Completed", OrderDate = DateTime.Now },
                new Order { ID = 5, OrderNumber = "ORD-2026-005", CustomerName = "Michael Brown", TotalAmount = 180.00m, Status = "Cancelled", OrderDate = DateTime.Now }
            };

            // 7. Bind Data and Remove Initial Blue Box Highlight
            OrderView.DataSource = null;
            OrderView.DataSource = orders;
            OrderView.ClearSelection();
        }
        public FormOrder()
        {
            InitializeComponent();
        }

       

       

        private void FormOrder_Load(object sender,EventArgs e)
        {
            ConfigureOrderView();
        }

        private void guna2Button1_Click(
            object sender,
            EventArgs e)
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

                using (FormCreateOrder createOrderForm =
                       new FormCreateOrder())
                {
                    createOrderForm.StartPosition =
                        FormStartPosition.CenterParent;

                    createOrderForm.ShowDialog(overlay);
                }
            }
        }
    }
}