using InventoryManagementSystem.Models;
using InventoryManagementSystem.Repositories;
using System;
using System.Configuration;
using System.Data;
using System.Drawing;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace InventoryManagementSystem.Forms
{
    public partial class FormCategory : Form
    {
        private readonly string connectionString =
            ConfigurationManager.ConnectionStrings["IMSDB"].ConnectionString;
        private readonly ICategoryRepository _categoryRepository;

        public FormCategory(ICategoryRepository categoryRepository = null)
        {
            InitializeComponent();

            // Default fallback if direct parameter isn't supplied (e.g., WinForms Designer)
            _categoryRepository = categoryRepository ?? new CategoryRepository();
        }

        private async void FormCategory_Load(object sender, EventArgs e)
        {
            ConfigureCategoryView();

            // Register formatting event to replace blank cells with "N/A"
            CategoryView.CellFormatting += CategoryView_CellFormatting;

            cmbStatusFilter.Items.Clear();
            cmbStatusFilter.Items.Add("All Status");
            cmbStatusFilter.Items.Add("Active");
            cmbStatusFilter.Items.Add("Inactive");
            cmbStatusFilter.SelectedIndex = 0;

            await RefreshDashboardAsync();
        }

        private async Task RefreshDashboardAsync()
        {
            await Task.WhenAll(LoadCategoriesDataAsync(), UpdateCategoryMetricsAsync());
        }

        private void ConfigureCategoryView()
        {
            CategoryView.AutoGenerateColumns = false;
            CategoryView.Theme = Guna.UI2.WinForms.Enums.DataGridViewPresetThemes.Default;

            Color headerBg = ColorTranslator.FromHtml("#1E90FF");
            Color headerFg = Color.White;

            CategoryView.ThemeStyle.HeaderStyle.BackColor = headerBg;
            CategoryView.ThemeStyle.HeaderStyle.ForeColor = headerFg;
            CategoryView.ThemeStyle.HeaderStyle.Font = new Font("Segoe UI", 11F, FontStyle.Bold);
            CategoryView.ThemeStyle.HeaderStyle.BorderStyle = DataGridViewHeaderBorderStyle.None;

            CategoryView.EnableHeadersVisualStyles = false;
            CategoryView.ColumnHeadersDefaultCellStyle.SelectionBackColor = headerBg;
            CategoryView.ColumnHeadersDefaultCellStyle.SelectionForeColor = headerFg;

            CategoryView.ThemeStyle.RowsStyle.Font = new Font("Segoe UI", 12F);
            CategoryView.ThemeStyle.RowsStyle.ForeColor = Color.FromArgb(51, 65, 85);
            CategoryView.ThemeStyle.RowsStyle.SelectionBackColor = Color.FromArgb(212, 230, 254);
            CategoryView.ThemeStyle.RowsStyle.SelectionForeColor = Color.Black;

            CategoryView.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            CategoryView.AllowUserToAddRows = false;
            CategoryView.AllowUserToResizeColumns = false;
            CategoryView.AllowUserToResizeRows = false;
            CategoryView.RowHeadersVisible = false;
            CategoryView.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.DisableResizing;
            CategoryView.ColumnHeadersHeight = 40;
            CategoryView.RowTemplate.Height = 42;
        }

        // Event handler to automatically display "N/A" for any empty or null cell value
        private void CategoryView_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
        {
            // 1. Format empty or null values as "N/A"
            if (e.Value == null || e.Value == DBNull.Value || string.IsNullOrWhiteSpace(e.Value.ToString()))
            {
                e.Value = "N/A";
                e.FormattingApplied = true;
                return;
            }

            if (e.RowIndex >= 0 && CategoryView.Columns[e.ColumnIndex].Name.Equals("Status", StringComparison.OrdinalIgnoreCase))
            {
                string status = e.Value.ToString().Trim();

                if (status.Equals("Active", StringComparison.OrdinalIgnoreCase))
                {
                    e.CellStyle.ForeColor = Color.FromArgb(46, 125, 50); // Green
                    e.CellStyle.SelectionForeColor = Color.FromArgb(46, 125, 50);
                    e.CellStyle.Font = new Font("Segoe UI", 11F, FontStyle.Bold); 
                }
                else if (status.Equals("Inactive", StringComparison.OrdinalIgnoreCase))
                {
                    e.CellStyle.ForeColor = Color.FromArgb(211, 47, 47); // Red
                    e.CellStyle.SelectionForeColor = Color.FromArgb(211, 47, 47);
                    e.CellStyle.Font = new Font("Segoe UI", 11F, FontStyle.Bold); 
                }
            }
        }

        public async Task LoadCategoriesDataAsync()
        {
            try
            {
                DataTable dt = await _categoryRepository.GetAllCategoriesDataTableAsync();
                CategoryView.DataSource = dt;
                CategoryView.ClearSelection();
                ApplyGridFilter();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error fetching categories: {ex.Message}", "Database Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        public async Task UpdateCategoryMetricsAsync()
        {
            try
            {
                CategoryMetrics metrics = await _categoryRepository.GetCategoryMetricsAsync();
                lblTotalCategories.Text = metrics.TotalCategories.ToString();
                lblActiveCategories.Text = metrics.ActiveCategories.ToString();
                lblInactiveCategories.Text = metrics.InactiveCategories.ToString();
                lblEmptyCategories.Text = metrics.EmptyCategories.ToString();
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine($"Error loading metrics: {ex.Message}");
            }
        }

        private void btnAddCategory_Click(object sender, EventArgs e)
        {
            ShowModalWithOverlay(new FormAddCategory(_categoryRepository, null, async () => await RefreshDashboardAsync()));
        }

        private void btnEdit_Click(object sender, EventArgs e)
        {
            if (CategoryView.SelectedRows.Count == 0)
            {
                MessageBox.Show("Please select a category from the table to edit.", "Selection Required", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            DataGridViewRow row = CategoryView.SelectedRows[0];
            Category selectedCategory = ExtractCategoryFromRow(row);

            ShowModalWithOverlay(new FormAddCategory(_categoryRepository, selectedCategory, async () => await RefreshDashboardAsync()));
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            if (CategoryView.SelectedRows.Count == 0)
            {
                MessageBox.Show("Please select a category from the table to delete.", "Selection Required", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            DataGridViewRow row = CategoryView.SelectedRows[0];
            Category selectedCategory = ExtractCategoryFromRow(row);

            ShowModalWithOverlay(new FormConfirmDelete(selectedCategory, async () => await RefreshDashboardAsync()));
        }

        private Category ExtractCategoryFromRow(DataGridViewRow row)
        {
            if (row.DataBoundItem is DataRowView drv)
            {
                DataRow dr = drv.Row;

                string descColumn = dr.Table.Columns.Contains("Descriptions") ? "Descriptions" : "Description";
                string rawDesc = dr.Table.Columns.Contains(descColumn) ? dr[descColumn]?.ToString() : string.Empty;

                return new Category
                {
                    ID = Convert.ToInt32(dr["ID"]),
                    Name = dr["Name"]?.ToString() ?? string.Empty,
                    Description = (string.IsNullOrWhiteSpace(rawDesc) || rawDesc == "N/A") ? string.Empty : rawDesc,
                    Status = dr["Status"]?.ToString() ?? "Active"
                };
            }

            // 2. Fallback method: Safe DataGridView cell extraction
            object GetCellValue(string columnName)
            {
                return row.DataGridView.Columns.Contains(columnName) ? row.Cells[columnName].Value : null;
            }

            string descriptionValue = Convert.ToString(GetCellValue("Descriptions") ?? GetCellValue("Description"));

            return new Category
            {
                ID = Convert.ToInt32(GetCellValue("ID") ?? row.Cells[0].Value),
                Name = Convert.ToString(GetCellValue("Name") ?? GetCellValue("colName")),
                Description = (string.IsNullOrWhiteSpace(descriptionValue) || descriptionValue == "N/A") ? string.Empty : descriptionValue,
                Status = Convert.ToString(GetCellValue("Status"))
            };
        }

        private void ShowModalWithOverlay(Form modalForm)
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

                using (modalForm)
                {
                    modalForm.StartPosition = FormStartPosition.CenterParent;
                    modalForm.ShowDialog(overlay);
                }
            }
        }

        private void ApplyGridFilter()
        {
            if (CategoryView.DataSource is DataTable dt)
            {
                string searchText = EscapeLikeValue(txtSearchCategory.Text.Trim());
                string selectedStatus = cmbStatusFilter.SelectedItem?.ToString() ?? "All Status";

                string filter = "";

                if (!string.IsNullOrEmpty(searchText))
                {
                    // Check if column is named Descriptions or Description
                    string descColumn = dt.Columns.Contains("Descriptions") ? "Descriptions" : "Description";
                    filter += $"(Name LIKE '%{searchText}%' OR {descColumn} LIKE '%{searchText}%')";
                }

                if (selectedStatus != "All Status")
                {
                    if (!string.IsNullOrEmpty(filter)) filter += " AND ";
                    filter += $"Status = '{selectedStatus}'";
                }

                dt.DefaultView.RowFilter = filter;
            }
        }

        private string EscapeLikeValue(string value)
        {
            return value.Replace("'", "''")
                        .Replace("[", "[[]")
                        .Replace("%", "[%]")
                        .Replace("*", "[*]");
        }

        private void txtSearchCategory_TextChanged(object sender, EventArgs e) => ApplyGridFilter();

        private void cmbStatusFilter_SelectedIndexChanged(object sender, EventArgs e) => ApplyGridFilter();

        private void CategoryView_Paint(object sender, PaintEventArgs e)
        {
            if (CategoryView.Rows.Count == 0)
            {
                string message = "No categories found";
                e.Graphics.TextRenderingHint = System.Drawing.Text.TextRenderingHint.ClearTypeGridFit;

                using (Font font = new Font("Segoe UI", 12F, FontStyle.Regular))
                {
                    Size textSize = TextRenderer.MeasureText(message, font);
                    int x = (CategoryView.Width - textSize.Width) / 2;
                    int y = CategoryView.ColumnHeadersHeight + 35;

                    TextRenderer.DrawText(e.Graphics, message, font, new Point(x, y), Color.Black);
                }
            }
        }

        private void btnSearch_Click(object sender, EventArgs e) => txtSearchCategory.Select();

        private void CategoryView_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
        }
    }
}