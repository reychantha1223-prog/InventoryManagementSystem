using System;
using System.Drawing;
using System.Threading.Tasks;
using System.Windows.Forms;
using InventoryManagementSystem.Models;
using InventoryManagementSystem.Repositories;

namespace InventoryManagementSystem.Forms
{
    public partial class FormAddSupplier : Form
    {
        private readonly SupplierRepository _repository = new SupplierRepository();
        private readonly Supplier _supplierToEdit;
        private readonly Action _onSaveSuccess;

        public FormAddSupplier()
        {
            InitializeComponent();
        }

        public FormAddSupplier(Supplier supplier, Action onSaveSuccess = null) : this()
        {
            this._supplierToEdit = supplier;
            this._onSaveSuccess = onSaveSuccess;
        }

        private void FormAddSupplier_Load(object sender, EventArgs e)
        {
            if (lblEmailRequired != null)
            {
                lblEmailRequired.Visible = false;
            }

            if (_supplierToEdit != null)
            {
                this.Text = "Update Supplier";
                if (lblAddSupplier != null) lblAddSupplier.Text = "Update Supplier";

                txtName.Text = _supplierToEdit.Name ?? string.Empty;
                txtContact.Text = _supplierToEdit.ContactPerson ?? string.Empty;
                txtPhone.Text = _supplierToEdit.Phone ?? string.Empty;
                txtEmail.Text = (_supplierToEdit.Email == "N/A") ? string.Empty : (_supplierToEdit.Email ?? string.Empty);
                txtAddress.Text = _supplierToEdit.Address ?? string.Empty;
            }
            else
            {
                this.Text = "Add Supplier";
                if (lblAddSupplier != null) lblAddSupplier.Text = "Add Supplier";
                if (btnSave != null) btnSave.Text = "Save";
            }
        }

        private bool ValidateInputs()
        {
            bool isValid = true;

            if (lblNameRequired != null) lblNameRequired.Visible = false;
            if (lblContactRequired != null) lblContactRequired.Visible = false;
            if (lblPhoneRequired != null) lblPhoneRequired.Visible = false;
            if (lblEmailRequired != null) lblEmailRequired.Visible = false; 
            if (lblAddRequired != null) lblAddRequired.Visible = false;
            if (string.IsNullOrWhiteSpace(txtName.Text))
            {
                if (lblNameRequired != null)
                {
                    lblNameRequired.Text = "This field is required";
                    lblNameRequired.Visible = true;
                }
                txtName.Focus();
                isValid = false;
            }
            if (string.IsNullOrWhiteSpace(txtContact.Text))
            {
                if (lblContactRequired != null)
                {
                    lblContactRequired.Text = "This field is required";
                    lblContactRequired.Visible = true;
                }
                if (isValid) txtContact.Focus();
                isValid = false;
            }
            if (string.IsNullOrWhiteSpace(txtPhone.Text))
            {
                if (lblPhoneRequired != null)
                {
                    lblPhoneRequired.Text = "This field is required";
                    lblPhoneRequired.Visible = true;
                }
                if (isValid) txtPhone.Focus();
                isValid = false;
            }
            if (string.IsNullOrWhiteSpace(txtAddress.Text))
            {
                if (lblAddRequired != null)
                {
                    lblAddRequired.Text = "This field is required";
                    lblAddRequired.Visible = true;
                }
                if (isValid) txtAddress.Focus();
                isValid = false;
            }

            return isValid;
        }

        private async void btnSave_Click(object sender, EventArgs e)
        {
            if (!ValidateInputs()) return;

            btnSave.Enabled = false;

            try
            {
                Supplier supplierData = new Supplier
                {
                    ID = _supplierToEdit?.ID ?? 0,
                    Name = txtName.Text.Trim(),
                    ContactPerson = txtContact.Text.Trim(),
                    Phone = txtPhone.Text.Trim(),
                    Email = txtEmail.Text.Trim(),
                    Address = txtAddress.Text.Trim()
                };

                if (_supplierToEdit != null && _supplierToEdit.ID > 0)
                {
                    await _repository.UpdateSupplierAsync(supplierData);
                }
                else
                {
                    await _repository.AddSupplierAsync(supplierData);
                }

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
                btnSave.Enabled = true;
            }
        }

        private void txtName_TextChanged(object sender, EventArgs e)
        {
            if (!string.IsNullOrWhiteSpace(txtName.Text) && lblNameRequired != null)
            {
                lblNameRequired.Visible = false;
            }
        }

        private void txtContact_TextChanged(object sender, EventArgs e)
        {
            if (!string.IsNullOrWhiteSpace(txtContact.Text) && lblContactRequired != null)
            {
                lblContactRequired.Visible = false;
            }
        }

        private void txtPhone_TextChanged(object sender, EventArgs e)
        {   
            if (!string.IsNullOrWhiteSpace(txtPhone.Text) && lblPhoneRequired != null)
            {
                lblPhoneRequired.Visible = false;
            }
        }

        private void txtEmail_TextChanged(object sender, EventArgs e)
        {
            if (!string.IsNullOrWhiteSpace(txtEmail.Text) && lblEmailRequired != null)
            {
                lblEmailRequired.Visible = false;
            }
        }

        private void txtAddress_TextChanged(object sender, EventArgs e)
        {
            if (!string.IsNullOrWhiteSpace(txtAddress.Text) && lblAddRequired != null)
            {
                lblAddRequired.Visible = false;
            }
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}