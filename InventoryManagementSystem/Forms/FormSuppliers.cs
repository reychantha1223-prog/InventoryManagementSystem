using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace InventoryManagementSystem.Forms
{
    public partial class FormSuppliers : Form
    {
        public FormSuppliers()
        {
            InitializeComponent();
        }

        private void FormSuppliers_Load(object sender, EventArgs e)
        {
            txtSupplier.ForeColor = Color.Gray;
        }

        private bool isFirstClick = true;
        private void txtSupplier_Click(object sender, EventArgs e)
        {
            if (isFirstClick)
            {
                txtSupplier.ForeColor = Color.Black;
                txtSupplier.Clear();
                isFirstClick = false;
            }
        
        }

        private void txtSupplier_TextChanged(object sender, EventArgs e)
        {

        }
    }
}
