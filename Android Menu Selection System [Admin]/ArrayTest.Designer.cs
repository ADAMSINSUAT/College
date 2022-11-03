namespace Android_Menu_Selection_System__Admin_
{
    partial class ArrayTest
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
            this.listArray = new System.Windows.Forms.ListBox();
            this.btnAddNumber = new System.Windows.Forms.Button();
            this.btnGetMaxNumber = new System.Windows.Forms.Button();
            this.txtAddNumber = new System.Windows.Forms.TextBox();
            this.btnRemoveNumber = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // listArray
            // 
            this.listArray.FormattingEnabled = true;
            this.listArray.Location = new System.Drawing.Point(12, 12);
            this.listArray.Name = "listArray";
            this.listArray.Size = new System.Drawing.Size(327, 95);
            this.listArray.TabIndex = 0;
            // 
            // btnAddNumber
            // 
            this.btnAddNumber.Location = new System.Drawing.Point(12, 169);
            this.btnAddNumber.Name = "btnAddNumber";
            this.btnAddNumber.Size = new System.Drawing.Size(84, 48);
            this.btnAddNumber.TabIndex = 1;
            this.btnAddNumber.Text = "Add Number";
            this.btnAddNumber.UseVisualStyleBackColor = true;
            this.btnAddNumber.Click += new System.EventHandler(this.btnAddNumber_Click);
            // 
            // btnGetMaxNumber
            // 
            this.btnGetMaxNumber.Location = new System.Drawing.Point(254, 169);
            this.btnGetMaxNumber.Name = "btnGetMaxNumber";
            this.btnGetMaxNumber.Size = new System.Drawing.Size(84, 48);
            this.btnGetMaxNumber.TabIndex = 1;
            this.btnGetMaxNumber.Text = "Compute max id";
            this.btnGetMaxNumber.UseVisualStyleBackColor = true;
            this.btnGetMaxNumber.Click += new System.EventHandler(this.btnGetMaxNumber_Click);
            // 
            // txtAddNumber
            // 
            this.txtAddNumber.Location = new System.Drawing.Point(12, 132);
            this.txtAddNumber.Name = "txtAddNumber";
            this.txtAddNumber.Size = new System.Drawing.Size(181, 20);
            this.txtAddNumber.TabIndex = 2;
            this.txtAddNumber.Text = "Enter a number to add to listbox:";
            this.txtAddNumber.Click += new System.EventHandler(this.txtAddNumber_Click);
            // 
            // btnRemoveNumber
            // 
            this.btnRemoveNumber.Location = new System.Drawing.Point(102, 169);
            this.btnRemoveNumber.Name = "btnRemoveNumber";
            this.btnRemoveNumber.Size = new System.Drawing.Size(84, 48);
            this.btnRemoveNumber.TabIndex = 1;
            this.btnRemoveNumber.Text = "Remove Number";
            this.btnRemoveNumber.UseVisualStyleBackColor = true;
            this.btnRemoveNumber.Click += new System.EventHandler(this.btnRemoveNumber_Click);
            // 
            // ArrayTest
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(370, 229);
            this.Controls.Add(this.txtAddNumber);
            this.Controls.Add(this.btnGetMaxNumber);
            this.Controls.Add(this.btnRemoveNumber);
            this.Controls.Add(this.btnAddNumber);
            this.Controls.Add(this.listArray);
            this.Name = "ArrayTest";
            this.Text = "ArrayTest";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ListBox listArray;
        private System.Windows.Forms.Button btnAddNumber;
        private System.Windows.Forms.Button btnGetMaxNumber;
        private System.Windows.Forms.TextBox txtAddNumber;
        private System.Windows.Forms.Button btnRemoveNumber;
    }
}