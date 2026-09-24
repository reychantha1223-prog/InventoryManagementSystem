using System;
using System.Collections.Generic;
using System.Drawing;
using System.Threading.Tasks;
using System.Windows.Forms;
using Guna.Charts.WinForms;
using InventoryManagementSystem.Models;
using InventoryManagementSystem.Repositories;

namespace InventoryManagementSystem.Forms
{
    public partial class FormDashboard : Form
    {
        private readonly DashboardRepository _dashboardRepo = new DashboardRepository();

        public FormDashboard()
        {
            InitializeComponent();
        }

        // 1. Primary Form Load Event
        private async void FormDashboard_Load(object sender, EventArgs e)
        {

            ConfigureRecentProductView();
            await LoadDashboardDataAsync();
        }

        // 2. Designer Alias (Fixes the WinForms Designer error)
        private void FormProduct_Load(object sender, EventArgs e)
        {
            FormDashboard_Load(sender, e);
        }

        public async Task LoadDashboardDataAsync()
        {

            try
            {
                // Load Summary Cards
                DashboardSummary summary = await _dashboardRepo.GetDashboardSummaryAsync();
                if (summary != null)
                {
                    lblTotalProducts.Text = summary.TotalProducts.ToString("N0");
                    lblTotalCategories.Text = summary.TotalCategories.ToString("N0");
                    lblTotalCustomers.Text = summary.TotalCustomers.ToString("N0");
                    lblTotalSuppliers.Text = summary.TotalSuppliers.ToString("N0");
                }

                // Load Charts & Table
                await LoadSalesOverviewChartAsync();
                await LoadStockStatusChartAsync();
                await LoadRecentProductsDataAsync();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error loading dashboard data: {ex.Message}", "Dashboard Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        // --- Guna Bar Chart: Sales Overview ---
        private async Task LoadSalesOverviewChartAsync()
        {
            List<MonthlySales> salesData = await _dashboardRepo.GetMonthlySalesOverviewAsync();

            guna2Chart1.Datasets.Clear();

            GunaBarDataset barDataset = new GunaBarDataset
            {
                Label = "Total Sales ($)",
                CornerRadius = 10
            };

            barDataset.FillColors.Add(Color.FromArgb(54, 140, 246));

            if (salesData != null && salesData.Count > 0)
            {
                foreach (var item in salesData)
                {
                    barDataset.DataPoints.Add(item.MonthName, (double)item.TotalSales);
                }
            }

            guna2Chart1.Datasets.Add(barDataset);
            guna2Chart1.XAxes.Display = true;

            guna2Chart1.Update();
        }
        // --- Guna Doughnut Chart: Stock Status ---
        private async Task LoadStockStatusChartAsync()
        {
            StockStatusMetrics stockData = await _dashboardRepo.GetStockStatusMetricsAsync();

            chartStockOverview.Datasets.Clear();

            GunaDoughnutDataset doughnutDataset = new GunaDoughnutDataset
            {
                Label = "Stock Status"
            };

            doughnutDataset.FillColors.Clear();
            doughnutDataset.DataPoints.Clear();

            if (stockData != null)
            {
                doughnutDataset.FillColors.Add(Color.FromArgb(34, 197, 94));
                doughnutDataset.DataPoints.Add("In Stock", stockData.InStockCount);

                doughnutDataset.FillColors.Add(Color.FromArgb(245, 158, 11));
                doughnutDataset.DataPoints.Add("Low Stock", stockData.LowStockCount);

                doughnutDataset.FillColors.Add(Color.FromArgb(239, 68, 68));
                doughnutDataset.DataPoints.Add("Out of Stock", stockData.OutOfStockCount);

                if (lblTotalStockCount != null)
                {
                    lblTotalStockCount.Text = stockData.TotalProducts.ToString("N0");
                }
            }

            chartStockOverview.Datasets.Add(doughnutDataset);
            chartStockOverview.Update();
        }

        // --- DataGridView Data Loading ---
        private async Task LoadRecentProductsDataAsync()
        {
            List<RecentProduct> products = await _dashboardRepo.GetRecentProductsAsync(5);

            recentProductView.DataSource = null;
            recentProductView.DataSource = products;
            recentProductView.ClearSelection();
        }

        // --- Guna DataGridView UI Configuration ---
        private void ConfigureRecentProductView()
        {
            recentProductView.Theme = Guna.UI2.WinForms.Enums.DataGridViewPresetThemes.Default;
            recentProductView.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            recentProductView.ReadOnly = true;
            recentProductView.AllowUserToAddRows = false;
            recentProductView.AllowUserToDeleteRows = false;

            Color headerBg = Color.FromArgb(200, 220, 248);
            Color headerFg = Color.FromArgb(25, 50, 90);

            recentProductView.EnableHeadersVisualStyles = false;
            recentProductView.ThemeStyle.HeaderStyle.BackColor = headerBg;
            recentProductView.ThemeStyle.HeaderStyle.ForeColor = headerFg;
            recentProductView.ThemeStyle.HeaderStyle.Font = new Font("Segoe UI", 10.5F, FontStyle.Bold);
            recentProductView.ThemeStyle.HeaderStyle.BorderStyle = DataGridViewHeaderBorderStyle.None;

            recentProductView.ColumnHeadersDefaultCellStyle.SelectionBackColor = headerBg;
            recentProductView.ColumnHeadersDefaultCellStyle.SelectionForeColor = headerFg;

            recentProductView.ThemeStyle.RowsStyle.Font = new Font("Segoe UI", 10F);
            recentProductView.ThemeStyle.RowsStyle.SelectionBackColor = Color.FromArgb(240, 246, 255);
            recentProductView.ThemeStyle.RowsStyle.SelectionForeColor = Color.Black;
            recentProductView.RowTemplate.Height = 32;

            recentProductView.AllowUserToResizeColumns = false;
            recentProductView.AllowUserToResizeRows = false;
            recentProductView.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.DisableResizing;
            recentProductView.ColumnHeadersHeight = 38;
        }

        private void label4_Click(object sender, EventArgs e)
        {

        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void recentProductView_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
        {
            if (recentProductView.Columns[e.ColumnIndex].Name.Equals("Status", StringComparison.OrdinalIgnoreCase) ||
                recentProductView.Columns[e.ColumnIndex].HeaderText.Equals("Status", StringComparison.OrdinalIgnoreCase))
            {
                if (e.Value != null)
                {
                    string status = e.Value.ToString().Trim();

                    e.CellStyle.Font = new Font(recentProductView.Font.FontFamily, 10.5f, FontStyle.Bold);

                    switch (status.ToLower())
                    {
                        case "in stock":
                            e.CellStyle.ForeColor = Color.Green;
                            break;
                        case "low stock":
                            e.CellStyle.ForeColor = Color.DarkOrange;
                            break;
                        case "out of stock":
                            e.CellStyle.ForeColor = Color.Red;
                            break;
                        default:
                            e.CellStyle.ForeColor = Color.DarkGray;
                            break;
                    }
                }
            }
        }
    }
}