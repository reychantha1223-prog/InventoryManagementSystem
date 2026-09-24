using System;
using System.Data;
using System.Drawing;
using System.Threading.Tasks;
using System.Windows.Forms;
using InventoryManagementSystem.Models;
using InventoryManagementSystem.Repositories;

namespace InventoryManagementSystem.Forms
{
    public partial class FormSuppliers : Form
    {
        private readonly SupplierRepository _repository = new SupplierRepository();
        private DataTable suppliersTable = new DataTable();

        public FormSuppliers()
        {
            InitializeComponent();
        }

        private async void FormSuppliers_Load(object sender, EventArgs e)
        {
            if (cmbSortSupplier != null)
            {
                cmbSortSupplier.Items.Clear();
                cmbSortSupplier.Items.Add("Default (Newest)");
                cmbSortSupplier.Items.Add("Name (A - Z)");
                cmbSortSupplier.Items.Add("Name (Z - A)");
                cmbSortSupplier.SelectedIndex = 0;
            }

            ConfigureSupplierView();
            await LoadSuppliersDataAsync();
        }

        private void ConfigureSupplierView()
        {
            SupplierView.AutoGenerateColumns = false;

            if (SupplierView.Columns.Count >= 6)
            {
                SupplierView.Columns[0].DataPropertyName = "ID";
                SupplierView.Columns[1].DataPropertyName = "Name";
                SupplierView.Columns[2].DataPropertyName = "ContactPerson";
                SupplierView.Columns[3].DataPropertyName = "Phone";
                SupplierView.Columns[4].DataPropertyName = "Email";
                SupplierView.Columns[5].DataPropertyName = "Address";
            }

            SupplierView.Theme = Guna.UI2.WinForms.Enums.DataGridViewPresetThemes.Default;
            Color headerBg = ColorTranslator.FromHtml("#1E90FF");
            Color headerFg = Color.White;

            SupplierView.ThemeStyle.HeaderStyle.BackColor = headerBg;
            SupplierView.ThemeStyle.HeaderStyle.ForeColor = headerFg;
            SupplierView.ThemeStyle.HeaderStyle.Font = new Font("Segoe UI", 11F, FontStyle.Bold);
            SupplierView.ThemeStyle.HeaderStyle.BorderStyle = DataGridViewHeaderBorderStyle.None;

            SupplierView.EnableHeadersVisualStyles = false;
            SupplierView.ColumnHeadersDefaultCellStyle.SelectionBackColor = headerBg;
            SupplierView.ColumnHeadersDefaultCellStyle.SelectionForeColor = headerFg;

            SupplierView.ThemeStyle.RowsStyle.Font = new Font("Segoe UI", 12F);
            SupplierView.ThemeStyle.RowsStyle.ForeColor = Color.FromArgb(51, 65, 85);
            SupplierView.ThemeStyle.RowsStyle.SelectionBackColor = Color.FromArgb(212, 230, 254);
            SupplierView.ThemeStyle.RowsStyle.SelectionForeColor = Color.Black;

            SupplierView.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            SupplierView.AllowUserToAddRows = false;
            SupplierView.AllowUserToResizeColumns = false;
            SupplierView.AllowUserToResizeRows = false;
            SupplierView.RowHeadersVisible = false;
            SupplierView.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.DisableResizing;
            SupplierView.ColumnHeadersHeight = 40;
            SupplierView.RowTemplate.Height = 42;

            SupplierView.Paint += SupplierView_Paint;
        }

        public async Task LoadSuppliersDataAsync()
        {
            try
            {
                suppliersTable = await _repository.GetAllSuppliersAsync();
                ApplySupplierFilter();
                SupplierView.ClearSelection();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error loading suppliers: {ex.Message}", "Database Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void ApplySupplierFilter()
        {
            if (suppliersTable == null || suppliersTable.Rows.Count == 0) return;

            DataView dv = suppliersTable.DefaultView;
            string searchText = txtSearchSupplier != null ? txtSearchSupplier.Text.Trim().Replace("'", "''") : string.Empty;

            if (!string.IsNullOrEmpty(searchText))
            {
                dv.RowFilter = $"Name LIKE '%{searchText}%' OR Phone LIKE '%{searchText}%' OR Email LIKE '%{searchText}%' OR Address LIKE '%{searchText}%'";
            }
            else
            {
                dv.RowFilter = string.Empty;
            }

            if (cmbSortSupplier?.SelectedItem != null)
            {
                string selectedSort = cmbSortSupplier.SelectedItem.ToString();
                switch (selectedSort)
                {
                    case "Name (A - Z)":
                        dv.Sort = "Name ASC";
                        break;
                    case "Name (Z - A)":
                        dv.Sort = "Name DESC";
                        break;
                    default:
                        dv.Sort = "ID DESC";
                        break;
                }
            }

            SupplierView.DataSource = dv;
        }

        private void SupplierView_Paint(object sender, PaintEventArgs e)
        {
            if (SupplierView.Rows.Count == 0)
            {
                string message = "No suppliers found";
                e.Graphics.TextRenderingHint = System.Drawing.Text.TextRenderingHint.ClearTypeGridFit;

                using (Font font = new Font("Segoe UI", 12F, FontStyle.Regular))
                {
                    Size textSize = TextRenderer.MeasureText(message, font);
                    int x = (SupplierView.Width - textSize.Width) / 2;
                    int y = SupplierView.ColumnHeadersHeight + 40;

                    Point point = new Point(x, y);
                    Color textMutedColor = Color.FromArgb(100, 116, 139);

                    TextRenderer.DrawText(e.Graphics, message, font, point, textMutedColor);
                }
            }
        }

        private void txtSearchSupplier_TextChanged(object sender, EventArgs e)
        {
            ApplySupplierFilter();
        }

        private void btnSearch_Click(object sender, EventArgs e)
        {
            ApplySupplierFilter();
        }

        private void cmbSortSupplier_SelectedIndexChanged(object sender, EventArgs e)
        {
            ApplySupplierFilter();
        }

        private void btnAddSupplier_Click(object sender, EventArgs e)
        {
            OpenSupplierDialog(null);
        }

        private void btnEdit_Click(object sender, EventArgs e)
        {
            if (SupplierView.SelectedRows.Count == 0)
            {
                MessageBox.Show("Please select a supplier from the list to edit.", "Selection Required", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            DataGridViewRow row = SupplierView.SelectedRows[0];
            Supplier selectedSupplier = ExtractSupplierFromRow(row);
            OpenSupplierDialog(selectedSupplier);
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            if (SupplierView.SelectedRows.Count == 0)
            {
                MessageBox.Show("Please select a supplier from the list to delete.", "Selection Required", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            DataGridViewRow row = SupplierView.SelectedRows[0];
            Supplier selectedSupplier = ExtractSupplierFromRow(row);

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

                using (FormConfirmDelete deleteModal = new FormConfirmDelete(selectedSupplier, async () => await LoadSuppliersDataAsync()))
                {
                    deleteModal.StartPosition = FormStartPosition.CenterParent;
                    deleteModal.ShowDialog(overlay);
                }
            }
        }

        private void OpenSupplierDialog(Supplier supplier)
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
                using (FormAddSupplier dialogForm = new FormAddSupplier(supplier, async () => await LoadSuppliersDataAsync()))
                {
                    dialogForm.StartPosition = FormStartPosition.CenterParent;
                    dialogForm.ShowDialog(overlay);
                }
            }
        }

        private Supplier ExtractSupplierFromRow(DataGridViewRow row)
        {
            Supplier supp = new Supplier();
            string Sanitize(object rawValue)
            {
                string value = rawValue?.ToString()?.Trim();
                return (string.IsNullOrEmpty(value) || value == "N/A") ? string.Empty : value;
            }

            if (row.DataBoundItem is DataRowView drv)
            {
                DataRow dr = drv.Row;

                supp.ID = dr.Table.Columns.Contains("ID") ? Convert.ToInt32(dr["ID"]) : Convert.ToInt32(row.Cells[0].Value);
                supp.Name = dr.Table.Columns.Contains("Name") ? dr["Name"]?.ToString() : row.Cells[1].Value?.ToString();
                supp.ContactPerson = Sanitize(dr.Table.Columns.Contains("ContactPerson") ? dr["ContactPerson"] : (dr.Table.Columns.Contains("Contact_Person") ? dr["Contact_Person"] : row.Cells[2].Value));
                supp.Phone = Sanitize(dr.Table.Columns.Contains("Phone") ? dr["Phone"] : row.Cells[3].Value);
                supp.Email = Sanitize(dr.Table.Columns.Contains("Email") ? dr["Email"] : row.Cells[4].Value);
                supp.Address = Sanitize(dr.Table.Columns.Contains("Address") ? dr["Address"] : row.Cells[5].Value);
            }
            else
            {
                supp.ID = Convert.ToInt32(row.Cells[0].Value);
                supp.Name = Convert.ToString(row.Cells[1].Value);
                supp.ContactPerson = Sanitize(row.Cells[2].Value);
                supp.Phone = Sanitize(row.Cells[3].Value);
                supp.Email = Sanitize(row.Cells[4].Value);
                supp.Address = Sanitize(row.Cells[5].Value);
            }

            return supp;
        }

        private void SupplierView_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }
    }
}