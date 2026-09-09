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

            OrderInfoView.AutoGenerateColumns = false;

            OrderInfoView.Columns[0].DataPropertyName = "ID";
            OrderInfoView.Columns[1].DataPropertyName = "Date";
            OrderInfoView.Columns[2].DataPropertyName = "Customer";
            OrderInfoView.Columns[3].DataPropertyName = "Total Amount";
            OrderInfoView.Columns[4].DataPropertyName = "Status";

            OrderInfoView.CellPainting += guna2DataGridView2_CellPainting;
            OrderInfoView.CellClick += guna2DataGridView2_CellClick;

            LoadSampleOrders();
        }

        private void LoadSampleOrders()
        {
            DataTable dt = new DataTable();

            dt.Columns.Add("ID");
            dt.Columns.Add("Date");
            dt.Columns.Add("Customer");
            dt.Columns.Add("Total Amount");
            dt.Columns.Add("Status");

            dt.Rows.Add("ORD-001", "2025-09-01", "Sok Dara", "$120.00", "Pending");
            dt.Rows.Add("ORD-002", "2025-08-30", "Chan Vibol", "$250.00", "Paid");
            dt.Rows.Add("ORD-003", "2025-08-28", "Srey Neang", "$80.00", "Shipped");
            dt.Rows.Add("ORD-004", "2025-08-27", "Long Ratha", "$175.00", "Pending");
            dt.Rows.Add("ORD-005", "2025-08-25", "Kim Sovan", "$320.00", "Delivered");

            OrderInfoView.DataSource = dt;

            SetupOrderGrid();
        }

        private void SetupOrderGrid()
        {
            OrderInfoView.ColumnHeadersHeightSizeMode =
                DataGridViewColumnHeadersHeightSizeMode.EnableResizing;

            OrderInfoView.ColumnHeadersHeight = 40;

            OrderInfoView.ColumnHeadersDefaultCellStyle.Font =
                new Font("Segoe UI", 12F, FontStyle.Bold);

            OrderInfoView.ColumnHeadersDefaultCellStyle.Alignment =
                DataGridViewContentAlignment.MiddleLeft;

            OrderInfoView.RowTemplate.Height = 35;

            OrderInfoView.DefaultCellStyle.Font =
                new Font("Segoe UI", 11F);

            OrderInfoView.DefaultCellStyle.Alignment =
                DataGridViewContentAlignment.MiddleLeft;

            OrderInfoView.CellBorderStyle =
                DataGridViewCellBorderStyle.Single;

            OrderInfoView.GridColor = Color.Gray;

            OrderInfoView.AllowUserToAddRows = false;
            OrderInfoView.AllowUserToDeleteRows = false;
            OrderInfoView.AllowUserToResizeRows = false;
            OrderInfoView.EnableHeadersVisualStyles = false;
            OrderInfoView.RowHeadersVisible = false;
            OrderInfoView.ReadOnly = true;

            OrderInfoView.AutoSizeColumnsMode =
                DataGridViewAutoSizeColumnsMode.None;

            OrderInfoView.Columns[0].Width = 205;
            OrderInfoView.Columns[1].Width = 205;
            OrderInfoView.Columns[2].Width = 205;
            OrderInfoView.Columns[3].Width = 205;
            OrderInfoView.Columns[4].Width = 205;
            OrderInfoView.Columns[5].Width = 205;

            foreach (DataGridViewColumn column in OrderInfoView.Columns)
            {
                column.Resizable = DataGridViewTriState.False;
            }

            OrderInfoView.DefaultCellStyle.SelectionBackColor =
                Color.FromArgb(0, 123, 255);

            OrderInfoView.DefaultCellStyle.SelectionForeColor =
                Color.White;
        }

        private void guna2DataGridView2_CellPainting(
            object sender,
            DataGridViewCellPaintingEventArgs e)
        {
            if (e.RowIndex < 0 || e.ColumnIndex < 0)
                return;

            if (OrderInfoView.Columns[e.ColumnIndex].HeaderText != "Action")
                return;

            e.PaintBackground(e.CellBounds, false);

            int buttonWidth = 55;
            int buttonHeight = 26;
            int spacing = 5;

            int totalWidth = (buttonWidth * 3) + (spacing * 2);

            int startX =
                e.CellBounds.X +
                (e.CellBounds.Width - totalWidth) / 2;

            int startY =
                e.CellBounds.Y +
                (e.CellBounds.Height - buttonHeight) / 2;

            Rectangle viewButton = new Rectangle(
                startX,
                startY,
                buttonWidth,
                buttonHeight);

            Rectangle editButton = new Rectangle(
                startX + buttonWidth + spacing,
                startY,
                buttonWidth,
                buttonHeight);

            Rectangle deleteButton = new Rectangle(
                startX + (buttonWidth + spacing) * 2,
                startY,
                buttonWidth,
                buttonHeight);

            using (SolidBrush brush =
                new SolidBrush(Color.FromArgb(235, 245, 255)))
            {
                e.Graphics.FillRectangle(brush, viewButton);
                e.Graphics.FillRectangle(brush, editButton);
            }

            using (SolidBrush brush =
                new SolidBrush(Color.FromArgb(255, 235, 235)))
            {
                e.Graphics.FillRectangle(brush, deleteButton);
            }

            using (Font font = new Font("Segoe UI", 9F))
            using (SolidBrush blueBrush =
                new SolidBrush(Color.FromArgb(0, 123, 255)))
            using (SolidBrush redBrush =
                new SolidBrush(Color.Red))
            using (StringFormat format = new StringFormat())
            {
                format.Alignment = StringAlignment.Center;
                format.LineAlignment = StringAlignment.Center;

                e.Graphics.DrawString(
                    "View",
                    font,
                    blueBrush,
                    viewButton,
                    format);

                e.Graphics.DrawString(
                    "Edit",
                    font,
                    blueBrush,
                    editButton,
                    format);

                e.Graphics.DrawString(
                    "Delete",
                    font,
                    redBrush,
                    deleteButton,
                    format);
            }

            e.Handled = true;
        }

        private void guna2DataGridView2_CellClick(
            object sender,
            DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0 || e.ColumnIndex < 0)
                return;

            if (OrderInfoView.Columns[e.ColumnIndex].HeaderText != "Action")
                return;

            Rectangle cellRect =
                OrderInfoView.GetCellDisplayRectangle(
                    e.ColumnIndex,
                    e.RowIndex,
                    false);

            Point mousePosition =
                OrderInfoView.PointToClient(Cursor.Position);

            int x = mousePosition.X - cellRect.X;

            int buttonWidth = 55;
            int spacing = 5;

            string orderID =
                OrderInfoView.Rows[e.RowIndex]
                .Cells[0]
                .Value?.ToString();

            if (x >= 0 && x < buttonWidth)
            {
                MessageBox.Show(
                    "View Order: " + orderID,
                    "View Order",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Information);
            }
            else if (
                x >= buttonWidth + spacing &&
                x < (buttonWidth * 2) + spacing)
            {
                MessageBox.Show(
                    "Edit Order: " + orderID,
                    "Edit Order",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Information);
            }
            else if (
                x >= (buttonWidth + spacing) * 2)
            {
                DialogResult result = MessageBox.Show(
                    "Are you sure you want to delete " + orderID + "?",
                    "Delete Order",
                    MessageBoxButtons.YesNo,
                    MessageBoxIcon.Warning);

                if (result == DialogResult.Yes)
                {
                    OrderInfoView.Rows.RemoveAt(e.RowIndex);
                }
            }
        }

        private void guna2DataGridView2_CellContentClick(
            object sender,
            DataGridViewCellEventArgs e)
        {
        }

        private void FormOrder_Load(
            object sender,
            EventArgs e)
        {
            OrderInfoView.ThemeStyle.HeaderStyle.BackColor =
                Color.FromArgb(15, 23, 42);
        }

        private void guna2Button1_Click(
            object sender,
            EventArgs e)
        {
            Form mainForm =
                this.TopLevelControl as Form
                ?? Form.ActiveForm
                ?? this;

            using (Form overlay = new Form())
            {
                overlay.StartPosition = FormStartPosition.Manual;
                overlay.FormBorderStyle = FormBorderStyle.None;
                overlay.Opacity = 0.50d;
                overlay.BackColor = Color.Black;
                overlay.ShowInTaskbar = false;

                overlay.Location =
                    mainForm.PointToScreen(Point.Empty);

                overlay.Size = mainForm.ClientSize;

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