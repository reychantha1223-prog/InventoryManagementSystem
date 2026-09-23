using System;
using System.Drawing;
using System.Threading.Tasks;
using System.Windows.Forms;
using InventoryManagementSystem.Models;
using InventoryManagementSystem.Repositories;

namespace InventoryManagementSystem.Forms
{
    public partial class FormAddCustomer : Form
    {
        private readonly CustomerRepository _repository = new CustomerRepository();
        private readonly Customer _customerToEdit;
        private readonly Action _onSaveSuccess;

        public FormAddCustomer()
        {
            InitializeComponent();
        }

        public FormAddCustomer(Customer customer, Action onSaveSuccess = null) : this()
        {
            this._customerToEdit = customer;
            this._onSaveSuccess = onSaveSuccess;
        }

        private void FormAddCustomer_Load(object sender, EventArgs e)
        {
            this.Text = "Update Customer";
            if (_customerToEdit != null)
            {
                if (lblAddCustomer != null) lblAddCustomer.Text = "Update Customer";
                txtName.Text = _customerToEdit.Name;
                txtPhone.Text = _customerToEdit.Phone;
                txtEmail.Text = _customerToEdit.Email;
                txtAddress.Text = _customerToEdit.Address;
            }
            else
            {
                if (lblAddCustomer != null) lblAddCustomer.Text = "Add Customer";
                btnSave.Text = "Save";
            }
        }

        private bool ValidateInputs()
        {
            bool isValid = true;

            if (lblNameRequired != null) lblNameRequired.Visible = false;
            if (lblPhoneRequired != null) lblPhoneRequired.Visible = false;
            if (lblAddressRequired != null) lblAddressRequired.Visible = false;

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
                if (lblAddressRequired != null)
                {
                    lblAddressRequired.Text = "This field is required";
                    lblAddressRequired.Visible = true;
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
                Customer customerData = new Customer
                {
                    ID = _customerToEdit?.ID ?? 0,
                    Name = txtName.Text.Trim(),
                    Phone = txtPhone.Text.Trim(),
                    Email = txtEmail.Text.Trim(),
                    Address = txtAddress.Text.Trim()
                };

                if (_customerToEdit != null)
                {
                    await _repository.UpdateCustomerAsync(customerData);
                }
                else
                {
                    await _repository.AddCustomerAsync(customerData);
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

        private void txtPhone_TextChanged(object sender, EventArgs e)
        {
            if (!string.IsNullOrWhiteSpace(txtPhone.Text) && lblPhoneRequired != null)
            {
                lblPhoneRequired.Visible = false;
            }
        }

        private void txtAddress_TextChanged(object sender, EventArgs e)
        {
            if (!string.IsNullOrWhiteSpace(txtAddress.Text) && lblAddressRequired != null)
            {
                lblAddressRequired.Visible = false;
            }
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}