using InventoryManagementSystem.Forms;
using InventoryManagementSystem.Repositories;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace InventoryManagementSystem
{
    public partial class MainLayout : Form
    {
        private Guna.UI2.WinForms.Guna2Button currentButton;
        public MainLayout()
        {
            InitializeComponent();
            this.StartPosition = FormStartPosition.CenterScreen;
            this.WindowState = FormWindowState.Maximized;

        }
        private void LoadForm(Form childForm, object btnSender)
        {
            ActivateButton(btnSender);

            panelDesktopPane.Controls.Clear();

            childForm.TopLevel = false;
            childForm.FormBorderStyle = FormBorderStyle.None;
            childForm.Dock = DockStyle.Fill;

            panelDesktopPane.Controls.Add(childForm);
            childForm.BringToFront();
            childForm.Show();
        }

        // Method to highlight the clicked button
        private void ActivateButton(object btnSender)
        {
            if (btnSender is Guna.UI2.WinForms.Guna2Button button)
            {
                if (currentButton != button)
                {
                    DisableButton(); // Reset previous button color

                    // Highlight active button
                    currentButton = button;
                    currentButton.FillColor = Color.FromArgb(0, 150, 255); // Use FillColor for Guna controls
                    currentButton.ForeColor = Color.White;
                }
            }
        }

        // Method to reset all buttons back to default sidebar color
        // Method to reset all buttons back to default sidebar color
        private void DisableButton()
        {
            foreach (Control previousBtn in panelMenu.Controls) // Ensures panelMenu matches your sidebar panel Name
            {
                // Check for Guna2Button instead of standard Button
                if (previousBtn is Guna.UI2.WinForms.Guna2Button gunaBtn && gunaBtn.Name != "btnLogout")
                {
                    gunaBtn.FillColor = Color.FromArgb(5, 5, 45); // Reset to Default Dark Blue
                    gunaBtn.ForeColor = Color.Gainsboro;
                }
            }
        }
        private void MainLayout_Load(object sender, EventArgs e)
        {
            btnHomeDashboard_Click(btnHome, e);
            this.WindowState = FormWindowState.Maximized;
        }
        private void btnHomeDashboard_Click(object sender, EventArgs e)
        {
            LoadForm(new FormDashboard(), sender);

        }

        private void btnAccount_Click(object sender, EventArgs e)
        {
            LoadForm (new FormAccount(),sender);
        }

        private void btnProducts_Click(object sender, EventArgs e)
        {
            LoadForm(new FormProducts(), sender);
        }

        private void btnCategory_Click(object sender, EventArgs e)
        {
            LoadForm(new FormCategory(), sender);
        }

        private void btnCustomers_Click(object sender, EventArgs e)
        {
            LoadForm(new FormCustomer(), sender);
        }

        private void btnSuppliers_Click(object sender, EventArgs e)
        {
            LoadForm(new FormSuppliers(), sender);
        }

        private void btnOrders_Click(object sender, EventArgs e)
        {
            LoadForm(new FormOrder(), sender);
        }

        private void btnLogout_Click(object sender, EventArgs e)
        {
            Form mainForm = this.TopLevelControl as Form ?? Form.ActiveForm ?? this;

            using (Form overlay = new Form())
            {
                overlay.StartPosition = FormStartPosition.Manual;
                overlay.FormBorderStyle = FormBorderStyle.None;
                overlay.Opacity = 0.50d;
                overlay.BackColor = Color.Black;
                overlay.ShowInTaskbar = false;
                overlay.Location = mainForm.PointToScreen(Point.Empty);
                overlay.Size = mainForm.ClientSize;
                overlay.Show(mainForm);

                bool shouldLogout = false;

                using (ConfirmLogout logoutModal = new ConfirmLogout())
                {
                    logoutModal.StartPosition = FormStartPosition.CenterParent;
                    if (logoutModal.ShowDialog(overlay) == DialogResult.OK)
                    {
                        shouldLogout = true;
                    }
                }

                overlay.Close();

                if (shouldLogout)
                {
                    // Set DialogResult so Program.cs loop knows to open FormLogin next
                    mainForm.DialogResult = DialogResult.OK;
                    mainForm.Close();
                }
            }
        }
    }
}
