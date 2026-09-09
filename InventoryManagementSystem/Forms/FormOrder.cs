using System;
using System.Data;
using System.Drawing;
using System.Windows.Forms;

namespace InventoryManagementSystem.Forms
{
    public partial class FormOrder : Form
    {
        public FormOrder()
        {
            InitializeComponent();

            // ==========================================
            // DATAGRIDVIEW
            // ==========================================

            dgvOrders.AutoGenerateColumns = false;

            // Connect Designer columns to DataTable
            dgvOrders.Columns[0].DataPropertyName = "ID";
            dgvOrders.Columns[1].DataPropertyName = "Date";
            dgvOrders.Columns[2].DataPropertyName = "Customer";
            dgvOrders.Columns[3].DataPropertyName = "Total Amount";
            dgvOrders.Columns[4].DataPropertyName = "Status";

            // DO NOT use:
            // dgvOrders.Columns[5].DataPropertyName = "Action";

            // ==========================================
            // EVENTS
            // ==========================================

            dgvOrders.CellPainting += dgvOrders_CellPainting;
            dgvOrders.CellClick += dgvOrders_CellClick;

            // ==========================================
            // LOAD DATA
            // ==========================================

            LoadSampleOrders();
        }


        // =====================================================
        // LOAD SAMPLE ORDERS
        // =====================================================

        private void LoadSampleOrders()
        {
            DataTable dt = new DataTable();

            dt.Columns.Add("ID");
            dt.Columns.Add("Date");
            dt.Columns.Add("Customer");
            dt.Columns.Add("Total Amount");
            dt.Columns.Add("Status");

            dt.Rows.Add(
                "ORD-001",
                "2025-09-01",
                "Sok Dara",
                "$120.00",
                "Pending"
            );

            dt.Rows.Add(
                "ORD-002",
                "2025-08-30",
                "Chan Vibol",
                "$250.00",
                "Paid"
            );

            dt.Rows.Add(
                "ORD-003",
                "2025-08-28",
                "Srey Neang",
                "$80.00",
                "Shipped"
            );

            dt.Rows.Add(
                "ORD-004",
                "2025-08-27",
                "Long Ratha",
                "$175.00",
                "Pending"
            );

            dt.Rows.Add(
                "ORD-005",
                "2025-08-25",
                "Kim Sovan",
                "$320.00",
                "Delivered"
            );

            dgvOrders.DataSource = dt;

            SetupOrderGrid();
        }


        // =====================================================
        // SETUP DATAGRIDVIEW
        // =====================================================

        private void SetupOrderGrid()
        {
            // Header
            dgvOrders.ColumnHeadersHeightSizeMode =
                DataGridViewColumnHeadersHeightSizeMode.EnableResizing;

            dgvOrders.ColumnHeadersHeight = 40;

            dgvOrders.ColumnHeadersDefaultCellStyle.Font =
                new Font("Segoe UI", 12F, FontStyle.Bold);

            dgvOrders.ColumnHeadersDefaultCellStyle.Alignment =
                DataGridViewContentAlignment.MiddleLeft;

            // Rows
            dgvOrders.RowTemplate.Height = 35;

            dgvOrders.DefaultCellStyle.Font =
                new Font("Segoe UI", 11F, FontStyle.Regular);

            dgvOrders.DefaultCellStyle.Alignment =
                DataGridViewContentAlignment.MiddleLeft;

            // Grid
            dgvOrders.CellBorderStyle =
                DataGridViewCellBorderStyle.Single;

            dgvOrders.GridColor = Color.Gray;

            // Settings
            dgvOrders.AllowUserToAddRows = false;
            dgvOrders.AllowUserToDeleteRows = false;
            dgvOrders.AllowUserToResizeRows = false;

            dgvOrders.EnableHeadersVisualStyles = false;
            dgvOrders.RowHeadersVisible = false;
            dgvOrders.ReadOnly = true;

            // IMPORTANT
            // Do NOT use Fill
            dgvOrders.AutoSizeColumnsMode =
                DataGridViewAutoSizeColumnsMode.None;

            // ==========================================
            // FIXED COLUMN WIDTH
            // ==========================================

            dgvOrders.Columns[0].Width = 205; // ID
            dgvOrders.Columns[1].Width = 205; // Date
            dgvOrders.Columns[2].Width = 205; // Customer
            dgvOrders.Columns[3].Width = 205; // Total Amount
            dgvOrders.Columns[4].Width = 205; // Status
            dgvOrders.Columns[5].Width = 205; // Action

            // Prevent user resizing columns
            foreach (DataGridViewColumn column in dgvOrders.Columns)
            {
                column.Resizable =
                    DataGridViewTriState.False;
            }

            // ==========================================
            // SELECTION COLOR
            // ==========================================

            dgvOrders.DefaultCellStyle.SelectionBackColor =
                Color.FromArgb(0, 123, 255);

            dgvOrders.DefaultCellStyle.SelectionForeColor =
                Color.White;
        }


        // =====================================================
        // DRAW ACTION BUTTONS
        // =====================================================

        private void dgvOrders_CellPainting(
            object sender,
            DataGridViewCellPaintingEventArgs e)
        {
            if (e.RowIndex < 0 || e.ColumnIndex < 0)
                return;

            // Only Action column
            if (dgvOrders.Columns[e.ColumnIndex].HeaderText != "Action")
                return;

            // Paint normal background
            e.PaintBackground(e.CellBounds, false);

            int buttonWidth = 55;
            int buttonHeight = 26;
            int spacing = 5;

            int totalWidth =
                (buttonWidth * 3) +
                (spacing * 2);

            int startX =
                e.CellBounds.X +
                (e.CellBounds.Width - totalWidth) / 2;

            int startY =
                e.CellBounds.Y +
                (e.CellBounds.Height - buttonHeight) / 2;

            // ==========================================
            // BUTTON RECTANGLES
            // ==========================================

            Rectangle viewButton = new Rectangle(
                startX,
                startY,
                buttonWidth,
                buttonHeight
            );

            Rectangle editButton = new Rectangle(
                startX + buttonWidth + spacing,
                startY,
                buttonWidth,
                buttonHeight
            );

            Rectangle deleteButton = new Rectangle(
                startX + (buttonWidth + spacing) * 2,
                startY,
                buttonWidth,
                buttonHeight
            );

            // ==========================================
            // VIEW BUTTON
            // ==========================================

            using (SolidBrush brush =
                new SolidBrush(Color.FromArgb(235, 245, 255)))
            {
                e.Graphics.FillRectangle(
                    brush,
                    viewButton
                );
            }

            using (Font font =
                new Font("Segoe UI", 9F))
            using (SolidBrush brush =
                new SolidBrush(Color.FromArgb(0, 123, 255)))
            {
                StringFormat format = new StringFormat
                {
                    Alignment = StringAlignment.Center,
                    LineAlignment = StringAlignment.Center
                };

                e.Graphics.DrawString(
                    "View",
                    font,
                    brush,
                    viewButton,
                    format
                );
            }

            // ==========================================
            // EDIT BUTTON
            // ==========================================

            using (SolidBrush brush =
                new SolidBrush(Color.FromArgb(235, 245, 255)))
            {
                e.Graphics.FillRectangle(
                    brush,
                    editButton
                );
            }

            using (Font font =
                new Font("Segoe UI", 9F))
            using (SolidBrush brush =
                new SolidBrush(Color.FromArgb(0, 123, 255)))
            {
                StringFormat format = new StringFormat
                {
                    Alignment = StringAlignment.Center,
                    LineAlignment = StringAlignment.Center
                };

                e.Graphics.DrawString(
                    "Edit",
                    font,
                    brush,
                    editButton,
                    format
                );
            }

            // ==========================================
            // DELETE BUTTON
            // ==========================================

            using (SolidBrush brush =
                new SolidBrush(Color.FromArgb(255, 235, 235)))
            {
                e.Graphics.FillRectangle(
                    brush,
                    deleteButton
                );
            }

            using (Font font =
                new Font("Segoe UI", 9F))
            using (SolidBrush brush =
                new SolidBrush(Color.Red))
            {
                StringFormat format = new StringFormat
                {
                    Alignment = StringAlignment.Center,
                    LineAlignment = StringAlignment.Center
                };

                e.Graphics.DrawString(
                    "Delete",
                    font,
                    brush,
                    deleteButton,
                    format
                );
            }

            e.Handled = true;
        }


        // =====================================================
        // ACTION BUTTON CLICK
        // =====================================================

        private void dgvOrders_CellClick(
            object sender,
            DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0 || e.ColumnIndex < 0)
                return;

            // Only Action column
            if (dgvOrders.Columns[e.ColumnIndex].HeaderText != "Action")
                return;

            // Get Action cell position
            Rectangle cellRect =
                dgvOrders.GetCellDisplayRectangle(
                    e.ColumnIndex,
                    e.RowIndex,
                    false
                );

            Point mousePosition =
                dgvOrders.PointToClient(
                    Cursor.Position
                );

            int x =
                mousePosition.X -
                cellRect.X;

            // ==========================================
            // BUTTON SIZE
            // ==========================================

            int buttonWidth = 55;
            int spacing = 5;

            string orderID =
                dgvOrders.Rows[e.RowIndex]
                .Cells[0]
                .Value?.ToString();

            // ==========================================
            // VIEW
            // ==========================================

            if (x >= 0 &&
                x < buttonWidth)
            {
                MessageBox.Show(
                    "View Order: " + orderID,
                    "View Order",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Information
                );
            }

            // ==========================================
            // EDIT
            // ==========================================

            else if (
                x >= buttonWidth + spacing &&
                x < (buttonWidth * 2) + spacing)
            {
                MessageBox.Show(
                    "Edit Order: " + orderID,
                    "Edit Order",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Information
                );
            }

            // ==========================================
            // DELETE
            // ==========================================

            else if (
                x >= (buttonWidth + spacing) * 2)
            {
                DialogResult result =
                    MessageBox.Show(
                        "Are you sure you want to delete "
                        + orderID
                        + "?",
                        "Delete Order",
                        MessageBoxButtons.YesNo,
                        MessageBoxIcon.Warning
                    );

                if (result == DialogResult.Yes)
                {
                    dgvOrders.Rows.RemoveAt(
                        e.RowIndex
                    );
                }
            }
        }


        // =====================================================
        // EXISTING EVENTS
        // =====================================================

        private void dgvOrders_CellContentClick(
            object sender,
            DataGridViewCellEventArgs e)
        {
        }


        private void FormOrder_Load(
            object sender,
            EventArgs e)
        {
        }


        // =====================================================
        // CREATE ORDER BUTTON
        // =====================================================

        private void guna2Button1_Click(
            object sender,
            EventArgs e)
        {
            // Get the main dashboard form
            Form mainForm =
                this.TopLevelControl as Form
                ?? Form.ActiveForm
                ?? this;

            // ==========================================
            // CREATE DARK OVERLAY
            // ==========================================

            using (Form overlay = new Form())
            {
                overlay.StartPosition =
                    FormStartPosition.Manual;

                overlay.FormBorderStyle =
                    FormBorderStyle.None;

                overlay.Opacity = 0.50d;

                overlay.BackColor =
                    Color.Black;

                overlay.ShowInTaskbar = false;

                // ==========================================
                // POSITION OVER MAIN DASHBOARD
                // ==========================================

                overlay.Location =
                    mainForm.PointToScreen(
                        Point.Empty
                    );

                overlay.Size =
                    mainForm.ClientSize;

                // ==========================================
                // SHOW OVERLAY
                // ==========================================

                overlay.Show(mainForm);

                // ==========================================
                // OPEN CREATE ORDER FORM
                // ==========================================

                using (FormCreateOrder createOrderForm =
                    new FormCreateOrder())
                {
                    createOrderForm.StartPosition =
                        FormStartPosition.CenterParent;

                    createOrderForm.ShowDialog(
                        overlay
                    );
                }
            }
        }
    }
}