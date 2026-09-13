using System;
using System.Media;
using System.Windows.Forms;
using System.Xml.Linq;

namespace InventoryManagementSystem.Forms
{
    public partial class FormLogin : Form
    {
        public FormLogin()
        {
            InitializeComponent();

            txtPassword.UseSystemPasswordChar = true;

            Usericon.Visible = true;
            Passwordicon.Visible = true;
            eyeicon.Visible = true;

            eyeicon.Image = Properties.Resources.hide;
            eyeicon.SizeMode = PictureBoxSizeMode.Zoom;
            AcceptButton = btnLogin;
        }

        private void pictureBox5_Click(object sender, EventArgs e)
        {
            if (txtPassword.UseSystemPasswordChar)
            {
                txtPassword.UseSystemPasswordChar = false;
                eyeicon.Image = Properties.Resources.show;
            }
            else
            {
                txtPassword.UseSystemPasswordChar = true;
                eyeicon.Image = Properties.Resources.hide;
            }
        }

        private void pictureBox3_Click(object sender, EventArgs e)
        {
            txtUsername.Focus();
        }

        private void pictureBox6_Click(object sender, EventArgs e)
        {
            txtPassword.Focus();
        }

        private void Passwordicon_Click(object sender, EventArgs e)
        {
            txtPassword.Focus();
        }
        private void Login()
        {
            string username = txtUsername.Text.Trim();
            string password = txtPassword.Text;

            if (username == "admin" && password == "admin")
            {
                SystemSounds.Asterisk.Play();
                Form1 mainForm = new Form1();
                mainForm.Show();
                Hide();
            }
            else
            {
                lblMessage.Text = "Invalid username or password! Please try again";
                SystemSounds.Hand.Play();
            }
        }

        private void btnLogin_Click(object sender, EventArgs e)
        {
            bool isValid = true;

            // Validate Username
            if (string.IsNullOrWhiteSpace(txtUsername.Text))
            {
                lblUsernameRequired.Visible = true;
                txtUsername.Focus();
                isValid = false;
            }
            else
            {
                lblUsernameRequired.Visible = false;
            }

            // Validate Password
            if (string.IsNullOrWhiteSpace(txtPassword.Text))
            {
                lblPasswordRequired.Visible = true;

                // Only focus Password if Username didn't already steal focus
                if (isValid)
                {
                    txtPassword.Focus();
                }

                isValid = false;
            }
            else
            {
                lblPasswordRequired.Visible = false;
            }

            if (!isValid) return;
            Login();
        }

        private void FormLogin_Load(object sender, EventArgs e)
        {
            txtUsername.Select();
        }

        private void txtUsername_TextChanged(object sender, EventArgs e)
        {
            if (!string.IsNullOrWhiteSpace(txtUsername.Text))
            {
                lblUsernameRequired.Visible = false;
            }
        }

        private void txtPassword_TextChanged(object sender, EventArgs e)
        {
            if (!string.IsNullOrWhiteSpace(txtPassword.Text))
            {
                lblPasswordRequired.Visible = false;
            }
        }
    }
}