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
    }
}