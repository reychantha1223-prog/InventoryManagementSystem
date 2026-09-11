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
    }
}