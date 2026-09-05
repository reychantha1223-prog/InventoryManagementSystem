using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace InventoryManagementSystem.Forms
{
    public partial class FormAddProduct : Form
    {
        public FormAddProduct()
        {
            InitializeComponent();
        }

        private void FormAddProduct_Load(object sender, EventArgs e)
        {

        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.OK;

            // Close the form
            this.Close();
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            //this.DialogResult = DialogResult.OK;

            // Close the form
            this.Close();
        }
    }
}
