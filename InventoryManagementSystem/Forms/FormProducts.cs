using System;
using System.Data;
using System.Drawing;
using System.Threading.Tasks;
using System.Windows.Forms;
using InventoryManagementSystem.Models;
using InventoryManagementSystem.Repositories;

namespace InventoryManagementSystem.Forms
{
    public partial class FormProducts : Form
    {
        private readonly ProductRepository _productRepository = new ProductRepository();
        private DataTable productsTable = new DataTable();

        public FormProducts()
        {
            InitializeComponent();
        }

        private async void FormProducts_Load(object sender, EventArgs e)
        {
            ConfigureProductView();

            cmbCategory.Items.Clear();
            cmbCategory.Items.Add("All Category");
            cmbCategory.SelectedIndex = 0;

            cmbStock.Items.Clear();
            cmbStock.Items.AddRange(new object[] { "All Stock", "In Stock", "Low Stock", "Out of Stock" });
            cmbStock.SelectedIndex = 0;

            await LoadCategoriesFilterAsync();
            await LoadProductsDataAsync();
            await LoadProductSummaryCardsAsync();
        }

        private void ConfigureProductView()
        {
            ProductView.AutoGenerateColumns = false;
            ProductView.Paint += ProductView_Paint;

            if (ProductView.Columns.Count >= 8)
            {
                ProductView.Columns[0].DataPropertyName = "ID";
                ProductView.Columns[1].DataPropertyName = "Name";
                ProductView.Columns[2].DataPropertyName = "Category";
                ProductView.Columns[3].DataPropertyName = "Supplier";
                ProductView.Columns[4].DataPropertyName = "Price";
                ProductView.Columns[5].DataPropertyName = "Stock";
                ProductView.Columns[6].DataPropertyName = "Status";
                ProductView.Columns[7].DataPropertyName = "Description";
            }

            ProductView.Theme = Guna.UI2.WinForms.Enums.DataGridViewPresetThemes.Default;
            Color headerBg = ColorTranslator.FromHtml("#1E90FF");
            Color headerFg = Color.White;

            ProductView.ThemeStyle.HeaderStyle.BackColor = headerBg;
            ProductView.ThemeStyle.HeaderStyle.ForeColor = headerFg;
            ProductView.ThemeStyle.HeaderStyle.Font = new Font("Segoe UI", 11F, FontStyle.Bold);
            ProductView.ThemeStyle.HeaderStyle.BorderStyle = DataGridViewHeaderBorderStyle.None;

            ProductView.EnableHeadersVisualStyles = false;
            ProductView.ColumnHeadersDefaultCellStyle.SelectionBackColor = headerBg;
            ProductView.ColumnHeadersDefaultCellStyle.SelectionForeColor = headerFg;

            ProductView.ThemeStyle.RowsStyle.Font = new Font("Segoe UI", 12F);
            ProductView.ThemeStyle.RowsStyle.ForeColor = Color.FromArgb(51, 65, 85);
            ProductView.ThemeStyle.RowsStyle.SelectionBackColor = Color.FromArgb(212, 230, 254);
            ProductView.ThemeStyle.RowsStyle.SelectionForeColor = Color.Black;

            ProductView.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            ProductView.AllowUserToAddRows = false;
            ProductView.AllowUserToResizeColumns = false;
            ProductView.AllowUserToResizeRows = false;
            ProductView.RowHeadersVisible = false;
            ProductView.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.DisableResizing;
            ProductView.ColumnHeadersHeight = 40;
            ProductView.RowTemplate.Height = 42;
        }

        private async Task LoadCategoriesFilterAsync()
        {
            try
            {
                DataTable categories = await _productRepository.GetCategoriesAsync();
                foreach (DataRow row in categories.Rows)
                {
                    cmbCategory.Items.Add(row["CategoryName"].ToString());
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error loading category filters: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        public async Task LoadProductsDataAsync()
        {
            try
            {
                productsTable = await _productRepository.GetProductsDataTableAsync();
                ProductView.DataSource = productsTable;

                if (ProductView.Columns.Contains("CategoryID")) ProductView.Columns["CategoryID"].Visible = false;
                if (ProductView.Columns.Contains("SupplierID")) ProductView.Columns["SupplierID"].Visible = false;
                if (ProductView.Columns.Contains("CreatedAt")) ProductView.Columns["CreatedAt"].Visible = false;

                ProductView.ClearSelection();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error fetching products: {ex.Message}", "Database Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private async Task LoadProductSummaryCardsAsync()
        {
            try
            {
                var (total, inStock, lowStock, outOfStock) = await _productRepository.GetSummaryCardsDataAsync();

                lblTotalProducts.Text = total.ToString();
                lblInStock.Text = inStock.ToString();
                lblLowStock.Text = lowStock.ToString();
                lblOutOfStock.Text = outOfStock.ToString();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error loading dashboard cards: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void ApplyProductFilter()
        {
            if (productsTable == null || productsTable.Rows.Count == 0) return;

            DataView dv = productsTable.DefaultView;
            string filterQuery = "1=1";

            string searchText = txtSearchProduct.Text.Trim().Replace("'", "''");
            if (!string.IsNullOrEmpty(searchText))
            {
                filterQuery += $" AND (Name LIKE '%{searchText}%' OR Description LIKE '%{searchText}%')";
            }

            if (cmbCategory.SelectedIndex > 0 && cmbCategory.SelectedItem != null)
            {
                string selectedCategory = cmbCategory.SelectedItem.ToString().Replace("'", "''");
                filterQuery += $" AND Category = '{selectedCategory}'";
            }

            if (cmbStock.SelectedIndex > 0 && cmbStock.SelectedItem != null)
            {
                string selectedStockOption = cmbStock.SelectedItem.ToString();
                switch (selectedStockOption)
                {
                    case "In Stock":
                        filterQuery += " AND Stock > 5";
                        break;
                    case "Low Stock":
                        filterQuery += " AND Stock > 0 AND Stock <= 5";
                        break;
                    case "Out of Stock":
                        filterQuery += " AND Stock = 0";
                        break;
                }
            }

            dv.RowFilter = filterQuery;
            ProductView.DataSource = dv;
        }

        private void btnAddProduct_Click(object sender, EventArgs e)
        {
            OpenProductDialog(null);
        }

        private void btnEdit_Click(object sender, EventArgs e)
        {
            if (ProductView.SelectedRows.Count == 0)
            {
                MessageBox.Show("Please select a product from the table to edit.", "Selection Required", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            DataGridViewRow row = ProductView.SelectedRows[0];
            Product selectedProduct = new Product();

            if (row.DataBoundItem is DataRowView drv)
            {
                DataRow dr = drv.Row;

                selectedProduct.ID = Convert.ToInt32(dr["ID"]);
                selectedProduct.Name = dr["Name"].ToString();
                selectedProduct.CategoryID = Convert.ToInt32(dr["CategoryID"]);

                if (dr["SupplierID"] != DBNull.Value)
                {
                    selectedProduct.SupplierID = Convert.ToInt32(dr["SupplierID"]);
                }

                selectedProduct.Price = Convert.ToDecimal(dr["Price"]);
                selectedProduct.Stock = Convert.ToInt32(dr["Stock"]);

                // Read description and convert "N/A" back to empty string
                string description = dr["Description"]?.ToString() ?? string.Empty;
                selectedProduct.Description = (description == "N/A") ? string.Empty : description;
            }

            OpenProductDialog(selectedProduct);
        }

        private void OpenProductDialog(Product product)
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

                using (FormAddProduct dialogForm = new FormAddProduct(product, async () => {
                    await LoadProductsDataAsync();
                    await LoadProductSummaryCardsAsync();
                }))
                {
                    dialogForm.StartPosition = FormStartPosition.CenterParent;
                    dialogForm.ShowDialog(overlay);
                }
            }
        }

        private void btnDeleteProduct_Click(object sender, EventArgs e)
        {
            if (ProductView.SelectedRows.Count == 0)
            {
                MessageBox.Show("Please select a product from the table to delete.", "Selection Required", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            DataGridViewRow row = ProductView.SelectedRows[0];
            Product selectedProduct = new Product();

            if (row.DataBoundItem is DataRowView drv)
            {
                DataRow dr = drv.Row;

                selectedProduct.ID = Convert.ToInt32(dr["ID"]);
                selectedProduct.Name = dr["Name"].ToString();
                selectedProduct.CategoryID = Convert.ToInt32(dr["CategoryID"]);

                if (dr["SupplierID"] != DBNull.Value)
                {
                    selectedProduct.SupplierID = Convert.ToInt32(dr["SupplierID"]);
                }

                selectedProduct.Price = Convert.ToDecimal(dr["Price"]);
                selectedProduct.Stock = Convert.ToInt32(dr["Stock"]);
                selectedProduct.Description = dr["Description"]?.ToString() ?? string.Empty;
            }

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

                using (FormConfirmDelete deleteModal = new FormConfirmDelete(selectedProduct, async () => {
                    await LoadProductsDataAsync();
                    await LoadProductSummaryCardsAsync();
                }))
                {
                    deleteModal.StartPosition = FormStartPosition.CenterParent;
                    deleteModal.ShowDialog(overlay);
                }
            }
        }

        private void txtSearchProduct_TextChanged(object sender, EventArgs e) => ApplyProductFilter();
        private void cmbCategory_SelectedIndexChanged(object sender, EventArgs e) => ApplyProductFilter();
        private void cmbStock_SelectedIndexChanged(object sender, EventArgs e) => ApplyProductFilter();
        private void btnSearch_Click(object sender, EventArgs e) => ApplyProductFilter();

        private void ProductView_Paint(object sender, PaintEventArgs e)
        {
            if (ProductView.Rows.Count == 0)
            {
                string message = "No products found";
                e.Graphics.TextRenderingHint = System.Drawing.Text.TextRenderingHint.ClearTypeGridFit;

                using (Font font = new Font("Segoe UI", 12F, FontStyle.Regular))
                {
                    Size textSize = TextRenderer.MeasureText(message, font);
                    int x = (ProductView.Width - textSize.Width) / 2;
                    int y = ProductView.ColumnHeadersHeight + 40;
                    Point point = new Point(x, y);
                    Color textMutedColor = Color.FromArgb(100, 116, 139);

                    TextRenderer.DrawText(e.Graphics, message, font, point, textMutedColor);
                }
            }
        }

        private void ProductView_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
        {
            // 1. Handle N/A formatting for null or empty values across all cells
            if (e.Value == null || e.Value == DBNull.Value || string.IsNullOrWhiteSpace(e.Value.ToString()))
            {
                e.Value = "N/A";
                e.FormattingApplied = true;
                return;
            }

            // 2. Custom color and font styling for the "Status" column
            if (e.RowIndex >= 0 && (ProductView.Columns[e.ColumnIndex].Name.Equals("Status", StringComparison.OrdinalIgnoreCase) ||
                                    ProductView.Columns[e.ColumnIndex].HeaderText.Equals("Status", StringComparison.OrdinalIgnoreCase)))
            {
                string status = e.Value.ToString().Trim();
                Font statusFont = new Font("Segoe UI", 11F, FontStyle.Bold); // Increased font size

                switch (status)
                {
                    case "In Stock":
                    case "InStock":
                    case "Active":
                        Color green = Color.FromArgb(40, 167, 69);
                        e.CellStyle.ForeColor = green;
                        e.CellStyle.SelectionForeColor = green;
                        e.CellStyle.Font = statusFont;
                        break;

                    case "Low Stock":
                    case "LowStock":
                        Color orange = Color.FromArgb(255, 152, 0);
                        e.CellStyle.ForeColor = orange;
                        e.CellStyle.SelectionForeColor = orange;
                        e.CellStyle.Font = statusFont;
                        break;

                    case "Out of Stock":
                    case "OutOfStock":
                        Color red = Color.FromArgb(220, 53, 69);
                        e.CellStyle.ForeColor = red;
                        e.CellStyle.SelectionForeColor = red;
                        e.CellStyle.Font = statusFont;
                        break;

                    case "Inactive":
                        Color gray = Color.FromArgb(108, 117, 125);
                        e.CellStyle.ForeColor = gray;
                        e.CellStyle.SelectionForeColor = gray;
                        e.CellStyle.Font = statusFont;
                        break;
                }
            }
        }
    }
}