namespace InventoryManagementSystem.Forms
{
    partial class FormCategory
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FormCategory));
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle2 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle3 = new System.Windows.Forms.DataGridViewCellStyle();
            this.label1 = new System.Windows.Forms.Label();
            this.guna2ContainerControl1 = new Guna.UI2.WinForms.Guna2ContainerControl();
            this.lblTotalCategories = new System.Windows.Forms.Label();
            this.pictureBox4 = new System.Windows.Forms.PictureBox();
            this.label9 = new System.Windows.Forms.Label();
            this.guna2ContainerControl2 = new Guna.UI2.WinForms.Guna2ContainerControl();
            this.pictureBox2 = new System.Windows.Forms.PictureBox();
            this.lblInactiveCategories = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.guna2ContainerControl3 = new Guna.UI2.WinForms.Guna2ContainerControl();
            this.pictureBox5 = new System.Windows.Forms.PictureBox();
            this.lblActiveCategories = new System.Windows.Forms.Label();
            this.label11 = new System.Windows.Forms.Label();
            this.guna2ContainerControl4 = new Guna.UI2.WinForms.Guna2ContainerControl();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.lblEmptyCategories = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.btnAddCategory = new Guna.UI2.WinForms.Guna2Button();
            this.CategoryView = new Guna.UI2.WinForms.Guna2DataGridView();
            this.cmbStatusFilter = new Guna.UI2.WinForms.Guna2ComboBox();
            this.btnSearch = new Guna.UI2.WinForms.Guna2Button();
            this.txtSearchCategory = new Guna.UI2.WinForms.Guna2TextBox();
            this.guna2ContainerControl5 = new Guna.UI2.WinForms.Guna2ContainerControl();
            this.button1 = new System.Windows.Forms.Button();
            this.btnDelete = new Guna.UI2.WinForms.Guna2Button();
            this.btnEdit = new Guna.UI2.WinForms.Guna2Button();
            this.ID = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colName = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.TotalProduct = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Descriptions = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Status = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.CreatedAt = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.guna2ContainerControl1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox4)).BeginInit();
            this.guna2ContainerControl2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox2)).BeginInit();
            this.guna2ContainerControl3.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox5)).BeginInit();
            this.guna2ContainerControl4.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.CategoryView)).BeginInit();
            this.guna2ContainerControl5.SuspendLayout();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Arial Narrow", 18F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(29, 17);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(123, 35);
            this.label1.TabIndex = 21;
            this.label1.Text = "Category";
            // 
            // guna2ContainerControl1
            // 
            this.guna2ContainerControl1.BackColor = System.Drawing.Color.Transparent;
            this.guna2ContainerControl1.BorderRadius = 20;
            this.guna2ContainerControl1.Controls.Add(this.lblTotalCategories);
            this.guna2ContainerControl1.Controls.Add(this.pictureBox4);
            this.guna2ContainerControl1.Controls.Add(this.label9);
            this.guna2ContainerControl1.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(243)))), ((int)(((byte)(238)))), ((int)(((byte)(255)))));
            this.guna2ContainerControl1.Location = new System.Drawing.Point(32, 90);
            this.guna2ContainerControl1.Name = "guna2ContainerControl1";
            this.guna2ContainerControl1.Size = new System.Drawing.Size(361, 188);
            this.guna2ContainerControl1.TabIndex = 29;
            this.guna2ContainerControl1.Text = "guna2ContainerControl1";
            // 
            // lblTotalCategories
            // 
            this.lblTotalCategories.AutoSize = true;
            this.lblTotalCategories.Font = new System.Drawing.Font("Segoe UI", 36F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblTotalCategories.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(109)))), ((int)(((byte)(40)))), ((int)(((byte)(217)))));
            this.lblTotalCategories.Location = new System.Drawing.Point(26, 86);
            this.lblTotalCategories.Name = "lblTotalCategories";
            this.lblTotalCategories.Size = new System.Drawing.Size(0, 81);
            this.lblTotalCategories.TabIndex = 5;
            // 
            // pictureBox4
            // 
            this.pictureBox4.Image = ((System.Drawing.Image)(resources.GetObject("pictureBox4.Image")));
            this.pictureBox4.Location = new System.Drawing.Point(238, 63);
            this.pictureBox4.Name = "pictureBox4";
            this.pictureBox4.Size = new System.Drawing.Size(97, 88);
            this.pictureBox4.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.pictureBox4.TabIndex = 4;
            this.pictureBox4.TabStop = false;
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Font = new System.Drawing.Font("Arial Narrow", 16.2F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label9.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(109)))), ((int)(((byte)(40)))), ((int)(((byte)(217)))));
            this.label9.Location = new System.Drawing.Point(34, 21);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(196, 33);
            this.label9.TabIndex = 3;
            this.label9.Text = "Total Categories";
            // 
            // guna2ContainerControl2
            // 
            this.guna2ContainerControl2.BackColor = System.Drawing.Color.Transparent;
            this.guna2ContainerControl2.BorderRadius = 20;
            this.guna2ContainerControl2.Controls.Add(this.pictureBox2);
            this.guna2ContainerControl2.Controls.Add(this.lblInactiveCategories);
            this.guna2ContainerControl2.Controls.Add(this.label7);
            this.guna2ContainerControl2.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(254)))), ((int)(((byte)(242)))), ((int)(((byte)(242)))));
            this.guna2ContainerControl2.Location = new System.Drawing.Point(873, 90);
            this.guna2ContainerControl2.Name = "guna2ContainerControl2";
            this.guna2ContainerControl2.Size = new System.Drawing.Size(361, 188);
            this.guna2ContainerControl2.TabIndex = 30;
            this.guna2ContainerControl2.Text = "guna2ContainerControl2";
            // 
            // pictureBox2
            // 
            this.pictureBox2.Image = ((System.Drawing.Image)(resources.GetObject("pictureBox2.Image")));
            this.pictureBox2.Location = new System.Drawing.Point(246, 68);
            this.pictureBox2.Name = "pictureBox2";
            this.pictureBox2.Size = new System.Drawing.Size(92, 83);
            this.pictureBox2.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.pictureBox2.TabIndex = 6;
            this.pictureBox2.TabStop = false;
            // 
            // lblInactiveCategories
            // 
            this.lblInactiveCategories.AutoSize = true;
            this.lblInactiveCategories.Font = new System.Drawing.Font("Segoe UI", 36F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblInactiveCategories.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(153)))), ((int)(((byte)(27)))), ((int)(((byte)(27)))));
            this.lblInactiveCategories.Location = new System.Drawing.Point(22, 86);
            this.lblInactiveCategories.Name = "lblInactiveCategories";
            this.lblInactiveCategories.Size = new System.Drawing.Size(0, 81);
            this.lblInactiveCategories.TabIndex = 5;
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Font = new System.Drawing.Font("Arial Narrow", 16.2F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label7.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(153)))), ((int)(((byte)(27)))), ((int)(((byte)(27)))));
            this.label7.Location = new System.Drawing.Point(30, 21);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(227, 33);
            this.label7.TabIndex = 4;
            this.label7.Text = "Inactive Categories";
            // 
            // guna2ContainerControl3
            // 
            this.guna2ContainerControl3.BackColor = System.Drawing.Color.Transparent;
            this.guna2ContainerControl3.BorderRadius = 20;
            this.guna2ContainerControl3.Controls.Add(this.pictureBox5);
            this.guna2ContainerControl3.Controls.Add(this.lblActiveCategories);
            this.guna2ContainerControl3.Controls.Add(this.label11);
            this.guna2ContainerControl3.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(233)))), ((int)(((byte)(249)))), ((int)(((byte)(242)))));
            this.guna2ContainerControl3.Location = new System.Drawing.Point(453, 90);
            this.guna2ContainerControl3.Name = "guna2ContainerControl3";
            this.guna2ContainerControl3.Size = new System.Drawing.Size(361, 188);
            this.guna2ContainerControl3.TabIndex = 31;
            this.guna2ContainerControl3.Text = "guna2ContainerControl3";
            // 
            // pictureBox5
            // 
            this.pictureBox5.Image = ((System.Drawing.Image)(resources.GetObject("pictureBox5.Image")));
            this.pictureBox5.Location = new System.Drawing.Point(253, 66);
            this.pictureBox5.Name = "pictureBox5";
            this.pictureBox5.Size = new System.Drawing.Size(88, 85);
            this.pictureBox5.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.pictureBox5.TabIndex = 6;
            this.pictureBox5.TabStop = false;
            // 
            // lblActiveCategories
            // 
            this.lblActiveCategories.AutoSize = true;
            this.lblActiveCategories.Font = new System.Drawing.Font("Segoe UI", 36F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblActiveCategories.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(21)))), ((int)(((byte)(128)))), ((int)(((byte)(61)))));
            this.lblActiveCategories.Location = new System.Drawing.Point(20, 86);
            this.lblActiveCategories.Name = "lblActiveCategories";
            this.lblActiveCategories.Size = new System.Drawing.Size(0, 81);
            this.lblActiveCategories.TabIndex = 5;
            // 
            // label11
            // 
            this.label11.AutoSize = true;
            this.label11.Font = new System.Drawing.Font("Arial Narrow", 16.2F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label11.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(21)))), ((int)(((byte)(128)))), ((int)(((byte)(61)))));
            this.label11.Location = new System.Drawing.Point(28, 21);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(211, 33);
            this.label11.TabIndex = 4;
            this.label11.Text = "Active Categories";
            // 
            // guna2ContainerControl4
            // 
            this.guna2ContainerControl4.BackColor = System.Drawing.Color.Transparent;
            this.guna2ContainerControl4.BorderRadius = 20;
            this.guna2ContainerControl4.Controls.Add(this.pictureBox1);
            this.guna2ContainerControl4.Controls.Add(this.lblEmptyCategories);
            this.guna2ContainerControl4.Controls.Add(this.label5);
            this.guna2ContainerControl4.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(251)))), ((int)(((byte)(235)))));
            this.guna2ContainerControl4.Location = new System.Drawing.Point(1295, 90);
            this.guna2ContainerControl4.Name = "guna2ContainerControl4";
            this.guna2ContainerControl4.Size = new System.Drawing.Size(361, 188);
            this.guna2ContainerControl4.TabIndex = 32;
            this.guna2ContainerControl4.Text = "guna2ContainerControl4";
            // 
            // pictureBox1
            // 
            this.pictureBox1.Image = ((System.Drawing.Image)(resources.GetObject("pictureBox1.Image")));
            this.pictureBox1.Location = new System.Drawing.Point(257, 66);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(83, 85);
            this.pictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.pictureBox1.TabIndex = 6;
            this.pictureBox1.TabStop = false;
            // 
            // lblEmptyCategories
            // 
            this.lblEmptyCategories.AutoSize = true;
            this.lblEmptyCategories.Font = new System.Drawing.Font("Segoe UI", 36F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblEmptyCategories.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(146)))), ((int)(((byte)(64)))), ((int)(((byte)(14)))));
            this.lblEmptyCategories.Location = new System.Drawing.Point(20, 86);
            this.lblEmptyCategories.Name = "lblEmptyCategories";
            this.lblEmptyCategories.Size = new System.Drawing.Size(0, 81);
            this.lblEmptyCategories.TabIndex = 5;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Font = new System.Drawing.Font("Arial Narrow", 16.2F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label5.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(146)))), ((int)(((byte)(64)))), ((int)(((byte)(14)))));
            this.label5.Location = new System.Drawing.Point(28, 21);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(211, 33);
            this.label5.TabIndex = 4;
            this.label5.Text = "Empty Categories";
            // 
            // btnAddCategory
            // 
            this.btnAddCategory.BorderRadius = 8;
            this.btnAddCategory.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnAddCategory.DisabledState.BorderColor = System.Drawing.Color.DarkGray;
            this.btnAddCategory.DisabledState.CustomBorderColor = System.Drawing.Color.DarkGray;
            this.btnAddCategory.DisabledState.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(169)))), ((int)(((byte)(169)))), ((int)(((byte)(169)))));
            this.btnAddCategory.DisabledState.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(141)))), ((int)(((byte)(141)))), ((int)(((byte)(141)))));
            this.btnAddCategory.FillColor = System.Drawing.Color.DodgerBlue;
            this.btnAddCategory.Font = new System.Drawing.Font("Arial", 12F);
            this.btnAddCategory.ForeColor = System.Drawing.Color.White;
            this.btnAddCategory.Location = new System.Drawing.Point(1458, 4);
            this.btnAddCategory.Name = "btnAddCategory";
            this.btnAddCategory.Size = new System.Drawing.Size(196, 57);
            this.btnAddCategory.TabIndex = 33;
            this.btnAddCategory.Text = "＋ Add Category";
            this.btnAddCategory.Click += new System.EventHandler(this.btnAddCategory_Click);
            // 
            // CategoryView
            // 
            dataGridViewCellStyle1.BackColor = System.Drawing.Color.White;
            this.CategoryView.AlternatingRowsDefaultCellStyle = dataGridViewCellStyle1;
            dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle2.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(100)))), ((int)(((byte)(88)))), ((int)(((byte)(255)))));
            dataGridViewCellStyle2.Font = new System.Drawing.Font("Microsoft Sans Serif", 7.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle2.ForeColor = System.Drawing.Color.White;
            dataGridViewCellStyle2.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle2.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle2.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.CategoryView.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle2;
            this.CategoryView.ColumnHeadersHeight = 18;
            this.CategoryView.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.EnableResizing;
            this.CategoryView.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.ID,
            this.colName,
            this.TotalProduct,
            this.Descriptions,
            this.Status,
            this.CreatedAt});
            dataGridViewCellStyle3.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle3.BackColor = System.Drawing.Color.White;
            dataGridViewCellStyle3.Font = new System.Drawing.Font("Microsoft Sans Serif", 7.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle3.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(71)))), ((int)(((byte)(69)))), ((int)(((byte)(94)))));
            dataGridViewCellStyle3.SelectionBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(231)))), ((int)(((byte)(229)))), ((int)(((byte)(255)))));
            dataGridViewCellStyle3.SelectionForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(71)))), ((int)(((byte)(69)))), ((int)(((byte)(94)))));
            dataGridViewCellStyle3.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.CategoryView.DefaultCellStyle = dataGridViewCellStyle3;
            this.CategoryView.GridColor = System.Drawing.Color.Silver;
            this.CategoryView.Location = new System.Drawing.Point(66, 416);
            this.CategoryView.Name = "CategoryView";
            this.CategoryView.RowHeadersVisible = false;
            this.CategoryView.RowHeadersWidth = 51;
            this.CategoryView.RowTemplate.Height = 24;
            this.CategoryView.Size = new System.Drawing.Size(1557, 563);
            this.CategoryView.TabIndex = 34;
            this.CategoryView.ThemeStyle.AlternatingRowsStyle.BackColor = System.Drawing.Color.White;
            this.CategoryView.ThemeStyle.GridColor = System.Drawing.Color.Silver;
            this.CategoryView.ThemeStyle.HeaderStyle.Font = new System.Drawing.Font("Microsoft Sans Serif", 7.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.CategoryView.ThemeStyle.HeaderStyle.Height = 18;
            this.CategoryView.ThemeStyle.RowsStyle.Font = new System.Drawing.Font("Microsoft Sans Serif", 7.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.CategoryView.ThemeStyle.RowsStyle.Height = 24;
            this.CategoryView.CellContentClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.CategoryView_CellContentClick);
            this.CategoryView.CellFormatting += new System.Windows.Forms.DataGridViewCellFormattingEventHandler(this.CategoryView_CellFormatting);
            this.CategoryView.Paint += new System.Windows.Forms.PaintEventHandler(this.CategoryView_Paint);
            // 
            // cmbStatusFilter
            // 
            this.cmbStatusFilter.BackColor = System.Drawing.Color.Transparent;
            this.cmbStatusFilter.BorderRadius = 10;
            this.cmbStatusFilter.Cursor = System.Windows.Forms.Cursors.Hand;
            this.cmbStatusFilter.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawFixed;
            this.cmbStatusFilter.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbStatusFilter.FocusedColor = System.Drawing.Color.FromArgb(((int)(((byte)(94)))), ((int)(((byte)(148)))), ((int)(((byte)(255)))));
            this.cmbStatusFilter.FocusedState.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(94)))), ((int)(((byte)(148)))), ((int)(((byte)(255)))));
            this.cmbStatusFilter.Font = new System.Drawing.Font("Segoe UI Semibold", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cmbStatusFilter.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(68)))), ((int)(((byte)(88)))), ((int)(((byte)(112)))));
            this.cmbStatusFilter.ItemHeight = 30;
            this.cmbStatusFilter.Location = new System.Drawing.Point(756, 347);
            this.cmbStatusFilter.Name = "cmbStatusFilter";
            this.cmbStatusFilter.Size = new System.Drawing.Size(333, 36);
            this.cmbStatusFilter.TabIndex = 37;
            this.cmbStatusFilter.SelectedIndexChanged += new System.EventHandler(this.cmbStatusFilter_SelectedIndexChanged);
            // 
            // btnSearch
            // 
            this.btnSearch.BorderRadius = 10;
            this.btnSearch.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnSearch.DisabledState.BorderColor = System.Drawing.Color.DarkGray;
            this.btnSearch.DisabledState.CustomBorderColor = System.Drawing.Color.DarkGray;
            this.btnSearch.DisabledState.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(169)))), ((int)(((byte)(169)))), ((int)(((byte)(169)))));
            this.btnSearch.DisabledState.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(141)))), ((int)(((byte)(141)))), ((int)(((byte)(141)))));
            this.btnSearch.FillColor = System.Drawing.Color.ForestGreen;
            this.btnSearch.Font = new System.Drawing.Font("Arial", 10.8F);
            this.btnSearch.ForeColor = System.Drawing.Color.White;
            this.btnSearch.Location = new System.Drawing.Point(607, 347);
            this.btnSearch.Name = "btnSearch";
            this.btnSearch.Size = new System.Drawing.Size(119, 48);
            this.btnSearch.TabIndex = 36;
            this.btnSearch.Text = "Search";
            this.btnSearch.Click += new System.EventHandler(this.btnSearch_Click);
            // 
            // txtSearchCategory
            // 
            this.txtSearchCategory.BorderRadius = 10;
            this.txtSearchCategory.Cursor = System.Windows.Forms.Cursors.IBeam;
            this.txtSearchCategory.DefaultText = "";
            this.txtSearchCategory.DisabledState.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(208)))), ((int)(((byte)(208)))), ((int)(((byte)(208)))));
            this.txtSearchCategory.DisabledState.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(226)))), ((int)(((byte)(226)))), ((int)(((byte)(226)))));
            this.txtSearchCategory.DisabledState.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(138)))), ((int)(((byte)(138)))), ((int)(((byte)(138)))));
            this.txtSearchCategory.DisabledState.PlaceholderForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(138)))), ((int)(((byte)(138)))), ((int)(((byte)(138)))));
            this.txtSearchCategory.FocusedState.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(94)))), ((int)(((byte)(148)))), ((int)(((byte)(255)))));
            this.txtSearchCategory.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtSearchCategory.ForeColor = System.Drawing.Color.Black;
            this.txtSearchCategory.HoverState.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(94)))), ((int)(((byte)(148)))), ((int)(((byte)(255)))));
            this.txtSearchCategory.Location = new System.Drawing.Point(34, 35);
            this.txtSearchCategory.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.txtSearchCategory.Name = "txtSearchCategory";
            this.txtSearchCategory.PlaceholderForeColor = System.Drawing.Color.Gray;
            this.txtSearchCategory.PlaceholderText = "Search categories";
            this.txtSearchCategory.SelectedText = "";
            this.txtSearchCategory.Size = new System.Drawing.Size(517, 48);
            this.txtSearchCategory.TabIndex = 35;
            this.txtSearchCategory.TextOffset = new System.Drawing.Point(32, 0);
            this.txtSearchCategory.TextChanged += new System.EventHandler(this.txtSearchCategory_TextChanged);
            // 
            // guna2ContainerControl5
            // 
            this.guna2ContainerControl5.BorderRadius = 20;
            this.guna2ContainerControl5.Controls.Add(this.button1);
            this.guna2ContainerControl5.Controls.Add(this.btnDelete);
            this.guna2ContainerControl5.Controls.Add(this.btnEdit);
            this.guna2ContainerControl5.Controls.Add(this.txtSearchCategory);
            this.guna2ContainerControl5.Location = new System.Drawing.Point(32, 312);
            this.guna2ContainerControl5.Name = "guna2ContainerControl5";
            this.guna2ContainerControl5.Size = new System.Drawing.Size(1625, 703);
            this.guna2ContainerControl5.TabIndex = 39;
            this.guna2ContainerControl5.Text = "guna2ContainerControl5";
            // 
            // button1
            // 
            this.button1.BackColor = System.Drawing.SystemColors.ButtonHighlight;
            this.button1.FlatAppearance.BorderSize = 0;
            this.button1.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.button1.Image = ((System.Drawing.Image)(resources.GetObject("button1.Image")));
            this.button1.Location = new System.Drawing.Point(46, 48);
            this.button1.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(33, 26);
            this.button1.TabIndex = 40;
            this.button1.UseVisualStyleBackColor = false;
            // 
            // btnDelete
            // 
            this.btnDelete.BorderRadius = 8;
            this.btnDelete.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnDelete.DisabledState.BorderColor = System.Drawing.Color.DarkGray;
            this.btnDelete.DisabledState.CustomBorderColor = System.Drawing.Color.DarkGray;
            this.btnDelete.DisabledState.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(169)))), ((int)(((byte)(169)))), ((int)(((byte)(169)))));
            this.btnDelete.DisabledState.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(141)))), ((int)(((byte)(141)))), ((int)(((byte)(141)))));
            this.btnDelete.FillColor = System.Drawing.Color.Red;
            this.btnDelete.Font = new System.Drawing.Font("Segoe UI Semibold", 10.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnDelete.ForeColor = System.Drawing.Color.White;
            this.btnDelete.Location = new System.Drawing.Point(1478, 35);
            this.btnDelete.Name = "btnDelete";
            this.btnDelete.Size = new System.Drawing.Size(113, 45);
            this.btnDelete.TabIndex = 26;
            this.btnDelete.Text = "Delete";
            this.btnDelete.Click += new System.EventHandler(this.btnDelete_Click);
            // 
            // btnEdit
            // 
            this.btnEdit.BorderRadius = 8;
            this.btnEdit.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnEdit.DisabledState.BorderColor = System.Drawing.Color.DarkGray;
            this.btnEdit.DisabledState.CustomBorderColor = System.Drawing.Color.DarkGray;
            this.btnEdit.DisabledState.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(169)))), ((int)(((byte)(169)))), ((int)(((byte)(169)))));
            this.btnEdit.DisabledState.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(141)))), ((int)(((byte)(141)))), ((int)(((byte)(141)))));
            this.btnEdit.FillColor = System.Drawing.Color.DodgerBlue;
            this.btnEdit.Font = new System.Drawing.Font("Segoe UI Semibold", 10.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnEdit.ForeColor = System.Drawing.Color.White;
            this.btnEdit.Location = new System.Drawing.Point(1352, 35);
            this.btnEdit.Name = "btnEdit";
            this.btnEdit.Size = new System.Drawing.Size(113, 45);
            this.btnEdit.TabIndex = 25;
            this.btnEdit.Text = "Edit";
            this.btnEdit.Click += new System.EventHandler(this.btnEdit_Click);
            // 
            // ID
            // 
            this.ID.DataPropertyName = "ID";
            this.ID.HeaderText = "ID";
            this.ID.MinimumWidth = 6;
            this.ID.Name = "ID";
            // 
            // colName
            // 
            this.colName.DataPropertyName = "Name";
            this.colName.HeaderText = "Name";
            this.colName.MinimumWidth = 6;
            this.colName.Name = "colName";
            // 
            // TotalProduct
            // 
            this.TotalProduct.DataPropertyName = "Total Products";
            this.TotalProduct.HeaderText = "Total Products";
            this.TotalProduct.MinimumWidth = 6;
            this.TotalProduct.Name = "TotalProduct";
            // 
            // Descriptions
            // 
            this.Descriptions.DataPropertyName = "Descriptions";
            this.Descriptions.HeaderText = "Descriptions";
            this.Descriptions.MinimumWidth = 6;
            this.Descriptions.Name = "Descriptions";
            // 
            // Status
            // 
            this.Status.DataPropertyName = "Status";
            this.Status.HeaderText = "Status";
            this.Status.MinimumWidth = 6;
            this.Status.Name = "Status";
            // 
            // CreatedAt
            // 
            this.CreatedAt.DataPropertyName = "Create_At";
            this.CreatedAt.HeaderText = "Create_At";
            this.CreatedAt.MinimumWidth = 6;
            this.CreatedAt.Name = "CreatedAt";
            // 
            // FormCategory
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(246)))), ((int)(((byte)(248)))), ((int)(((byte)(252)))));
            this.ClientSize = new System.Drawing.Size(1682, 1043);
            this.Controls.Add(this.CategoryView);
            this.Controls.Add(this.cmbStatusFilter);
            this.Controls.Add(this.btnSearch);
            this.Controls.Add(this.guna2ContainerControl5);
            this.Controls.Add(this.btnAddCategory);
            this.Controls.Add(this.guna2ContainerControl4);
            this.Controls.Add(this.guna2ContainerControl3);
            this.Controls.Add(this.guna2ContainerControl2);
            this.Controls.Add(this.guna2ContainerControl1);
            this.Controls.Add(this.label1);
            this.Name = "FormCategory";
            this.Text = "FormCategory";
            this.Load += new System.EventHandler(this.FormCategory_Load);
            this.guna2ContainerControl1.ResumeLayout(false);
            this.guna2ContainerControl1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox4)).EndInit();
            this.guna2ContainerControl2.ResumeLayout(false);
            this.guna2ContainerControl2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox2)).EndInit();
            this.guna2ContainerControl3.ResumeLayout(false);
            this.guna2ContainerControl3.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox5)).EndInit();
            this.guna2ContainerControl4.ResumeLayout(false);
            this.guna2ContainerControl4.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.CategoryView)).EndInit();
            this.guna2ContainerControl5.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.Label label1;
        private Guna.UI2.WinForms.Guna2ContainerControl guna2ContainerControl1;
        private System.Windows.Forms.Label lblTotalCategories;
        private System.Windows.Forms.PictureBox pictureBox4;
        private System.Windows.Forms.Label label9;
        private Guna.UI2.WinForms.Guna2ContainerControl guna2ContainerControl2;
        private System.Windows.Forms.PictureBox pictureBox2;
        private System.Windows.Forms.Label lblInactiveCategories;
        private System.Windows.Forms.Label label7;
        private Guna.UI2.WinForms.Guna2ContainerControl guna2ContainerControl3;
        private System.Windows.Forms.PictureBox pictureBox5;
        private System.Windows.Forms.Label lblActiveCategories;
        private System.Windows.Forms.Label label11;
        private Guna.UI2.WinForms.Guna2ContainerControl guna2ContainerControl4;
        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.Label lblEmptyCategories;
        private System.Windows.Forms.Label label5;
        private Guna.UI2.WinForms.Guna2Button btnAddCategory;
        private Guna.UI2.WinForms.Guna2DataGridView CategoryView;
        private Guna.UI2.WinForms.Guna2ComboBox cmbStatusFilter;
        private Guna.UI2.WinForms.Guna2Button btnSearch;
        private Guna.UI2.WinForms.Guna2TextBox txtSearchCategory;
        private Guna.UI2.WinForms.Guna2ContainerControl guna2ContainerControl5;
        private Guna.UI2.WinForms.Guna2Button btnDelete;
        private Guna.UI2.WinForms.Guna2Button btnEdit;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.DataGridViewTextBoxColumn ID;
        private System.Windows.Forms.DataGridViewTextBoxColumn colName;
        private System.Windows.Forms.DataGridViewTextBoxColumn TotalProduct;
        private System.Windows.Forms.DataGridViewTextBoxColumn Descriptions;
        private System.Windows.Forms.DataGridViewTextBoxColumn Status;
        private System.Windows.Forms.DataGridViewTextBoxColumn CreatedAt;
    }
}