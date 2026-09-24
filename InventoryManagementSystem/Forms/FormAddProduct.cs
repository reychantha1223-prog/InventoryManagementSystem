using InventoryManagementSystem.Models;
using InventoryManagementSystem.Repositories;
using System;
using System.Configuration;
using System.Data;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace InventoryManagementSystem.Forms
{
    public partial class FormAddProduct : Form
    {
        private readonly string connectionString =
            ConfigurationManager.ConnectionStrings["IMSDB"].ConnectionString;
        private readonly ProductRepository _productRepository = new ProductRepository();
        private Product currentProduct = new Product();
        private readonly Action onSaveSuccess;

        public FormAddProduct(Product productToEdit = null, Action onSaveSuccess = null)
        {
            InitializeComponent();

            if (productToEdit != null)
            {
                this.currentProduct = productToEdit;
            }

            this.onSaveSuccess = onSaveSuccess;
        }

        private async void FormAddProduct_Load(object sender, EventArgs e)
        {
            try
            {
                // Configure numPrice to support decimals without auto-rounding
                if (numPrice != null)
                {
                    numPrice.DecimalPlaces = 2;          // Allows decimals like 2.50
                    numPrice.ThousandsSeparator = false; // Prevents formatting rounding on blur
                    numPrice.Increment = 0.01m;          // Decimal step value
                }

                await LoadCategoriesAsync();
                await LoadSuppliersAsync();

                if (currentProduct != null && currentProduct.ID > 0)
                {
                    lblAddProduct.Text = "Update Product";
                    this.Text = "Update Product";

                    txtName.Text = currentProduct.Name ?? string.Empty;
                    numPrice.Value = currentProduct.Price;
                    numStock.Value = currentProduct.Stock;
                    txtDescription.Text = currentProduct.Description ?? string.Empty;

                    if (currentProduct.CategoryID > 0)
                    {
                        cmbCategory.SelectedValue = currentProduct.CategoryID;
                    }

                    if (currentProduct.SupplierID.HasValue && currentProduct.SupplierID.Value > 0)
                    {
                        cmbSupplier.SelectedValue = currentProduct.SupplierID.Value;
                    }
                    else
                    {
                        cmbSupplier.SelectedIndex = 0;
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Initialization Error: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

            txtName.Select();
        }

        private async Task LoadCategoriesAsync()
        {
            DataTable dt = await _productRepository.GetCategoriesAsync();
            cmbCategory.DataSource = dt;
            cmbCategory.DisplayMember = "CategoryName";
            cmbCategory.ValueMember = "CategoryID";
            cmbCategory.SelectedIndex = -1;
        }

        private async Task LoadSuppliersAsync()
        {
            DataTable dt = await _productRepository.GetSuppliersAsync();
            cmbSupplier.DataSource = dt;
            cmbSupplier.DisplayMember = "SupplierName";
            cmbSupplier.ValueMember = "SupplierID";
            cmbSupplier.SelectedIndex = 0;
        }

        private void txtName_TextChanged(object sender, EventArgs e)
        {
            if (!string.IsNullOrWhiteSpace(txtName.Text)) lblNameRequired.Visible = false;
        }

        private void numPrice_ValueChanged(object sender, EventArgs e)
        {
            if (numPrice.Value > 0) lblPriceRequired.Visible = false;
        }

        private void numStock_ValueChanged(object sender, EventArgs e)
        {
            if (numStock.Value > 0) lblStockRequired.Visible = false;
        }

        private async Task<bool> ValidateInputsAsync()
        {
            bool isValid = true;

            // 1. Name Check (Required)
            if (string.IsNullOrWhiteSpace(txtName.Text))
            {
                lblNameRequired.Text = "This field is required";
                lblNameRequired.Visible = true;
                txtName.Focus();
                isValid = false;
            }
            else
            {
                // 2. Name Check (Duplicate in Database)
                int currentProductId = currentProduct?.ID ?? 0;
                bool exists = await _productRepository.IsProductNameExistsAsync(txtName.Text.Trim(), currentProductId);

                if (exists)
                {
                    lblNameRequired.Text = "Product name already exists";
                    lblNameRequired.Visible = true;
                    txtName.Focus();
                    txtName.SelectAll();
                    isValid = false;
                }
                else
                {
                    lblNameRequired.Visible = false;
                }
            }

            if (cmbCategory.SelectedValue == null ||
                cmbCategory.SelectedValue == DBNull.Value ||
                !int.TryParse(cmbCategory.SelectedValue.ToString(), out int categoryId) ||
                categoryId <= 0)
            {
                lblCategoryRequired.Visible = true;
                if (isValid) cmbCategory.Focus();
                isValid = false;
            }
            else
            {
                lblCategoryRequired.Visible = false;
            }

            if (cmbSupplier.SelectedValue == null ||
                cmbSupplier.SelectedValue == DBNull.Value ||
                !int.TryParse(cmbSupplier.SelectedValue.ToString(), out int supplierId) ||
                supplierId <= 0)
            {
                lblSupplierRequired.Visible = true;
                if (isValid) cmbSupplier.Focus();
                isValid = false;
            }
            else
            {
                lblSupplierRequired.Visible = false;
            }

            if (numPrice.Value <= 0)
            {
                lblPriceRequired.Visible = true;
                if (isValid) numPrice.Focus();
                isValid = false;
            }
            else
            {
                lblPriceRequired.Visible = false;
            }

            if (numStock.Value < 0)
            {
                lblStockRequired.Visible = true;
                if (isValid) numStock.Focus();
                isValid = false;
            }
            else
            {
                lblStockRequired.Visible = false;
            }

            return isValid;
        }

        private async void btnSave_Click(object sender, EventArgs e)
        {
            if (!await ValidateInputsAsync()) return;

            btnSave.Enabled = false;

            try
            {
                currentProduct.Name = txtName.Text.Trim();
                currentProduct.CategoryID = Convert.ToInt32(cmbCategory.SelectedValue);

                if (cmbSupplier.SelectedValue != null && cmbSupplier.SelectedValue != DBNull.Value && Convert.ToInt32(cmbSupplier.SelectedValue) > 0)
                {
                    currentProduct.SupplierID = Convert.ToInt32(cmbSupplier.SelectedValue);
                }
                else
                {
                    currentProduct.SupplierID = null;
                }

                currentProduct.Price = numPrice.Value;
                currentProduct.Stock = (int)numStock.Value;
                currentProduct.Description = txtDescription.Text;

                await _productRepository.SaveProductAsync(currentProduct);

                onSaveSuccess?.Invoke();

                this.DialogResult = DialogResult.OK;
                this.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Database error: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                btnSave.Enabled = true;
            }
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void label8_Click(object sender, EventArgs e) { }
    }
}