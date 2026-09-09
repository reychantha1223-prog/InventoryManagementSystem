using System;
using System.Drawing;
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
            SetupComboBoxes();
            SetupDataGridView();
            LoadSampleData();
        }

        private void SetupComboBoxes()
        {
            // Category Filter ComboBox
            comboBox1.Items.Add("Category"); // Placeholder at Index 0
            comboBox1.Items.Add("Electronics");
            comboBox1.Items.Add("Clothing");
            comboBox1.Items.Add("Food & Beverages");
            comboBox1.SelectedIndex = 0;

            // Stock Filter ComboBox
            comboBox2.Items.Add("Stock"); // Placeholder at Index 0
            comboBox2.Items.Add("Low Stock");
            comboBox2.Items.Add("Out of Stock");
            comboBox2.SelectedIndex = 0;
        }

        private void SetupDataGridView()
        {
            // Grid Layout & Behavior
            dataGridView1.AllowUserToAddRows = false;
            dataGridView1.EnableHeadersVisualStyles = false;
            dataGridView1.RowHeadersVisible = false;
            dataGridView1.ReadOnly = true;
            dataGridView1.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;

            // Grid Lines & Borders
            dataGridView1.CellBorderStyle = DataGridViewCellBorderStyle.Single;
            dataGridView1.GridColor = Color.Gray;

            // Header & Row Formatting
            dataGridView1.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.EnableResizing;
            dataGridView1.ColumnHeadersHeight = 40;
            dataGridView1.ColumnHeadersDefaultCellStyle.Font = new Font("Segoe UI", 12F, FontStyle.Bold);
            dataGridView1.RowTemplate.Height = 35;
            dataGridView1.DefaultCellStyle.Font = new Font("Segoe UI", 11F, FontStyle.Regular);

            // Configure Edit Button Column
            if (dataGridView1.Columns["Edit"] is DataGridViewButtonColumn editCol)
            {
                editCol.Text = "Edit";
                editCol.UseColumnTextForButtonValue = true;
                editCol.FlatStyle = FlatStyle.Flat;
                editCol.DefaultCellStyle.BackColor = Color.DodgerBlue;
                editCol.DefaultCellStyle.ForeColor = Color.White;
                editCol.DefaultCellStyle.SelectionBackColor = Color.RoyalBlue;
            }

            // Configure Delete Button Column
            if (dataGridView1.Columns["Delete"] is DataGridViewButtonColumn deleteCol)
            {
                deleteCol.Text = "Delete";
                deleteCol.UseColumnTextForButtonValue = true;
                deleteCol.FlatStyle = FlatStyle.Flat;
                deleteCol.DefaultCellStyle.BackColor = Color.Crimson;
                deleteCol.DefaultCellStyle.ForeColor = Color.White;
                deleteCol.DefaultCellStyle.SelectionBackColor = Color.DarkRed;
            }
        }

        private void LoadSampleData()
        {
            dataGridView1.Rows.Clear();
            dataGridView1.Rows.Add("1", "Laptop", "Electronics", "$999", "15", "In Stock", "Gaming Laptop", "Edit", "Delete");
            dataGridView1.Rows.Add("2", "T-Shirt", "Clothing", "$19", "50", "In Stock", "Cotton T-Shirt", "Edit", "Delete");
        }
        private bool isFirstClick = true;
        private void txtProduct_Click(object sender, EventArgs e)
        {
            if (isFirstClick)
            {
                txtProducts.ForeColor = Color.Black;
                txtProducts.Clear();
                isFirstClick = false;
            }
        }

        private void btnAddProduct_Click(object sender, EventArgs e)
        {
            Form mainForm = this.TopLevelControl as Form ?? Form.ActiveForm ?? this;

            using (Form overlay = new Form())
            {
                overlay.StartPosition = FormStartPosition.Manual;
                overlay.FormBorderStyle = FormBorderStyle.None;
                overlay.Opacity = 0.50d;
                overlay.BackColor = Color.Black;
                overlay.ShowInTaskbar = false;

                // Position over the entire main dashboard screen
                overlay.Location = mainForm.PointToScreen(Point.Empty);
                overlay.Size = mainForm.ClientSize;

                // Display overlay over main window
                overlay.Show(mainForm);

                // Open FormAddProduct popup centered
                using (FormAddProduct addProductForm = new FormAddProduct())
                {
                    addProductForm.StartPosition = FormStartPosition.CenterParent;
                    addProductForm.ShowDialog(overlay);
                }
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {

        }
    }
}