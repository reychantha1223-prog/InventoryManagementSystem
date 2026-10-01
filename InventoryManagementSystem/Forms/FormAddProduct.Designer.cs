namespace InventoryManagementSystem.Forms
{
    partial class FormAddProduct
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FormAddProduct));
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.lblAddProduct = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.panel1 = new System.Windows.Forms.Panel();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.txtName = new Guna.UI2.WinForms.Guna2TextBox();
            this.cmbCategory = new Guna.UI2.WinForms.Guna2ComboBox();
            this.txtDescription = new Guna.UI2.WinForms.Guna2TextBox();
            this.btnCancel = new Guna.UI2.WinForms.Guna2Button();
            this.btnSave = new Guna.UI2.WinForms.Guna2Button();
            this.lblNameRequired = new System.Windows.Forms.Label();
            this.numPrice = new Guna.UI2.WinForms.Guna2NumericUpDown();
            this.numStock = new Guna.UI2.WinForms.Guna2NumericUpDown();
            this.lblPriceRequired = new System.Windows.Forms.Label();
            this.lblStockRequired = new System.Windows.Forms.Label();
            this.cmbSupplier = new Guna.UI2.WinForms.Guna2ComboBox();
            this.label1 = new System.Windows.Forms.Label();
            this.lblCategoryRequired = new System.Windows.Forms.Label();
            this.lblSupplierRequired = new System.Windows.Forms.Label();
            this.label8 = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numPrice)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numStock)).BeginInit();
            this.SuspendLayout();
            // 
            // pictureBox1
            // 
            this.pictureBox1.Image = ((System.Drawing.Image)(resources.GetObject("pictureBox1.Image")));
            this.pictureBox1.Location = new System.Drawing.Point(25, 17);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(109, 79);
            this.pictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.CenterImage;
            this.pictureBox1.TabIndex = 0;
            this.pictureBox1.TabStop = false;
            // 
            // lblAddProduct
            // 
            this.lblAddProduct.AutoSize = true;
            this.lblAddProduct.Font = new System.Drawing.Font("Microsoft JhengHei UI", 13.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblAddProduct.Location = new System.Drawing.Point(153, 27);
            this.lblAddProduct.Name = "lblAddProduct";
            this.lblAddProduct.Size = new System.Drawing.Size(154, 29);
            this.lblAddProduct.TabIndex = 1;
            this.lblAddProduct.Text = "Add Product";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Microsoft JhengHei UI", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(153, 63);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(247, 22);
            this.label2.TabIndex = 2;
            this.label2.Text = "Fill in the product detail below";
            // 
            // panel1
            // 
            this.panel1.BackColor = System.Drawing.Color.LightSteelBlue;
            this.panel1.Location = new System.Drawing.Point(-1, 111);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(660, 1);
            this.panel1.TabIndex = 3;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.8F);
            this.label3.Location = new System.Drawing.Point(36, 138);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(124, 22);
            this.label3.TabIndex = 4;
            this.label3.Text = "Product Name";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.8F);
            this.label4.Location = new System.Drawing.Point(37, 231);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(83, 22);
            this.label4.TabIndex = 6;
            this.label4.Text = "Category";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.8F);
            this.label5.Location = new System.Drawing.Point(37, 332);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(51, 22);
            this.label5.TabIndex = 8;
            this.label5.Text = "Price";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.8F);
            this.label6.Location = new System.Drawing.Point(35, 437);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(55, 22);
            this.label6.TabIndex = 10;
            this.label6.Text = "Stock";
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.8F);
            this.label7.Location = new System.Drawing.Point(36, 530);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(109, 22);
            this.label7.TabIndex = 12;
            this.label7.Text = "Descriptions";
            // 
            // txtName
            // 
            this.txtName.BorderRadius = 8;
            this.txtName.Cursor = System.Windows.Forms.Cursors.IBeam;
            this.txtName.DefaultText = "";
            this.txtName.DisabledState.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(208)))), ((int)(((byte)(208)))), ((int)(((byte)(208)))));
            this.txtName.DisabledState.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(226)))), ((int)(((byte)(226)))), ((int)(((byte)(226)))));
            this.txtName.DisabledState.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(138)))), ((int)(((byte)(138)))), ((int)(((byte)(138)))));
            this.txtName.DisabledState.PlaceholderForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(138)))), ((int)(((byte)(138)))), ((int)(((byte)(138)))));
            this.txtName.FocusedState.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(94)))), ((int)(((byte)(148)))), ((int)(((byte)(255)))));
            this.txtName.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F);
            this.txtName.ForeColor = System.Drawing.Color.Black;
            this.txtName.HoverState.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(94)))), ((int)(((byte)(148)))), ((int)(((byte)(255)))));
            this.txtName.Location = new System.Drawing.Point(36, 172);
            this.txtName.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.txtName.Name = "txtName";
            this.txtName.PlaceholderText = "";
            this.txtName.SelectedText = "";
            this.txtName.Size = new System.Drawing.Size(589, 44);
            this.txtName.TabIndex = 16;
            this.txtName.TextChanged += new System.EventHandler(this.txtName_TextChanged);
            // 
            // cmbCategory
            // 
            this.cmbCategory.BackColor = System.Drawing.Color.Transparent;
            this.cmbCategory.BorderRadius = 8;
            this.cmbCategory.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawFixed;
            this.cmbCategory.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbCategory.FocusedColor = System.Drawing.Color.FromArgb(((int)(((byte)(94)))), ((int)(((byte)(148)))), ((int)(((byte)(255)))));
            this.cmbCategory.FocusedState.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(94)))), ((int)(((byte)(148)))), ((int)(((byte)(255)))));
            this.cmbCategory.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F);
            this.cmbCategory.ForeColor = System.Drawing.Color.Black;
            this.cmbCategory.ItemHeight = 32;
            this.cmbCategory.Location = new System.Drawing.Point(36, 266);
            this.cmbCategory.Name = "cmbCategory";
            this.cmbCategory.Size = new System.Drawing.Size(270, 38);
            this.cmbCategory.TabIndex = 17;
            // 
            // txtDescription
            // 
            this.txtDescription.BorderRadius = 8;
            this.txtDescription.Cursor = System.Windows.Forms.Cursors.IBeam;
            this.txtDescription.DefaultText = "";
            this.txtDescription.DisabledState.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(208)))), ((int)(((byte)(208)))), ((int)(((byte)(208)))));
            this.txtDescription.DisabledState.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(226)))), ((int)(((byte)(226)))), ((int)(((byte)(226)))));
            this.txtDescription.DisabledState.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(138)))), ((int)(((byte)(138)))), ((int)(((byte)(138)))));
            this.txtDescription.DisabledState.PlaceholderForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(138)))), ((int)(((byte)(138)))), ((int)(((byte)(138)))));
            this.txtDescription.FocusedState.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(94)))), ((int)(((byte)(148)))), ((int)(((byte)(255)))));
            this.txtDescription.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F);
            this.txtDescription.ForeColor = System.Drawing.Color.Black;
            this.txtDescription.HoverState.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(94)))), ((int)(((byte)(148)))), ((int)(((byte)(255)))));
            this.txtDescription.Location = new System.Drawing.Point(35, 567);
            this.txtDescription.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.txtDescription.Multiline = true;
            this.txtDescription.Name = "txtDescription";
            this.txtDescription.PlaceholderText = "";
            this.txtDescription.SelectedText = "";
            this.txtDescription.Size = new System.Drawing.Size(590, 131);
            this.txtDescription.TabIndex = 20;
            // 
            // btnCancel
            // 
            this.btnCancel.BorderColor = System.Drawing.Color.Red;
            this.btnCancel.BorderRadius = 8;
            this.btnCancel.BorderThickness = 1;
            this.btnCancel.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnCancel.DisabledState.BorderColor = System.Drawing.Color.DarkGray;
            this.btnCancel.DisabledState.CustomBorderColor = System.Drawing.Color.DarkGray;
            this.btnCancel.DisabledState.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(169)))), ((int)(((byte)(169)))), ((int)(((byte)(169)))));
            this.btnCancel.DisabledState.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(141)))), ((int)(((byte)(141)))), ((int)(((byte)(141)))));
            this.btnCancel.FillColor = System.Drawing.Color.Transparent;
            this.btnCancel.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F);
            this.btnCancel.ForeColor = System.Drawing.Color.Red;
            this.btnCancel.HoverState.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(254)))), ((int)(((byte)(226)))), ((int)(((byte)(226)))));
            this.btnCancel.Location = new System.Drawing.Point(297, 741);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.PressedColor = System.Drawing.Color.FromArgb(((int)(((byte)(254)))), ((int)(((byte)(202)))), ((int)(((byte)(202)))));
            this.btnCancel.Size = new System.Drawing.Size(149, 45);
            this.btnCancel.TabIndex = 21;
            this.btnCancel.Text = "Cancel";
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
            // 
            // btnSave
            // 
            this.btnSave.BorderRadius = 8;
            this.btnSave.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnSave.DisabledState.BorderColor = System.Drawing.Color.DarkGray;
            this.btnSave.DisabledState.CustomBorderColor = System.Drawing.Color.DarkGray;
            this.btnSave.DisabledState.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(169)))), ((int)(((byte)(169)))), ((int)(((byte)(169)))));
            this.btnSave.DisabledState.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(141)))), ((int)(((byte)(141)))), ((int)(((byte)(141)))));
            this.btnSave.FillColor = System.Drawing.Color.DodgerBlue;
            this.btnSave.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F);
            this.btnSave.ForeColor = System.Drawing.Color.White;
            this.btnSave.Location = new System.Drawing.Point(476, 741);
            this.btnSave.Name = "btnSave";
            this.btnSave.Size = new System.Drawing.Size(149, 45);
            this.btnSave.TabIndex = 22;
            this.btnSave.Text = "Save";
            this.btnSave.Click += new System.EventHandler(this.btnSave_Click);
            // 
            // lblNameRequired
            // 
            this.lblNameRequired.Font = new System.Drawing.Font("Segoe UI", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblNameRequired.ForeColor = System.Drawing.Color.Red;
            this.lblNameRequired.Location = new System.Drawing.Point(331, 137);
            this.lblNameRequired.Name = "lblNameRequired";
            this.lblNameRequired.Size = new System.Drawing.Size(281, 23);
            this.lblNameRequired.TabIndex = 65;
            this.lblNameRequired.Text = "This field is required";
            this.lblNameRequired.TextAlign = System.Drawing.ContentAlignment.TopRight;
            this.lblNameRequired.Visible = false;
            // 
            // numPrice
            // 
            this.numPrice.BackColor = System.Drawing.Color.Transparent;
            this.numPrice.BorderColor = System.Drawing.Color.WhiteSmoke;
            this.numPrice.BorderRadius = 8;
            this.numPrice.Cursor = System.Windows.Forms.Cursors.IBeam;
            this.numPrice.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F);
            this.numPrice.Location = new System.Drawing.Point(35, 364);
            this.numPrice.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.numPrice.Maximum = new decimal(new int[] {
            99999999,
            0,
            0,
            0});
            this.numPrice.Name = "numPrice";
            this.numPrice.Size = new System.Drawing.Size(589, 44);
            this.numPrice.TabIndex = 68;
            this.numPrice.TextOffset = new System.Drawing.Point(20, 0);
            this.numPrice.UpDownButtonFillColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.numPrice.ValueChanged += new System.EventHandler(this.numPrice_ValueChanged);
            // 
            // numStock
            // 
            this.numStock.BackColor = System.Drawing.Color.Transparent;
            this.numStock.BorderColor = System.Drawing.Color.WhiteSmoke;
            this.numStock.BorderRadius = 8;
            this.numStock.Cursor = System.Windows.Forms.Cursors.IBeam;
            this.numStock.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F);
            this.numStock.Location = new System.Drawing.Point(35, 467);
            this.numStock.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.numStock.Maximum = new decimal(new int[] {
            99999999,
            0,
            0,
            0});
            this.numStock.Name = "numStock";
            this.numStock.Size = new System.Drawing.Size(589, 44);
            this.numStock.TabIndex = 69;
            this.numStock.UpDownButtonFillColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.numStock.ValueChanged += new System.EventHandler(this.numStock_ValueChanged);
            // 
            // lblPriceRequired
            // 
            this.lblPriceRequired.AutoSize = true;
            this.lblPriceRequired.Font = new System.Drawing.Font("Segoe UI", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblPriceRequired.ForeColor = System.Drawing.Color.Red;
            this.lblPriceRequired.Location = new System.Drawing.Point(423, 331);
            this.lblPriceRequired.Name = "lblPriceRequired";
            this.lblPriceRequired.Size = new System.Drawing.Size(186, 23);
            this.lblPriceRequired.TabIndex = 70;
            this.lblPriceRequired.Text = "Must be greater than 0";
            this.lblPriceRequired.Visible = false;
            // 
            // lblStockRequired
            // 
            this.lblStockRequired.AutoSize = true;
            this.lblStockRequired.Font = new System.Drawing.Font("Segoe UI", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblStockRequired.ForeColor = System.Drawing.Color.Red;
            this.lblStockRequired.Location = new System.Drawing.Point(423, 436);
            this.lblStockRequired.Name = "lblStockRequired";
            this.lblStockRequired.Size = new System.Drawing.Size(186, 23);
            this.lblStockRequired.TabIndex = 71;
            this.lblStockRequired.Text = "Must be greater than 0";
            this.lblStockRequired.Visible = false;
            // 
            // cmbSupplier
            // 
            this.cmbSupplier.BackColor = System.Drawing.Color.Transparent;
            this.cmbSupplier.BorderRadius = 8;
            this.cmbSupplier.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawFixed;
            this.cmbSupplier.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbSupplier.FocusedColor = System.Drawing.Color.FromArgb(((int)(((byte)(94)))), ((int)(((byte)(148)))), ((int)(((byte)(255)))));
            this.cmbSupplier.FocusedState.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(94)))), ((int)(((byte)(148)))), ((int)(((byte)(255)))));
            this.cmbSupplier.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F);
            this.cmbSupplier.ForeColor = System.Drawing.Color.Black;
            this.cmbSupplier.ItemHeight = 32;
            this.cmbSupplier.Location = new System.Drawing.Point(355, 266);
            this.cmbSupplier.Name = "cmbSupplier";
            this.cmbSupplier.Size = new System.Drawing.Size(270, 38);
            this.cmbSupplier.TabIndex = 72;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.8F);
            this.label1.Location = new System.Drawing.Point(357, 231);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(76, 22);
            this.label1.TabIndex = 73;
            this.label1.Text = "Supplier";
            // 
            // lblCategoryRequired
            // 
            this.lblCategoryRequired.AutoSize = true;
            this.lblCategoryRequired.Font = new System.Drawing.Font("Segoe UI", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblCategoryRequired.ForeColor = System.Drawing.Color.Red;
            this.lblCategoryRequired.Location = new System.Drawing.Point(135, 232);
            this.lblCategoryRequired.Name = "lblCategoryRequired";
            this.lblCategoryRequired.Size = new System.Drawing.Size(162, 23);
            this.lblCategoryRequired.TabIndex = 74;
            this.lblCategoryRequired.Text = "This field is required";
            this.lblCategoryRequired.Visible = false;
            this.lblCategoryRequired.Click += new System.EventHandler(this.label8_Click);
            // 
            // lblSupplierRequired
            // 
            this.lblSupplierRequired.AutoSize = true;
            this.lblSupplierRequired.Font = new System.Drawing.Font("Segoe UI", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblSupplierRequired.ForeColor = System.Drawing.Color.Red;
            this.lblSupplierRequired.Location = new System.Drawing.Point(453, 231);
            this.lblSupplierRequired.Name = "lblSupplierRequired";
            this.lblSupplierRequired.Size = new System.Drawing.Size(162, 23);
            this.lblSupplierRequired.TabIndex = 75;
            this.lblSupplierRequired.Text = "This field is required";
            this.lblSupplierRequired.Visible = false;
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.BackColor = System.Drawing.Color.White;
            this.label8.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F);
            this.label8.ImageAlign = System.Drawing.ContentAlignment.TopLeft;
            this.label8.Location = new System.Drawing.Point(42, 374);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(23, 25);
            this.label8.TabIndex = 76;
            this.label8.Text = "$";
            // 
            // FormAddProduct
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(657, 830);
            this.Controls.Add(this.label8);
            this.Controls.Add(this.lblSupplierRequired);
            this.Controls.Add(this.lblCategoryRequired);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.cmbSupplier);
            this.Controls.Add(this.lblStockRequired);
            this.Controls.Add(this.lblPriceRequired);
            this.Controls.Add(this.numStock);
            this.Controls.Add(this.numPrice);
            this.Controls.Add(this.lblNameRequired);
            this.Controls.Add(this.btnSave);
            this.Controls.Add(this.btnCancel);
            this.Controls.Add(this.txtDescription);
            this.Controls.Add(this.cmbCategory);
            this.Controls.Add(this.txtName);
            this.Controls.Add(this.label7);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.lblAddProduct);
            this.Controls.Add(this.pictureBox1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.Name = "FormAddProduct";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Add Product";
            this.Load += new System.EventHandler(this.FormAddProduct_Load);
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numPrice)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numStock)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label7;
        private Guna.UI2.WinForms.Guna2TextBox txtName;
        private Guna.UI2.WinForms.Guna2ComboBox cmbCategory;
        private Guna.UI2.WinForms.Guna2TextBox txtDescription;
        private Guna.UI2.WinForms.Guna2Button btnCancel;
        private Guna.UI2.WinForms.Guna2Button btnSave;
        public System.Windows.Forms.Label lblAddProduct;
        private System.Windows.Forms.Label lblNameRequired;
        private Guna.UI2.WinForms.Guna2NumericUpDown numPrice;
        private Guna.UI2.WinForms.Guna2NumericUpDown numStock;
        private System.Windows.Forms.Label lblPriceRequired;
        private System.Windows.Forms.Label lblStockRequired;
        private Guna.UI2.WinForms.Guna2ComboBox cmbSupplier;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label lblCategoryRequired;
        private System.Windows.Forms.Label lblSupplierRequired;
        private System.Windows.Forms.Label label8;
    }
}