using System;
using System.Windows.Forms;
using System.Windows.Forms.VisualStyles;

namespace InventoryManagementSystem.Forms
{
    public partial class FormLogin : Form
    {
        public FormLogin()
        {
            InitializeComponent();
            textBox2.UseSystemPasswordChar = true;
            this.AcceptButton = button1;
            pictureBox3.Visible = true;
            pictureBox4.Visible = true;
            pictureBox5.Visible = true;
            pictureBox5.Image = Properties.Resources.hide;
            pictureBox5.SizeMode = PictureBoxSizeMode.Zoom;
        }

        private void pictureBox5_Click(object sender, EventArgs e)
        {
            textBox2.UseSystemPasswordChar =
                !textBox2.UseSystemPasswordChar;
            if (textBox2.UseSystemPasswordChar)
            {
                pictureBox5.Image = Properties.Resources.show;
            }
            else
            {
                pictureBox5.Image = Properties.Resources.hide;
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            string username = textBox1.Text.Trim();
            string password = textBox2.Text;
            if (username == "admin" && password == "admin")
            {
                MessageBox.Show(
                    "login successfull!",
                    "success",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Information
                );
                Form1 MainForm = new Form1();
                MainForm.Show();
                this.Hide();
            }
            else
            {
                MessageBox.Show(
                    "Invaild username or password.",
                    "Login Failed",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error
                );
                textBox2.Clear();
                textBox2.Focus();
            }
        }

        private void pictureBox3_Click(object sender, EventArgs e)
        {
            textBox1.Focus();
        }

        private void pictureBox4_Click(object sender, EventArgs e)
        {
            textBox2.Focus();
        }
    }
}
