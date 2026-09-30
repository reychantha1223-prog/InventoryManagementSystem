using InventoryManagementSystem;
using InventoryManagementSystem.Database;
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

        string savedType = DbConnection.GetSavedServerType();
        string connStr = DbConnection.BuildConnectionString(savedType);

        if (!DbConnection.TestConnectionAsync(connStr).GetAwaiter().GetResult())
        {
            string fallbackType = savedType.Equals("SQLExpress", StringComparison.OrdinalIgnoreCase) ? "Localhost" : "SQLExpress";
            string fallbackConnStr = DbConnection.BuildConnectionString(fallbackType);

            if (DbConnection.TestConnectionAsync(fallbackConnStr).GetAwaiter().GetResult())
            {
                DbConnection.SaveConnectionString(fallbackConnStr, fallbackType);
            }
        }

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