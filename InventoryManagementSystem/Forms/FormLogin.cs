using System;
using System.Windows.Forms;
using System.Windows.Forms.VisualStyles;

namespace InventoryManagementSystem.Forms
{
    public partial class FormLogin : Form
    {
        public FormLogin()
        {
            InitializeComponent();
            txtPassword.UseSystemPasswordChar = true;
            pictureBox3.Visible = true;
            pictureBox4.Visible = true;
            pictureBox5.Visible = true;
            pictureBox5.Image = Properties.Resources.hide;
            pictureBox5.SizeMode = PictureBoxSizeMode.Zoom;
        }

        private void pictureBox5_Click(object sender, EventArgs e)
        {
            txtPassword.UseSystemPasswordChar =
                !txtPassword.UseSystemPasswordChar;
            if (txtPassword.UseSystemPasswordChar)
            {
                pictureBox5.Image = Properties.Resources.hide;
            }
            else
            {
                pictureBox5.Image = Properties.Resources.show;
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            string username = txtUsername.Text.Trim();
            string password = txtPassword.Text;
            if (username == "admin" && password == "admin")
            {
                Form1 MainForm = new Form1();
                MainForm.Show();
                this.Hide();
            }
            else
            {
                lblMessage.Text = "Invalid username or password !";
                txtPassword.Focus();
            }
        }

        private void pictureBox3_Click(object sender, EventArgs e)
        {
            txtUsername.Focus();
        }

        private void pictureBox4_Click(object sender, EventArgs e)
        {
            txtPassword.Focus();
        }

        private void textBox2_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                e.SuppressKeyPress = true;
                button1_Click(sender, e);  // Performs login check
            }
        }

        private void txtUsername_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                e.SuppressKeyPress = true; 
                txtPassword.Focus();       
            }
        }
    }
}
