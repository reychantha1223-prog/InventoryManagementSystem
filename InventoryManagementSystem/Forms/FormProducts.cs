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
    public partial class FormProducts : Form
    {
        public FormProducts()
        {
            InitializeComponent();
        }

        private void FormProducts_Load(object sender, EventArgs e)
        {
            comboBox1.Items.Add("Category"); // Placeholder at Index 0
            comboBox1.Items.Add("Electronics");
            comboBox1.Items.Add("Clothing");
            comboBox1.Items.Add("Food & Beverages");

            // Display "Category" by default on startup
            comboBox1.SelectedIndex = 0;
            comboBox2.Items.Add("Stock"); // Placeholder at Index 0
            comboBox2.Items.Add("Low Stock");
            comboBox2.Items.Add("Out of Stock");

            // Display "Category" by default on startup
            comboBox2.SelectedIndex = 0;
        }

        private void panel3_Paint(object sender, PaintEventArgs e)
        {

        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void panel1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void panel2_Paint(object sender, PaintEventArgs e)
        {

        }

        private void panel7_Paint(object sender, PaintEventArgs e)
        {

        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            // If adding string items directly
            if (comboBox1.SelectedIndex > 0)
            {
                string selectedCategory = comboBox1.SelectedItem.ToString();
                // Do your filtering or logic here
            }
        }

        private void comboBox2_SelectedIndexChanged(object sender, EventArgs e)
        {

        }
    }
}
