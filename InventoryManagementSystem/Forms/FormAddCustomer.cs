using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml.Linq;

namespace InventoryManagementSystem.Forms
{
    public partial class FormAddCustomer : Form
    {
        public FormAddCustomer()
        {
            InitializeComponent();
        }

        private void guna2Button2_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void FormAddCustomer_Load(object sender, EventArgs e)
        {

        }
        private bool ValidateInputs()
        {
            bool isValid = true;

            // 1. Validate Name
            if (string.IsNullOrWhiteSpace(txtName.Text))
            {
                lblNameRequired.Text = "This field is required";
                lblNameRequired.Visible = true;
                txtName.Focus();
                isValid = false;
            }

            // 2. Validate Phone
            if (string.IsNullOrWhiteSpace(txtPhone.Text))
            {
                lblPhoneRequired.Text = "This field is required";
                lblPhoneRequired.Visible = true;
                if (isValid) txtPhone.Focus();
                isValid = false;
            }

            // 3. Validate Address
            if (string.IsNullOrWhiteSpace(txtAddress.Text))
            {
                lblAddressRequired.Text = "This field is required";
                lblAddressRequired.Visible = true;
                if (isValid) txtAddress.Focus();
                isValid = false;
            }

            return isValid;
        }
        private void btnSave_Click(object sender, EventArgs e)
        {
            if (!ValidateInputs()) return;
            
        }

        private void txtName_TextChanged(object sender, EventArgs e)
        {
            if (!string.IsNullOrWhiteSpace(txtName.Text))
            {
                lblNameRequired.Visible = false;
            }
        }

        private void txtPhone_TextChanged(object sender, EventArgs e)
        {
            if (!string.IsNullOrWhiteSpace(txtPhone.Text))
            {
                lblPhoneRequired.Visible = false;
            }
        }

        private void txtAddress_TextChanged(object sender, EventArgs e)
        {
            if (!string.IsNullOrWhiteSpace(txtAddress.Text))
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
