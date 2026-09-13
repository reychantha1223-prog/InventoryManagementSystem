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
    public partial class FormAddProduct : Form
    {
        public FormAddProduct()
        {
            InitializeComponent();
        }

        private bool ValidateInputs()
        {
            bool isValid = true;

            // Reset default label text
            lblNameRequired.Text = "This field is required";
            lblPriceRequired.Text = "Must be greater than 0";
            lblStockRequired.Text = "Must be greater than 0";

            // 1. Validate Product Name
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

            // 3. Validate Stock (Check if greater than 0)
            if (numStock.Value <= 0)
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
        private void btnCancel_Click(object sender, EventArgs e)
        {
            
            this.Close();
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            if (!ValidateInputs()) return;

            // Save logic here...
            MessageBox.Show("Product saved successfully!", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void txtName_TextChanged(object sender, EventArgs e)
        {
            if (!string.IsNullOrWhiteSpace(txtName.Text))
                lblNameRequired.Visible = false;
        }

        

        private void FormAddProduct_Load(object sender, EventArgs e)
        {

        }

        private void numPrice_ValueChanged(object sender, EventArgs e)
        {
            if (numPrice.Value > 0) lblPriceRequired.Visible = false;
        }

        private void numStock_ValueChanged(object sender, EventArgs e)
        {
            if (numStock.Value > 0) lblStockRequired.Visible = false;
        }
    }
}
