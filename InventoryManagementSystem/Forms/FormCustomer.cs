using Guna.UI2.WinForms;
using System;
using System.Drawing;
using System.Windows.Forms;

namespace InventoryManagementSystem.Forms
{
    public partial class FormCustomer : Form
    {
        private bool isFirstClick = true;

        public FormCustomer()
        {
            InitializeComponent();
        }

        private void FormCustomer_Load(object sender, EventArgs e)
        {
            customerInfoView.ThemeStyle.HeaderStyle.BackColor =
                Color.FromArgb(15, 23, 42);

            SetupCustomerGrid();

            guna2TextBox1.Text = "Search customer...";
            guna2TextBox1.ForeColor = Color.Gray;

            guna2ComboBox1.Items.Clear();
            guna2ComboBox1.Items.Add("All");
            guna2ComboBox1.Items.Add("Phnom Penh");
            guna2ComboBox1.Items.Add("Kandal");
            guna2ComboBox1.Items.Add("Takeo");
            guna2ComboBox1.Items.Add("Siem Reap");
            guna2ComboBox1.Items.Add("Battambang");
            guna2ComboBox1.SelectedIndex = 0;

            guna2ComboBox2.Items.Clear();
            guna2ComboBox2.Items.Add("All");
            guna2ComboBox2.Items.Add("Newest");
            guna2ComboBox2.Items.Add("Oldest");
            guna2ComboBox2.SelectedIndex = 0;

            LoadCustomers();
        }

        private void SetupCustomerGrid()
        {
            customerInfoView.ColumnHeadersHeightSizeMode =
                DataGridViewColumnHeadersHeightSizeMode.EnableResizing;

            customerInfoView.ColumnHeadersHeight = 40;

            customerInfoView.ColumnHeadersDefaultCellStyle.Font =
                new Font("Segoe UI", 11F, FontStyle.Bold);

            customerInfoView.ColumnHeadersDefaultCellStyle.ForeColor =
                Color.White;

            customerInfoView.RowTemplate.Height = 35;

            customerInfoView.DefaultCellStyle.Font =
                new Font("Segoe UI", 10F);

            customerInfoView.DefaultCellStyle.ForeColor =
                Color.FromArgb(50, 70, 100);

            customerInfoView.CellBorderStyle =
                DataGridViewCellBorderStyle.Single;

            customerInfoView.GridColor = Color.Gray;

            customerInfoView.AllowUserToAddRows = false;
            customerInfoView.AllowUserToDeleteRows = false;
            customerInfoView.AllowUserToResizeRows = false;
            customerInfoView.EnableHeadersVisualStyles = false;
            customerInfoView.RowHeadersVisible = false;
            customerInfoView.ReadOnly = true;

            customerInfoView.AutoSizeColumnsMode =
                DataGridViewAutoSizeColumnsMode.Fill;

            customerInfoView.SelectionMode =
                DataGridViewSelectionMode.CellSelect;

            customerInfoView.MultiSelect = false;

            customerInfoView.DefaultCellStyle.SelectionBackColor =
                Color.FromArgb(0, 123, 255);
            customerInfoView.DefaultCellStyle.SelectionForeColor =
                Color.White;

            if (!customerInfoView.Columns.Contains("colAction"))
            {
                DataGridViewTextBoxColumn actionColumn =
                    new DataGridViewTextBoxColumn();

                actionColumn.Name = "colAction";
                actionColumn.HeaderText = "Action";
                actionColumn.ReadOnly = true;
                actionColumn.SortMode =
                    DataGridViewColumnSortMode.NotSortable;

                customerInfoView.Columns.Add(actionColumn);
            }
        }

        private void LoadCustomers()
        {
            customerInfoView.Rows.Clear();

            customerInfoView.Rows.Add(
                "C001",
                "Sok Dara",
                "+855 12 345 678",
                "dara@gmail.com",
                "Phnom Penh"
            );

            customerInfoView.Rows.Add(
                "C002",
                "Chan Vibol",
                "+855 96 789 012",
                "vibol@gmail.com",
                "Kandal"
            );

            customerInfoView.Rows.Add(
                "C003",
                "Srey Neang",
                "+855 11 222 333",
                "neang@gmail.com",
                "Takeo"
            );

            customerInfoView.Rows.Add(
                "C004",
                "Long Ratha",
                "+855 92 444 555",
                "ratha@gmail.com",
                "Siem Reap"
            );

            customerInfoView.Rows.Add(
                "C005",
                "Kim Sovan",
                "+855 15 666 777",
                "sovan@gmail.com",
                "Battambang"
            );
        }

        private void customerInfoView_CellPainting(
            object sender,
            DataGridViewCellPaintingEventArgs e)
        {
            if (e.RowIndex < 0 || e.ColumnIndex < 0)
                return;

            if (customerInfoView.Columns[e.ColumnIndex].Name != "colAction")
                return;

            e.PaintBackground(e.CellBounds, false);

            int buttonWidth = 60;
            int buttonHeight = 26;
            int spacing = 8;

            int totalWidth =
                (buttonWidth * 2) + spacing;

            int startX =
                e.CellBounds.X +
                (e.CellBounds.Width - totalWidth) / 2;

            int startY =
                e.CellBounds.Y +
                (e.CellBounds.Height - buttonHeight) / 2;

            Rectangle editButton = new Rectangle(
                startX,
                startY,
                buttonWidth,
                buttonHeight
            );

            Rectangle deleteButton = new Rectangle(
                startX + buttonWidth + spacing,
                startY,
                buttonWidth,
                buttonHeight
            );

            using (SolidBrush editBackground =
                new SolidBrush(Color.FromArgb(235, 245, 255)))
            {
                e.Graphics.FillRectangle(
                    editBackground,
                    editButton
                );
            }

            using (SolidBrush deleteBackground =
                new SolidBrush(Color.FromArgb(255, 235, 235)))
            {
                e.Graphics.FillRectangle(
                    deleteBackground,
                    deleteButton
                );
            }

            using (Font font = new Font("Segoe UI", 9F))
            using (SolidBrush editText =
                new SolidBrush(Color.FromArgb(0, 123, 255)))
            using (SolidBrush deleteText =
                new SolidBrush(Color.FromArgb(220, 53, 69)))
            using (StringFormat format = new StringFormat())
            {
                format.Alignment = StringAlignment.Center;
                format.LineAlignment = StringAlignment.Center;

                e.Graphics.DrawString(
                    "Edit",
                    font,
                    editText,
                    editButton,
                    format
                );

                e.Graphics.DrawString(
                    "Delete",
                    font,
                    deleteText,
                    deleteButton,
                    format
                );
            }

            e.Handled = true;
        }

        private void customerInfoView_CellClick(
            object sender,
            DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0 || e.ColumnIndex < 0)
                return;

            if (customerInfoView.Columns[e.ColumnIndex].Name != "colAction")
                return;

            Rectangle cellRect =
                customerInfoView.GetCellDisplayRectangle(
                    e.ColumnIndex,
                    e.RowIndex,
                    false
                );

            Point mousePosition =
                customerInfoView.PointToClient(Cursor.Position);

            int x =
                mousePosition.X - cellRect.X;

            int buttonWidth = 60;
            int spacing = 8;

            string customerID =
                customerInfoView.Rows[e.RowIndex]
                .Cells[0]
                .Value?.ToString();

            if (x >= 0 && x < buttonWidth)
            {
                MessageBox.Show(
                    "Edit Customer: " + customerID,
                    "Edit Customer",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Information
                );
            }
            else if (
                x >= buttonWidth + spacing &&
                x < (buttonWidth * 2) + spacing)
            {
                DialogResult result =
                    MessageBox.Show(
                        "Are you sure you want to delete "
                        + customerID + "?",
                        "Delete Customer",
                        MessageBoxButtons.YesNo,
                        MessageBoxIcon.Warning
                    );

                if (result == DialogResult.Yes)
                {
                    customerInfoView.Rows.RemoveAt(e.RowIndex);
                }
            }
        }

        private void guna2TextBox1_Click(
            object sender,
            EventArgs e)
        {
            if (isFirstClick)
            {
                guna2TextBox1.Clear();
                guna2TextBox1.ForeColor = Color.Black;
                isFirstClick = false;
            }
        }

        private void guna2Button2_Click(
            object sender,
            EventArgs e)
        {
            string searchText =
                guna2TextBox1.Text.Trim();

            if (isFirstClick ||
                string.IsNullOrWhiteSpace(searchText))
            {
                MessageBox.Show(
                    "Please enter customer name.",
                    "Search",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Warning
                );

                guna2TextBox1.Focus();
                return;
            }

            MessageBox.Show(
                "Searching for: " + searchText,
                "Search",
                MessageBoxButtons.OK,
                MessageBoxIcon.Information
            );
        }

        private void guna2Button1_Click(
            object sender,
            EventArgs e)
        {
            Form mainForm =
                this.TopLevelControl as Form
                ?? Form.ActiveForm
                ?? this;

            using (Form overlay = new Form())
            {
                overlay.StartPosition =
                    FormStartPosition.Manual;

                overlay.FormBorderStyle =
                    FormBorderStyle.None;

                overlay.Opacity = 0.50d;
                overlay.BackColor = Color.Black;
                overlay.ShowInTaskbar = false;

                overlay.Location =
                    mainForm.PointToScreen(Point.Empty);

                overlay.Size =
                    mainForm.ClientSize;

                overlay.Show(mainForm);

                using (FormAddCustomer addCustomerForm =
                    new FormAddCustomer())
                {
                    addCustomerForm.StartPosition =
                        FormStartPosition.CenterParent;

                    addCustomerForm.ShowDialog(overlay);
                }
            }
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
    }
}