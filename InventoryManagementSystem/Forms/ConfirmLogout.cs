using System;
using System.Windows.Forms;

namespace InventoryManagementSystem.Forms
{
    public partial class ConfirmLogout : Form
    {
        public ConfirmLogout()
        {
            InitializeComponent();
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
            this.Close();
        }

        private void btnYes_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.OK;
            this.Close();
        }
    }
}