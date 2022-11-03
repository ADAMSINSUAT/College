namespace Android_Menu_Selection_System__Admin_
{
    partial class AddFoodForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(AddFoodForm));
            this.label1 = new System.Windows.Forms.Label();
            this.txtFoodName = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.cmbCategory = new System.Windows.Forms.ComboBox();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.txtpicpath = new System.Windows.Forms.Label();
            this.btnAddphoto = new System.Windows.Forms.Button();
            this.toggleAvailability = new JCS.ToggleSwitch();
            this.btnSave = new System.Windows.Forms.Button();
            this.OFD = new System.Windows.Forms.OpenFileDialog();
            this.txtPrice = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.txtFoodID = new System.Windows.Forms.TextBox();
            this.label6 = new System.Windows.Forms.Label();
            this.lblVaT = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.dTPTimeToCook = new System.Windows.Forms.DateTimePicker();
            this.label8 = new System.Windows.Forms.Label();
            this.lblPricewithVAT = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 20F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.ForeColor = System.Drawing.Color.White;
            this.label1.Location = new System.Drawing.Point(247, 87);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(158, 31);
            this.label1.TabIndex = 0;
            this.label1.Text = "Food name:";
            // 
            // txtFoodName
            // 
            this.txtFoodName.BackColor = System.Drawing.Color.White;
            this.txtFoodName.Location = new System.Drawing.Point(411, 98);
            this.txtFoodName.Multiline = true;
            this.txtFoodName.Name = "txtFoodName";
            this.txtFoodName.Size = new System.Drawing.Size(206, 45);
            this.txtFoodName.TabIndex = 2;
            this.txtFoodName.TabStop = false;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 20F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.ForeColor = System.Drawing.Color.White;
            this.label2.Location = new System.Drawing.Point(246, 138);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(84, 31);
            this.label2.TabIndex = 0;
            this.label2.Text = "Price:";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Microsoft Sans Serif", 20F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.ForeColor = System.Drawing.Color.White;
            this.label3.Location = new System.Drawing.Point(248, 275);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(133, 31);
            this.label3.TabIndex = 0;
            this.label3.Text = "Category:";
            // 
            // cmbCategory
            // 
            this.cmbCategory.FormattingEnabled = true;
            this.cmbCategory.Location = new System.Drawing.Point(411, 285);
            this.cmbCategory.Name = "cmbCategory";
            this.cmbCategory.Size = new System.Drawing.Size(121, 21);
            this.cmbCategory.TabIndex = 4;
            this.cmbCategory.TabStop = false;
            // 
            // pictureBox1
            // 
            this.pictureBox1.Image = ((System.Drawing.Image)(resources.GetObject("pictureBox1.Image")));
            this.pictureBox1.Location = new System.Drawing.Point(13, 55);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(213, 187);
            this.pictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.pictureBox1.TabIndex = 4;
            this.pictureBox1.TabStop = false;
            // 
            // txtpicpath
            // 
            this.txtpicpath.AutoSize = true;
            this.txtpicpath.Location = new System.Drawing.Point(13, 249);
            this.txtpicpath.Name = "txtpicpath";
            this.txtpicpath.Size = new System.Drawing.Size(42, 13);
            this.txtpicpath.TabIndex = 5;
            this.txtpicpath.Text = "picpath";
            this.txtpicpath.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.txtpicpath.Visible = false;
            // 
            // btnAddphoto
            // 
            this.btnAddphoto.Font = new System.Drawing.Font("Microsoft Sans Serif", 20F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnAddphoto.ForeColor = System.Drawing.Color.DodgerBlue;
            this.btnAddphoto.Location = new System.Drawing.Point(12, 249);
            this.btnAddphoto.Name = "btnAddphoto";
            this.btnAddphoto.Size = new System.Drawing.Size(214, 42);
            this.btnAddphoto.TabIndex = 6;
            this.btnAddphoto.TabStop = false;
            this.btnAddphoto.Text = "Add Image";
            this.btnAddphoto.UseVisualStyleBackColor = true;
            this.btnAddphoto.Click += new System.EventHandler(this.btnAddphoto_Click);
            // 
            // toggleAvailability
            // 
            this.toggleAvailability.Location = new System.Drawing.Point(411, 312);
            this.toggleAvailability.Name = "toggleAvailability";
            this.toggleAvailability.OffFont = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.toggleAvailability.OffText = "Unavailable";
            this.toggleAvailability.OnFont = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.toggleAvailability.OnText = "Available";
            this.toggleAvailability.Size = new System.Drawing.Size(102, 19);
            this.toggleAvailability.Style = JCS.ToggleSwitch.ToggleSwitchStyle.Fancy;
            this.toggleAvailability.TabIndex = 5;
            this.toggleAvailability.TabStop = false;
            this.toggleAvailability.ThresholdPercentage = 1;
            this.toggleAvailability.Visible = false;
            // 
            // btnSave
            // 
            this.btnSave.FlatAppearance.BorderColor = System.Drawing.Color.Black;
            this.btnSave.FlatAppearance.BorderSize = 10;
            this.btnSave.Font = new System.Drawing.Font("Myriad Arabic", 20F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnSave.ForeColor = System.Drawing.Color.DimGray;
            this.btnSave.Location = new System.Drawing.Point(410, 341);
            this.btnSave.Name = "btnSave";
            this.btnSave.Size = new System.Drawing.Size(180, 47);
            this.btnSave.TabIndex = 7;
            this.btnSave.TabStop = false;
            this.btnSave.Text = "Save Food";
            this.btnSave.UseVisualStyleBackColor = true;
            this.btnSave.Click += new System.EventHandler(this.btnSave_Click);
            // 
            // OFD
            // 
            this.OFD.FileName = "openFileDialog1";
            // 
            // txtPrice
            // 
            this.txtPrice.BackColor = System.Drawing.Color.White;
            this.txtPrice.Location = new System.Drawing.Point(410, 149);
            this.txtPrice.Name = "txtPrice";
            this.txtPrice.Size = new System.Drawing.Size(120, 20);
            this.txtPrice.TabIndex = 3;
            this.txtPrice.TabStop = false;
            this.txtPrice.Text = "0";
            this.txtPrice.Click += new System.EventHandler(this.txtPrice_Click);
            this.txtPrice.TextChanged += new System.EventHandler(this.txtPrice_TextChanged_1);
            this.txtPrice.Leave += new System.EventHandler(this.txtPrice_Leave);
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("Microsoft Sans Serif", 15F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label4.ForeColor = System.Drawing.Color.White;
            this.label4.Location = new System.Drawing.Point(249, 306);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(110, 25);
            this.label4.TabIndex = 0;
            this.label4.Text = "Availability:";
            this.label4.Visible = false;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Font = new System.Drawing.Font("Microsoft Sans Serif", 20F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label5.ForeColor = System.Drawing.Color.White;
            this.label5.Location = new System.Drawing.Point(247, 56);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(119, 31);
            this.label5.TabIndex = 0;
            this.label5.Text = "Food ID:";
            // 
            // txtFoodID
            // 
            this.txtFoodID.BackColor = System.Drawing.Color.White;
            this.txtFoodID.Location = new System.Drawing.Point(411, 67);
            this.txtFoodID.Name = "txtFoodID";
            this.txtFoodID.Size = new System.Drawing.Size(206, 20);
            this.txtFoodID.TabIndex = 1;
            this.txtFoodID.TabStop = false;
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Font = new System.Drawing.Font("Microsoft Sans Serif", 20F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label6.ForeColor = System.Drawing.Color.White;
            this.label6.Location = new System.Drawing.Point(247, 179);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(75, 31);
            this.label6.TabIndex = 0;
            this.label6.Text = "VAT:";
            // 
            // lblVaT
            // 
            this.lblVaT.AutoSize = true;
            this.lblVaT.Font = new System.Drawing.Font("Microsoft Sans Serif", 20F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblVaT.ForeColor = System.Drawing.Color.White;
            this.lblVaT.Location = new System.Drawing.Point(405, 179);
            this.lblVaT.Name = "lblVaT";
            this.lblVaT.Size = new System.Drawing.Size(29, 31);
            this.lblVaT.TabIndex = 0;
            this.lblVaT.Text = "0";
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Font = new System.Drawing.Font("Microsoft Sans Serif", 15F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label7.ForeColor = System.Drawing.Color.White;
            this.label7.Location = new System.Drawing.Point(249, 307);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(130, 25);
            this.label7.TabIndex = 0;
            this.label7.Text = "Time to cook:";
            // 
            // dTPTimeToCook
            // 
            this.dTPTimeToCook.CustomFormat = "HH:mm:ss";
            this.dTPTimeToCook.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dTPTimeToCook.Location = new System.Drawing.Point(411, 312);
            this.dTPTimeToCook.Name = "dTPTimeToCook";
            this.dTPTimeToCook.Size = new System.Drawing.Size(200, 20);
            this.dTPTimeToCook.TabIndex = 9;
            this.dTPTimeToCook.TabStop = false;
            this.dTPTimeToCook.Value = new System.DateTime(2022, 5, 11, 0, 0, 0, 0);
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Font = new System.Drawing.Font("Microsoft Sans Serif", 20F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label8.ForeColor = System.Drawing.Color.White;
            this.label8.Location = new System.Drawing.Point(247, 210);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(111, 62);
            this.label8.TabIndex = 0;
            this.label8.Text = "Price w/\r\nVAT:";
            // 
            // lblPricewithVAT
            // 
            this.lblPricewithVAT.AutoSize = true;
            this.lblPricewithVAT.Font = new System.Drawing.Font("Microsoft Sans Serif", 20F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblPricewithVAT.ForeColor = System.Drawing.Color.White;
            this.lblPricewithVAT.Location = new System.Drawing.Point(405, 231);
            this.lblPricewithVAT.Name = "lblPricewithVAT";
            this.lblPricewithVAT.Size = new System.Drawing.Size(29, 31);
            this.lblPricewithVAT.TabIndex = 0;
            this.lblPricewithVAT.Text = "0";
            // 
            // AddFoodForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoSize = true;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(46)))), ((int)(((byte)(51)))), ((int)(((byte)(73)))));
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.ControlBox = false;
            this.Controls.Add(this.dTPTimeToCook);
            this.Controls.Add(this.btnSave);
            this.Controls.Add(this.toggleAvailability);
            this.Controls.Add(this.btnAddphoto);
            this.Controls.Add(this.txtpicpath);
            this.Controls.Add(this.pictureBox1);
            this.Controls.Add(this.cmbCategory);
            this.Controls.Add(this.label7);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.txtPrice);
            this.Controls.Add(this.txtFoodID);
            this.Controls.Add(this.lblPricewithVAT);
            this.Controls.Add(this.label8);
            this.Controls.Add(this.lblVaT);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.txtFoodName);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.label1);
            this.Name = "AddFoodForm";
            this.Load += new System.EventHandler(this.Manage_Food_Menu_Load);
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox txtFoodName;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.ComboBox cmbCategory;
        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.Label txtpicpath;
        private System.Windows.Forms.Button btnAddphoto;
        private JCS.ToggleSwitch toggleAvailability;
        private System.Windows.Forms.Button btnSave;
        private System.Windows.Forms.OpenFileDialog OFD;
        private System.Windows.Forms.TextBox txtPrice;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.TextBox txtFoodID;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label lblVaT;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.DateTimePicker dTPTimeToCook;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.Label lblPricewithVAT;
    }
}