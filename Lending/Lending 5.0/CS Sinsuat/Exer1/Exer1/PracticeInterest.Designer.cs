namespace Exer1
{
    partial class PracticeInterest
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
            this.Calculate = new System.Windows.Forms.Button();
            this.textBox1 = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.Interest = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.Penalty = new System.Windows.Forms.Label();
            this.DateofLoan = new System.Windows.Forms.DateTimePicker();
            this.DuedateofLoan = new System.Windows.Forms.DateTimePicker();
            this.label4 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.Dateloanmonths = new System.Windows.Forms.TextBox();
            this.Dateloansum = new System.Windows.Forms.TextBox();
            this.label6 = new System.Windows.Forms.Label();
            this.Penaltypayment = new System.Windows.Forms.Label();
            this.sum2 = new System.Windows.Forms.TextBox();
            this.dateTimePicker1 = new System.Windows.Forms.DateTimePicker();
            this.SuspendLayout();
            // 
            // Calculate
            // 
            this.Calculate.Location = new System.Drawing.Point(256, 82);
            this.Calculate.Name = "Calculate";
            this.Calculate.Size = new System.Drawing.Size(75, 23);
            this.Calculate.TabIndex = 0;
            this.Calculate.Text = "Calculate";
            this.Calculate.UseVisualStyleBackColor = true;
            this.Calculate.Click += new System.EventHandler(this.Calculate_Click);
            // 
            // textBox1
            // 
            this.textBox1.Location = new System.Drawing.Point(109, 84);
            this.textBox1.Name = "textBox1";
            this.textBox1.Size = new System.Drawing.Size(100, 20);
            this.textBox1.TabIndex = 1;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(40, 92);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(34, 13);
            this.label1.TabIndex = 2;
            this.label1.Text = "Loan:";
            // 
            // Interest
            // 
            this.Interest.AutoSize = true;
            this.Interest.Location = new System.Drawing.Point(109, 111);
            this.Interest.Name = "Interest";
            this.Interest.Size = new System.Drawing.Size(28, 13);
            this.Interest.TabIndex = 3;
            this.Interest.Text = "0.02";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(40, 111);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(45, 13);
            this.label2.TabIndex = 4;
            this.label2.Text = "Interest:";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(40, 127);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(45, 13);
            this.label3.TabIndex = 5;
            this.label3.Text = "Penalty:";
            // 
            // Penalty
            // 
            this.Penalty.AutoSize = true;
            this.Penalty.Location = new System.Drawing.Point(109, 127);
            this.Penalty.Name = "Penalty";
            this.Penalty.Size = new System.Drawing.Size(28, 13);
            this.Penalty.TabIndex = 5;
            this.Penalty.Text = "0.08";
            // 
            // DateofLoan
            // 
            this.DateofLoan.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.DateofLoan.Location = new System.Drawing.Point(109, 12);
            this.DateofLoan.Name = "DateofLoan";
            this.DateofLoan.Size = new System.Drawing.Size(200, 20);
            this.DateofLoan.TabIndex = 6;
            this.DateofLoan.ValueChanged += new System.EventHandler(this.DateofLoan_ValueChanged);
            // 
            // DuedateofLoan
            // 
            this.DuedateofLoan.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.DuedateofLoan.Location = new System.Drawing.Point(109, 38);
            this.DuedateofLoan.Name = "DuedateofLoan";
            this.DuedateofLoan.Size = new System.Drawing.Size(200, 20);
            this.DuedateofLoan.TabIndex = 6;
            //this.DuedateofLoan.ValueChanged += new System.EventHandler(this.DuedateofLoan_ValueChanged);
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(12, 19);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(68, 13);
            this.label4.TabIndex = 7;
            this.label4.Text = "Date of loan:";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(12, 45);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(89, 13);
            this.label5.TabIndex = 7;
            this.label5.Text = "Due date of loan:";
            // 
            // Dateloanmonths
            // 
            this.Dateloanmonths.Location = new System.Drawing.Point(112, 188);
            this.Dateloanmonths.Name = "Dateloanmonths";
            this.Dateloanmonths.Size = new System.Drawing.Size(100, 20);
            this.Dateloanmonths.TabIndex = 8;
            // 
            // Dateloansum
            // 
            this.Dateloansum.Location = new System.Drawing.Point(112, 214);
            this.Dateloansum.Name = "Dateloansum";
            this.Dateloansum.Size = new System.Drawing.Size(100, 20);
            this.Dateloansum.TabIndex = 8;
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(-3, 140);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(88, 13);
            this.label6.TabIndex = 9;
            this.label6.Text = "Penalty payment:";
            // 
            // Penaltypayment
            // 
            this.Penaltypayment.AutoSize = true;
            this.Penaltypayment.Location = new System.Drawing.Point(109, 140);
            this.Penaltypayment.Name = "Penaltypayment";
            this.Penaltypayment.Size = new System.Drawing.Size(13, 13);
            this.Penaltypayment.TabIndex = 5;
            this.Penaltypayment.Text = "0";
            // 
            // sum2
            // 
            this.sum2.Location = new System.Drawing.Point(112, 156);
            this.sum2.Name = "sum2";
            this.sum2.Size = new System.Drawing.Size(100, 20);
            this.sum2.TabIndex = 8;
            // 
            // dateTimePicker1
            // 
            this.dateTimePicker1.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.dateTimePicker1.Location = new System.Drawing.Point(208, 240);
            this.dateTimePicker1.Name = "dateTimePicker1";
            this.dateTimePicker1.Size = new System.Drawing.Size(200, 20);
            this.dateTimePicker1.TabIndex = 10;
            // 
            // PracticeInterest
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(402, 261);
            this.Controls.Add(this.dateTimePicker1);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.sum2);
            this.Controls.Add(this.Dateloansum);
            this.Controls.Add(this.Dateloanmonths);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.DuedateofLoan);
            this.Controls.Add(this.DateofLoan);
            this.Controls.Add(this.Penaltypayment);
            this.Controls.Add(this.Penalty);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.Interest);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.textBox1);
            this.Controls.Add(this.Calculate);
            this.Name = "PracticeInterest";
            this.Text = "PracticeInterest";
            this.Load += new System.EventHandler(this.PracticeInterest_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button Calculate;
        private System.Windows.Forms.TextBox textBox1;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label Interest;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label Penalty;
        private System.Windows.Forms.DateTimePicker DateofLoan;
        private System.Windows.Forms.DateTimePicker DuedateofLoan;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.TextBox Dateloanmonths;
        private System.Windows.Forms.TextBox Dateloansum;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label Penaltypayment;
        private System.Windows.Forms.TextBox sum2;
        private System.Windows.Forms.DateTimePicker dateTimePicker1;
    }
}