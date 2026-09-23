using System;
using System.Data;
using System.Drawing;
using System.Threading.Tasks;
using System.Windows.Forms;
using InventoryManagementSystem.Repositories;

namespace InventoryManagementSystem.Forms
{
    public partial class FormOrder : Form
    {
        private readonly OrderRepository _orderRepo = new OrderRepository();

        public FormOrder()
        {
            InitializeComponent();

            // Subscribe to the grid paint event for rendering "No orders found" message natively
            OrderView.Paint += OrderView_Paint;
        }

        private async void FormOrder_Load(object sender, EventArgs e)
        {
            ConfigureOrderView();
            PopulateStatusFilter();
            await ApplyFilterAsync();
        }

        private void ConfigureOrderView()
        {
            // 1. Force Guna to use Custom Theme Preset
            OrderView.Theme = Guna.UI2.WinForms.Enums.DataGridViewPresetThemes.Default;

            // Define Custom Colors (Dodger Blue Header Theme: #1E90FF)
            Color headerBg = ColorTranslator.FromHtml("#1E90FF");
            Color headerFg = Color.White;

            // 2. Apply Header Styling
            OrderView.ThemeStyle.HeaderStyle.BackColor = headerBg;
            OrderView.ThemeStyle.HeaderStyle.ForeColor = headerFg;
            OrderView.ThemeStyle.HeaderStyle.Font = new Font("Segoe UI", 11F, FontStyle.Bold);
            OrderView.ThemeStyle.HeaderStyle.BorderStyle = DataGridViewHeaderBorderStyle.None;

            // 3. Prevent Header Selection Highlight
            OrderView.EnableHeadersVisualStyles = false;
            OrderView.ColumnHeadersDefaultCellStyle.SelectionBackColor = headerBg;
            OrderView.ColumnHeadersDefaultCellStyle.SelectionForeColor = headerFg;

            // 4. Apply Row Styling
            OrderView.ThemeStyle.RowsStyle.Font = new Font("Segoe UI", 12F);
            OrderView.ThemeStyle.RowsStyle.ForeColor = Color.FromArgb(51, 65, 85);
            OrderView.ThemeStyle.RowsStyle.SelectionBackColor = Color.FromArgb(212, 230, 254);
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

            // Clear existing columns before binding
            OrderView.Columns.Clear();
        }

        private void PopulateStatusFilter()
        {
            if (cmbStatusFilter != null)
            {
                cmbStatusFilter.Items.Clear();
                cmbStatusFilter.Items.Add("All");
                cmbStatusFilter.Items.Add("Pending");
                cmbStatusFilter.Items.Add("Processing");
                cmbStatusFilter.Items.Add("Completed");
                cmbStatusFilter.Items.Add("Cancelled");
                cmbStatusFilter.SelectedIndex = 0;
            }
        }

        public async Task ApplyFilterAsync()
        {
            try
            {
                string search = txtSearch != null ? txtSearch.Text.Trim() : "";
                string status = cmbStatusFilter?.SelectedItem != null ? cmbStatusFilter.SelectedItem.ToString() : "All";

                DataTable dt = await _orderRepo.GetAllOrdersAsync(search, status);

                OrderView.DataSource = null;
                OrderView.DataSource = dt;

                if (dt != null && dt.Rows.Count > 0)
                {
                    // Configure Grid Headers
                    if (OrderView.Columns["ID"] != null) OrderView.Columns["ID"].HeaderText = "ID";
                    if (OrderView.Columns["OrderNumber"] != null) OrderView.Columns["OrderNumber"].HeaderText = "Order Number";
                    if (OrderView.Columns["CustomerName"] != null) OrderView.Columns["CustomerName"].HeaderText = "Customer Name";
                    if (OrderView.Columns["Status"] != null) OrderView.Columns["Status"].HeaderText = "Status";

                    if (OrderView.Columns["TotalAmount"] != null)
                    {
                        OrderView.Columns["TotalAmount"].HeaderText = "Total Amount";
                        OrderView.Columns["TotalAmount"].DefaultCellStyle.Format = "$#,##0.00";
                    }

                    if (OrderView.Columns["OrderDate"] != null)
                    {
                        OrderView.Columns["OrderDate"].HeaderText = "Order Date";
                        OrderView.Columns["OrderDate"].DefaultCellStyle.Format = "G";
                    }

                    OrderView.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
                    OrderView.ClearSelection();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error fetching orders: {ex.Message}", "Database Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        // Draw centered empty text when DataGridView contains zero records
        private void OrderView_Paint(object sender, PaintEventArgs e)
        {
            if (OrderView.Rows.Count == 0)
            {
                string message = "No orders found";

                // Enable crisp text rendering
                e.Graphics.TextRenderingHint = System.Drawing.Text.TextRenderingHint.ClearTypeGridFit;

                using (Font font = new Font("Segoe UI", 12F, FontStyle.Regular))
                using (SolidBrush brush = new SolidBrush(Color.FromArgb(100, 116, 139)))
                {
                    SizeF textSize = e.Graphics.MeasureString(message, font);
                    float x = (OrderView.Width - textSize.Width) / 2;
                    float y = OrderView.ColumnHeadersHeight + 35;

                    e.Graphics.DrawString(message, font, brush, x, y);
                }
            }
        }

        private async void guna2Button1_Click(object sender, EventArgs e)
        {
            Form mainForm = TopLevelControl as Form ?? Form.ActiveForm ?? this;

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

                using (FormCreateOrder createOrderForm = new FormCreateOrder(async () => await ApplyFilterAsync()))
                {
                    createOrderForm.StartPosition = FormStartPosition.CenterParent;
                    if (createOrderForm.ShowDialog(overlay) == DialogResult.OK)
                    {
                        await ApplyFilterAsync();
                    }
                }
            }
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            if (OrderView.SelectedRows.Count == 0)
            {
                MessageBox.Show("Please select an order to delete.", "Selection Required", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            var selectedRow = OrderView.SelectedRows[0];
            int selectedOrderId = Convert.ToInt32(selectedRow.Cells[OrderView.Columns.Contains("ID") ? "ID" : "Order ID"].Value);
            string orderDisplayName = $"Order #{selectedOrderId}";
            using (var deleteForm = new FormConfirmDelete(selectedOrderId, orderDisplayName, async () =>
            {
                await ApplyFilterAsync();
            }))
            {
                deleteForm.ShowDialog(this);
            }
        }

        private async void btnEdit_Click(object sender, EventArgs e)
        {
            if (OrderView.SelectedRows.Count == 0)
            {
                MessageBox.Show("Please select an order to edit.", "Selection Required", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            int selectedOrderId = Convert.ToInt32(OrderView.SelectedRows[0].Cells["Order ID"].Value);
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

                using (FormCreateOrder editOrderForm = new FormCreateOrder(selectedOrderId, async () => await ApplyFilterAsync()))
                {
                    if (editOrderForm.Controls.Find("lblAddOrder", true).Length > 0)
                        editOrderForm.Controls.Find("lblAddOrder", true)[0].Text = "Update Order";

                    editOrderForm.Text = "Update Order";
                    editOrderForm.StartPosition = FormStartPosition.CenterParent;

                    if (editOrderForm.ShowDialog(overlay) == DialogResult.OK)
                    {
                        await ApplyFilterAsync();
                    }
                }
            }
        }

        private void btnView_Click(object sender, EventArgs e)
        {
            if (OrderView.SelectedRows.Count == 0)
            {
                MessageBox.Show("Please select an order to view.", "Selection Required", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            int selectedOrderId = Convert.ToInt32(OrderView.SelectedRows[0].Cells["Order ID"].Value);
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

                using (FormCreateOrder viewOrderForm = new FormCreateOrder(selectedOrderId, isViewOnly: true))
                {
                    viewOrderForm.StartPosition = FormStartPosition.CenterParent;
                    viewOrderForm.ShowDialog(overlay);
                }
            }
        }

        private async void btnSearch_Click(object sender, EventArgs e)
        {
            await ApplyFilterAsync();
        }

        private async void txtSearch_TextChanged(object sender, EventArgs e)
        {
            await ApplyFilterAsync();
        }

        private async void cmbStatusFilter_SelectedIndexChanged(object sender, EventArgs e)
        {
            await ApplyFilterAsync();
        }

        private void OrderView_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
        {
            if (OrderView.Columns[e.ColumnIndex].Name.Equals("Status", StringComparison.OrdinalIgnoreCase) ||
                OrderView.Columns[e.ColumnIndex].HeaderText.Equals("Status", StringComparison.OrdinalIgnoreCase))
            {
                if (e.Value != null)
                {
                    string status = e.Value.ToString().Trim();

                    e.CellStyle.Font = new Font(OrderView.Font.FontFamily, 11f, FontStyle.Bold);

                    switch (status.ToLower())
                    {
                        case "pending":
                            e.CellStyle.ForeColor = Color.DarkOrange;
                            break;
                        case "processing":
                            e.CellStyle.ForeColor = Color.DodgerBlue;
                            break;
                        case "completed":
                            e.CellStyle.ForeColor = Color.Green;
                            break;
                        case "cancelled":
                        case "canceled":
                            e.CellStyle.ForeColor = Color.Red;
                            break;
                        default:
                            e.CellStyle.ForeColor = Color.DarkGray;
                            break;
                    }
                }
            }
        }
    }
}