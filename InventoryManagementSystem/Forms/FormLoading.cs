using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Linq;
using System.Reflection.Emit;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace InventoryManagementSystem.Forms
{
    public partial class FormLoading : Form
    {
        private Timer timer;
        private int progress = 0;
        public FormLoading()
        {
            InitializeComponent();

            panelProgress.Controls.Add(panelProgressFill);
            panelProgress.Location = new Point(82, 465);
            panelProgress.Size = new Size(544,18);
            panelProgress.BackColor = Color.Gainsboro;
            panelProgressFill.Location = new Point(0, 0);
            panelProgressFill.Size = new Size(0, panelProgress.Height);
            panelProgressFill.BackColor = Color.FromArgb(0, 120, 215);
            panelProgressFill.BringToFront();

            timer= new Timer();
            timer.Interval = 30;
            timer.Tick += Timer_Tick;
            timer.Start();
        }
        private void Timer_Tick(object sender, EventArgs e)
        {
            if (progress < 100)
            {
                progress++;
                int newWidth=
                    (panelProgress.ClientSize.Width * progress) / 100;

                panelProgressFill.Width = newWidth;

                label1.Text = $"Loading... {progress}%";
                panelProgressFill.Refresh();
            }
            else
            {
                timer.Stop();

                FormLogin login = new FormLogin();
                login.Show();

                this.Hide();
            }
        }
        private void FormLoading_Load(object sender, EventArgs e)
        {

        }
    }
}
