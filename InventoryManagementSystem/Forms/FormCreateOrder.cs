using InventoryManagementSystem.Models;
using InventoryManagementSystem.Repositories;
using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace InventoryManagementSystem.Forms
{
    public partial class FormCreateOrder : Form
    {
        private readonly OrderRepository _repository = new OrderRepository();
        private readonly Action _onSaveSuccess;
        private readonly int? _orderId;
        private readonly bool _isViewOnly;

        private DataTable _dtCustomers;
        private DataTable _dtProducts;
        private bool _isLoadingData = false;

        // Field to store available stock for selected product
        private int _currentSelectedProductStock = 0;

        // Default Constructor
        public FormCreateOrder()
        {
            InitializeComponent();
            SetupForm();
        }

        // Create Mode Constructor
        public FormCreateOrder(Action onSaveSuccess) : this()
        {
            this._onSaveSuccess = onSaveSuccess;
        }

        // View Mode Constructor
        public FormCreateOrder(int orderId, bool isViewOnly = false) : this()
        {
            this._orderId = orderId;
            this._isViewOnly = isViewOnly;
        }

        // Edit Mode Constructor
        public FormCreateOrder(int orderId, Action onSaveSuccess) : this()
        {
            this._orderId = orderId;
            this._onSaveSuccess = onSaveSuccess;
        }

        private void SetupForm()
        {
            if (numDiscount != null)
            {
                numDiscount.Minimum = 0m;
                numDiscount.Maximum = 100m; // Assuming percentage discount (0% - 100%)
                numDiscount.DecimalPlaces = 2;
                numDiscount.ValueChanged += numDiscount_ValueChanged;
            }
            dtpOrderDate.Value = DateTime.Now;
            dtpOrderDate.FillColor = Color.White;
            dtpOrderDate.ForeColor = Color.Black;
            dtpOrderDate.BorderColor = Color.LightGray;

            txtTotalAmount.Text = "0.00";
            txtTotalAmount.ReadOnly = true;

            // Configure Price numeric control to support decimals
            if (numPrice != null)
            {
                numPrice.DecimalPlaces = 2; // Prevents rounding 2.5 to 3
                numPrice.Increment = 0.50m; // Optional step value for up/down buttons
                numPrice.ValueChanged += numPrice_ValueChanged;
            }

            // Wire up remaining event listeners for instant total calculation
            if (numQuantity != null) numQuantity.ValueChanged += numQuantity_ValueChanged;
            if (txtProduct != null) txtProduct.TextChanged += txtProduct_TextChanged;
            if (cmbCustomer != null) cmbCustomer.TextChanged += cmbCustomer_TextChanged;

            // Wire status change event to re-evaluate validation labels instantly
            if (cmbStatus != null) cmbStatus.SelectedIndexChanged += cmbStatus_SelectedIndexChanged;

            ClearValidationErrors();
        }

        private async void FormCreateOrder_Load(object sender, EventArgs e)
        {
            this.Size = new Size(this.Width, 760);

            // Styling Guna DatePicker
            dtpOrderDate.FillColor = Color.White;
            dtpOrderDate.CheckedState.FillColor = Color.White;
            dtpOrderDate.ForeColor = Color.Black;
            dtpOrderDate.CheckedState.ForeColor = Color.Black;
            dtpOrderDate.BorderColor = Color.FromArgb(217, 221, 226);
            dtpOrderDate.BorderThickness = 1;
            dtpOrderDate.BorderRadius = 5;

            PopulateStatusDropdown();

            // Load lookup data first
            await LoadDataAndSetupAutoCompleteAsync();

            // Load existing order data if editing or viewing
            if (_orderId.HasValue)
            {
                await LoadOrderDataAsync(_orderId.Value);

                if (_isViewOnly)
                {
                    ApplyViewOnlyMode();
                }
            }

            // Clear validation labels AFTER all data loading finishes
            ClearValidationErrors();
        }

        private void ClearValidationErrors()
        {
            if (lblCustomerRequired != null) lblCustomerRequired.Visible = false;
            if (lblProductRequired != null) lblProductRequired.Visible = false;
            if (lblPriceRequired != null) lblPriceRequired.Visible = false;
            if (lblQtyRequired != null) lblQtyRequired.Visible = false;
        }

        private void ApplyViewOnlyMode()
        {
            this.Text = "View Order";
            if (lblAddOrder != null) lblAddOrder.Text = "View Order";

            if (txtProduct != null) txtProduct.ReadOnly = true;
            if (txtDescription != null) txtDescription.ReadOnly = true;

            if (numPrice != null)
            {
                numPrice.UpDownButtonFillColor = Color.White;
                numPrice.UpDownButtonForeColor = Color.White;
            }

            if (numQuantity != null)
            {
                numQuantity.UpDownButtonFillColor = Color.White;
                numQuantity.UpDownButtonForeColor = Color.White;
            }

            if (numDiscount != null)
            {
                numDiscount.UpDownButtonFillColor = Color.White;
                numDiscount.UpDownButtonForeColor = Color.White;
            }

            LockGunaControl(cmbCustomer);
            LockGunaControl(dtpOrderDate);
            LockGunaControl(cmbStatus);
            LockGunaControl(numPrice);
            LockGunaControl(numQuantity);
            LockGunaControl(numDiscount);

            if (btnSave != null)
            {
                btnSave.Text = "Close";
                if (btnSave is Guna.UI2.WinForms.Guna2Button gunaBtn)
                {
                    gunaBtn.FillColor = Color.FromArgb(239, 68, 68);
                    gunaBtn.ForeColor = Color.White;
                }
            }

            if (btnCancel != null) btnCancel.Visible = false;
        }

        private void LockGunaControl(Control control)
        {
            if (control == null || control.Parent == null) return;

            control.Enabled = true;
            control.ForeColor = Color.Black;
            control.BackColor = Color.Transparent;

            Guna.UI2.WinForms.Guna2Panel blocker = new Guna.UI2.WinForms.Guna2Panel
            {
                Size = control.Size,
                Location = control.Location,
                BackColor = Color.Transparent,
                UseTransparentBackground = true
            };

            control.Parent.Controls.Add(blocker);
            blocker.BringToFront();
        }

        private async Task LoadOrderDataAsync(int orderId)
        {
            try
            {
                _isLoadingData = true;

                DataTable dtOrder = await _repository.GetOrderByIdAsync(orderId);
                if (dtOrder != null && dtOrder.Rows.Count > 0)
                {
                    DataRow row = dtOrder.Rows[0];

                    // 1. Customer Selection
                    if (cmbCustomer != null)
                    {
                        string customerVal = string.Empty;
                        if (dtOrder.Columns.Contains("CustomerName") && row["CustomerName"] != DBNull.Value)
                            customerVal = row["CustomerName"].ToString();
                        else if (dtOrder.Columns.Contains("Customer") && row["Customer"] != DBNull.Value)
                            customerVal = row["Customer"].ToString();

                        cmbCustomer.Text = customerVal;
                    }

                    // 2. Order Date
                    if (dtOrder.Columns.Contains("OrderDate") && row["OrderDate"] != DBNull.Value && dtpOrderDate != null)
                        dtpOrderDate.Value = Convert.ToDateTime(row["OrderDate"]);

                    // 3. Status
                    if (dtOrder.Columns.Contains("Status") && row["Status"] != DBNull.Value && cmbStatus != null)
                    {
                        string status = row["Status"].ToString();
                        if (cmbStatus.Items.Contains(status))
                            cmbStatus.SelectedItem = status;
                        else
                            cmbStatus.Text = status;
                    }

                    // 4. Discount
                    if (numDiscount != null && dtOrder.Columns.Contains("Discount") && row["Discount"] != DBNull.Value)
                    {
                        numDiscount.Value = Convert.ToDecimal(row["Discount"]);
                    }

                    // 5. Description
                    if (dtOrder.Columns.Contains("Description") && txtDescription != null)
                        txtDescription.Text = row["Description"]?.ToString();

                    // 6. Product Name
                    string productName = string.Empty;
                    if (txtProduct != null)
                    {
                        if (dtOrder.Columns.Contains("ProductName") && row["ProductName"] != DBNull.Value)
                            productName = row["ProductName"].ToString();
                        else if (dtOrder.Columns.Contains("Product") && row["Product"] != DBNull.Value)
                            productName = row["Product"].ToString();

                        txtProduct.Text = productName;
                    }

                    // 7. Quantity
                    int currentOrderQty = 0;
                    if (numQuantity != null)
                    {
                        if (dtOrder.Columns.Contains("Quantity") && row["Quantity"] != DBNull.Value)
                        {
                            currentOrderQty = Convert.ToInt32(row["Quantity"]);
                            numQuantity.Value = currentOrderQty;
                        }
                    }

                    // 8. Price
                    if (numPrice != null)
                    {
                        if (dtOrder.Columns.Contains("UnitPrice") && row["UnitPrice"] != DBNull.Value)
                            numPrice.Value = Convert.ToDecimal(row["UnitPrice"]);
                        else if (dtOrder.Columns.Contains("Price") && row["Price"] != DBNull.Value)
                            numPrice.Value = Convert.ToDecimal(row["Price"]);
                    }

                    // 9. Stock Allowance Calculation (Warehouse Stock + Existing Order Quantity)
                    if (_dtProducts != null && !string.IsNullOrWhiteSpace(productName))
                    {
                        string nameCol = _dtProducts.Columns.Contains("ProductName") ? "ProductName"
                                       : (_dtProducts.Columns.Contains("Product") ? "Product" : "Name");

                        DataRow productRow = _dtProducts.AsEnumerable()
                            .FirstOrDefault(r => r[nameCol].ToString().Equals(productName.Trim(), StringComparison.OrdinalIgnoreCase));

                        if (productRow != null && _dtProducts.Columns.Contains("Stock") && productRow["Stock"] != DBNull.Value)
                        {
                            int warehouseStock = Convert.ToInt32(productRow["Stock"]);
                            _currentSelectedProductStock = warehouseStock + currentOrderQty;
                        }
                        else
                        {
                            _currentSelectedProductStock = currentOrderQty;
                        }
                    }
                    else
                    {
                        _currentSelectedProductStock = currentOrderQty;
                    }

                    CalculateTotal();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error loading order details: {ex.Message}", "Database Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                _isLoadingData = false;
            }
        }

        private void PopulateStatusDropdown()
        {
            if (cmbStatus != null)
            {
                cmbStatus.Items.Clear();
                cmbStatus.Items.Add("Pending");
                cmbStatus.Items.Add("Processing");
                cmbStatus.Items.Add("Completed");
                cmbStatus.Items.Add("Cancelled");
                cmbStatus.SelectedIndex = 0;
            }
        }

        private async Task LoadDataAndSetupAutoCompleteAsync()
        {
            try
            {
                _dtCustomers = await _repository.GetCustomersLookupAsync();
                AutoCompleteStringCollection customerCollection = new AutoCompleteStringCollection();

                if (_dtCustomers != null)
                {
                    foreach (DataRow row in _dtCustomers.Rows)
                    {
                        string cName = _dtCustomers.Columns.Contains("CustomerName") ? row["CustomerName"].ToString() :
                                      (_dtCustomers.Columns.Contains("Customer") ? row["Customer"].ToString() :
                                      (_dtCustomers.Columns.Contains("Name") ? row["Name"].ToString() : string.Empty));

                        if (!string.IsNullOrEmpty(cName))
                            customerCollection.Add(cName);
                    }
                }

                if (cmbCustomer != null)
                {
                    cmbCustomer.AutoCompleteMode = AutoCompleteMode.SuggestAppend;
                    cmbCustomer.AutoCompleteSource = AutoCompleteSource.CustomSource;
                    cmbCustomer.AutoCompleteCustomSource = customerCollection;
                }

                _dtProducts = await _repository.GetProductsLookupAsync();
                AutoCompleteStringCollection productCollection = new AutoCompleteStringCollection();

                if (_dtProducts != null)
                {
                    foreach (DataRow row in _dtProducts.Rows)
                    {
                        string pName = _dtProducts.Columns.Contains("ProductName") ? row["ProductName"].ToString() :
                                      (_dtProducts.Columns.Contains("Product") ? row["Product"].ToString() :
                                      (_dtProducts.Columns.Contains("Name") ? row["Name"].ToString() : string.Empty));

                        if (!string.IsNullOrEmpty(pName))
                            productCollection.Add(pName);
                    }
                }

                if (txtProduct != null)
                {
                    txtProduct.AutoCompleteMode = AutoCompleteMode.SuggestAppend;
                    txtProduct.AutoCompleteSource = AutoCompleteSource.CustomSource;
                    txtProduct.AutoCompleteCustomSource = productCollection;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error loading lookup options: {ex.Message}", "Database Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void txtProduct_TextChanged(object sender, EventArgs e)
        {
            if (_isViewOnly || _isLoadingData) return;

            if (txtProduct != null && !string.IsNullOrWhiteSpace(txtProduct.Text))
            {
                if (lblProductRequired != null) lblProductRequired.Visible = false;

                if (_dtProducts != null)
                {
                    string nameCol = _dtProducts.Columns.Contains("ProductName") ? "ProductName"
                                   : (_dtProducts.Columns.Contains("Product") ? "Product" : "Name");

                    DataRow matchedRow = _dtProducts.AsEnumerable()
                        .FirstOrDefault(r => r[nameCol].ToString().Equals(txtProduct.Text.Trim(), StringComparison.OrdinalIgnoreCase));

                    if (matchedRow != null)
                    {
                        // Assign Unit Price
                        if (_dtProducts.Columns.Contains("Price") && matchedRow["Price"] != DBNull.Value)
                        {
                            numPrice.Value = Convert.ToDecimal(matchedRow["Price"]);
                        }

                        // Store Current Product Stock from lookup
                        if (_dtProducts.Columns.Contains("Stock") && matchedRow["Stock"] != DBNull.Value)
                        {
                            int availableStock = Convert.ToInt32(matchedRow["Stock"]);
                            _currentSelectedProductStock = availableStock;
                        }
                        else
                        {
                            _currentSelectedProductStock = -1; // -1 represents unknown/unrestricted stock
                        }

                        CalculateTotal();
                        ValidateInputs();
                        return;
                    }
                }
            }

            _currentSelectedProductStock = -1;
            if (lblQtyRequired != null) lblQtyRequired.Visible = false;
        }

        private void CalculateTotal()
        {
            decimal price = numPrice != null ? numPrice.Value : 0m;
            int quantity = numQuantity != null ? (int)numQuantity.Value : 0;
            decimal discountPercentage = numDiscount != null ? numDiscount.Value : 0m;

            decimal grossTotal = price * quantity;
            decimal discountAmount = grossTotal * (discountPercentage / 100m);
            decimal netTotal = Math.Max(0m, grossTotal - discountAmount);

            if (txtTotalAmount != null)
                txtTotalAmount.Text = netTotal.ToString("N2");
        }

        private async Task<int> GetOrCreateCustomerIdAsync()
        {
            if (cmbCustomer == null || string.IsNullOrWhiteSpace(cmbCustomer.Text))
                return 0;

            string customerName = cmbCustomer.Text.Trim();

            if (_dtCustomers != null)
            {
                string nameCol = _dtCustomers.Columns.Contains("CustomerName") ? "CustomerName" : (_dtCustomers.Columns.Contains("Customer") ? "Customer" : "Name");
                DataRow row = _dtCustomers.AsEnumerable()
                    .FirstOrDefault(r => r[nameCol].ToString().Equals(customerName, StringComparison.OrdinalIgnoreCase));

                if (row != null)
                    return Convert.ToInt32(row["CustomerID"]);
            }

            return await _repository.GetOrCreateCustomerByNameAsync(customerName);
        }

        private int GetSelectedProductId()
        {
            if (_dtProducts == null || txtProduct == null || string.IsNullOrWhiteSpace(txtProduct.Text))
                return 0;

            string nameCol = _dtProducts.Columns.Contains("ProductName") ? "ProductName" : (_dtProducts.Columns.Contains("Product") ? "Product" : "Name");
            DataRow row = _dtProducts.AsEnumerable()
                .FirstOrDefault(r => r[nameCol].ToString().Equals(txtProduct.Text.Trim(), StringComparison.OrdinalIgnoreCase));

            return row != null ? Convert.ToInt32(row["ProductID"]) : 0;
        }

        private bool ValidateInputs()
        {
            bool isValid = true;

            // 1. Customer Validation
            if (cmbCustomer == null || string.IsNullOrWhiteSpace(cmbCustomer.Text))
            {
                if (lblCustomerRequired != null)
                {
                    lblCustomerRequired.Text = "This field is required";
                    lblCustomerRequired.Visible = true;
                }
                isValid = false;
            }
            else if (lblCustomerRequired != null)
            {
                lblCustomerRequired.Visible = false;
            }

            // 2. Product Validation
            int productId = GetSelectedProductId();
            if (txtProduct == null || string.IsNullOrWhiteSpace(txtProduct.Text) || productId == 0)
            {
                if (lblProductRequired != null)
                {
                    lblProductRequired.Text = productId == 0 && !string.IsNullOrWhiteSpace(txtProduct?.Text)
                        ? "Product not found"
                        : "This field is required";
                    lblProductRequired.Visible = true;
                }
                isValid = false;
            }
            else if (lblProductRequired != null)
            {
                lblProductRequired.Visible = false;
            }

            // 3. Price Validation
            if (numPrice == null || numPrice.Value <= 0)
            {
                if (lblPriceRequired != null)
                {
                    lblPriceRequired.Text = "Must be greater than 0";
                    lblPriceRequired.Visible = true;
                }
                isValid = false;
            }
            else if (lblPriceRequired != null)
            {
                lblPriceRequired.Visible = false;
            }

            // 4. Quantity & Stock Validation
            int requestedQty = numQuantity != null ? (int)numQuantity.Value : 0;

            if (requestedQty <= 0)
            {
                if (lblQtyRequired != null)
                {
                    lblQtyRequired.Text = "Must be greater than 0";
                    lblQtyRequired.ForeColor = System.Drawing.Color.Red;
                    lblQtyRequired.Visible = true;
                }
                isValid = false;
            }
            else if (productId > 0 && _currentSelectedProductStock >= 0 && requestedQty > _currentSelectedProductStock)
            {
                if (lblQtyRequired != null)
                {
                    lblQtyRequired.Text = $"Not enough stock!";
                    lblQtyRequired.ForeColor = System.Drawing.Color.Red;
                    lblQtyRequired.Visible = true;
                }
                isValid = false;
            }
            else if (lblQtyRequired != null)
            {
                lblQtyRequired.Visible = false;
            }

            return isValid;
        }

        private async void btnSave_Click(object sender, EventArgs e)
        {
            if (_isViewOnly || (btnSave != null && btnSave.Text == "Close"))
            {
                this.Close();
                return;
            }

            if (!ValidateInputs()) return;

            if (btnSave != null) btnSave.Enabled = false;

            try
            {
                int selectedCustomerId = await GetOrCreateCustomerIdAsync();
                int selectedProductId = GetSelectedProductId();

                decimal price = numPrice != null ? numPrice.Value : 0m;
                int quantity = numQuantity != null ? (int)numQuantity.Value : 0;
                decimal discount = numDiscount != null ? numDiscount.Value : 0m;
                decimal grossTotal = price * quantity;
                decimal finalTotal = Math.Max(0m, grossTotal - (grossTotal * (discount / 100m)));

                Order orderData = new Order
                {
                    OrderID = _orderId ?? 0,
                    CustomerID = selectedCustomerId,
                    OrderDate = dtpOrderDate.Value,
                    Discount = discount,
                    TotalAmount = finalTotal,
                    Status = cmbStatus?.Text ?? "Pending",
                    Description = txtDescription != null ? txtDescription.Text.Trim() : string.Empty
                };

                OrderDetail detailData = new OrderDetail
                {
                    OrderID = _orderId ?? 0,
                    ProductID = selectedProductId,
                    Quantity = quantity,
                    UnitPrice = price
                };

                bool success = _orderId.HasValue
                    ? await _repository.UpdateOrderAsync(orderData, detailData)
                    : await _repository.CreateOrderAndReduceStockAsync(orderData, detailData);

                if (success)
                {
                    _onSaveSuccess?.Invoke();
                    this.DialogResult = DialogResult.OK;
                    this.Close();
                }
            }
            catch (InvalidOperationException ex)
            {
                if (lblQtyRequired != null)
                {
                    lblQtyRequired.Text = ex.Message;
                    lblQtyRequired.ForeColor = System.Drawing.Color.Red;
                    lblQtyRequired.Visible = true;
                }
            }
            catch (Exception ex)
            {
                if (lblQtyRequired != null)
                {
                    lblQtyRequired.Text = ex.Message;
                    lblQtyRequired.ForeColor = System.Drawing.Color.Red;
                    lblQtyRequired.Visible = true;
                }
            }
            finally
            {
                if (btnSave != null) btnSave.Enabled = true;
            }
        }

        private void numPrice_ValueChanged(object sender, EventArgs e)
        {
            if (numPrice.Value > 0 && lblPriceRequired != null) lblPriceRequired.Visible = false;
            CalculateTotal();
        }

        private void numQuantity_ValueChanged(object sender, EventArgs e)
        {
            if (_isViewOnly || _isLoadingData) return;

            ValidateInputs();
            CalculateTotal();
        }

        private void cmbStatus_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (_isViewOnly || _isLoadingData) return;

            ValidateInputs();
        }

        private void cmbCustomer_TextChanged(object sender, EventArgs e)
        {
            if (cmbCustomer != null && !string.IsNullOrWhiteSpace(cmbCustomer.Text) && lblCustomerRequired != null)
            {
                lblCustomerRequired.Visible = false;
            }
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void numDiscount_ValueChanged(object sender, EventArgs e)
        {
            if (_isViewOnly || _isLoadingData) return;

            CalculateTotal();
        }
    }
}