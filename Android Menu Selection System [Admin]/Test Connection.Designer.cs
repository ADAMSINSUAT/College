namespace Android_Menu_Selection_System__Admin_
{
    partial class Test_Connection
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
            this.btnTestConnection = new System.Windows.Forms.Button();
            this.btnMonth = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // btnTestConnection
            // 
            this.btnTestConnection.Location = new System.Drawing.Point(97, 49);
            this.btnTestConnection.Name = "btnTestConnection";
            this.btnTestConnection.Size = new System.Drawing.Size(125, 23);
            this.btnTestConnection.TabIndex = 1;
            this.btnTestConnection.Text = "Test Connection";
            this.btnTestConnection.Click += new System.EventHandler(this.btnTestConnection_Click);
            // 
            // btnMonth
            // 
            this.btnMonth.Location = new System.Drawing.Point(97, 78);
            this.btnMonth.Name = "btnMonth";
            this.btnMonth.Size = new System.Drawing.Size(125, 23);
            this.btnMonth.TabIndex = 0;
            this.btnMonth.Text = "Get Month";
            this.btnMonth.UseVisualStyleBackColor = true;
            this.btnMonth.Click += new System.EventHandler(this.btnMonth_Click);
            // 
            // Test_Connection
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(307, 143);
            this.Controls.Add(this.btnMonth);
            this.Controls.Add(this.btnTestConnection);
            this.Name = "Test_Connection";
            this.Text = "Test_Connection";
            this.Load += new System.EventHandler(this.Test_Connection_Load);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button btnTestConnection;
        private System.Windows.Forms.Button btnMonth;
    }
}