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

            SetupActionColumns();

            OrderInfoView.CellFormatting +=
                OrderInfoView_CellFormatting;

            OrderInfoView.CellMouseMove +=
                OrderInfoView_CellMouseMove;

            OrderInfoView.CellContentClick +=
                OrderInfoView_CellContentClick;

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

            dt.Rows.Add(
                "ORD-001",
                "2025-09-01",
                "Sok Dara",
                "$120.00",
                "Pending");

            dt.Rows.Add(
                "ORD-002",
                "2025-08-30",
                "Chan Vibol",
                "$250.00",
                "Paid");

            dt.Rows.Add(
                "ORD-003",
                "2025-08-28",
                "Srey Neang",
                "$80.00",
                "Shipped");

            dt.Rows.Add(
                "ORD-004",
                "2025-08-27",
                "Long Ratha",
                "$175.00",
                "Pending");

            dt.Rows.Add(
                "ORD-005",
                "2025-08-25",
                "Kim Sovan",
                "$320.00",
                "Delivered");

            OrderInfoView.DataSource = dt;

            SetupOrderGrid();
        }

        private void SetupActionColumns()
        {
            if (OrderInfoView.Columns.Contains("Action"))
            {
                OrderInfoView.Columns.Remove("Action");
            }

            if (OrderInfoView.Columns.Contains("View"))
            {
                OrderInfoView.Columns.Remove("View");
            }

            if (OrderInfoView.Columns.Contains("Edit"))
            {
                OrderInfoView.Columns.Remove("Edit");
            }

            if (OrderInfoView.Columns.Contains("Delete"))
            {
                OrderInfoView.Columns.Remove("Delete");
            }

            DataGridViewButtonColumn viewColumn =
                new DataGridViewButtonColumn();

            viewColumn.Name = "View";
            viewColumn.HeaderText = "View";
            viewColumn.Text = "View";
            viewColumn.UseColumnTextForButtonValue = true;
            viewColumn.FlatStyle = FlatStyle.Flat;

            viewColumn.DefaultCellStyle.BackColor =
                Color.FromArgb(235, 245, 255);

            viewColumn.DefaultCellStyle.ForeColor =
                Color.FromArgb(0, 123, 255);

            viewColumn.DefaultCellStyle.Alignment =
                DataGridViewContentAlignment.MiddleCenter;

            OrderInfoView.Columns.Add(viewColumn);

            DataGridViewButtonColumn editColumn =
                new DataGridViewButtonColumn();

            editColumn.Name = "Edit";
            editColumn.HeaderText = "Edit";
            editColumn.Text = "Edit";
            editColumn.UseColumnTextForButtonValue = true;
            editColumn.FlatStyle = FlatStyle.Flat;

            editColumn.DefaultCellStyle.BackColor =
                Color.FromArgb(235, 245, 255);

            editColumn.DefaultCellStyle.ForeColor =
                Color.FromArgb(0, 123, 255);

            editColumn.DefaultCellStyle.Alignment =
                DataGridViewContentAlignment.MiddleCenter;

            OrderInfoView.Columns.Add(editColumn);

            DataGridViewButtonColumn deleteColumn =
                new DataGridViewButtonColumn();

            deleteColumn.Name = "Delete";
            deleteColumn.HeaderText = "Delete";
            deleteColumn.Text = "Delete";
            deleteColumn.UseColumnTextForButtonValue = true;
            deleteColumn.FlatStyle = FlatStyle.Flat;

            deleteColumn.DefaultCellStyle.BackColor =
                Color.FromArgb(255, 235, 235);

            deleteColumn.DefaultCellStyle.ForeColor =
                Color.FromArgb(220, 53, 69);

            deleteColumn.DefaultCellStyle.Alignment =
                DataGridViewContentAlignment.MiddleCenter;

            OrderInfoView.Columns.Add(deleteColumn);
        }

        private void SetupOrderGrid()
        {
            OrderInfoView.ClearSelection();

            OrderInfoView.ColumnHeadersHeightSizeMode =
                DataGridViewColumnHeadersHeightSizeMode.EnableResizing;

            OrderInfoView.ColumnHeadersHeight = 40;

            OrderInfoView.ColumnHeadersDefaultCellStyle.Font =
                new Font("Segoe UI", 11F, FontStyle.Bold);

            OrderInfoView.ColumnHeadersDefaultCellStyle.Alignment =
                DataGridViewContentAlignment.MiddleLeft;

            OrderInfoView.RowTemplate.Height = 35;

            OrderInfoView.DefaultCellStyle.Font =
                new Font("Segoe UI", 10F);

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
                DataGridViewAutoSizeColumnsMode.Fill;

            OrderInfoView.SelectionMode =
                DataGridViewSelectionMode.CellSelect;

            OrderInfoView.MultiSelect = false;

            OrderInfoView.Columns[0].FillWeight = 90;
            OrderInfoView.Columns[1].FillWeight = 100;
            OrderInfoView.Columns[2].FillWeight = 120;
            OrderInfoView.Columns[3].FillWeight = 100;
            OrderInfoView.Columns[4].FillWeight = 100;
            OrderInfoView.Columns[5].FillWeight = 60;
            OrderInfoView.Columns[6].FillWeight = 60;
            OrderInfoView.Columns[7].FillWeight = 70;

            foreach (DataGridViewColumn column
                     in OrderInfoView.Columns)
            {
                column.Resizable =
                    DataGridViewTriState.False;
            }

            OrderInfoView.DefaultCellStyle.SelectionBackColor =
                Color.FromArgb(0, 123, 255);

            OrderInfoView.DefaultCellStyle.SelectionForeColor =
                Color.White;
        }

        private void OrderInfoView_CellFormatting(
            object sender,
            DataGridViewCellFormattingEventArgs e)
        {
            if (e.RowIndex < 0 || e.ColumnIndex != 4)
                return;

            switch (e.Value?.ToString())
            {
                case "Pending":
                    

                    e.CellStyle.ForeColor =
                        Color.FromArgb(133, 100, 4);
                    break;

                case "Paid":
                    

                    e.CellStyle.ForeColor =
                        Color.FromArgb(21, 87, 36);
                    break;

                case "Shipped":
                    

                    e.CellStyle.ForeColor =
                        Color.FromArgb(12, 84, 96);
                    break;

                case "Delivered":
                    

                    e.CellStyle.ForeColor =
                        Color.FromArgb(46, 125, 50);
                    break;
            }

            e.CellStyle.Alignment =
                DataGridViewContentAlignment.MiddleCenter;
        }

        private void OrderInfoView_CellMouseMove(
            object sender,
            DataGridViewCellMouseEventArgs e)
        {
            if (e.ColumnIndex >= 4 && e.ColumnIndex <= 7)
            {
                OrderInfoView.Cursor = Cursors.Hand;
            }
            else
            {
                OrderInfoView.Cursor = Cursors.Default;
            }
        }

        private void OrderInfoView_CellContentClick(
            object sender,
            DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0 || e.ColumnIndex < 5)
                return;

            string orderID =
                OrderInfoView.Rows[e.RowIndex]
                .Cells[0]
                .Value?.ToString();

            string columnName =
                OrderInfoView.Columns[e.ColumnIndex].Name;

            if (columnName == "View")
            {
                MessageBox.Show(
                    "View Order: " + orderID,
                    "View Order",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Information);
            }
            else if (columnName == "Edit")
            {
                MessageBox.Show(
                    "Edit Order: " + orderID,
                    "Edit Order",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Information);
            }
            else if (columnName == "Delete")
            {
                DialogResult result =
                    MessageBox.Show(
                        "Are you sure you want to delete "
                        + orderID + "?",
                        "Delete Order",
                        MessageBoxButtons.YesNo,
                        MessageBoxIcon.Warning);

                if (result == DialogResult.Yes)
                {
                    OrderInfoView.Rows.RemoveAt(e.RowIndex);
                }
            }
        }

        private void FormOrder_Load(
            object sender,
            EventArgs e)
        {
            OrderInfoView.ThemeStyle.HeaderStyle.BackColor =
                Color.FromArgb(15, 23, 42);

            OrderInfoView.ClearSelection();
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