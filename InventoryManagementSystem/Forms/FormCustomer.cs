using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace InventoryManagementSystem.Forms
{
    public partial class FormCustomer : Form
    {
        public FormCustomer()
        {
            InitializeComponent();
        }
        private void SetupCustomerGrid()
        {
            dgvCustomers.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.EnableResizing;
            dgvCustomers.ColumnHeadersHeight = 40;
            dgvCustomers.ColumnHeadersDefaultCellStyle.Font = new Font("Segoe UI", 12F, FontStyle.Bold);
            dgvCustomers.RowTemplate.Height = 35;
            dgvCustomers.DefaultCellStyle.Font = new Font("Segoe UI", 11F, FontStyle.Regular);
            dgvCustomers.CellBorderStyle = DataGridViewCellBorderStyle.Single;
            dgvCustomers.GridColor = Color.Gray;
            
            dgvCustomers.AllowUserToAddRows = false;
            dgvCustomers.EnableHeadersVisualStyles = false;
            dgvCustomers.RowHeadersVisible = false;
            dgvCustomers.ReadOnly = true;
            dgvCustomers.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            // Edit button
            DataGridViewButtonColumn editButton = new DataGridViewButtonColumn();
            editButton.FlatStyle = FlatStyle.Flat;
            editButton.Name = "colEdit";
            editButton.HeaderText = "Edit";
            editButton.Text = "Edit";
            editButton.UseColumnTextForButtonValue = true;
            editButton.Width = 55;

            editButton.DefaultCellStyle.BackColor = Color.FromArgb(0, 123, 255);
            editButton.DefaultCellStyle.ForeColor = Color.White;
            editButton.DefaultCellStyle.SelectionBackColor = Color.FromArgb(0, 123, 255);
            editButton.DefaultCellStyle.SelectionForeColor = Color.White;

            // Delete button
            DataGridViewButtonColumn deleteButton = new DataGridViewButtonColumn();
            deleteButton.FlatStyle = FlatStyle.Flat;
            deleteButton.Name = "colDelete";
            deleteButton.HeaderText = "Delete";
            deleteButton.Text = "Delete";
            deleteButton.UseColumnTextForButtonValue = true;
            deleteButton.Width = 60;

            deleteButton.DefaultCellStyle.BackColor = Color.FromArgb(220, 53, 69);
            deleteButton.DefaultCellStyle.ForeColor = Color.White;
            deleteButton.DefaultCellStyle.SelectionBackColor = Color.FromArgb(220, 53, 69);
            deleteButton.DefaultCellStyle.SelectionForeColor = Color.White;

            dgvCustomers.Columns.Add(editButton);
            dgvCustomers.Columns.Add(deleteButton);
        }

        private void FormCustomer_Load(object sender, EventArgs e)
        {
            SetupCustomerGrid();
            dgvCustomers.Rows.Add(
        "C001",
        "Sok Dara",
        "+855 12 345 678",
        "dara@gmail.com",
        "Phnom Penh"
    );

            dgvCustomers.Rows.Add(
                "C002",
                "Chan Vibol",
                "+855 96 789 012",
                "vibol@gmail.com",
                "Kandal"
            );

            dgvCustomers.Rows.Add(
                "C003",
                "Srey Neang",
                "+855 11 222 333",
                "neang@gmail.com",
                "Takeo"
            );

            dgvCustomers.Rows.Add(
                "C004",
                "Long Ratha",
                "+855 92 444 555",
                "ratha@gmail.com",
                "Siem Reap"
            );

            dgvCustomers.Rows.Add(
                "C005",
                "Kim Sovan",
                "+855 15 666 777",
                "sovan@gmail.com",
                "Battambang"
            );
        }

        private void button1_Click(object sender, EventArgs e)
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


                // Open FormAddCustomer popup centered
                using (FormAddCustomer addCustomerForm = new FormAddCustomer())
                {
                    addCustomerForm.StartPosition = FormStartPosition.CenterParent;
                    addCustomerForm.ShowDialog(overlay);
                }
            }
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }
        private bool isFirstClick = true;
        private void txtCustomer_Click(object sender, EventArgs e)
        {
            if (isFirstClick)
            {
                txtCustomer.Clear();
                isFirstClick = false;
            }
        
        }
    }

}
