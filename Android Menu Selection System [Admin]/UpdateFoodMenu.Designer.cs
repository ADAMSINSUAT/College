namespace Android_Menu_Selection_System__Admin_
{
    partial class UpdateFoodMenu
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(UpdateFoodMenu));
            this.btnUpdate = new System.Windows.Forms.Button();
            this.toggleAvailability = new JCS.ToggleSwitch();
            this.btnAddphoto = new System.Windows.Forms.Button();
            this.txtpicpath = new System.Windows.Forms.Label();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.cmbCategory = new System.Windows.Forms.ComboBox();
            this.label3 = new System.Windows.Forms.Label();
            this.txtFoodName = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.OFD = new System.Windows.Forms.OpenFileDialog();
            this.label4 = new System.Windows.Forms.Label();
            this.txtPrice = new System.Windows.Forms.TextBox();
            this.txtFoodID = new System.Windows.Forms.TextBox();
            this.label5 = new System.Windows.Forms.Label();
            this.btnExit = new System.Windows.Forms.Button();
            this.label6 = new System.Windows.Forms.Label();
            this.lblVaT = new System.Windows.Forms.Label();
            this.dTPTimeToCook = new System.Windows.Forms.DateTimePicker();
            this.label7 = new System.Windows.Forms.Label();
            this.lblPricewithVAT = new System.Windows.Forms.Label();
            this.label8 = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.SuspendLayout();
            // 
            // btnUpdate
            // 
            this.btnUpdate.FlatAppearance.BorderColor = System.Drawing.Color.Black;
            this.btnUpdate.FlatAppearance.BorderSize = 10;
            this.btnUpdate.Font = new System.Drawing.Font("Myriad Arabic", 20F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnUpdate.ForeColor = System.Drawing.Color.DimGray;
            this.btnUpdate.Location = new System.Drawing.Point(414, 413);
            this.btnUpdate.Name = "btnUpdate";
            this.btnUpdate.Size = new System.Drawing.Size(206, 41);
            this.btnUpdate.TabIndex = 21;
            this.btnUpdate.Text = "Update Food";
            this.btnUpdate.UseVisualStyleBackColor = true;
            this.btnUpdate.Click += new System.EventHandler(this.btnUpdate_Click);
            // 
            // toggleAvailability
            // 
            this.toggleAvailability.Location = new System.Drawing.Point(413, 352);
            this.toggleAvailability.Name = "toggleAvailability";
            this.toggleAvailability.OffFont = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.toggleAvailability.OffText = "Unavailable";
            this.toggleAvailability.OnFont = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.toggleAvailability.OnText = "Available";
            this.toggleAvailability.Size = new System.Drawing.Size(102, 19);
            this.toggleAvailability.Style = JCS.ToggleSwitch.ToggleSwitchStyle.Fancy;
            this.toggleAvailability.TabIndex = 18;
            this.toggleAvailability.ThresholdPercentage = 1;
            // 
            // btnAddphoto
            // 
            this.btnAddphoto.Font = new System.Drawing.Font("Microsoft Sans Serif", 20F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnAddphoto.ForeColor = System.Drawing.Color.DodgerBlue;
            this.btnAddphoto.Location = new System.Drawing.Point(11, 278);
            this.btnAddphoto.Name = "btnAddphoto";
            this.btnAddphoto.Size = new System.Drawing.Size(214, 42);
            this.btnAddphoto.TabIndex = 20;
            this.btnAddphoto.Text = "Add Image";
            this.btnAddphoto.UseVisualStyleBackColor = true;
            this.btnAddphoto.Click += new System.EventHandler(this.btnAddphoto_Click);
            // 
            // txtpicpath
            // 
            this.txtpicpath.AutoSize = true;
            this.txtpicpath.Location = new System.Drawing.Point(12, 278);
            this.txtpicpath.Name = "txtpicpath";
            this.txtpicpath.Size = new System.Drawing.Size(42, 13);
            this.txtpicpath.TabIndex = 19;
            this.txtpicpath.Text = "picpath";
            this.txtpicpath.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.txtpicpath.Visible = false;
            // 
            // pictureBox1
            // 
            this.pictureBox1.Image = ((System.Drawing.Image)(resources.GetObject("pictureBox1.Image")));
            this.pictureBox1.Location = new System.Drawing.Point(12, 84);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(213, 187);
            this.pictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.pictureBox1.TabIndex = 16;
            this.pictureBox1.TabStop = false;
            // 
            // cmbCategory
            // 
            this.cmbCategory.FormattingEnabled = true;
            this.cmbCategory.Location = new System.Drawing.Point(413, 325);
            this.cmbCategory.Name = "cmbCategory";
            this.cmbCategory.Size = new System.Drawing.Size(121, 21);
            this.cmbCategory.TabIndex = 17;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Microsoft Sans Serif", 20F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.ForeColor = System.Drawing.Color.White;
            this.label3.Location = new System.Drawing.Point(250, 315);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(133, 31);
            this.label3.TabIndex = 8;
            this.label3.Text = "Category:";
            // 
            // txtFoodName
            // 
            this.txtFoodName.BackColor = System.Drawing.Color.White;
            this.txtFoodName.Location = new System.Drawing.Point(414, 166);
            this.txtFoodName.Multiline = true;
            this.txtFoodName.Name = "txtFoodName";
            this.txtFoodName.Size = new System.Drawing.Size(206, 37);
            this.txtFoodName.TabIndex = 14;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 20F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.ForeColor = System.Drawing.Color.White;
            this.label2.Location = new System.Drawing.Point(250, 198);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(84, 31);
            this.label2.TabIndex = 9;
            this.label2.Text = "Price:";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 20F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.ForeColor = System.Drawing.Color.White;
            this.label1.Location = new System.Drawing.Point(250, 155);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(158, 31);
            this.label1.TabIndex = 10;
            this.label1.Text = "Food name:";
            // 
            // OFD
            // 
            this.OFD.FileName = "openFileDialog1";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("Microsoft Sans Serif", 15F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label4.ForeColor = System.Drawing.Color.White;
            this.label4.Location = new System.Drawing.Point(251, 346);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(110, 25);
            this.label4.TabIndex = 11;
            this.label4.Text = "Availability:";
            // 
            // txtPrice
            // 
            this.txtPrice.BackColor = System.Drawing.Color.White;
            this.txtPrice.Location = new System.Drawing.Point(414, 209);
            this.txtPrice.Name = "txtPrice";
            this.txtPrice.Size = new System.Drawing.Size(120, 20);
            this.txtPrice.TabIndex = 15;
            this.txtPrice.Text = "0";
            this.txtPrice.TextChanged += new System.EventHandler(this.txtPrice_TextChanged);
            // 
            // txtFoodID
            // 
            this.txtFoodID.BackColor = System.Drawing.Color.White;
            this.txtFoodID.Location = new System.Drawing.Point(414, 135);
            this.txtFoodID.Name = "txtFoodID";
            this.txtFoodID.Size = new System.Drawing.Size(206, 20);
            this.txtFoodID.TabIndex = 13;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Font = new System.Drawing.Font("Microsoft Sans Serif", 20F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label5.ForeColor = System.Drawing.Color.White;
            this.label5.Location = new System.Drawing.Point(250, 124);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(119, 31);
            this.label5.TabIndex = 12;
            this.label5.Text = "Food ID:";
            // 
            // btnExit
            // 
            this.btnExit.Font = new System.Drawing.Font("Myriad Arabic", 20F, System.Drawing.FontStyle.Bold);
            this.btnExit.ForeColor = System.Drawing.Color.DimGray;
            this.btnExit.Location = new System.Drawing.Point(668, 12);
            this.btnExit.Name = "btnExit";
            this.btnExit.Size = new System.Drawing.Size(84, 42);
            this.btnExit.TabIndex = 22;
            this.btnExit.Text = "Exit";
            this.btnExit.UseVisualStyleBackColor = true;
            this.btnExit.Click += new System.EventHandler(this.btnExit_Click);
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Font = new System.Drawing.Font("Microsoft Sans Serif", 20F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label6.ForeColor = System.Drawing.Color.White;
            this.label6.Location = new System.Drawing.Point(250, 284);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(75, 31);
            this.label6.TabIndex = 9;
            this.label6.Text = "VAT:";
            // 
            // lblVaT
            // 
            this.lblVaT.AutoSize = true;
            this.lblVaT.Font = new System.Drawing.Font("Microsoft Sans Serif", 20F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblVaT.ForeColor = System.Drawing.Color.White;
            this.lblVaT.Location = new System.Drawing.Point(408, 284);
            this.lblVaT.Name = "lblVaT";
            this.lblVaT.Size = new System.Drawing.Size(29, 31);
            this.lblVaT.TabIndex = 9;
            this.lblVaT.Text = "0";
            // 
            // dTPTimeToCook
            // 
            this.dTPTimeToCook.CustomFormat = "HH:mm:ss";
            this.dTPTimeToCook.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dTPTimeToCook.Location = new System.Drawing.Point(413, 386);
            this.dTPTimeToCook.Name = "dTPTimeToCook";
            this.dTPTimeToCook.Size = new System.Drawing.Size(200, 20);
            this.dTPTimeToCook.TabIndex = 24;
            this.dTPTimeToCook.TabStop = false;
            this.dTPTimeToCook.Value = new System.DateTime(2022, 5, 11, 0, 0, 0, 0);
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Font = new System.Drawing.Font("Microsoft Sans Serif", 15F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label7.ForeColor = System.Drawing.Color.White;
            this.label7.Location = new System.Drawing.Point(251, 381);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(130, 25);
            this.label7.TabIndex = 23;
            this.label7.Text = "Time to cook:";
            // 
            // lblPricewithVAT
            // 
            this.lblPricewithVAT.AutoSize = true;
            this.lblPricewithVAT.Font = new System.Drawing.Font("Microsoft Sans Serif", 20F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblPricewithVAT.ForeColor = System.Drawing.Color.White;
            this.lblPricewithVAT.Location = new System.Drawing.Point(408, 243);
            this.lblPricewithVAT.Name = "lblPricewithVAT";
            this.lblPricewithVAT.Size = new System.Drawing.Size(29, 31);
            this.lblPricewithVAT.TabIndex = 25;
            this.lblPricewithVAT.Text = "0";
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Font = new System.Drawing.Font("Microsoft Sans Serif", 20F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label8.ForeColor = System.Drawing.Color.White;
            this.label8.Location = new System.Drawing.Point(250, 222);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(111, 62);
            this.label8.TabIndex = 26;
            this.label8.Text = "Price w/\r\nVAT:";
            // 
            // UpdateFoodMenu
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(46)))), ((int)(((byte)(51)))), ((int)(((byte)(73)))));
            this.ClientSize = new System.Drawing.Size(816, 466);
            this.ControlBox = false;
            this.Controls.Add(this.lblPricewithVAT);
            this.Controls.Add(this.label8);
            this.Controls.Add(this.dTPTimeToCook);
            this.Controls.Add(this.label7);
            this.Controls.Add(this.btnExit);
            this.Controls.Add(this.btnUpdate);
            this.Controls.Add(this.toggleAvailability);
            this.Controls.Add(this.btnAddphoto);
            this.Controls.Add(this.txtpicpath);
            this.Controls.Add(this.pictureBox1);
            this.Controls.Add(this.cmbCategory);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.txtFoodName);
            this.Controls.Add(this.lblVaT);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.txtPrice);
            this.Controls.Add(this.txtFoodID);
            this.Controls.Add(this.label5);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "UpdateFoodMenu";
            this.Load += new System.EventHandler(this.UpdateFoodMenu_Load);
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.OpenFileDialog OFD;
        public System.Windows.Forms.TextBox txtFoodID;
        public System.Windows.Forms.Button btnUpdate;
        public JCS.ToggleSwitch toggleAvailability;
        public System.Windows.Forms.Button btnAddphoto;
        public System.Windows.Forms.Label txtpicpath;
        public System.Windows.Forms.PictureBox pictureBox1;
        public System.Windows.Forms.ComboBox cmbCategory;
        public System.Windows.Forms.Label label3;
        public System.Windows.Forms.TextBox txtFoodName;
        public System.Windows.Forms.Label label2;
        public System.Windows.Forms.Label label1;
        public System.Windows.Forms.Label label4;
        public System.Windows.Forms.TextBox txtPrice;
        public System.Windows.Forms.Label label5;
        private System.Windows.Forms.Button btnExit;
        public System.Windows.Forms.Label label6;
        public System.Windows.Forms.Label lblVaT;
        private System.Windows.Forms.Label label7;
        public System.Windows.Forms.DateTimePicker dTPTimeToCook;
        private System.Windows.Forms.Label lblPricewithVAT;
        private System.Windows.Forms.Label label8;
    }
}