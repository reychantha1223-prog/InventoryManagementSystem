using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace InventoryManagementSystem.Forms
{
    public partial class FormAddSupplier : Form
    {
        public FormAddSupplier()
        {
            InitializeComponent();
        }

        private void FormAddSupplier_Load(object sender, EventArgs e)
        {

        }
        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }
        private bool ValidateInputs()
        {
            bool isValid = true;

            // Reset default label text
            lblNameRequired.Text = "This field is required";
            lblEmailRequired.Text = "This field is required";
            lblContactRequired.Text = "This field is required";
            lblAddRequired.Text = "This field is required";
            lblPhoneRequired.Text = "This field is required";

            // 1. Validate Supplier Name
            if (string.IsNullOrWhiteSpace(txtName.Text))
            {
                lblNameRequired.Visible = true;
                txtName.Focus();
                isValid = false;
            }
            else
            {
                lblNameRequired.Visible = false;
            }

            // 2. Validate Email
            if (string.IsNullOrWhiteSpace(txtEmail.Text))
            {
                lblEmailRequired.Text = "This field is required";
                lblEmailRequired.Visible = true;
                if (isValid) txtEmail.Focus();
                isValid = false;
            }
            else
            {
                lblEmailRequired.Visible = false;
            }

            // 3. Validate Contact Person
            if (string.IsNullOrWhiteSpace(txtContact.Text))
            {
                lblContactRequired.Text = "This field is required";
                lblContactRequired.Visible = true;
                if (isValid) txtContact.Focus();
                isValid = false;
            }
            else
            {
                lblContactRequired.Visible = false;
            }

            // 4. Validate Address
            if (string.IsNullOrWhiteSpace(txtAddress.Text))
            {
                lblAddRequired.Text = "This field is required";
                lblAddRequired.Visible = true;
                if (isValid) txtAddress.Focus();
                isValid = false;
            }
            else
            {
                lblAddRequired.Visible = false;
            }

            // 5. Validate Phone
            if (string.IsNullOrWhiteSpace(txtPhone.Text))
            {
                lblPhoneRequired.Text = "This field is required";
                lblPhoneRequired.Visible = true;
                if (isValid) txtPhone.Focus();
                isValid = false;
            }
            else
            {
                lblPhoneRequired.Visible = false;
            }

            return isValid;
        }
        private void btnSave_Click(object sender, EventArgs e)
        {
            if (!ValidateInputs()) return;
        }

        private void txtName_TextChanged(object sender, EventArgs e)
        {
            if (!string.IsNullOrWhiteSpace(txtName.Text)) lblNameRequired.Visible = false;
        }

        private void txtEmail_TextChanged(object sender, EventArgs e)
        {
            if (!string.IsNullOrWhiteSpace(txtEmail.Text)) lblEmailRequired.Visible = false;
        }

        private void txtContact_TextChanged(object sender, EventArgs e)
        {
            if (!string.IsNullOrWhiteSpace(txtContact.Text)) lblContactRequired.Visible = false;
        }

        private void txtAddress_TextChanged(object sender, EventArgs e)
        {
            if (!string.IsNullOrWhiteSpace(txtAddress.Text)) lblAddRequired.Visible = false;
        }

        private void txtPhone_TextChanged(object sender, EventArgs e)
        {
            if (!string.IsNullOrWhiteSpace(txtPhone.Text)) lblPhoneRequired.Visible = false;
        }
    }
}
