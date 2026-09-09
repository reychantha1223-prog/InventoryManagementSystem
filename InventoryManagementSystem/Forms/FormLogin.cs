using System;
using System.Windows.Forms;

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

            txtUsername.KeyDown += txtUsername_KeyDown;
            txtPassword.KeyDown += txtPassword_KeyDown;
            txtUsername.PreviewKeyDown += txtUsername_PreviewKeyDown;

            AcceptButton = button1;
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

        private void txtUsername_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                e.SuppressKeyPress = true;
                e.Handled = true;

                txtPassword.Focus();
            }
        }
        private void txtUsername_PreviewKeyDown(object sender, PreviewKeyDownEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                e.IsInputKey = true;
            }
        }

        private void txtPassword_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                e.SuppressKeyPress = true;
                e.Handled = true;

                Login();
            }
        }

        private void button1_Click_1(object sender, EventArgs e)
        {
            Login();
        }

        private void Login()
        {
            string username = txtUsername.Text.Trim();
            string password = txtPassword.Text;

            if (username == "admin" && password == "admin")
            {
                Form1 mainForm = new Form1();
                mainForm.Show();
                Hide();
            }
            else
            {
                MessageBox.Show(
                    "Invalid username or password!",
                    "Login Failed",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Warning
                );

                txtPassword.Focus();
                txtPassword.SelectAll();
            }
        }

        private void FormLogin_Load(object sender, EventArgs e)
        {
        }

        private void txtUsername_TextChanged(object sender, EventArgs e)
        {
        }

        private void txtPassword_TextChanged(object sender, EventArgs e)
        {
        }
    }
}