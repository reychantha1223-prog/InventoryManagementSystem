namespace InventoryManagementSystem
{
    partial class MainLayout
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MainLayout));
            this.flowLayoutPanel1 = new System.Windows.Forms.FlowLayoutPanel();
            this.pictureBox3 = new System.Windows.Forms.PictureBox();
            this.label3 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.panelMenu = new System.Windows.Forms.Panel();
            this.btnAccount = new Guna.UI2.WinForms.Guna2Button();
            this.btnOrders = new Guna.UI2.WinForms.Guna2Button();
            this.btnSuppliers = new Guna.UI2.WinForms.Guna2Button();
            this.btnCustomers = new Guna.UI2.WinForms.Guna2Button();
            this.btnCategory = new Guna.UI2.WinForms.Guna2Button();
            this.btnProducts = new Guna.UI2.WinForms.Guna2Button();
            this.btnHome = new Guna.UI2.WinForms.Guna2Button();
            this.btnLogout = new Guna.UI2.WinForms.Guna2Button();
            this.panel3 = new System.Windows.Forms.Panel();
            this.panel2 = new System.Windows.Forms.Panel();
            this.panelDesktopPane = new System.Windows.Forms.Panel();
            this.flowLayoutPanel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox3)).BeginInit();
            this.panelMenu.SuspendLayout();
            this.SuspendLayout();
            // 
            // flowLayoutPanel1
            // 
            this.flowLayoutPanel1.Controls.Add(this.pictureBox3);
            this.flowLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Top;
            this.flowLayoutPanel1.Location = new System.Drawing.Point(0, 0);
            this.flowLayoutPanel1.Name = "flowLayoutPanel1";
            this.flowLayoutPanel1.Size = new System.Drawing.Size(270, 163);
            this.flowLayoutPanel1.TabIndex = 0;
            // 
            // pictureBox3
            // 
            this.pictureBox3.Image = ((System.Drawing.Image)(resources.GetObject("pictureBox3.Image")));
            this.pictureBox3.Location = new System.Drawing.Point(3, 3);
            this.pictureBox3.Name = "pictureBox3";
            this.pictureBox3.Size = new System.Drawing.Size(264, 94);
            this.pictureBox3.SizeMode = System.Windows.Forms.PictureBoxSizeMode.CenterImage;
            this.pictureBox3.TabIndex = 0;
            this.pictureBox3.TabStop = false;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.ForeColor = System.Drawing.SystemColors.Control;
            this.label3.Location = new System.Drawing.Point(108, 129);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(56, 20);
            this.label3.TabIndex = 2;
            this.label3.Text = "Admin";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.2F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.ForeColor = System.Drawing.SystemColors.Control;
            this.label2.Location = new System.Drawing.Point(80, 100);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(116, 20);
            this.label2.TabIndex = 1;
            this.label2.Text = "Rey Chantha";
            // 
            // panelMenu
            // 
            this.panelMenu.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(45)))));
            this.panelMenu.Controls.Add(this.btnAccount);
            this.panelMenu.Controls.Add(this.btnOrders);
            this.panelMenu.Controls.Add(this.btnSuppliers);
            this.panelMenu.Controls.Add(this.btnCustomers);
            this.panelMenu.Controls.Add(this.btnCategory);
            this.panelMenu.Controls.Add(this.btnProducts);
            this.panelMenu.Controls.Add(this.btnHome);
            this.panelMenu.Controls.Add(this.btnLogout);
            this.panelMenu.Controls.Add(this.panel3);
            this.panelMenu.Controls.Add(this.panel2);
            this.panelMenu.Controls.Add(this.label2);
            this.panelMenu.Controls.Add(this.label3);
            this.panelMenu.Controls.Add(this.flowLayoutPanel1);
            this.panelMenu.Dock = System.Windows.Forms.DockStyle.Left;
            this.panelMenu.Location = new System.Drawing.Point(0, 0);
            this.panelMenu.Name = "panelMenu";
            this.panelMenu.Size = new System.Drawing.Size(270, 876);
            this.panelMenu.TabIndex = 1;
            // 
            // btnAccount
            // 
            this.btnAccount.BorderRadius = 12;
            this.btnAccount.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnAccount.DisabledState.BorderColor = System.Drawing.Color.DarkGray;
            this.btnAccount.DisabledState.CustomBorderColor = System.Drawing.Color.DarkGray;
            this.btnAccount.DisabledState.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(169)))), ((int)(((byte)(169)))), ((int)(((byte)(169)))));
            this.btnAccount.DisabledState.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(141)))), ((int)(((byte)(141)))), ((int)(((byte)(141)))));
            this.btnAccount.FillColor = System.Drawing.Color.Empty;
            this.btnAccount.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.2F);
            this.btnAccount.ForeColor = System.Drawing.Color.White;
            this.btnAccount.Image = ((System.Drawing.Image)(resources.GetObject("btnAccount.Image")));
            this.btnAccount.ImageOffset = new System.Drawing.Point(-14, 0);
            this.btnAccount.ImageSize = new System.Drawing.Size(24, 24);
            this.btnAccount.Location = new System.Drawing.Point(17, 641);
            this.btnAccount.Name = "btnAccount";
            this.btnAccount.Size = new System.Drawing.Size(234, 45);
            this.btnAccount.TabIndex = 22;
            this.btnAccount.Text = "Account";
            this.btnAccount.TextOffset = new System.Drawing.Point(-5, 0);
            this.btnAccount.Click += new System.EventHandler(this.btnAccount_Click);
            // 
            // btnOrders
            // 
            this.btnOrders.BorderRadius = 12;
            this.btnOrders.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnOrders.DisabledState.BorderColor = System.Drawing.Color.DarkGray;
            this.btnOrders.DisabledState.CustomBorderColor = System.Drawing.Color.DarkGray;
            this.btnOrders.DisabledState.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(169)))), ((int)(((byte)(169)))), ((int)(((byte)(169)))));
            this.btnOrders.DisabledState.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(141)))), ((int)(((byte)(141)))), ((int)(((byte)(141)))));
            this.btnOrders.FillColor = System.Drawing.Color.Empty;
            this.btnOrders.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.2F);
            this.btnOrders.ForeColor = System.Drawing.Color.White;
            this.btnOrders.Image = ((System.Drawing.Image)(resources.GetObject("btnOrders.Image")));
            this.btnOrders.ImageOffset = new System.Drawing.Point(-16, 0);
            this.btnOrders.ImageSize = new System.Drawing.Size(24, 24);
            this.btnOrders.Location = new System.Drawing.Point(17, 567);
            this.btnOrders.Name = "btnOrders";
            this.btnOrders.Size = new System.Drawing.Size(234, 45);
            this.btnOrders.TabIndex = 21;
            this.btnOrders.Text = "Orders";
            this.btnOrders.TextOffset = new System.Drawing.Point(-7, 0);
            this.btnOrders.Click += new System.EventHandler(this.btnOrders_Click);
            // 
            // btnSuppliers
            // 
            this.btnSuppliers.BorderRadius = 12;
            this.btnSuppliers.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnSuppliers.DisabledState.BorderColor = System.Drawing.Color.DarkGray;
            this.btnSuppliers.DisabledState.CustomBorderColor = System.Drawing.Color.DarkGray;
            this.btnSuppliers.DisabledState.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(169)))), ((int)(((byte)(169)))), ((int)(((byte)(169)))));
            this.btnSuppliers.DisabledState.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(141)))), ((int)(((byte)(141)))), ((int)(((byte)(141)))));
            this.btnSuppliers.FillColor = System.Drawing.Color.Empty;
            this.btnSuppliers.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.2F);
            this.btnSuppliers.ForeColor = System.Drawing.Color.White;
            this.btnSuppliers.Image = ((System.Drawing.Image)(resources.GetObject("btnSuppliers.Image")));
            this.btnSuppliers.ImageOffset = new System.Drawing.Point(-10, 0);
            this.btnSuppliers.ImageSize = new System.Drawing.Size(24, 24);
            this.btnSuppliers.Location = new System.Drawing.Point(17, 493);
            this.btnSuppliers.Name = "btnSuppliers";
            this.btnSuppliers.Size = new System.Drawing.Size(234, 45);
            this.btnSuppliers.TabIndex = 20;
            this.btnSuppliers.Text = "Suppliers";
            this.btnSuppliers.TextOffset = new System.Drawing.Point(-1, 0);
            this.btnSuppliers.Click += new System.EventHandler(this.btnSuppliers_Click);
            // 
            // btnCustomers
            // 
            this.btnCustomers.BorderRadius = 12;
            this.btnCustomers.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnCustomers.DisabledState.BorderColor = System.Drawing.Color.DarkGray;
            this.btnCustomers.DisabledState.CustomBorderColor = System.Drawing.Color.DarkGray;
            this.btnCustomers.DisabledState.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(169)))), ((int)(((byte)(169)))), ((int)(((byte)(169)))));
            this.btnCustomers.DisabledState.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(141)))), ((int)(((byte)(141)))), ((int)(((byte)(141)))));
            this.btnCustomers.FillColor = System.Drawing.Color.Empty;
            this.btnCustomers.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.2F);
            this.btnCustomers.ForeColor = System.Drawing.Color.White;
            this.btnCustomers.Image = ((System.Drawing.Image)(resources.GetObject("btnCustomers.Image")));
            this.btnCustomers.ImageOffset = new System.Drawing.Point(-6, 0);
            this.btnCustomers.ImageSize = new System.Drawing.Size(24, 24);
            this.btnCustomers.Location = new System.Drawing.Point(17, 420);
            this.btnCustomers.Name = "btnCustomers";
            this.btnCustomers.Size = new System.Drawing.Size(234, 45);
            this.btnCustomers.TabIndex = 19;
            this.btnCustomers.Text = "Customers";
            this.btnCustomers.TextOffset = new System.Drawing.Point(3, 0);
            this.btnCustomers.Click += new System.EventHandler(this.btnCustomers_Click);
            // 
            // btnCategory
            // 
            this.btnCategory.BorderRadius = 12;
            this.btnCategory.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnCategory.DisabledState.BorderColor = System.Drawing.Color.DarkGray;
            this.btnCategory.DisabledState.CustomBorderColor = System.Drawing.Color.DarkGray;
            this.btnCategory.DisabledState.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(169)))), ((int)(((byte)(169)))), ((int)(((byte)(169)))));
            this.btnCategory.DisabledState.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(141)))), ((int)(((byte)(141)))), ((int)(((byte)(141)))));
            this.btnCategory.FillColor = System.Drawing.Color.Empty;
            this.btnCategory.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.2F);
            this.btnCategory.ForeColor = System.Drawing.Color.White;
            this.btnCategory.Image = ((System.Drawing.Image)(resources.GetObject("btnCategory.Image")));
            this.btnCategory.ImageOffset = new System.Drawing.Point(-9, 0);
            this.btnCategory.ImageSize = new System.Drawing.Size(24, 24);
            this.btnCategory.Location = new System.Drawing.Point(17, 345);
            this.btnCategory.Name = "btnCategory";
            this.btnCategory.Size = new System.Drawing.Size(234, 45);
            this.btnCategory.TabIndex = 18;
            this.btnCategory.Text = "Category";
            this.btnCategory.Click += new System.EventHandler(this.btnCategory_Click);
            // 
            // btnProducts
            // 
            this.btnProducts.BorderRadius = 12;
            this.btnProducts.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnProducts.DisabledState.BorderColor = System.Drawing.Color.DarkGray;
            this.btnProducts.DisabledState.CustomBorderColor = System.Drawing.Color.DarkGray;
            this.btnProducts.DisabledState.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(169)))), ((int)(((byte)(169)))), ((int)(((byte)(169)))));
            this.btnProducts.DisabledState.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(141)))), ((int)(((byte)(141)))), ((int)(((byte)(141)))));
            this.btnProducts.FillColor = System.Drawing.Color.Empty;
            this.btnProducts.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.2F);
            this.btnProducts.ForeColor = System.Drawing.Color.White;
            this.btnProducts.Image = ((System.Drawing.Image)(resources.GetObject("btnProducts.Image")));
            this.btnProducts.ImageOffset = new System.Drawing.Point(-9, 0);
            this.btnProducts.ImageSize = new System.Drawing.Size(24, 24);
            this.btnProducts.Location = new System.Drawing.Point(17, 270);
            this.btnProducts.Name = "btnProducts";
            this.btnProducts.Size = new System.Drawing.Size(234, 45);
            this.btnProducts.TabIndex = 17;
            this.btnProducts.Text = "Products";
            this.btnProducts.TextOffset = new System.Drawing.Point(1, 0);
            this.btnProducts.Click += new System.EventHandler(this.btnProducts_Click);
            // 
            // btnHome
            // 
            this.btnHome.BorderRadius = 12;
            this.btnHome.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnHome.DisabledState.BorderColor = System.Drawing.Color.DarkGray;
            this.btnHome.DisabledState.CustomBorderColor = System.Drawing.Color.DarkGray;
            this.btnHome.DisabledState.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(169)))), ((int)(((byte)(169)))), ((int)(((byte)(169)))));
            this.btnHome.DisabledState.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(141)))), ((int)(((byte)(141)))), ((int)(((byte)(141)))));
            this.btnHome.FillColor = System.Drawing.Color.DodgerBlue;
            this.btnHome.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.2F);
            this.btnHome.ForeColor = System.Drawing.Color.White;
            this.btnHome.Image = ((System.Drawing.Image)(resources.GetObject("btnHome.Image")));
            this.btnHome.ImageSize = new System.Drawing.Size(24, 24);
            this.btnHome.Location = new System.Drawing.Point(17, 196);
            this.btnHome.Name = "btnHome";
            this.btnHome.Size = new System.Drawing.Size(234, 45);
            this.btnHome.TabIndex = 0;
            this.btnHome.Text = "    Dashboard";
            this.btnHome.Click += new System.EventHandler(this.btnHomeDashboard_Click);
            // 
            // btnLogout
            // 
            this.btnLogout.BorderRadius = 12;
            this.btnLogout.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnLogout.DisabledState.BorderColor = System.Drawing.Color.DarkGray;
            this.btnLogout.DisabledState.CustomBorderColor = System.Drawing.Color.DarkGray;
            this.btnLogout.DisabledState.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(169)))), ((int)(((byte)(169)))), ((int)(((byte)(169)))));
            this.btnLogout.DisabledState.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(141)))), ((int)(((byte)(141)))), ((int)(((byte)(141)))));
            this.btnLogout.FillColor = System.Drawing.SystemColors.ButtonHighlight;
            this.btnLogout.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.2F);
            this.btnLogout.ForeColor = System.Drawing.Color.Red;
            this.btnLogout.Image = ((System.Drawing.Image)(resources.GetObject("btnLogout.Image")));
            this.btnLogout.ImageOffset = new System.Drawing.Point(-11, 0);
            this.btnLogout.ImageSize = new System.Drawing.Size(22, 22);
            this.btnLogout.Location = new System.Drawing.Point(17, 776);
            this.btnLogout.Name = "btnLogout";
            this.btnLogout.Size = new System.Drawing.Size(234, 45);
            this.btnLogout.TabIndex = 1;
            this.btnLogout.Text = "    Logout";
            this.btnLogout.TextOffset = new System.Drawing.Point(-10, 0);
            this.btnLogout.Click += new System.EventHandler(this.btnLogout_Click);
            // 
            // panel3
            // 
            this.panel3.BackColor = System.Drawing.Color.Gray;
            this.panel3.ForeColor = System.Drawing.Color.Coral;
            this.panel3.Location = new System.Drawing.Point(-2, 750);
            this.panel3.Name = "panel3";
            this.panel3.Size = new System.Drawing.Size(273, 1);
            this.panel3.TabIndex = 15;
            // 
            // panel2
            // 
            this.panel2.BackColor = System.Drawing.Color.Gray;
            this.panel2.Location = new System.Drawing.Point(-2, 162);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(273, 1);
            this.panel2.TabIndex = 1;
            // 
            // panelDesktopPane
            // 
            this.panelDesktopPane.BackColor = System.Drawing.Color.Transparent;
            this.panelDesktopPane.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panelDesktopPane.Location = new System.Drawing.Point(270, 0);
            this.panelDesktopPane.Name = "panelDesktopPane";
            this.panelDesktopPane.Size = new System.Drawing.Size(1271, 876);
            this.panelDesktopPane.TabIndex = 2;
            // 
            // MainLayout
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 17F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoScroll = true;
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(1550, 876);
            this.Controls.Add(this.panelDesktopPane);
            this.Controls.Add(this.panelMenu);
            this.Font = new System.Drawing.Font("Segoe UI", 7.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "MainLayout";
            this.Padding = new System.Windows.Forms.Padding(0, 0, 9, 0);
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Inventory Management System";
            this.Load += new System.EventHandler(this.MainLayout_Load);
            this.flowLayoutPanel1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox3)).EndInit();
            this.panelMenu.ResumeLayout(false);
            this.panelMenu.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion
        private System.Windows.Forms.FlowLayoutPanel flowLayoutPanel1;
        private System.Windows.Forms.PictureBox pictureBox3;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Panel panelMenu;
        private System.Windows.Forms.Panel panelDesktopPane;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.Panel panel3;
        private Guna.UI2.WinForms.Guna2Button btnHome;
        private Guna.UI2.WinForms.Guna2Button btnLogout;
        private Guna.UI2.WinForms.Guna2Button btnProducts;
        private Guna.UI2.WinForms.Guna2Button btnCategory;
        private Guna.UI2.WinForms.Guna2Button btnCustomers;
        private Guna.UI2.WinForms.Guna2Button btnSuppliers;
        private Guna.UI2.WinForms.Guna2Button btnOrders;
        private Guna.UI2.WinForms.Guna2Button btnAccount;
    }
}

