using System;
using System.Windows.Forms;
using InventoryManagementSystem.Models;
using InventoryManagementSystem.Repositories;

namespace InventoryManagementSystem.Forms
{
    public partial class FormAddCategory : Form
    {
        private readonly ICategoryRepository _categoryRepository;
        private readonly Category _currentCategory;
        private readonly Action _onSaveSuccess;

        public FormAddCategory(ICategoryRepository categoryRepository = null, Category categoryToEdit = null, Action onSaveSuccess = null)
        {
            InitializeComponent();

            string connectionString = "Server=localhost;Database=IMSDB;Trusted_Connection=True;TrustServerCertificate=True;";
            _categoryRepository = categoryRepository ?? new CategoryRepository(connectionString);

            _currentCategory = categoryToEdit ?? new Category();
            _onSaveSuccess = onSaveSuccess;
        }

        private void FormAddCategory_Load(object sender, EventArgs e)
        {
            cmbStatus.Items.Clear();
            cmbStatus.Items.Add("Active");
            cmbStatus.Items.Add("Inactive");

            if (_currentCategory != null && _currentCategory.ID > 0)
            {
                lblAddCategory.Text = "Update Category";
                this.Text = "Update Category";

                txtCategoryName.Text = _currentCategory.Name ?? string.Empty;
                txtDescription.Text = _currentCategory.Description ?? string.Empty;

                if (!string.IsNullOrWhiteSpace(_currentCategory.Status))
                {
                    int statusIndex = cmbStatus.FindStringExact(_currentCategory.Status.Trim());
                    cmbStatus.SelectedIndex = statusIndex >= 0 ? statusIndex : 0;
                }
                else
                {
                    cmbStatus.SelectedIndex = 0;
                }
            }
            else
            {
                lblAddCategory.Text = "Add Category";
                this.Text = "Add Category";
                cmbStatus.SelectedIndex = 0;
            }

            txtCategoryName.Select();
        }

        private bool ValidateInputs()
        {
            bool isValid = true;

            if (string.IsNullOrWhiteSpace(txtCategoryName.Text))
            {
                lblCategoryRequired.Text = "This field is required";
                lblCategoryRequired.Visible = true;
                txtCategoryName.Focus();
                isValid = false;
            }
            else
            {
                lblCategoryRequired.Visible = false;
            }

            if (cmbStatus.SelectedItem == null)
            {
                MessageBox.Show("Please select a status.", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                if (isValid) cmbStatus.Focus();
                isValid = false;
            }

            return isValid;
        }

        private async void btnSaveCategory_Click(object sender, EventArgs e)
        {
            if (!ValidateInputs()) return;

            btnSaveCategory.Enabled = false;

            try
            {
                _currentCategory.Name = txtCategoryName.Text;
                _currentCategory.Description = txtDescription.Text;
                _currentCategory.Status = cmbStatus.SelectedItem.ToString();

                await _categoryRepository.SaveAsync(_currentCategory);

                _onSaveSuccess?.Invoke();

                this.DialogResult = DialogResult.OK;
                this.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Database error: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                btnSaveCategory.Enabled = true;
            }
        }

        private void txtCategoryName_TextChanged(object sender, EventArgs e)
        {
            if (!string.IsNullOrWhiteSpace(txtCategoryName.Text))
            {
                lblCategoryRequired.Visible = false;
            }
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {
        }
    }
}