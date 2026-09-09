using System;
using System.Collections.Generic;
using System.Windows.Forms;

namespace InventoryManagementSystem.Forms
{
    public partial class FormCreateOrder : Form
    {
        // Product + Price
        private Dictionary<string, decimal> productPrices =
            new Dictionary<string, decimal>()
            {
                { "Coca Cola", 1.50m },
                { "Sting", 1.25m },
                { "Pepsi", 1.50m },
                { "Water", 0.75m },
                { "Coffee", 2.00m }
            };

        public FormCreateOrder()
        {
            InitializeComponent();

            LoadCustomers();
            LoadProducts();
            LoadStatuses();

            SetupForm();

            // Events
            guna2ComboBox2.SelectedIndexChanged +=
                guna2ComboBox2_SelectedIndexChanged;

            guna2NumericUpDown1.ValueChanged +=
                guna2NumericUpDown1_ValueChanged;

            guna2Button1.Click +=
                guna2Button1_Click;

            guna2Button2.Click +=
                guna2Button2_Click;
        }

        // ==========================================
        // SETUP FORM
        // ==========================================

        private void SetupForm()
        {
            // Date
            guna2DateTimePicker1.Value = DateTime.Now;

            // Quantity
            guna2NumericUpDown1.Minimum = 1;
            guna2NumericUpDown1.Maximum = 100;
            guna2NumericUpDown1.Value = 1;

            // Total
            guna2TextBox1.Text = "$0.00";
            guna2TextBox1.ReadOnly = true;

            // Default selection
            guna2ComboBox1.SelectedIndex = -1;
            guna2ComboBox2.SelectedIndex = -1;

            guna2ComboBox3.SelectedIndex = -1;
        }

        // ==========================================
        // CUSTOMERS
        // ==========================================

        private void LoadCustomers()
        {
            guna2ComboBox1.Items.Clear();

            guna2ComboBox1.Items.Add("Sok Dara");
            guna2ComboBox1.Items.Add("Chan Vibol");
            guna2ComboBox1.Items.Add("Srey Neang");
            guna2ComboBox1.Items.Add("Long Ratha");
            guna2ComboBox1.Items.Add("Kim Sovan");
        }

        // ==========================================
        // PRODUCTS
        // ==========================================

        private void LoadProducts()
        {
            guna2ComboBox2.Items.Clear();

            foreach (string product in productPrices.Keys)
            {
                guna2ComboBox2.Items.Add(product);
            }
        }

        // ==========================================
        // STATUS
        // ==========================================

        private void LoadStatuses()
        {
            guna2ComboBox3.Items.Clear();

            guna2ComboBox3.Items.Add("Pending");
            guna2ComboBox3.Items.Add("Paid");
            guna2ComboBox3.Items.Add("Shipped");
            guna2ComboBox3.Items.Add("Delivered");
            guna2ComboBox3.Items.Add("Cancelled");
        }

        // ==========================================
        // PRODUCT CHANGED
        // ==========================================

        private void guna2ComboBox2_SelectedIndexChanged(
            object sender,
            EventArgs e)
        {
            CalculateTotal();
        }

        // ==========================================
        // QUANTITY CHANGED
        // ==========================================

        private void guna2NumericUpDown1_ValueChanged(
            object sender,
            EventArgs e)
        {
            CalculateTotal();
        }

        // ==========================================
        // CALCULATE TOTAL
        // ==========================================

        private void CalculateTotal()
        {
            if (guna2ComboBox2.SelectedItem == null)
            {
                guna2TextBox1.Text = "$0.00";
                return;
            }

            string product =
                guna2ComboBox2.SelectedItem.ToString();

            if (productPrices.ContainsKey(product))
            {
                decimal price =
                    productPrices[product];

                decimal quantity =
                    guna2NumericUpDown1.Value;

                decimal total =
                    price * quantity;

                guna2TextBox1.Text =
                    total.ToString("$0.00");
            }
        }

        // ==========================================
        // SAVE
        // ==========================================

        private void guna2Button2_Click(
            object sender,
            EventArgs e)
        {
            // Customer validation
            if (guna2ComboBox1.SelectedIndex == 0 || guna2ComboBox1.SelectedItem == null)
            {
                MessageBox.Show(
                    "Please select a customer.",
                    "Validation",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Warning
                );

                guna2ComboBox1.Focus();
                return;
            }

            // Product validation
            if (guna2ComboBox2.SelectedIndex == 0 || guna2ComboBox2.SelectedItem == null)
            {
                MessageBox.Show(
                    "Please select a product.",
                    "Validation",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Warning
                );

                guna2ComboBox2.Focus();
                return;
            }

            // Status validation
            if (guna2ComboBox3.SelectedIndex == 0 || guna2ComboBox3.SelectedItem == null)
            {
                MessageBox.Show(
                    "Please select a status.",
                    "Validation",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Warning
                );

                guna2ComboBox3.Focus();
                return;
            }

            // Get values
            string customer =
                guna2ComboBox1.SelectedItem.ToString();

            DateTime orderDate =
                guna2DateTimePicker1.Value;

            string product =
                guna2ComboBox2.SelectedItem.ToString();

            int quantity =
                Convert.ToInt32(guna2NumericUpDown1.Value);

            string total =
                guna2TextBox1.Text;

            string status =
                guna2ComboBox3.SelectedItem.ToString();

            // Show confirmation
            MessageBox.Show(
                "Order created successfully!\n\n" +
                "Customer: " + customer + "\n" +
                "Date: " +
                orderDate.ToString("yyyy-MM-dd") + "\n" +
                "Product: " + product + "\n" +
                "Quantity: " + quantity + "\n" +
                "Total: " + total + "\n" +
                "Status: " + status,
                "Create Order",
                MessageBoxButtons.OK,
                MessageBoxIcon.Information
            );

            // Return OK
            this.DialogResult = DialogResult.OK;
            this.Close();
        }

        // ==========================================
        // CANCEL
        // ==========================================

        private void guna2Button1_Click(
            object sender,
            EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
            this.Close();
        }

        // ==========================================
        // FORM LOAD
        // ==========================================

        private void FormCreateOrder_Load(
            object sender,
            EventArgs e)
        {
        }
    }
}