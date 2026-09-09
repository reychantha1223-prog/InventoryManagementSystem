using System;
using System.Drawing;
using System.Windows.Forms;

namespace InventoryManagementSystem.Forms
{
    public partial class FormSuppliers : Form
    {
        private bool isFirstClick = true;

        public FormSuppliers()
        {
            InitializeComponent();

            guna2TextBox1.Click += guna2TextBox1_Click;
            guna2Button1.Click += guna2Button1_Click;
        }

        private void FormSuppliers_Load(object sender, EventArgs e)
        {
            supplierinfoView.ThemeStyle.HeaderStyle.BackColor =
                Color.FromArgb(15, 23, 42);

            guna2TextBox1.Text = "Search supplier...";
            guna2TextBox1.ForeColor = Color.Gray;

            isFirstClick = true;

            guna2ComboBox1.Items.Clear();
            guna2ComboBox1.Items.Add("All Status");
            guna2ComboBox1.Items.Add("Active");
            guna2ComboBox1.Items.Add("Inactive");
            guna2ComboBox1.SelectedIndex = 0;

            guna2ComboBox2.Items.Clear();
            guna2ComboBox2.Items.Add("All");
            guna2ComboBox2.Items.Add("Newest");
            guna2ComboBox2.Items.Add("Oldest");
            guna2ComboBox2.SelectedIndex = 0;
        }

        private void guna2TextBox1_Click(object sender, EventArgs e)
        {
            if (isFirstClick)
            {
                guna2TextBox1.Clear();
                guna2TextBox1.ForeColor = Color.Black;
                isFirstClick = false;
            }
        }

        private void guna2Button1_Click(object sender, EventArgs e)
        {
        }

        private void guna2ComboBox1_SelectedIndexChanged(
            object sender,
            EventArgs e)
        {
        }

        private void guna2ComboBox2_SelectedIndexChanged(
            object sender,
            EventArgs e)
        {
        }
        private void guna2Button1_Click_1(object sender, EventArgs e) 
        {
            Form mainForm =
                this.TopLevelControl as Form
                ?? Form.ActiveForm
                ?? this;

            using (Form overlay = new Form())
            {
                overlay.StartPosition = FormStartPosition.Manual;
                overlay.FormBorderStyle = FormBorderStyle.None;
                overlay.Opacity = 0.50d;
                overlay.BackColor = Color.Black;
                overlay.ShowInTaskbar = false;

                overlay.Location =
                    mainForm.PointToScreen(Point.Empty);

                overlay.Size = mainForm.ClientSize;

                overlay.Show(mainForm);

                using (FormAddSupplier addSupplierForm =
                    new FormAddSupplier())
                {
                    addSupplierForm.StartPosition =
                        FormStartPosition.CenterParent;

                    addSupplierForm.ShowDialog(overlay);
                }
            }
        }
    }
}