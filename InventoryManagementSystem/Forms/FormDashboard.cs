using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Windows.Forms.DataVisualization.Charting;

namespace InventoryManagementSystem.Forms
{
    public partial class FormDashboard : Form
    {
        public FormDashboard()
        {
            InitializeComponent();
        }
        private void ConfigureBarChart()
        {
            // Series setup
            var categorySeries = chart1.Series["Series1"];
            categorySeries.Name = "Category";
            categorySeries.Points.Clear();

            // Populate data
            categorySeries.Points.AddXY("Phone", 120);
            categorySeries.Points.AddXY("Laptop", 85);
            categorySeries.Points.AddXY("Bottle", 45);
            categorySeries.Points.AddXY("Headphones", 60);
            categorySeries.Points.AddXY("Accessories", 14);
            categorySeries.Points.AddXY("Monitors", 42);
            categorySeries.Points.AddXY("Smartwatches", 78);
            categorySeries.Points.AddXY("Cameras", 29);
            categorySeries.Points.AddXY("Tablets", 53);

            // Styling & Tooltips
            categorySeries.ToolTip = "#VALX: #VALY units";
            chart1.ChartAreas[0].AxisX.LabelStyle.Font = new Font("Segoe UI", 9F);
            chart1.ChartAreas[0].AxisY.LabelStyle.Font = new Font("Segoe UI", 9F);
        }
        private void ConfigurePieChart()
        {
            // Series setup
            var genderSeries = chart2.Series["Series1"];
            genderSeries.Name = "Gender";
            genderSeries.ChartType = SeriesChartType.Pie;
            genderSeries.Points.Clear();

            // Populate data
            genderSeries.Points.AddXY("Male", 320);
            genderSeries.Points.AddXY("Female", 250);

            // Styling & Labels
            genderSeries.Label = "#VALX (#PERCENT{P0})";
            genderSeries.Font = new Font("Segoe UI", 10F, FontStyle.Bold);

            if (chart2.Legends.Count > 0)
            {
                chart2.Legends[0].Font = new Font("Segoe UI", 9.5F, FontStyle.Bold);
            }
        }
        private void ConfigureDataGridView()
        {
            // 1. Structural and Visual Properties (Set BEFORE adding rows)
            dataGridView1.Rows.Clear();
            dataGridView1.RowHeadersVisible = false;
            dataGridView1.AllowUserToAddRows = false;
            dataGridView1.ReadOnly = true;
            dataGridView1.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;

            // 2. Font & Height Settings
            dataGridView1.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.EnableResizing;
            dataGridView1.ColumnHeadersHeight = 40;
            dataGridView1.ColumnHeadersDefaultCellStyle.Font = new Font("Segoe UI", 12F, FontStyle.Bold);

            dataGridView1.RowTemplate.Height = 35;
            dataGridView1.DefaultCellStyle.Font = new Font("Segoe UI", 11F, FontStyle.Regular);

            // 3. Populate Rows
            dataGridView1.Rows.Add(101, "iPhone 15 Pro", "$999.00", 25, "Phone", "In Stock", "2026-09-04 10:30 AM");
            dataGridView1.Rows.Add(102, "Dell XPS 15", "$1,200.00", 8, "Laptop", "Low Stock", "2026-09-04 09:15 AM");
            dataGridView1.Rows.Add(103, "Water Bottle 1L", "$12.50", 120, "Bottle", "In Stock", "2026-09-03 04:45 PM");
            dataGridView1.Rows.Add(104, "Sony WH-1000XM5", "$348.00", 15, "Headphones", "In Stock", "2026-09-03 02:10 PM");
            dataGridView1.Rows.Add(105, "Logitech MX Master 3S", "$99.00", 3, "Accessories", "Critical", "2026-09-02 11:20 AM");
        }
        private void FormProduct_Load(object sender, EventArgs e)
        {
            ConfigureBarChart();
            ConfigurePieChart();
            ConfigureDataGridView();
        }
        private void chart1_Click(object sender, EventArgs e)
        {

        }

        private void chart1_MouseMove(object sender, MouseEventArgs e)
        {

        }

        private void chart2_Click(object sender, EventArgs e)
        {

        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }
    }
}
