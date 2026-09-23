using System;
using System.Data.SqlClient;
using System.Drawing;
using System.Threading.Tasks;
using System.Windows.Forms;
using InventoryManagementSystem.Models;

namespace InventoryManagementSystem.Forms
{
    public partial class FormConfirmDelete : Form
    {
        private readonly string connectionString = "Server=localhost;Database=IMSDB;Trusted_Connection=True;TrustServerCertificate=True;";

        public enum ItemType { Category, Product, Customer, Supplier, Order }
        private readonly ItemType targetType;
        private readonly int itemId;
        private readonly string itemName;
        private readonly Action onDeleteSuccess;

        // Constructor for Orders
        public FormConfirmDelete(int orderId, string orderName, Action onDeleteSuccess = null)
        {
            InitializeComponent();
            this.targetType = ItemType.Order;
            this.itemId = orderId;
            this.itemName = orderName;
            this.onDeleteSuccess = onDeleteSuccess;

            ConfigureModalUI("Delete Order", "order?");
        }

        // Constructor for Suppliers
        public FormConfirmDelete(Supplier supplier, Action onDeleteSuccess = null)
        {
            InitializeComponent();
            this.targetType = ItemType.Supplier;
            this.itemId = supplier?.ID ?? 0;
            this.itemName = supplier?.Name ?? "Selected Supplier";
            this.onDeleteSuccess = onDeleteSuccess;

            ConfigureModalUI("Delete Supplier", "supplier?");
        }

        // Constructor for Categories
        public FormConfirmDelete(Category category, Action onDeleteSuccess = null)
        {
            InitializeComponent();
            this.targetType = ItemType.Category;
            this.itemId = category?.ID ?? 0;
            this.itemName = category?.Name ?? "Selected Category";
            this.onDeleteSuccess = onDeleteSuccess;

            ConfigureModalUI("Delete Category", "category?");
        }

        // Constructor for Products
        public FormConfirmDelete(Product product, Action onDeleteSuccess = null)
        {
            InitializeComponent();
            this.targetType = ItemType.Product;
            this.itemId = product?.ID ?? 0;
            this.itemName = product?.Name ?? "Selected Product";
            this.onDeleteSuccess = onDeleteSuccess;

            ConfigureModalUI("Delete Product", "product?");
        }

        // Constructor for Customers
        public FormConfirmDelete(Customer customer, Action onDeleteSuccess = null)
        {
            InitializeComponent();
            this.targetType = ItemType.Customer;
            this.itemId = customer?.ID ?? 0;
            this.itemName = customer?.Name ?? "Selected Customer";
            this.onDeleteSuccess = onDeleteSuccess;

            ConfigureModalUI("Delete Customer", "customer?");
        }

        private void ConfigureModalUI(string title, string typeText)
        {
            if (lblDeleteTitle != null)
            {
                lblDeleteTitle.Text = title;
            }

            if (lblQuestion != null)
            {
                lblQuestion.Text = $"Are you sure you want to delete this {typeText}";
                lblQuestion.AutoSize = true;
                lblQuestion.Left = (this.ClientSize.Width - lblQuestion.Width) / 2;
            }

            if (lblItemName != null)
            {
                lblItemName.Text = $"\"{itemName}\"";
                lblItemName.AutoSize = true;
                lblItemName.ForeColor = Color.FromArgb(220, 38, 38);
                lblItemName.Left = (this.ClientSize.Width - lblItemName.Width) / 2;
            }
        }

        private void FormConfirmDelete_Load(object sender, EventArgs e)
        {
            switch (targetType)
            {
                case ItemType.Category:
                    ConfigureModalUI("Delete Category", "category?");
                    break;
                case ItemType.Product:
                    ConfigureModalUI("Delete Product", "product?");
                    break;
                case ItemType.Customer:
                    ConfigureModalUI("Delete Customer", "customer?");
                    break;
                case ItemType.Supplier:
                    ConfigureModalUI("Delete Supplier", "supplier?");
                    break;
                case ItemType.Order:
                    ConfigureModalUI("Delete Order", "order?");
                    break;
            }
        }

        private async void btnConfirmDelete_Click(object sender, EventArgs e)
        {
            if (itemId <= 0) return;

            btnConfirmDelete.Enabled = false;

            try
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    await conn.OpenAsync();

                    if (targetType == ItemType.Order)
                    {
                        // Clean up child details first, then parent order
                        using (SqlTransaction transaction = conn.BeginTransaction())
                        {
                            try
                            {
                                string deleteDetailsQuery = "DELETE FROM OrderDetails WHERE OrderID = @ID";
                                using (SqlCommand cmdDetail = new SqlCommand(deleteDetailsQuery, conn, transaction))
                                {
                                    cmdDetail.Parameters.AddWithValue("@ID", itemId);
                                    await cmdDetail.ExecuteNonQueryAsync();
                                }

                                string deleteOrderQuery = "DELETE FROM Orders WHERE OrderID = @ID";
                                using (SqlCommand cmdOrder = new SqlCommand(deleteOrderQuery, conn, transaction))
                                {
                                    cmdOrder.Parameters.AddWithValue("@ID", itemId);
                                    await cmdOrder.ExecuteNonQueryAsync();
                                }

                                transaction.Commit();
                            }
                            catch
                            {
                                transaction.Rollback();
                                throw;
                            }
                        }
                    }
                    else
                    {
                        string query = string.Empty;
                        switch (targetType)
                        {
                            case ItemType.Category:
                                query = "DELETE FROM Categories WHERE CategoryID = @ID";
                                break;
                            case ItemType.Product:
                                query = "DELETE FROM Products WHERE ProductID = @ID";
                                break;
                            case ItemType.Customer:
                                query = "DELETE FROM Customers WHERE CustomerID = @ID";
                                break;
                            case ItemType.Supplier:
                                query = "DELETE FROM Suppliers WHERE SupplierID = @ID";
                                break;
                        }

                        using (SqlCommand cmd = new SqlCommand(query, conn))
                        {
                            cmd.Parameters.AddWithValue("@ID", itemId);
                            await cmd.ExecuteNonQueryAsync();
                        }
                    }
                }

                onDeleteSuccess?.Invoke();

                this.DialogResult = DialogResult.OK;
                this.Close();
            }
            catch (SqlException ex) when (ex.Number == 547)
            {
                string msg = "Cannot delete item due to database reference constraints.";
                switch (targetType)
                {
                    case ItemType.Category:
                        msg = "Cannot delete this category because it has associated products.";
                        break;
                    case ItemType.Product:
                        msg = "Cannot delete this product because it is referenced in active orders/sales.";
                        break;
                    case ItemType.Customer:
                        msg = "Cannot delete this customer because they have existing order histories.";
                        break;
                    case ItemType.Supplier:
                        msg = "Cannot delete this supplier because they have associated purchase orders or products.";
                        break;
                    case ItemType.Order:
                        msg = "Cannot delete this order because it is referenced elsewhere in the system.";
                        break;
                }

                MessageBox.Show(msg, "Delete Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Database error: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                btnConfirmDelete.Enabled = true;
            }
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}