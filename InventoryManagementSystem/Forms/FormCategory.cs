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
    public partial class FormCategory : Form
    {
        public FormCategory()
        {
            InitializeComponent();
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

      

        private void btnAddCategory_Click(object sender, EventArgs e)
        {
            Form mainForm = this.TopLevelControl as Form ?? Form.ActiveForm ?? this;

            using (Form overlay = new Form())
            {
                overlay.StartPosition = FormStartPosition.Manual;
                overlay.FormBorderStyle = FormBorderStyle.None;
                overlay.Opacity = 0.50d; // Controls dark overlay intensity
                overlay.BackColor = Color.Black;
                overlay.ShowInTaskbar = false;

                // Cover the exact client area of the entire dashboard window
                overlay.Location = mainForm.PointToScreen(Point.Empty);
                overlay.Size = mainForm.ClientSize;

                // Display overlay over main form
                overlay.Show(mainForm);

                // Open FormAddCategory popup centered on top of the overlay
                using (FormAddCategory addCategoryForm = new FormAddCategory())
                {
                    addCategoryForm.StartPosition = FormStartPosition.CenterParent;
                    addCategoryForm.ShowDialog(overlay);
                }
            }
        }
        private bool isFirstClick = true;
        private void txtCategory_Click(object sender, EventArgs e)
        {
            if (isFirstClick)
            {
                txtCategory.Clear();
                isFirstClick = false;
            }
        
        }

        private void FormCategory_Load(object sender, EventArgs e)
        {

        }
    }
}