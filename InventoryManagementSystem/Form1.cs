using InventoryManagementSystem.Forms;
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
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();

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
        private Button currentButton;

        // Method to highlight the clicked button
        private void ActivateButton(object btnSender)
        {
            if (btnSender != null)
            {
                if (currentButton != (Button)btnSender)
                {
                    DisableButton(); // Reset previous button color

                    // Highlight active button
                    currentButton = (Button)btnSender;
                    currentButton.BackColor = Color.FromArgb(0, 150, 255); // Active Light Blue
                    currentButton.ForeColor = Color.White;
                }
            }
        }

        // Method to reset all buttons back to default sidebar color
        private void DisableButton()
        {
            foreach (Control previousBtn in panelMenu.Controls) // Replace panelMenu with your sidebar panel Name
            {
                // Skip the Logout button so its color remains white/red
                if (previousBtn is Button && previousBtn.Name != "btnLogout")
                {
                    previousBtn.BackColor = Color.FromArgb(5, 5, 45); // Default Dark Blue
                    previousBtn.ForeColor = Color.Gainsboro;
                }
            }
        }
        private void Form1_Load(object sender, EventArgs e)
        {
            button1_Click(btnDashboard, e);
        }

        private void pictureBox2_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void label2_Click(object sender, EventArgs e)
        {

        }

        private void label2_Click_1(object sender, EventArgs e)
        {

        }

        private void flowLayoutPanel1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            LoadForm(new FormDashboard(), sender);
        }

        private void panel1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void button3_Click(object sender, EventArgs e)
        {
            LoadForm(new FormProducts(), sender);
        }

        private void tableLayoutPanel1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void button2_Click(object sender, EventArgs e)
        {
            LoadForm(new FormCategory(), sender);
        }

        private void button4_Click(object sender, EventArgs e)
        {
            LoadForm(new FormCustomer(), sender);
        }
    }
}
