using InventoryManagementSystem;
using InventoryManagementSystem.Forms;
using System;
using System.Windows.Forms;

static class Program
{
    [STAThread]
    static void Main()
    {
        Application.EnableVisualStyles();
        Application.SetCompatibleTextRenderingDefault(false);
        using (FormLoading loading = new FormLoading())
        {
            loading.ShowDialog();
        }

        bool keepRunning = true;

        while (keepRunning)
        {
            using (FormLogin login = new FormLogin())
            {
                if (login.ShowDialog() == DialogResult.OK)
                {
                    using (MainLayout mainLayout = new MainLayout())
                    {
                        DialogResult result = mainLayout.ShowDialog();

                        if (result != DialogResult.OK)
                        {
                            keepRunning = false;
                        }
                    }
                }
                else
                {
                    keepRunning = false;
                }
            }
        }
    }
}