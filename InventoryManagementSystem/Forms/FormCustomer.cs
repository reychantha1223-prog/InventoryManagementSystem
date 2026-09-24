using System;
using System.Data;
using System.Drawing;
using System.Threading.Tasks;
using System.Windows.Forms;
using InventoryManagementSystem.Models;
using InventoryManagementSystem.Repositories;

namespace InventoryManagementSystem.Forms
{
    public partial class FormCustomer : Form
    {
        private readonly CustomerRepository _repository = new CustomerRepository();
        private DataTable customersTable = new DataTable();

        public FormCustomer()
        {
            InitializeComponent();
        }

        private async void FormCustomer_Load(object sender, EventArgs e)
        {
            // 1. Populate ComboBox items first
            cmbSortCustomer.Items.Clear();
            cmbSortCustomer.Items.Add("Default (Newest)");
            cmbSortCustomer.Items.Add("Name (A - Z)");
            cmbSortCustomer.Items.Add("Name (Z - A)");

            // 2. Set the default selected index (now index 0 exists!)
            cmbSortCustomer.SelectedIndex = 0;
            ConfigureCustomerView();
            await LoadCustomersDataAsync();
        }

        private void ConfigureCustomerView()
        {
            CustomerView.AutoGenerateColumns = false;

            // Map DataGridView columns to SQL SELECT Aliases
            if (CustomerView.Columns.Count >= 5)
            {
                CustomerView.Columns[0].DataPropertyName = "ID";
                CustomerView.Columns[1].DataPropertyName = "Name";
                CustomerView.Columns[2].DataPropertyName = "Phone";
                CustomerView.Columns[3].DataPropertyName = "Email";
                CustomerView.Columns[4].DataPropertyName = "Address";
            }

            // Guna DataGridView Styling
            CustomerView.Theme = Guna.UI2.WinForms.Enums.DataGridViewPresetThemes.Default;
            Color headerBg = ColorTranslator.FromHtml("#1E90FF");
            Color headerFg = Color.White;

            CustomerView.ThemeStyle.HeaderStyle.BackColor = headerBg;
            CustomerView.ThemeStyle.HeaderStyle.ForeColor = headerFg;
            CustomerView.ThemeStyle.HeaderStyle.Font = new Font("Segoe UI", 11F, FontStyle.Bold);
            CustomerView.ThemeStyle.HeaderStyle.BorderStyle = DataGridViewHeaderBorderStyle.None;

            CustomerView.EnableHeadersVisualStyles = false;
            CustomerView.ColumnHeadersDefaultCellStyle.SelectionBackColor = headerBg;
            CustomerView.ColumnHeadersDefaultCellStyle.SelectionForeColor = headerFg;

            CustomerView.ThemeStyle.RowsStyle.Font = new Font("Segoe UI", 12F);
            CustomerView.ThemeStyle.RowsStyle.ForeColor = Color.FromArgb(51, 65, 85);
            CustomerView.ThemeStyle.RowsStyle.SelectionBackColor = Color.FromArgb(212, 230, 254);
            CustomerView.ThemeStyle.RowsStyle.SelectionForeColor = Color.Black;

            CustomerView.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            CustomerView.AllowUserToAddRows = false;
            CustomerView.AllowUserToResizeColumns = false;
            CustomerView.AllowUserToResizeRows = false;
            CustomerView.RowHeadersVisible = false;
            CustomerView.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.DisableResizing;
            CustomerView.ColumnHeadersHeight = 40;
            CustomerView.RowTemplate.Height = 42;

            // Empty state custom painter
            CustomerView.Paint += CustomerView_Paint;
        }

        public async Task LoadCustomersDataAsync()
        {
            try
            {
                // Delegation to CustomerRepository (OOP - Separation of Concerns)
                customersTable = await _repository.GetAllCustomersAsync();
                CustomerView.DataSource = customersTable;
                CustomerView.ClearSelection();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error loading customers: {ex.Message}", "Database Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        private void ApplyCustomerFilterAtoZ()
        {
            if (customersTable == null || customersTable.Rows.Count == 0) return;

            DataView dv = customersTable.DefaultView;
            string searchText = txtSearchCustomer.Text.Trim().Replace("'", "''");

            // 1. Apply Search Filter
            if (!string.IsNullOrEmpty(searchText))
            {
                dv.RowFilter = $"Name LIKE '%{searchText}%' OR Phone LIKE '%{searchText}%' OR Email LIKE '%{searchText}%' OR Address LIKE '%{searchText}%'";
            }
            else
            {
                dv.RowFilter = string.Empty;
            }

            // 2. Apply Sorting
            if (cmbSortCustomer != null && cmbSortCustomer.SelectedItem != null)
            {
                string selectedSort = cmbSortCustomer.SelectedItem.ToString();

                switch (selectedSort)
                {
                    case "Name (A - Z)":
                        dv.Sort = "Name ASC";
                        break;
                    case "Name (Z - A)":
                        dv.Sort = "Name DESC";
                        break;
                    default:
                        dv.Sort = "ID DESC"; // Default: latest added customers first
                        break;
                }
            }

            CustomerView.DataSource = dv;
        }
        private void ApplyCustomerFilter()
        {
            if (customersTable == null || customersTable.Rows.Count == 0) return;

            DataView dv = customersTable.DefaultView;
            string searchText = txtSearchCustomer.Text.Trim().Replace("'", "''");

            if (!string.IsNullOrEmpty(searchText))
            {
                dv.RowFilter = $"Name LIKE '%{searchText}%' OR Phone LIKE '%{searchText}%' OR Email LIKE '%{searchText}%' OR Address LIKE '%{searchText}%'";
            }
            else
            {
                dv.RowFilter = string.Empty;
            }

            CustomerView.DataSource = dv;
        }

        private void CustomerView_Paint(object sender, PaintEventArgs e)
        {
            if (CustomerView.Rows.Count == 0)
            {
                string message = "No customers found";

                e.Graphics.TextRenderingHint = System.Drawing.Text.TextRenderingHint.ClearTypeGridFit;

                using (Font font = new Font("Segoe UI", 12F, FontStyle.Regular))
                {
                    Size textSize = TextRenderer.MeasureText(message, font);
                    int x = (CustomerView.Width - textSize.Width) / 2;
                    int y = CustomerView.ColumnHeadersHeight + 40;

                    Point point = new Point(x, y);
                    Color textMutedColor = Color.FromArgb(100, 116, 139);

                    TextRenderer.DrawText(e.Graphics, message, font, point, textMutedColor);
                }
            }
        }

        private void txtSearchCustomer_TextChanged(object sender, EventArgs e)
        {
            ApplyCustomerFilter();
        }

        private void btnSearch_Click(object sender, EventArgs e)
        {
            ApplyCustomerFilter();
        }

        private void btnAddCustomer_Click(object sender, EventArgs e)
        {
            OpenCustomerDialog(null);
        }

        private void btnEdit_Click(object sender, EventArgs e)
        {
            if (CustomerView.SelectedRows.Count == 0)
            {
                MessageBox.Show("Please select a customer from the list to edit.", "Selection Required", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            DataGridViewRow row = CustomerView.SelectedRows[0];
            Customer selectedCustomer = ExtractCustomerFromRow(row);

            OpenCustomerDialog(selectedCustomer);
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            if (CustomerView.SelectedRows.Count == 0)
            {
                MessageBox.Show("Please select a customer from the list to delete.", "Selection Required", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            DataGridViewRow row = CustomerView.SelectedRows[0];
            Customer selectedCustomer = ExtractCustomerFromRow(row);

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

                using (FormConfirmDelete deleteModal = new FormConfirmDelete(selectedCustomer, async () => await LoadCustomersDataAsync()))
                {
                    deleteModal.StartPosition = FormStartPosition.CenterParent;
                    deleteModal.ShowDialog(overlay);
                }
            }
        }

        private void OpenCustomerDialog(Customer customer)
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

                using (FormAddCustomer dialogForm = new FormAddCustomer(customer, async () => await LoadCustomersDataAsync()))
                {
                    dialogForm.StartPosition = FormStartPosition.CenterParent;
                    dialogForm.ShowDialog(overlay);
                }
            }
        }

        private Customer ExtractCustomerFromRow(DataGridViewRow row)
        {
            Customer cust = new Customer();

            if (row.DataBoundItem is DataRowView drv)
            {
                DataRow dr = drv.Row;

                // Local helper function to clean up "N/A" and nulls
                string Sanitize(object rawValue)
                {
                    string value = rawValue?.ToString()?.Trim();
                    return (string.IsNullOrEmpty(value) || value == "N/A") ? string.Empty : value;
                }

                cust.ID = Convert.ToInt32(dr["ID"]);
                cust.Name = dr["Name"]?.ToString() ?? string.Empty;
                cust.Phone = Sanitize(dr["Phone"]);
                cust.Email = Sanitize(dr["Email"]);
                cust.Address = Sanitize(dr["Address"]);
            }

            return cust;
        }
        private void cmbSortCustomer_SelectedIndexChanged(object sender, EventArgs e)
        {
            ApplyCustomerFilterAtoZ();
        }

        private void label4_Click(object sender, EventArgs e)
        {

        }

        private void CustomerView_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {

        }

        private void guna2ContainerControl5_Click(object sender, EventArgs e)
        {

        }

        private void label3_Click(object sender, EventArgs e)
        {

        }
    }
}