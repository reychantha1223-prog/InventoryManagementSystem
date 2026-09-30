using System;
using System.Drawing;
using System.IO;
using System.Threading.Tasks;
using System.Windows.Forms;
using InventoryManagementSystem.Database;
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
            // 1. Load User Profile Data
            await LoadUserProfileDataAsync();

            // 2. Load Database Server Connection Settings
            LoadDatabaseServerSettings();
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

        private void LoadDatabaseServerSettings()
        {
            // Read saved server type preference directly from configuration
            string savedServerType = DbConnection.GetSavedServerType();

            if (savedServerType.Equals("SQLExpress", StringComparison.OrdinalIgnoreCase))
            {
                rbSqlExpress.Checked = true;
                txtServerInstance.Text = @".\SQLEXPRESS";
            }
            else
            {
                rbLocalhost.Checked = true;
                txtServerInstance.Text = "localhost"; // Displays "localhost" as requested
            }

            // Lock text box so user cannot input text
            txtServerInstance.ReadOnly = true;
            txtServerInstance.BackColor = Color.White;
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

        // Radio Button: Localhost Selection
        private void rbLocalhost_CheckedChanged(object sender, EventArgs e)
        {
            if (rbLocalhost.Checked)
            {
                txtServerInstance.Text = "localhost"; // Display text only
            }
        }

        // Radio Button: SQL Server Express Selection
        private void rbSqlExpress_CheckedChanged(object sender, EventArgs e)
        {
            if (rbSqlExpress.Checked)
            {
                txtServerInstance.Text = @".\SQLEXPRESS";
            }
        }

        private bool _isTestingConnection = false;

        private async void btnTestConnection_Click(object sender, EventArgs e)
        {
            // Prevent double-clicking while request is running
            if (_isTestingConnection) return;
            _isTestingConnection = true;

            // Show loading state on text without graying out the button
            string originalText = btnTestConnection.Text;
            btnTestConnection.Text = " Testing...";

            // Load loading icon while testing
            string loadingPath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Images", "loading.png");
            if (File.Exists(loadingPath))
            {
                picStatusIcon.Image = Image.FromFile(loadingPath);
            }

            // Setup Status Label Font & Alignment
            lblStatus.Font = new Font("Segoe UI", 10F, FontStyle.Regular);
            lblStatus.Text = "Testing connection...";
            lblStatus.ForeColor = Color.DarkOrange;

            string selectedType = rbSqlExpress.Checked ? "SQLExpress" : "Localhost";
            string connStr = DbConnection.BuildConnectionString(selectedType);

            bool isConnected = await DbConnection.TestConnectionAsync(connStr);

            if (isConnected)
            {
                // Load green check icon (check (1).png)
                string successPath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Images", "check (1).png");
                if (File.Exists(successPath))
                {
                    picStatusIcon.Image = Image.FromFile(successPath);
                }

                lblStatus.Text = "Connection Successful!\nThe Database server is reachable and ready to use.";
                lblStatus.Font = new Font("Segoe UI", 10F, FontStyle.Regular);
                lblStatus.ForeColor = Color.FromArgb(25, 169, 87);
                pnlStatus.FillColor = Color.FromArgb(235, 247, 238);
            }
            else
            {
                // Load warning icon (warning.png)
                string failPath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Images", "warning.png");
                if (File.Exists(failPath))
                {
                    picStatusIcon.Image = Image.FromFile(failPath);
                }

                lblStatus.Text = "Connection Failed!\nUnable to connect to the selected SQL Server instance.";
                lblStatus.Font = new Font("Segoe UI", 10F, FontStyle.Regular);
                lblStatus.ForeColor = Color.Red;
                pnlStatus.FillColor = ColorTranslator.FromHtml("#FEE2E2");
            }

            // Reset button state
            btnTestConnection.Text = originalText;
            _isTestingConnection = false;
        }
        // Save All Changes (User Details + Database Connection)
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

            string originalText = btnSaveChange.Text;

            try
            {
                btnSaveChange.Text = "Saving...";
                this.Cursor = Cursors.WaitCursor;

                int userId = AuthenticationRepository.CurrentUser.UserID;

                // 1. Save User Profile Details
                bool isProfileSuccess = await _accountRepo.UpdateProfileInfoAsync(userId, fullName, email, selectedImageBytes);

                // 2. Save Database Connection Settings
                string selectedServerType = rbSqlExpress.Checked ? "SQLExpress" : "Localhost";
                string newConnStr = DbConnection.BuildConnectionString(selectedServerType);
                DbConnection.SaveConnectionString(newConnStr, selectedServerType);

                if (isProfileSuccess)
                {
                    // Update session cache
                    AuthenticationRepository.CurrentUser.FullName = fullName;
                    AuthenticationRepository.CurrentUser.Email = email;
                    if (selectedImageBytes != null)
                    {
                        AuthenticationRepository.CurrentUser.ProfileImageBytes = selectedImageBytes;
                    }

                    selectedImageBytes = null;

                    MessageBox.Show("Account details and database settings updated successfully!", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);

                    await LoadUserProfileDataAsync();
                }
                else
                {
                    MessageBox.Show("Failed to update profile details.", "Update Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error saving settings: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
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
            LoadDatabaseServerSettings();
        }
    }
}