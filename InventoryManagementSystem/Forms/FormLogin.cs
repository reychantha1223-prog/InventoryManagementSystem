using System;
using System.Media;
using System.Threading.Tasks;
using System.Windows.Forms;
using InventoryManagementSystem.Models;
using InventoryManagementSystem.Repositories;

namespace InventoryManagementSystem.Forms
{
    public partial class FormLogin : Form
    {
        private readonly AuthenticationRepository _authRepo = new AuthenticationRepository();

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

            lblUsernameRequired.Visible = false;
            lblPasswordRequired.Visible = false;
            if (lblMessage != null) lblMessage.Text = string.Empty;
        }

        private async void FormLogin_Load(object sender, EventArgs e)
        {
            // 1. Load saved Remember Me settings
            if (Properties.Settings.Default.RememberMe)
            {
                txtUsername.Text = Properties.Settings.Default.Username;
                chkRememberMe.Checked = true;
                txtPassword.Select(); // Focus password field if username is restored
            }
            else
            {
                txtUsername.Select();
            }
        }

        private void SaveRememberMeSettings()
        {
            if (chkRememberMe.Checked)
            {
                Properties.Settings.Default.Username = txtUsername.Text.Trim();
                Properties.Settings.Default.RememberMe = true;
            }
            else
            {
                Properties.Settings.Default.Username = string.Empty;
                Properties.Settings.Default.RememberMe = false;
            }

            Properties.Settings.Default.Save(); // Save changes permanently
        }

        private async void btnLogin_Click(object sender, EventArgs e)
        {
            if (lblMessage != null) lblMessage.Text = string.Empty;

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

            string originalText = btnLogin.Text;

            try
            {
                // 1. Unhook click handler to prevent double-clicks & change text
                btnLogin.Click -= btnLogin_Click;
                btnLogin.Text = "Logging in...";
                this.Cursor = Cursors.WaitCursor;

                string username = txtUsername.Text.Trim();
                string password = txtPassword.Text;

                Authentication user = await _authRepo.ValidateLoginAsync(username, password);

                if (user != null)
                {
                    SaveRememberMeSettings();
                    this.DialogResult = DialogResult.OK;
                    this.Close(); // Close the login form cleanly
                }
                else
                {
                    if (lblMessage != null)
                    {
                        lblMessage.Text = "Invalid username or password! Please try again";
                    }
                    SystemSounds.Hand.Play();
                }
            }
            catch (Exception ex)
            {
                if (lblMessage != null)
                {
                    lblMessage.Text = "Connection error!";
                }
                MessageBox.Show($"Database connection error: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                // 2. Restore original text, cursor, and re-enable click handler
                btnLogin.Text = originalText;
                this.Cursor = Cursors.Default;
                btnLogin.Click += btnLogin_Click;
            }
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