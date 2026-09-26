using InventoryManagementSystem.Models;
using InventoryManagementSystem.Repositories;
using iTextSharp.text;
using iTextSharp.text.pdf;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Drawing.Printing;
using System.IO;
using System.Threading.Tasks;
using System.Windows.Forms;
using PdfFont = iTextSharp.text.Font;
using PdfRectangle = iTextSharp.text.Rectangle;

namespace InventoryManagementSystem.Forms
{
    public partial class FormInvoices : Form
    {
        private readonly InvoiceRepository _invoiceRepository = new InvoiceRepository();

        public FormInvoices()
        {
            InitializeComponent();

            if (!DesignMode && LicenseManager.UsageMode != LicenseUsageMode.Designtime)
            {
                ApplyDgvOrderStyling();
                ApplyDgvItemsStyling();
            }
        }

        private async void FormInvoices_Load(object sender, EventArgs e)
        {
            ConfigureDgvOrderColumns();
            ConfigureDgvItemsColumns();

            // Wire up DataGridView event handlers
            dgvOrder.CellFormatting += dgvOrder_CellFormatting;
            dgvOrder.Paint += dgvOrder_Paint;
            dgvItems.CellFormatting += dgvItems_CellFormatting;

            // Prevent header clicks from selecting headers
            dgvOrder.ColumnHeaderMouseClick += (s, ev) =>
            {
                dgvOrder.ClearSelection();
                dgvOrder.CurrentCell = null;
            };

            // Search input live filtering
            txtSearch.TextChanged += async (s, ev) => await LoadInvoicesDataAsync();
            btnSearch.Click += async (s, ev) => await LoadInvoicesDataAsync();

            // View Invoice Details
            btnView.Click += async (s, ev) =>
            {
                if (dgvOrder.SelectedRows.Count > 0 &&
                    dgvOrder.SelectedRows[0].DataBoundItem is InvoiceHeader selectedInvoice)
                {
                    await DisplayInvoiceDetailsAsync(selectedInvoice.InvoiceID);
                }
                else
                {
                    MessageBox.Show("Please select an invoice row from the list first.", "Select Invoice", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            };

            // CLOSE BUTTON: Clear details and completely remove row selection and active cell cursor
            btnClose.Click += (s, ev) =>
            {
                ClearInvoiceDetails();
                dgvOrder.ClearSelection();
                dgvOrder.CurrentCell = null;
            };

            // Ensure details panel starts completely empty
            ClearInvoiceDetails();

            // Load initial invoice list
            await LoadInvoicesDataAsync();
        }

        private void ConfigureDgvOrderColumns()
        {
            if (LicenseManager.UsageMode == LicenseUsageMode.Designtime || DesignMode)
                return;

            dgvOrder.AutoGenerateColumns = false;

            foreach (DataGridViewColumn col in dgvOrder.Columns)
            {
                if (col == null) continue;

                string header = (col.HeaderText ?? "").Replace(" ", "").ToLower();
                string name = (col.Name ?? "").ToLower();

                if (name.Contains("id") || header.Contains("id"))
                    col.DataPropertyName = "InvoiceID";
                else if (name.Contains("date") || header.Contains("date"))
                    col.DataPropertyName = "Date";
                else if (name.Contains("customer") || header.Contains("customer"))
                    col.DataPropertyName = "CustomerName";
                else if (name.Contains("amount") || header.Contains("amount") || header.Contains("total"))
                    col.DataPropertyName = "TotalAmount";
                else if (name.Contains("status") || header.Contains("status"))
                    col.DataPropertyName = "Status";
            }
        }

        private void ConfigureDgvItemsColumns()
        {
            dgvItems.AutoGenerateColumns = false;

            foreach (DataGridViewColumn col in dgvItems.Columns)
            {
                string header = (col.HeaderText ?? "").Replace(" ", "").ToLower();
                string name = (col.Name ?? "").ToLower();

                if (name.Contains("no") || header.Contains("#") || header.Contains("itemno"))
                    col.DataPropertyName = "ItemNo";
                else if (name.Contains("product") || header.Contains("product"))
                    col.DataPropertyName = "ProductName";
                else if (name.Contains("quantity") || header.Contains("quant"))
                    col.DataPropertyName = "Quantity";
                else if (name.Contains("price") || header.Contains("price"))
                    col.DataPropertyName = "UnitPrice";
                else if (name.Contains("total") || header.Contains("total"))
                    col.DataPropertyName = "Total";
            }
        }

        public async Task LoadInvoicesDataAsync()
        {
            try
            {
                string search = txtSearch.Text.Trim();
                List<InvoiceHeader> invoices = await _invoiceRepository.GetInvoicesAsync(search, filterDate: null);

                dgvOrder.DataSource = null;
                dgvOrder.DataSource = invoices;

                // Unset active cell and selection so no row is selected by default on load/search
                dgvOrder.ClearSelection();
                dgvOrder.CurrentCell = null;

                // Keep details clear when refreshing or searching
                ClearInvoiceDetails();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Database Error: {ex.Message}", "Error Loading Data", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void dgvOrder_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
        {
            if (e.RowIndex < 0 || e.Value == null || e.Value == DBNull.Value) return;

            string colName = dgvOrder.Columns[e.ColumnIndex].Name;
            string colHeader = dgvOrder.Columns[e.ColumnIndex].HeaderText;

            // Format InvoiceID
            if (colName.Equals("InvoiceID", StringComparison.OrdinalIgnoreCase) ||
                colHeader.Equals("Invoice ID", StringComparison.OrdinalIgnoreCase))
            {
                if (int.TryParse(e.Value.ToString(), out int id))
                {
                    e.Value = $"INV-{id:D4}";
                    e.FormattingApplied = true;
                    return;
                }
            }

            // Format Date
            if (colName.Equals("Date", StringComparison.OrdinalIgnoreCase) ||
                colHeader.Equals("Date", StringComparison.OrdinalIgnoreCase))
            {
                if (DateTime.TryParse(e.Value.ToString(), out DateTime dt))
                {
                    e.Value = dt.ToString("M/d/yyyy");
                    e.FormattingApplied = true;
                    return;
                }
            }

            // Format Total Amount
            if (colName.Equals("TotalAmount", StringComparison.OrdinalIgnoreCase) ||
                colHeader.Equals("Total Amount", StringComparison.OrdinalIgnoreCase))
            {
                if (decimal.TryParse(e.Value.ToString(), out decimal amount))
                {
                    e.Value = $"{amount:N2} $";
                    e.FormattingApplied = true;
                    return;
                }
            }

            // Format Status Colors
            if (colName.Equals("Status", StringComparison.OrdinalIgnoreCase) ||
                colHeader.Equals("Status", StringComparison.OrdinalIgnoreCase))
            {
                string status = e.Value.ToString().Trim();
                e.CellStyle.Font = new System.Drawing.Font("Segoe UI", 9.5f, System.Drawing.FontStyle.Bold);

                switch (status.ToLower())
                {
                    case "paid":
                        e.CellStyle.ForeColor = System.Drawing.Color.Green;
                        e.CellStyle.SelectionForeColor = System.Drawing.Color.Green;
                        break;
                    case "pending":
                    case "processing":
                        e.CellStyle.ForeColor = System.Drawing.Color.DarkOrange;
                        e.CellStyle.SelectionForeColor = System.Drawing.Color.DarkOrange;
                        break;
                    case "cancelled":
                    case "canceled":
                        e.CellStyle.ForeColor = System.Drawing.Color.Red;
                        e.CellStyle.SelectionForeColor = System.Drawing.Color.Red;
                        break;
                }
            }
        }

        private void dgvOrder_Paint(object sender, PaintEventArgs e)
        {
            if (dgvOrder.Rows.Count == 0)
            {
                string message = "No invoices found";
                e.Graphics.TextRenderingHint = System.Drawing.Text.TextRenderingHint.ClearTypeGridFit;

                using (System.Drawing.Font font = new System.Drawing.Font("Segoe UI", 11F, System.Drawing.FontStyle.Regular))
                {
                    System.Drawing.Size textSize = TextRenderer.MeasureText(message, font);
                    int x = (dgvOrder.Width - textSize.Width) / 2;
                    int y = dgvOrder.ColumnHeadersHeight + 40;
                    System.Drawing.Point point = new System.Drawing.Point(x, y);
                    System.Drawing.Color textMutedColor = System.Drawing.Color.FromArgb(100, 116, 139);

                    TextRenderer.DrawText(e.Graphics, message, font, point, textMutedColor);
                }
            }
        }

        private void ApplyDgvOrderStyling()
        {
            dgvOrder.EnableHeadersVisualStyles = false;
            dgvOrder.ColumnHeadersDefaultCellStyle.BackColor = System.Drawing.Color.FromArgb(30, 144, 255);
            dgvOrder.ColumnHeadersDefaultCellStyle.ForeColor = System.Drawing.Color.White;
            dgvOrder.ColumnHeadersDefaultCellStyle.Font = new System.Drawing.Font("Segoe UI", 11f, System.Drawing.FontStyle.Bold);

            dgvOrder.ColumnHeadersDefaultCellStyle.SelectionBackColor = System.Drawing.Color.FromArgb(30, 144, 255);
            dgvOrder.ColumnHeadersDefaultCellStyle.SelectionForeColor = System.Drawing.Color.White;

            dgvOrder.ColumnHeadersHeight = 38;
            dgvOrder.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.DisableResizing;
            dgvOrder.ColumnHeadersDefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleLeft;

            dgvOrder.RowTemplate.Height = 42;
            dgvOrder.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleLeft;

            dgvOrder.DefaultCellStyle.Font = new System.Drawing.Font("Segoe UI", 11f, System.Drawing.FontStyle.Regular);
            dgvOrder.DefaultCellStyle.SelectionBackColor = System.Drawing.Color.FromArgb(235, 243, 255);
            dgvOrder.DefaultCellStyle.SelectionForeColor = System.Drawing.Color.Black;
            dgvOrder.RowHeadersVisible = false;

            dgvOrder.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dgvOrder.AllowUserToAddRows = false;
            dgvOrder.AllowUserToResizeColumns = false;
            dgvOrder.AllowUserToResizeRows = false;

            foreach (DataGridViewColumn col in dgvOrder.Columns)
            {
                col.SortMode = DataGridViewColumnSortMode.NotSortable;
            }
        }

        private void ApplyDgvItemsStyling()
        {
            dgvItems.AutoGenerateColumns = false;

            // Remove horizontal grid lines / cell borders
            dgvItems.CellBorderStyle = DataGridViewCellBorderStyle.None;
            dgvItems.ColumnHeadersBorderStyle = DataGridViewHeaderBorderStyle.None;
            dgvItems.AdvancedColumnHeadersBorderStyle.Bottom = DataGridViewAdvancedCellBorderStyle.None;

            // Soft pastel header styling
            dgvItems.EnableHeadersVisualStyles = false;
            dgvItems.ColumnHeadersDefaultCellStyle.BackColor = System.Drawing.Color.FromArgb(232, 240, 254);
            dgvItems.ColumnHeadersDefaultCellStyle.ForeColor = System.Drawing.Color.FromArgb(45, 70, 110);
            dgvItems.ColumnHeadersDefaultCellStyle.Font = new System.Drawing.Font("Segoe UI", 9.7f, System.Drawing.FontStyle.Bold);
            dgvItems.ColumnHeadersHeight = 42;
            dgvItems.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.DisableResizing;
            dgvItems.ColumnHeadersDefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleLeft;

            // Data row sizing & font
            dgvItems.RowTemplate.Height = 45;
            dgvItems.DefaultCellStyle.Font = new System.Drawing.Font("Segoe UI", 11.5f, System.Drawing.FontStyle.Regular);
            dgvItems.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleLeft;

            // Selection & grid layout
            dgvItems.AllowUserToResizeColumns = false;
            dgvItems.AllowUserToResizeRows = false;
            dgvItems.SelectionMode = DataGridViewSelectionMode.CellSelect;
            dgvItems.MultiSelect = false;
            dgvItems.DefaultCellStyle.SelectionBackColor = dgvItems.DefaultCellStyle.BackColor;
            dgvItems.DefaultCellStyle.SelectionForeColor = dgvItems.DefaultCellStyle.ForeColor;
            dgvItems.RowHeadersVisible = false;
        }

        private async Task DisplayInvoiceDetailsAsync(int orderId)
        {
            try
            {
                var details = await _invoiceRepository.GetInvoiceDetailsAsync(orderId);

                if (details != null)
                {
                    lblInvoiceIdValue.Text = $"INV-{details.InvoiceID:D4}";
                    lblDateValue.Text = details.Date.ToString("M/d/yyyy h:mm tt");
                    lblCustomerValue.Text = details.CustomerName;
                    lblPaymentMethodValue.Text = string.IsNullOrEmpty(details.PaymentMethod) ? "Cash" : details.PaymentMethod;

                    // Header Total Amount & Items Footer Total Amount
                    lblTotalAmountValue.Text = $"${details.TotalAmount:N2}";
                    lblItemTotalAmountValue.Text = $"${details.TotalAmount:N2}";

                    lblDiscountValue.Text = $"{details.DiscountPercent:0}%";
                    lblNotesValue.Text = string.IsNullOrWhiteSpace(details.Notes) ? "Thank you for choosing us. \nWe appreciate your support!" : details.Notes;
                    lblCreatedByValue.Text = string.IsNullOrEmpty(details.CreatedBy) ? "Admin" : details.CreatedBy;
                    lblCreatedAtValue.Text = details.CreatedAt.ToString("M/d/yyyy h:mm tt");

                    // Show/Enable Paid badge
                    lblStatusBadge.Text = "Paid";
                    lblStatusBadge.Visible = true;

                    // Load items for selected invoice
                    var items = await _invoiceRepository.GetInvoiceItemsAsync(orderId);
                    dgvItems.DataSource = null;
                    dgvItems.DataSource = items;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error loading details: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                ClearInvoiceDetails();
            }
        }

        private void ClearInvoiceDetails()
        {
            lblInvoiceIdValue.Text = "-";
            lblDateValue.Text = "-";
            lblCustomerValue.Text = "-";
            lblPaymentMethodValue.Text = "-";
            lblTotalAmountValue.Text = "$0.00";
            lblItemTotalAmountValue.Text = "$0.00";
            lblDiscountValue.Text = "0%";
            lblNotesValue.Text = "-";
            lblCreatedByValue.Text = "-";
            lblCreatedAtValue.Text = "-";

            // Hide status badge when cleared
            lblStatusBadge.Text = "-";
            lblStatusBadge.Visible = false;

            dgvItems.DataSource = null;
        }

        private void dgvItems_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
        {
            if (e.RowIndex < 0 || e.Value == null || e.Value == DBNull.Value) return;

            string colName = dgvItems.Columns[e.ColumnIndex].Name;
            string colHeader = dgvItems.Columns[e.ColumnIndex].HeaderText;

            // Format Unit Price and Total columns to include '$' at the back
            if (colName.Equals("UnitPrice", StringComparison.OrdinalIgnoreCase) ||
                colName.Equals("Total", StringComparison.OrdinalIgnoreCase) ||
                colHeader.Equals("Unit Price", StringComparison.OrdinalIgnoreCase) ||
                colHeader.Equals("Total", StringComparison.OrdinalIgnoreCase))
            {
                if (decimal.TryParse(e.Value.ToString(), out decimal amount))
                {
                    e.Value = $"{amount:N2} $";
                    e.FormattingApplied = true;
                }
            }
        }

        private async void btnDownloadPdf_Click(object sender, EventArgs e)
        {
            // Check if an invoice is currently loaded
            if (lblInvoiceIdValue.Text == "-" || string.IsNullOrEmpty(lblInvoiceIdValue.Text))
            {
                MessageBox.Show("Please select an invoice to download.", "No Invoice Selected", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            using (SaveFileDialog saveFileDialog = new SaveFileDialog())
            {
                saveFileDialog.Filter = "PDF Files (*.pdf)|*.pdf";
                saveFileDialog.FileName = $"Invoice_{lblInvoiceIdValue.Text}.pdf";
                saveFileDialog.Title = "Save Invoice PDF";

                if (saveFileDialog.ShowDialog() == DialogResult.OK)
                {
                    try
                    {
                        this.Cursor = Cursors.WaitCursor;
                        string filePath = saveFileDialog.FileName;

                        await GenerateInvoicePdfAsync(filePath);

                        this.Cursor = Cursors.Default;

                        var result = MessageBox.Show(
                            "Invoice PDF generated successfully!\nDo you want to open it now?",
                            "Success",
                            MessageBoxButtons.YesNo,
                            MessageBoxIcon.Information);

                        if (result == DialogResult.Yes)
                        {
                            System.Diagnostics.Process.Start(new System.Diagnostics.ProcessStartInfo(filePath) { UseShellExecute = true });
                        }
                    }
                    catch (Exception ex)
                    {
                        this.Cursor = Cursors.Default;
                        MessageBox.Show($"Failed to generate PDF: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
            }
        }

        private async Task GenerateInvoicePdfAsync(string filePath)
        {
            string invoiceId = lblInvoiceIdValue.Text;
            string dateStr = lblDateValue.Text;
            string payment = lblPaymentMethodValue.Text;
            string customer = lblCustomerValue.Text;
            string createdBy = lblCreatedByValue.Text;
            string status = lblStatusBadge.Text;
            string discount = lblDiscountValue.Text;
            string totalAmount = lblTotalAmountValue.Text;
            string notesStr = lblNotesValue.Text;

            List<string[]> tableRows = new List<string[]>();
            int itemNo = 1;

            foreach (DataGridViewRow row in dgvItems.Rows)
            {
                if (row.IsNewRow) continue;

                string pName = row.Cells[1].Value?.ToString() ?? "";
                string qty = row.Cells[2].Value?.ToString() ?? "";
                string priceRaw = row.Cells[3].Value?.ToString().Replace("$", "").Trim() ?? "0";
                string totalRaw = row.Cells[4].Value?.ToString().Replace("$", "").Trim() ?? "0";

                if (decimal.TryParse(priceRaw, out decimal priceVal))
                    priceRaw = $"${priceVal:N2}";
                else
                    priceRaw = $"${priceRaw}";

                if (decimal.TryParse(totalRaw, out decimal totalVal))
                    totalRaw = $"${totalVal:N2}";
                else
                    totalRaw = $"${totalRaw}";

                tableRows.Add(new string[] { itemNo.ToString(), pName, qty, priceRaw, totalRaw });
                itemNo++;
            }

            await Task.Run(() =>
            {
                Document document = new Document(PageSize.A4, 45, 45, 45, 45);

                using (FileStream fs = new FileStream(filePath, FileMode.Create, FileAccess.Write, FileShare.None))
                {
                    PdfWriter writer = PdfWriter.GetInstance(document, fs);
                    document.Open();

                    BaseColor primaryBlue = new BaseColor(30, 144, 255);
                    BaseColor darkBlue = new BaseColor(30, 64, 95);
                    BaseColor textColor = new BaseColor(51, 65, 85);
                    BaseColor secondaryText = new BaseColor(100, 116, 139);
                    BaseColor lightBlue = new BaseColor(239, 246, 255);
                    BaseColor headerBg = new BaseColor(241, 245, 249);
                    BaseColor alternateRowBg = new BaseColor(248, 250, 252);
                    BaseColor borderColor = new BaseColor(226, 232, 240);

                    BaseFont bf = BaseFont.CreateFont(BaseFont.HELVETICA, BaseFont.CP1252, BaseFont.NOT_EMBEDDED);

                    PdfFont invoiceFont = new PdfFont(bf, 26, PdfFont.BOLD, primaryBlue);
                    PdfFont systemFont = new PdfFont(bf, 9, PdfFont.NORMAL, secondaryText);
                    PdfFont labelFont = new PdfFont(bf, 8, PdfFont.BOLD, secondaryText);
                    PdfFont valueFont = new PdfFont(bf, 10, PdfFont.NORMAL, textColor);
                    PdfFont headerFont = new PdfFont(bf, 9, PdfFont.BOLD, textColor);
                    PdfFont tableFont = new PdfFont(bf, 9, PdfFont.NORMAL, textColor);
                    PdfFont totalLabelFont = new PdfFont(bf, 11, PdfFont.BOLD, textColor);
                    PdfFont totalFont = new PdfFont(bf, 15, PdfFont.BOLD, primaryBlue);
                    PdfFont noteFont = new PdfFont(bf, 9, PdfFont.NORMAL, secondaryText);

                    PdfPTable topTable = new PdfPTable(2) { WidthPercentage = 100 };
                    topTable.SetWidths(new float[] { 60f, 40f });

                    PdfPCell titleCell = new PdfPCell { Border = PdfRectangle.NO_BORDER, Padding = 0 };
                    titleCell.AddElement(new Paragraph("INVOICE", invoiceFont) { SpacingAfter = 3 });
                    titleCell.AddElement(new Paragraph("Inventory Management System", systemFont));

                    PdfPCell invoiceInfoCell = new PdfPCell { Border = PdfRectangle.NO_BORDER, Padding = 0, HorizontalAlignment = Element.ALIGN_RIGHT };
                    invoiceInfoCell.AddElement(new Paragraph($"Invoice #{invoiceId}", new PdfFont(bf, 11, PdfFont.BOLD, darkBlue)) { Alignment = Element.ALIGN_RIGHT, SpacingAfter = 4 });
                    invoiceInfoCell.AddElement(new Paragraph(dateStr, systemFont) { Alignment = Element.ALIGN_RIGHT });

                    topTable.AddCell(titleCell);
                    topTable.AddCell(invoiceInfoCell);
                    document.Add(topTable);

                    PdfPTable lineTable = new PdfPTable(1) { WidthPercentage = 100, SpacingBefore = 12, SpacingAfter = 20 };
                    PdfPCell lineCell = new PdfPCell { BackgroundColor = primaryBlue, FixedHeight = 2, Border = PdfRectangle.NO_BORDER };
                    lineTable.AddCell(lineCell);
                    document.Add(lineTable);

                    PdfPTable infoTable = new PdfPTable(3) { WidthPercentage = 100 };
                    infoTable.SetWidths(new float[] { 34f, 33f, 33f });

                    PdfPCell customerCell = new PdfPCell { Border = PdfRectangle.NO_BORDER, Padding = 8, BackgroundColor = lightBlue };
                    customerCell.AddElement(new Paragraph("CUSTOMER", labelFont));
                    customerCell.AddElement(new Paragraph(customer, valueFont));

                    PdfPCell paymentCell = new PdfPCell { Border = PdfRectangle.NO_BORDER, Padding = 8, BackgroundColor = lightBlue };
                    paymentCell.AddElement(new Paragraph("PAYMENT METHOD", labelFont));
                    paymentCell.AddElement(new Paragraph(payment, valueFont));

                    PdfPCell createdCell = new PdfPCell { Border = PdfRectangle.NO_BORDER, Padding = 8, BackgroundColor = lightBlue };
                    createdCell.AddElement(new Paragraph("CREATED BY", labelFont));
                    createdCell.AddElement(new Paragraph(createdBy, valueFont));

                    infoTable.AddCell(customerCell);
                    infoTable.AddCell(paymentCell);
                    infoTable.AddCell(createdCell);
                    infoTable.SpacingAfter = 20;
                    document.Add(infoTable);

                    PdfPTable statusTable = new PdfPTable(1) { WidthPercentage = 100, SpacingAfter = 15 };
                    PdfPCell statusCell = new PdfPCell { Border = PdfRectangle.NO_BORDER, PaddingLeft = 0, PaddingBottom = 4 };

                    Paragraph statusParagraph = new Paragraph();
                    statusParagraph.Add(new Chunk("STATUS   ", new PdfFont(bf, 9, PdfFont.BOLD, secondaryText)));
                    statusParagraph.Add(new Chunk(status, new PdfFont(bf, 9, PdfFont.BOLD, new BaseColor(34, 197, 94))));

                    statusCell.AddElement(statusParagraph);
                    statusTable.AddCell(statusCell);
                    document.Add(statusTable);

                    PdfPTable itemsTable = new PdfPTable(5) { WidthPercentage = 100 };
                    itemsTable.SetWidths(new float[] { 7f, 45f, 14f, 17f, 17f });

                    string[] headers = { "#", "PRODUCT", "QUANTITY", "UNIT PRICE", "TOTAL" };

                    foreach (string header in headers)
                    {
                        PdfPCell cell = new PdfPCell(new Phrase(header, headerFont))
                        {
                            BackgroundColor = headerBg,
                            BorderColor = borderColor,
                            BorderWidth = 0.8f,
                            PaddingTop = 9,
                            PaddingBottom = 9,
                            PaddingLeft = 7,
                            PaddingRight = 7,
                            VerticalAlignment = Element.ALIGN_MIDDLE
                        };

                        if (header == "#" || header == "QUANTITY" || header == "UNIT PRICE" || header == "TOTAL")
                            cell.HorizontalAlignment = Element.ALIGN_RIGHT;

                        itemsTable.AddCell(cell);
                    }

                    bool alternate = false;

                    foreach (var rowData in tableRows)
                    {
                        BaseColor rowColor = alternate ? alternateRowBg : BaseColor.WHITE;

                        for (int col = 0; col < rowData.Length; col++)
                        {
                            PdfPCell cell = new PdfPCell(new Phrase(rowData[col], tableFont))
                            {
                                BackgroundColor = rowColor,
                                BorderColor = borderColor,
                                BorderWidth = 0.6f,
                                PaddingTop = 9,
                                PaddingBottom = 9,
                                PaddingLeft = 7,
                                PaddingRight = 7,
                                VerticalAlignment = Element.ALIGN_MIDDLE
                            };

                            if (col == 0)
                                cell.HorizontalAlignment = Element.ALIGN_CENTER;
                            else if (col >= 2)
                                cell.HorizontalAlignment = Element.ALIGN_RIGHT;
                            else
                                cell.HorizontalAlignment = Element.ALIGN_LEFT;

                            itemsTable.AddCell(cell);
                        }

                        alternate = !alternate;
                    }

                    itemsTable.SpacingAfter = 20;
                    document.Add(itemsTable);

                    PdfPTable summaryTable = new PdfPTable(2) { WidthPercentage = 42, HorizontalAlignment = Element.ALIGN_RIGHT };
                    summaryTable.SetWidths(new float[] { 55f, 45f });

                    PdfPCell discountLabel = new PdfPCell(new Phrase("Discount", valueFont))
                    {
                        Border = PdfRectangle.NO_BORDER,
                        Padding = 5,
                        HorizontalAlignment = Element.ALIGN_RIGHT
                    };

                    PdfPCell discountValue = new PdfPCell(new Phrase(discount, valueFont))
                    {
                        Border = PdfRectangle.NO_BORDER,
                        Padding = 5,
                        HorizontalAlignment = Element.ALIGN_RIGHT
                    };

                    summaryTable.AddCell(discountLabel);
                    summaryTable.AddCell(discountValue);

                    PdfPCell separator = new PdfPCell
                    {
                        Colspan = 2,
                        Border = PdfRectangle.NO_BORDER,
                        BorderWidthTop = 0.8f,
                        BorderColorTop = borderColor,
                        PaddingTop = 5,
                        PaddingBottom = 5
                    };

                    summaryTable.AddCell(separator);

                    PdfPCell totalLabel = new PdfPCell(new Phrase("TOTAL AMOUNT", totalLabelFont))
                    {
                        Border = PdfRectangle.NO_BORDER,
                        Padding = 6,
                        HorizontalAlignment = Element.ALIGN_RIGHT
                    };

                    PdfPCell totalValue = new PdfPCell(new Phrase(totalAmount, totalFont))
                    {
                        Border = PdfRectangle.NO_BORDER,
                        Padding = 6,
                        HorizontalAlignment = Element.ALIGN_RIGHT
                    };

                    summaryTable.AddCell(totalLabel);
                    summaryTable.AddCell(totalValue);
                    summaryTable.SpacingAfter = 25;
                    document.Add(summaryTable);

                    if (!string.IsNullOrWhiteSpace(notesStr))
                    {
                        PdfPTable notesTable = new PdfPTable(1) { WidthPercentage = 100 };

                        PdfPCell notesCell = new PdfPCell
                        {
                            BackgroundColor = new BaseColor(248, 250, 252),
                            BorderColor = borderColor,
                            BorderWidth = 0.7f,
                            Padding = 10
                        };

                        notesCell.AddElement(new Paragraph("NOTES", labelFont) { SpacingAfter = 4 });
                        notesCell.AddElement(new Paragraph(notesStr, noteFont));

                        notesTable.AddCell(notesCell);
                        document.Add(notesTable);
                    }

                    Paragraph footer = new Paragraph("Thank you for choosing us. We appreciate your support!", new PdfFont(bf, 9, PdfFont.NORMAL, secondaryText))
                    {
                        Alignment = Element.ALIGN_CENTER,
                        SpacingBefore = 25
                    };

                    document.Add(footer);
                    document.Close();
                }
            });
        }

        private PdfPCell CreatePdfCell(string text, PdfFont font)
        {
            return new PdfPCell(new Phrase(text, font))
            {
                Border = PdfRectangle.NO_BORDER,
                Padding = 4
            };
        }
        private void btnPrint_Click(object sender, EventArgs e)
        {
            if (lblInvoiceIdValue.Text == "-" || string.IsNullOrEmpty(lblInvoiceIdValue.Text))
            {
                MessageBox.Show("Please select an invoice to print.", "No Invoice Selected", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            using (PrintDialog printDialog = new PrintDialog())
            {
                PrintDocument printDoc = new PrintDocument();
                printDoc.PrintPage += PrintDocument_PrintPage;
                printDialog.Document = printDoc;

                if (printDialog.ShowDialog() == DialogResult.OK)
                {
                    try
                    {
                        printDoc.Print();
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show($"Printing error: {ex.Message}", "Print Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
            }
        }

        private void PrintDocument_PrintPage(object sender, PrintPageEventArgs e)
        {
            System.Drawing.Graphics g = e.Graphics;
            System.Drawing.Font titleFont = new System.Drawing.Font("Segoe UI", 18, System.Drawing.FontStyle.Bold);
            System.Drawing.Font headerFont = new System.Drawing.Font("Segoe UI", 10, System.Drawing.FontStyle.Bold);
            System.Drawing.Font regularFont = new System.Drawing.Font("Segoe UI", 10, System.Drawing.FontStyle.Regular);
            System.Drawing.Font totalFont = new System.Drawing.Font("Segoe UI", 12, System.Drawing.FontStyle.Bold);

            System.Drawing.Brush primaryBrush = new System.Drawing.SolidBrush(System.Drawing.Color.FromArgb(30, 144, 255));
            System.Drawing.Brush textBrush = System.Drawing.Brushes.Black;
            System.Drawing.Pen borderPen = new System.Drawing.Pen(System.Drawing.Color.FromArgb(226, 232, 240));

            int startX = 40;
            int startY = 40;
            int currentY = startY;

            // 1. Title
            g.DrawString("INVOICE DETAILS", titleFont, primaryBrush, startX, currentY);
            currentY += 40;

            // 2. Info Section
            g.DrawString($"Invoice ID: {lblInvoiceIdValue.Text}", headerFont, textBrush, startX, currentY);
            g.DrawString($"Customer: {lblCustomerValue.Text}", headerFont, textBrush, startX + 300, currentY);
            currentY += 20;

            g.DrawString($"Date: {lblDateValue.Text}", regularFont, textBrush, startX, currentY);
            g.DrawString($"Created By: {lblCreatedByValue.Text}", regularFont, textBrush, startX + 300, currentY);
            currentY += 20;

            g.DrawString($"Payment: {lblPaymentMethodValue.Text}", regularFont, textBrush, startX, currentY);
            g.DrawString($"Status: {lblStatusBadge.Text}", regularFont, textBrush, startX + 300, currentY);
            currentY += 35;

            // 3. Table Header
            int[] colWidths = { 40, 260, 80, 100, 100 };
            string[] headers = { "#", "Product", "Quantity", "Unit Price", "Total" };

            int xPos = startX;
            g.FillRectangle(new System.Drawing.SolidBrush(System.Drawing.Color.FromArgb(240, 244, 248)), startX, currentY, 580, 25);

            for (int i = 0; i < headers.Length; i++)
            {
                g.DrawString(headers[i], headerFont, textBrush, xPos + 5, currentY + 4);
                g.DrawRectangle(borderPen, xPos, currentY, colWidths[i], 25);
                xPos += colWidths[i];
            }
            currentY += 25;

            // 4. Table Rows
            int itemNo = 1;
            foreach (DataGridViewRow row in dgvItems.Rows)
            {
                if (row.IsNewRow) continue;

                xPos = startX;
                string[] cellTexts = {
                    itemNo.ToString(),
                    row.Cells[1].Value?.ToString() ?? "",
                    row.Cells[2].Value?.ToString() ?? "",
                    row.Cells[3].Value?.ToString() ?? "",
                    row.Cells[4].Value?.ToString() ?? ""
                };

                // Add Currency formatting if missing
                if (decimal.TryParse(cellTexts[3].Replace("$", ""), out decimal pVal)) cellTexts[3] = $"${pVal:N2}";
                if (decimal.TryParse(cellTexts[4].Replace("$", ""), out decimal tVal)) cellTexts[4] = $"${tVal:N2}";

                for (int i = 0; i < cellTexts.Length; i++)
                {
                    g.DrawRectangle(borderPen, xPos, currentY, colWidths[i], 25);
                    g.DrawString(cellTexts[i], regularFont, textBrush, xPos + 5, currentY + 4);
                    xPos += colWidths[i];
                }

                currentY += 25;
                itemNo++;
            }

            currentY += 20;

            // 5. Totals
            g.DrawString($"Discount: {lblDiscountValue.Text}", regularFont, textBrush, startX + 380, currentY);
            currentY += 20;
            g.DrawString($"Total Amount: {lblTotalAmountValue.Text}", totalFont, primaryBrush, startX + 380, currentY);
            currentY += 35;

            // 6. Notes
            if (!string.IsNullOrWhiteSpace(lblNotesValue.Text))
            {
                g.DrawString("Notes:", headerFont, textBrush, startX, currentY);
                currentY += 18;
                g.DrawString(lblNotesValue.Text, regularFont, textBrush, startX, currentY);
            }
        }
    }
}