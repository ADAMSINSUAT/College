namespace Android_Menu_Selection_System__Admin_
{
    partial class ManageCashier
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
            this.picPath = new System.Windows.Forms.TextBox();
            this.label12 = new System.Windows.Forms.Label();
            this.label13 = new System.Windows.Forms.Label();
            this.label14 = new System.Windows.Forms.Label();
            this.txtUsername = new System.Windows.Forms.TextBox();
            this.txtPassword = new System.Windows.Forms.TextBox();
            this.txtRePassword = new System.Windows.Forms.TextBox();
            this.OFD = new System.Windows.Forms.OpenFileDialog();
            this.btnAddPhoto = new System.Windows.Forms.Button();
            this.btnSave = new System.Windows.Forms.Button();
            this.btnUpdate = new System.Windows.Forms.Button();
            this.btnClear = new System.Windows.Forms.Button();
            this.btnSearchUser = new System.Windows.Forms.Button();
            this.panel1 = new System.Windows.Forms.Panel();
            this.txtSearchCashier = new System.Windows.Forms.TextBox();
            this.btnCancelSearch = new System.Windows.Forms.Button();
            this.btnSearch = new System.Windows.Forms.Button();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.btnExit = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.CashierID = new System.Windows.Forms.Label();
            this.panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.SuspendLayout();
            // 
            // picPath
            // 
            this.picPath.Location = new System.Drawing.Point(516, 215);
            this.picPath.Name = "picPath";
            this.picPath.Size = new System.Drawing.Size(200, 20);
            this.picPath.TabIndex = 1;
            this.picPath.Visible = false;
            // 
            // label12
            // 
            this.label12.AutoSize = true;
            this.label12.ForeColor = System.Drawing.Color.White;
            this.label12.Location = new System.Drawing.Point(17, 92);
            this.label12.Name = "label12";
            this.label12.Size = new System.Drawing.Size(58, 13);
            this.label12.TabIndex = 0;
            this.label12.Text = "Username:";
            // 
            // label13
            // 
            this.label13.AutoSize = true;
            this.label13.ForeColor = System.Drawing.Color.White;
            this.label13.Location = new System.Drawing.Point(17, 117);
            this.label13.Name = "label13";
            this.label13.Size = new System.Drawing.Size(56, 13);
            this.label13.TabIndex = 0;
            this.label13.Text = "Password:";
            // 
            // label14
            // 
            this.label14.AutoSize = true;
            this.label14.ForeColor = System.Drawing.Color.White;
            this.label14.Location = new System.Drawing.Point(17, 140);
            this.label14.Name = "label14";
            this.label14.Size = new System.Drawing.Size(95, 13);
            this.label14.TabIndex = 0;
            this.label14.Text = "Re-type password:";
            // 
            // txtUsername
            // 
            this.txtUsername.Location = new System.Drawing.Point(110, 85);
            this.txtUsername.Name = "txtUsername";
            this.txtUsername.Size = new System.Drawing.Size(125, 20);
            this.txtUsername.TabIndex = 10;
            // 
            // txtPassword
            // 
            this.txtPassword.Location = new System.Drawing.Point(110, 110);
            this.txtPassword.Name = "txtPassword";
            this.txtPassword.PasswordChar = '*';
            this.txtPassword.Size = new System.Drawing.Size(125, 20);
            this.txtPassword.TabIndex = 11;
            // 
            // txtRePassword
            // 
            this.txtRePassword.Location = new System.Drawing.Point(110, 133);
            this.txtRePassword.Name = "txtRePassword";
            this.txtRePassword.PasswordChar = '*';
            this.txtRePassword.Size = new System.Drawing.Size(125, 20);
            this.txtRePassword.TabIndex = 12;
            // 
            // OFD
            // 
            this.OFD.FileName = "openFileDialog1";
            // 
            // btnAddPhoto
            // 
            this.btnAddPhoto.Location = new System.Drawing.Point(516, 215);
            this.btnAddPhoto.Name = "btnAddPhoto";
            this.btnAddPhoto.Size = new System.Drawing.Size(207, 50);
            this.btnAddPhoto.TabIndex = 13;
            this.btnAddPhoto.Text = "Add Photo";
            this.btnAddPhoto.UseVisualStyleBackColor = true;
            this.btnAddPhoto.Click += new System.EventHandler(this.btnAddPhoto_Click);
            // 
            // btnSave
            // 
            this.btnSave.Location = new System.Drawing.Point(20, 177);
            this.btnSave.Name = "btnSave";
            this.btnSave.Size = new System.Drawing.Size(142, 35);
            this.btnSave.TabIndex = 14;
            this.btnSave.Text = "Save";
            this.btnSave.UseVisualStyleBackColor = true;
            this.btnSave.Click += new System.EventHandler(this.btnSave_Click);
            // 
            // btnUpdate
            // 
            this.btnUpdate.Location = new System.Drawing.Point(168, 177);
            this.btnUpdate.Name = "btnUpdate";
            this.btnUpdate.Size = new System.Drawing.Size(142, 35);
            this.btnUpdate.TabIndex = 15;
            this.btnUpdate.Text = "Update";
            this.btnUpdate.UseVisualStyleBackColor = true;
            this.btnUpdate.Click += new System.EventHandler(this.btnUpdate_Click);
            // 
            // btnClear
            // 
            this.btnClear.Location = new System.Drawing.Point(168, 223);
            this.btnClear.Name = "btnClear";
            this.btnClear.Size = new System.Drawing.Size(142, 35);
            this.btnClear.TabIndex = 16;
            this.btnClear.Text = "Clear";
            this.btnClear.UseVisualStyleBackColor = true;
            this.btnClear.Click += new System.EventHandler(this.btnClear_Click);
            // 
            // btnSearchUser
            // 
            this.btnSearchUser.Location = new System.Drawing.Point(4, 38);
            this.btnSearchUser.Name = "btnSearchUser";
            this.btnSearchUser.Size = new System.Drawing.Size(130, 31);
            this.btnSearchUser.TabIndex = 20;
            this.btnSearchUser.Text = "Search ID/Username";
            this.btnSearchUser.UseVisualStyleBackColor = true;
            this.btnSearchUser.Click += new System.EventHandler(this.btnSearchUser_Click);
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.txtSearchCashier);
            this.panel1.Controls.Add(this.btnCancelSearch);
            this.panel1.Controls.Add(this.btnSearchUser);
            this.panel1.Location = new System.Drawing.Point(274, 85);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(306, 74);
            this.panel1.TabIndex = 22;
            this.panel1.Visible = false;
            // 
            // txtSearchCashier
            // 
            this.txtSearchCashier.AllowDrop = true;
            this.txtSearchCashier.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend;
            this.txtSearchCashier.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.CustomSource;
            this.txtSearchCashier.Location = new System.Drawing.Point(4, 12);
            this.txtSearchCashier.Name = "txtSearchCashier";
            this.txtSearchCashier.Size = new System.Drawing.Size(292, 20);
            this.txtSearchCashier.TabIndex = 0;
            // 
            // btnCancelSearch
            // 
            this.btnCancelSearch.Location = new System.Drawing.Point(163, 38);
            this.btnCancelSearch.Name = "btnCancelSearch";
            this.btnCancelSearch.Size = new System.Drawing.Size(130, 31);
            this.btnCancelSearch.TabIndex = 20;
            this.btnCancelSearch.Text = "Cancel Search";
            this.btnCancelSearch.UseVisualStyleBackColor = true;
            this.btnCancelSearch.Click += new System.EventHandler(this.btnCancelSearch_Click);
            // 
            // btnSearch
            // 
            this.btnSearch.Location = new System.Drawing.Point(20, 223);
            this.btnSearch.Name = "btnSearch";
            this.btnSearch.Size = new System.Drawing.Size(142, 35);
            this.btnSearch.TabIndex = 16;
            this.btnSearch.Text = "Search for Cashier";
            this.btnSearch.UseVisualStyleBackColor = true;
            this.btnSearch.Click += new System.EventHandler(this.btnSearch_Click);
            // 
            // pictureBox1
            // 
            this.pictureBox1.Image = global::Android_Menu_Selection_System__Admin_.Properties.Resources.Userpic;
            this.pictureBox1.Location = new System.Drawing.Point(516, 12);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(200, 200);
            this.pictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.pictureBox1.TabIndex = 4;
            this.pictureBox1.TabStop = false;
            // 
            // btnExit
            // 
            this.btnExit.Location = new System.Drawing.Point(723, 6);
            this.btnExit.Name = "btnExit";
            this.btnExit.Size = new System.Drawing.Size(75, 23);
            this.btnExit.TabIndex = 23;
            this.btnExit.Text = "Exit";
            this.btnExit.UseVisualStyleBackColor = true;
            this.btnExit.Visible = false;
            this.btnExit.Click += new System.EventHandler(this.btnExit_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.ForeColor = System.Drawing.Color.White;
            this.label1.Location = new System.Drawing.Point(17, 16);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(58, 13);
            this.label1.TabIndex = 0;
            this.label1.Text = "Username:";
            // 
            // CashierID
            // 
            this.CashierID.AutoSize = true;
            this.CashierID.ForeColor = System.Drawing.Color.White;
            this.CashierID.Location = new System.Drawing.Point(107, 16);
            this.CashierID.Name = "CashierID";
            this.CashierID.Size = new System.Drawing.Size(13, 13);
            this.CashierID.TabIndex = 0;
            this.CashierID.Text = "0";
            // 
            // ManageCashier
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(46)))), ((int)(((byte)(51)))), ((int)(((byte)(73)))));
            this.ClientSize = new System.Drawing.Size(764, 271);
            this.ControlBox = false;
            this.Controls.Add(this.btnExit);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.btnSearch);
            this.Controls.Add(this.btnClear);
            this.Controls.Add(this.btnUpdate);
            this.Controls.Add(this.btnSave);
            this.Controls.Add(this.btnAddPhoto);
            this.Controls.Add(this.pictureBox1);
            this.Controls.Add(this.picPath);
            this.Controls.Add(this.txtRePassword);
            this.Controls.Add(this.txtPassword);
            this.Controls.Add(this.txtUsername);
            this.Controls.Add(this.label14);
            this.Controls.Add(this.label13);
            this.Controls.Add(this.CashierID);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.label12);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Name = "ManageCashier";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "ManageCashierForm";
            this.Load += new System.EventHandler(this.AddUserForm_Load);
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.TextBox picPath;
        private System.Windows.Forms.Label label12;
        private System.Windows.Forms.Label label13;
        private System.Windows.Forms.Label label14;
        private System.Windows.Forms.TextBox txtUsername;
        private System.Windows.Forms.TextBox txtPassword;
        private System.Windows.Forms.TextBox txtRePassword;
        private System.Windows.Forms.OpenFileDialog OFD;
        private System.Windows.Forms.Button btnAddPhoto;
        private System.Windows.Forms.Button btnSave;
        private System.Windows.Forms.Button btnUpdate;
        private System.Windows.Forms.Button btnClear;
        private System.Windows.Forms.Button btnSearchUser;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.TextBox txtSearchCashier;
        private System.Windows.Forms.Button btnSearch;
        private System.Windows.Forms.Button btnCancelSearch;
        public System.Windows.Forms.Button btnExit;
        private System.Windows.Forms.Label label1;
        public System.Windows.Forms.Label CashierID;
    }
}