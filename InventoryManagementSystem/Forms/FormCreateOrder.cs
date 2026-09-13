using System;
using System.Drawing;
using System.Windows.Forms;

namespace InventoryManagementSystem.Forms
{
    public partial class FormCreateOrder : Form
    {
        public FormCreateOrder()
        {
            InitializeComponent();

            SetupForm();
        }

        private void SetupForm()
        {
            guna2DateTimePicker1.Value = DateTime.Now;
            guna2DateTimePicker1.FillColor = Color.White;
            guna2DateTimePicker1.ForeColor = Color.Black;
            guna2DateTimePicker1.BorderColor = Color.LightGray;

            guna2TextBox1.Text = "$0.00";
            guna2TextBox1.ReadOnly = true;
        }

        private void guna2Button2_Click(object sender, EventArgs e)
        {
            DateTime orderDate = guna2DateTimePicker1.Value;
            string total = guna2TextBox1.Text;

            MessageBox.Show(
                "Order created successfully!\n\n" +
                $"Date: {orderDate:yyyy-MM-dd}\n" +
                $"Total: {total}",
                "Create Order",
                MessageBoxButtons.OK,
                MessageBoxIcon.Information);

            DialogResult = DialogResult.OK;
            Close();
        }

        private void guna2Button1_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.Cancel;
            Close();
        }

        private void guna2Button2_Click_1(object sender, EventArgs e)
        {
            this.Close();
        }

        private void FormCreateOrder_Load(object sender, EventArgs e)
        {
            this.Size = new Size(this.Width, 760); 
            // 1. Force main background to White
            guna2DateTimePicker1.FillColor = Color.White;

            // 2. Force Checked state background to White (Prevents Guna runtime gray override)
            guna2DateTimePicker1.CheckedState.FillColor = Color.White;

            // 3. Keep text and icon visible on white background
            guna2DateTimePicker1.ForeColor = Color.Black;
            guna2DateTimePicker1.CheckedState.ForeColor = Color.Black;

            // 4. Add a clean border to match your other textboxes
            guna2DateTimePicker1.BorderColor = Color.FromArgb(217, 221, 226);
            guna2DateTimePicker1.BorderThickness = 1;
            guna2DateTimePicker1.BorderRadius = 5;
        }
        private bool ValidateInputs()
        {
            bool isValid = true;

            // 1. Validate Customer
            if (string.IsNullOrWhiteSpace(txtCustomer.Text))
            {
                lblCustomerRequired.Text = "This field is required";
                lblCustomerRequired.Visible = true;
                txtCustomer.Focus();
                isValid = false;
            }
            else lblCustomerRequired.Visible = false;

            // 2. Validate Product
            if (string.IsNullOrWhiteSpace(txtProduct.Text))
            {
                lblProductRequired.Text = "This field is required";
                lblProductRequired.Visible = true;
                if (isValid) txtProduct.Focus();
                isValid = false;
            }
            else lblProductRequired.Visible = false;

            // 3. Validate Price
            if (numPrice.Value <= 0)
            {
                lblPriceRequired.Text = "Must be greater than 0";
                lblPriceRequired.Visible = true;
                if (isValid) numPrice.Focus();
                isValid = false;
            }
            else lblPriceRequired.Visible = false;

            // 4. Validate Quantity
            if (numQuantity.Value <= 0)
            {
                lblQtyRequired.Text = "Must be greater than 0";
                lblQtyRequired.Visible = true;
                if (isValid) numQuantity.Focus();
                isValid = false;
            }
            else lblQtyRequired.Visible = false;

            return isValid;
        }
        private void btnSave_Click(object sender, EventArgs e)
        {
            if (!ValidateInputs()) return;
        }

        private void txtCustomer_TextChanged(object sender, EventArgs e)
        {
            if (!string.IsNullOrWhiteSpace(txtCustomer.Text)) lblCustomerRequired.Visible = false;
        }

        private void txtProduct_TextChanged(object sender, EventArgs e)
        {
            if (!string.IsNullOrWhiteSpace(txtProduct.Text)) lblProductRequired.Visible = false;
        }

        private void numPrice_ValueChanged(object sender, EventArgs e)
        {
            if (numPrice.Value > 0) lblPriceRequired.Visible = false;
        }

        private void numQuantity_ValueChanged(object sender, EventArgs e)
        {
            if (numQuantity.Value > 0) lblQtyRequired.Visible = false;
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}