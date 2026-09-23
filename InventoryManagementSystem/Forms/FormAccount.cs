using System;
using System.Drawing;
using System.IO;
using System.Threading.Tasks;
using System.Windows.Forms;
using InventoryManagementSystem.Models;
using InventoryManagementSystem.Repositories;

namespace InventoryManagementSystem.Forms
{
    public partial class FormAccount : Form
    {
        private readonly AccountRepository _accountRepo = new AccountRepository();
        private byte[] selectedImageBytes = null;

        public FormAccount()
        {
            InitializeComponent();
        }

        private async void FormAccount_Load(object sender, EventArgs e)
        {
            await LoadUserProfileDataAsync();
        }

        private async Task LoadUserProfileDataAsync()
        {
            if (AuthenticationRepository.CurrentUser == null) return;

            int currentUserId = AuthenticationRepository.CurrentUser.UserID;

            // Load latest profile data using the Authentication model
            Authentication user = await _accountRepo.GetUserProfileAsync(currentUserId);

            if (user != null)
            {
                txtFullName.Text = user.FullName;
                txtUsername.Text = user.Username;
                txtEmail.Text = user.Email;
                txtRole.Text = user.Role;

                // Set readonly fields according to UI design
                txtUsername.ReadOnly = true;
                txtRole.ReadOnly = true;

                // Render user profile image if available
                if (user.ProfileImageBytes != null && user.ProfileImageBytes.Length > 0)
                {
                    using (MemoryStream ms = new MemoryStream(user.ProfileImageBytes))
                    {
                        picProfile.Image = Image.FromStream(ms);
                    }
                }
            }
        }

        // Handle Change Photo button click
        private void btnChangePhoto_Click(object sender, EventArgs e)
        {
            using (OpenFileDialog ofd = new OpenFileDialog())
            {
                ofd.Filter = "Image Files (*.jpg; *.jpeg; *.png)|*.jpg; *.jpeg; *.png";
                ofd.Title = "Select Profile Photo";

                if (ofd.ShowDialog() == DialogResult.OK)
                {
                    picProfile.Image = Image.FromFile(ofd.FileName);
                    selectedImageBytes = File.ReadAllBytes(ofd.FileName);
                }
            }
        }

        private async void btnSaveChange_Click(object sender, EventArgs e)
        {
            if (AuthenticationRepository.CurrentUser == null)
            {
                MessageBox.Show("User session not found. Please log in again.", "Session Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            string fullName = txtFullName.Text.Trim();
            string email = txtEmail.Text.Trim();

            if (string.IsNullOrWhiteSpace(fullName))
            {
                MessageBox.Show("Full Name is required.", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtFullName.Focus();
                return;
            }

            // Save original button properties
            string originalText = btnSaveChange.Text;

            try
            {
                // 1. Give visual feedback WITHOUT disabling the button color
                btnSaveChange.Text = "Saving...";
                this.Cursor = Cursors.WaitCursor;

                int userId = AuthenticationRepository.CurrentUser.UserID;

                // 2. Perform DB update
                bool isSuccess = await _accountRepo.UpdateProfileInfoAsync(userId, fullName, email, selectedImageBytes);

                if (isSuccess)
                {
                    // Update session cache
                    AuthenticationRepository.CurrentUser.FullName = fullName;
                    AuthenticationRepository.CurrentUser.Email = email;
                    if (selectedImageBytes != null)
                    {
                        AuthenticationRepository.CurrentUser.ProfileImageBytes = selectedImageBytes;
                    }

                    selectedImageBytes = null;

                    MessageBox.Show("Account details updated successfully!", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);

                    await LoadUserProfileDataAsync();
                }
                else
                {
                    MessageBox.Show("Failed to update profile details.", "Update Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Database error: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                // Restore original button text and cursor
                btnSaveChange.Text = originalText;
                this.Cursor = Cursors.Default;
            }
        }
        private async void btnChangePassword_Click(object sender, EventArgs e)
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

                using (FormChangePassword changePasswordForm = new FormChangePassword())
                {
                    changePasswordForm.StartPosition = FormStartPosition.CenterParent;

                    changePasswordForm.FormClosed += async (s, args) =>
                    {
                        // Refresh user data if password change modal performs changes
                        await LoadUserProfileDataAsync();
                    };

                    changePasswordForm.ShowDialog(overlay);
                }
            }
        }

        // Handle Cancel button click
        private async void btnCancel_Click(object sender, EventArgs e)
        {
            selectedImageBytes = null;
            await LoadUserProfileDataAsync();
        }
    }
}