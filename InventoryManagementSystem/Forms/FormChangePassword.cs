using System;
using System.Media;
using System.Threading.Tasks;
using System.Windows.Forms;
using InventoryManagementSystem.Repositories;

namespace InventoryManagementSystem.Forms
{
    public partial class FormChangePassword : Form
    {
        private readonly AccountRepository _accountRepo = new AccountRepository();

        public FormChangePassword()
        {
            InitializeComponent();

            // Mask all password fields
            txtCurrentPassword.UseSystemPasswordChar = true;
            txtNewPassword.UseSystemPasswordChar = true;
            txtConfirmPassword.UseSystemPasswordChar = true;

            //Eye icons for showing/hiding password
            cureye.Image = Properties.Resources.hide;
            neweye.Image = Properties.Resources.hide;
            coneye.Image = Properties.Resources.hide;

            AcceptButton = btnSave;
            CancelButton = btnCancel;
        }

        private void FormChangePassword_Load(object sender, EventArgs e)
        {
            // Hide all error labels on load
            HideErrorLabels();
        }

        private void HideErrorLabels()
        {
            if (lblCurrentPasswordError != null) lblCurrentPasswordError.Visible = false;
            if (lblNewPasswordError != null) lblNewPasswordError.Visible = false;
            if (lblConfirmPasswordError != null) lblConfirmPasswordError.Visible = false;
        }

        private bool ValidateInputs()
        {
            HideErrorLabels();
            bool isValid = true;

            string currentPassword = txtCurrentPassword.Text;
            string newPassword = txtNewPassword.Text;
            string confirmPassword = txtConfirmPassword.Text;

            // 1. Current Password Validation
            if (string.IsNullOrWhiteSpace(currentPassword))
            {
                if (lblCurrentPasswordError != null)
                {
                    lblCurrentPasswordError.Text = "This field is required";
                    lblCurrentPasswordError.Visible = true;
                }
                txtCurrentPassword.Focus();
                isValid = false;
            }

            // 2. New Password Validation
            if (string.IsNullOrWhiteSpace(newPassword))
            {
                if (lblNewPasswordError != null)
                {
                    lblNewPasswordError.Text = "This field is required";
                    lblNewPasswordError.Visible = true;
                }
                if (isValid) txtNewPassword.Focus();
                isValid = false;
            }
            else if (newPassword.Length < 6)
            {
                if (lblNewPasswordError != null)
                {
                    lblNewPasswordError.Text = "Must be at least 6 characters";
                    lblNewPasswordError.Visible = true;
                }
                if (isValid) txtNewPassword.Focus();
                isValid = false;
            }
            else if (currentPassword == newPassword)
            {
                if (lblNewPasswordError != null)
                {
                    lblNewPasswordError.Text = "Cannot match current password";
                    lblNewPasswordError.Visible = true;
                }
                if (isValid) txtNewPassword.Focus();
                isValid = false;
            }

            // 3. Confirm Password Validation
            if (string.IsNullOrWhiteSpace(confirmPassword))
            {
                if (lblConfirmPasswordError != null)
                {
                    lblConfirmPasswordError.Text = "This field is required";
                    lblConfirmPasswordError.Visible = true;
                }
                if (isValid) txtConfirmPassword.Focus();
                isValid = false;
            }
            else if (newPassword != confirmPassword)
            {
                if (lblConfirmPasswordError != null)
                {
                    lblConfirmPasswordError.Text = "Passwords do not match";
                    lblConfirmPasswordError.Visible = true;
                }
                if (isValid) txtConfirmPassword.Focus();
                isValid = false;
            }

            return isValid;
        }

        private async void btnSave_Click(object sender, EventArgs e)
        {
            if (!ValidateInputs()) return;

            try
            {
                btnSave.Enabled = false;

                if (AuthenticationRepository.CurrentUser == null)
                {
                    MessageBox.Show("Session expired. Please log in again.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    this.Close();
                    return;
                }

                int userId = AuthenticationRepository.CurrentUser.UserID;

                bool isSuccess = await _accountRepo.ChangePasswordAsync(userId, txtCurrentPassword.Text, txtNewPassword.Text);

                if (isSuccess)
                {
                    SystemSounds.Asterisk.Play();
                    MessageUpdatePassword ms = new MessageUpdatePassword();
                    ms.Show();
                    this.DialogResult = DialogResult.OK;
                    this.Close();
                }
                else
                {
                    SystemSounds.Hand.Play();
                    if (lblCurrentPasswordError != null)
                    {
                        lblCurrentPasswordError.Text = "Incorrect current password";
                        lblCurrentPasswordError.Visible = true;
                    }
                    txtCurrentPassword.SelectAll();
                    txtCurrentPassword.Focus();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Database connection error: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                btnSave.Enabled = true;
            }
        }
        private void cureye_Click(object sender, EventArgs e)
        {
            txtCurrentPassword.UseSystemPasswordChar =
                !txtCurrentPassword.UseSystemPasswordChar;

            cureye.Image = txtCurrentPassword.UseSystemPasswordChar
                ? Properties.Resources.hide
                : Properties.Resources.show;
        }
        private void newEye_Click(object sender, EventArgs e)
        {
            txtNewPassword.UseSystemPasswordChar =
                !txtNewPassword.UseSystemPasswordChar;

            neweye.Image = txtNewPassword.UseSystemPasswordChar
                ? Properties.Resources.hide
                : Properties.Resources.show;
        }
        private void conEye_Click(object sender, EventArgs e)
        {
            txtConfirmPassword.UseSystemPasswordChar =
                !txtConfirmPassword.UseSystemPasswordChar;

            coneye.Image = txtConfirmPassword.UseSystemPasswordChar
                ? Properties.Resources.hide
                : Properties.Resources.show;
        }

        // Real-time error hiding when user types in textboxes
        private void txtCurrentPassword_TextChanged(object sender, EventArgs e)
        {
            if (lblCurrentPasswordError != null) lblCurrentPasswordError.Visible = false;
        }

        private void txtNewPassword_TextChanged(object sender, EventArgs e)
        {
            if (lblNewPasswordError != null) lblNewPasswordError.Visible = false;
        }

        private void txtConfirmPassword_TextChanged(object sender, EventArgs e)
        {
            if (lblConfirmPasswordError != null) lblConfirmPasswordError.Visible = false;
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
            this.Close();
        }
    }
}