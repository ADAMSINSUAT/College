namespace Android_Menu_Selection_System__Admin_
{
    partial class ManageDiscountForm
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
            this.dGVDiscount = new System.Windows.Forms.DataGridView();
            this.Column1 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column2 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column3 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.txtDiscountName = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.cmbDiscountType = new System.Windows.Forms.ComboBox();
            this.txtDiscountPrice = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.btnSaveDiscount = new System.Windows.Forms.Button();
            this.btnExit = new System.Windows.Forms.Button();
            this.btnClear = new System.Windows.Forms.Button();
            this.btnDelete = new System.Windows.Forms.Button();
            this.btnUpdate = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.dGVDiscount)).BeginInit();
            this.SuspendLayout();
            // 
            // dGVDiscount
            // 
            this.dGVDiscount.AllowUserToAddRows = false;
            this.dGVDiscount.AllowUserToDeleteRows = false;
            this.dGVDiscount.AllowUserToResizeColumns = false;
            this.dGVDiscount.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dGVDiscount.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.Column1,
            this.Column2,
            this.Column3});
            this.dGVDiscount.Location = new System.Drawing.Point(12, 128);
            this.dGVDiscount.Name = "dGVDiscount";
            this.dGVDiscount.ReadOnly = true;
            this.dGVDiscount.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dGVDiscount.Size = new System.Drawing.Size(776, 310);
            this.dGVDiscount.TabIndex = 0;
            this.dGVDiscount.TabStop = false;
            this.dGVDiscount.MouseDoubleClick += new System.Windows.Forms.MouseEventHandler(this.ManageDiscountForm_MouseDoubleClick);
            // 
            // Column1
            // 
            this.Column1.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.Column1.HeaderText = "Discount Type";
            this.Column1.Name = "Column1";
            this.Column1.ReadOnly = true;
            // 
            // Column2
            // 
            this.Column2.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.Column2.HeaderText = "Discount Name";
            this.Column2.Name = "Column2";
            this.Column2.ReadOnly = true;
            // 
            // Column3
            // 
            this.Column3.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.Column3.HeaderText = "Discount Price";
            this.Column3.Name = "Column3";
            this.Column3.ReadOnly = true;
            // 
            // txtDiscountName
            // 
            this.txtDiscountName.Location = new System.Drawing.Point(103, 28);
            this.txtDiscountName.Name = "txtDiscountName";
            this.txtDiscountName.Size = new System.Drawing.Size(177, 20);
            this.txtDiscountName.TabIndex = 1;
            this.txtDiscountName.TabStop = false;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.ForeColor = System.Drawing.SystemColors.ControlLightLight;
            this.label1.Location = new System.Drawing.Point(14, 31);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(83, 13);
            this.label1.TabIndex = 2;
            this.label1.Text = "Discount Name:";
            // 
            // cmbDiscountType
            // 
            this.cmbDiscountType.FormattingEnabled = true;
            this.cmbDiscountType.Items.AddRange(new object[] {
            "PWDs and Senior Citizens Discounts",
            "Holiday Discounts"});
            this.cmbDiscountType.Location = new System.Drawing.Point(435, 27);
            this.cmbDiscountType.Name = "cmbDiscountType";
            this.cmbDiscountType.Size = new System.Drawing.Size(121, 21);
            this.cmbDiscountType.TabIndex = 3;
            this.cmbDiscountType.TabStop = false;
            // 
            // txtDiscountPrice
            // 
            this.txtDiscountPrice.Location = new System.Drawing.Point(103, 54);
            this.txtDiscountPrice.Name = "txtDiscountPrice";
            this.txtDiscountPrice.Size = new System.Drawing.Size(177, 20);
            this.txtDiscountPrice.TabIndex = 1;
            this.txtDiscountPrice.TabStop = false;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.ForeColor = System.Drawing.SystemColors.ControlLightLight;
            this.label2.Location = new System.Drawing.Point(14, 57);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(79, 13);
            this.label2.TabIndex = 2;
            this.label2.Text = "Discount Price:";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.ForeColor = System.Drawing.SystemColors.ControlLightLight;
            this.label3.Location = new System.Drawing.Point(350, 28);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(79, 13);
            this.label3.TabIndex = 2;
            this.label3.Text = "Discount Type:";
            // 
            // btnSaveDiscount
            // 
            this.btnSaveDiscount.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnSaveDiscount.Location = new System.Drawing.Point(13, 88);
            this.btnSaveDiscount.Name = "btnSaveDiscount";
            this.btnSaveDiscount.Size = new System.Drawing.Size(177, 23);
            this.btnSaveDiscount.TabIndex = 4;
            this.btnSaveDiscount.TabStop = false;
            this.btnSaveDiscount.Text = "Save Discount";
            this.btnSaveDiscount.UseVisualStyleBackColor = true;
            this.btnSaveDiscount.Click += new System.EventHandler(this.btnSaveDiscount_Click);
            // 
            // btnExit
            // 
            this.btnExit.Font = new System.Drawing.Font("Adobe Arabic", 15F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnExit.Location = new System.Drawing.Point(677, 9);
            this.btnExit.Name = "btnExit";
            this.btnExit.Size = new System.Drawing.Size(111, 39);
            this.btnExit.TabIndex = 4;
            this.btnExit.TabStop = false;
            this.btnExit.Text = "Exit";
            this.btnExit.UseVisualStyleBackColor = true;
            this.btnExit.Click += new System.EventHandler(this.btnExit_Click);
            // 
            // btnClear
            // 
            this.btnClear.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnClear.Location = new System.Drawing.Point(379, 88);
            this.btnClear.Name = "btnClear";
            this.btnClear.Size = new System.Drawing.Size(177, 23);
            this.btnClear.TabIndex = 4;
            this.btnClear.TabStop = false;
            this.btnClear.Text = "Clear";
            this.btnClear.UseVisualStyleBackColor = true;
            this.btnClear.Click += new System.EventHandler(this.btnClear_Click);
            // 
            // btnDelete
            // 
            this.btnDelete.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnDelete.Location = new System.Drawing.Point(562, 88);
            this.btnDelete.Name = "btnDelete";
            this.btnDelete.Size = new System.Drawing.Size(177, 23);
            this.btnDelete.TabIndex = 4;
            this.btnDelete.TabStop = false;
            this.btnDelete.Text = "Delete";
            this.btnDelete.UseVisualStyleBackColor = true;
            this.btnDelete.Click += new System.EventHandler(this.btnDelete_Click);
            // 
            // btnUpdate
            // 
            this.btnUpdate.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnUpdate.Location = new System.Drawing.Point(196, 88);
            this.btnUpdate.Name = "btnUpdate";
            this.btnUpdate.Size = new System.Drawing.Size(177, 23);
            this.btnUpdate.TabIndex = 4;
            this.btnUpdate.TabStop = false;
            this.btnUpdate.Text = "Update";
            this.btnUpdate.UseVisualStyleBackColor = true;
            this.btnUpdate.Click += new System.EventHandler(this.btnUpdate_Click);
            // 
            // ManageDiscountForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(46)))), ((int)(((byte)(51)))), ((int)(((byte)(73)))));
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.btnExit);
            this.Controls.Add(this.btnUpdate);
            this.Controls.Add(this.btnDelete);
            this.Controls.Add(this.btnClear);
            this.Controls.Add(this.btnSaveDiscount);
            this.Controls.Add(this.cmbDiscountType);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.txtDiscountPrice);
            this.Controls.Add(this.txtDiscountName);
            this.Controls.Add(this.dGVDiscount);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "ManageDiscountForm";
            this.Text = "ManageDiscountForm";
            this.Load += new System.EventHandler(this.ManageDiscountForm_Load);
            this.MouseDoubleClick += new System.Windows.Forms.MouseEventHandler(this.ManageDiscountForm_MouseDoubleClick);
            ((System.ComponentModel.ISupportInitialize)(this.dGVDiscount)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.DataGridView dGVDiscount;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column1;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column2;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column3;
        private System.Windows.Forms.TextBox txtDiscountName;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.ComboBox cmbDiscountType;
        private System.Windows.Forms.TextBox txtDiscountPrice;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Button btnSaveDiscount;
        private System.Windows.Forms.Button btnExit;
        private System.Windows.Forms.Button btnClear;
        private System.Windows.Forms.Button btnDelete;
        private System.Windows.Forms.Button btnUpdate;
    }
}