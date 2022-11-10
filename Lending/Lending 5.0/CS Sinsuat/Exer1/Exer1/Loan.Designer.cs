namespace Exer1
{
    partial class Loan
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
            this.components = new System.ComponentModel.Container();
            this.loanamount = new System.Windows.Forms.TextBox();
            this.interest = new System.Windows.Forms.TextBox();
            this.balance = new System.Windows.Forms.TextBox();
            this.penalty = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.dateTimePicker1 = new System.Windows.Forms.DateTimePicker();
            this.dateTimePicker2 = new System.Windows.Forms.DateTimePicker();
            this.button1 = new System.Windows.Forms.Button();
            this.label7 = new System.Windows.Forms.Label();
            this.comboBox1 = new System.Windows.Forms.ComboBox();
            this.brrwersPInfoBindingSource1 = new System.Windows.Forms.BindingSource(this.components);
            //this.db2DataSet2 = new Exer1.db2DataSet2();
            this.brrwersPInfoBindingSource = new System.Windows.Forms.BindingSource(this.components);
            //this.brrwersPInfoTableAdapter = new Exer1.db2DataSet2TableAdapters.BrrwersPInfoTableAdapter();
            ((System.ComponentModel.ISupportInitialize)(this.brrwersPInfoBindingSource1)).BeginInit();
            //((System.ComponentModel.ISupportInitialize)(this.db2DataSet2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.brrwersPInfoBindingSource)).BeginInit();
            this.SuspendLayout();
            // 
            // loanamount
            // 
            this.loanamount.Location = new System.Drawing.Point(127, 31);
            this.loanamount.Name = "loanamount";
            this.loanamount.Size = new System.Drawing.Size(100, 20);
            this.loanamount.TabIndex = 1;
            // 
            // interest
            // 
            this.interest.Location = new System.Drawing.Point(128, 83);
            this.interest.Name = "interest";
            this.interest.Size = new System.Drawing.Size(100, 20);
            this.interest.TabIndex = 3;
            // 
            // balance
            // 
            this.balance.Location = new System.Drawing.Point(127, 161);
            this.balance.Name = "balance";
            this.balance.Size = new System.Drawing.Size(100, 20);
            this.balance.TabIndex = 6;
            // 
            // penalty
            // 
            this.penalty.Location = new System.Drawing.Point(127, 109);
            this.penalty.Name = "penalty";
            this.penalty.Size = new System.Drawing.Size(100, 20);
            this.penalty.TabIndex = 4;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(13, 37);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(72, 13);
            this.label1.TabIndex = 6;
            this.label1.Text = "Loan amount:";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(13, 64);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(68, 13);
            this.label2.TabIndex = 7;
            this.label2.Text = "Date of loan:";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(13, 90);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(45, 13);
            this.label3.TabIndex = 8;
            this.label3.Text = "Interest:";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(13, 168);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(49, 13);
            this.label4.TabIndex = 9;
            this.label4.Text = "Balance:";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(12, 116);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(45, 13);
            this.label5.TabIndex = 10;
            this.label5.Text = "Penalty:";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(12, 142);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(89, 13);
            this.label6.TabIndex = 11;
            this.label6.Text = "Due date of loan:";
            // 
            // dateTimePicker1
            // 
            this.dateTimePicker1.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.dateTimePicker1.Location = new System.Drawing.Point(127, 58);
            this.dateTimePicker1.Name = "dateTimePicker1";
            this.dateTimePicker1.Size = new System.Drawing.Size(200, 20);
            this.dateTimePicker1.TabIndex = 2;
            this.dateTimePicker1.ValueChanged += new System.EventHandler(this.dateTimePicker1_ValueChanged);
            // 
            // dateTimePicker2
            // 
            this.dateTimePicker2.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.dateTimePicker2.Location = new System.Drawing.Point(128, 135);
            this.dateTimePicker2.Name = "dateTimePicker2";
            this.dateTimePicker2.Size = new System.Drawing.Size(200, 20);
            this.dateTimePicker2.TabIndex = 5;
            this.dateTimePicker2.ValueChanged += new System.EventHandler(this.dateTimePicker2_ValueChanged);
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(128, 207);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(159, 23);
            this.button1.TabIndex = 12;
            this.button1.Text = "Enter";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(13, 12);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(90, 13);
            this.label7.TabIndex = 14;
            this.label7.Text = "Borrower\'s Name:";
            // 
            // comboBox1
            // 
            this.comboBox1.DataBindings.Add(new System.Windows.Forms.Binding("SelectedValue", this.brrwersPInfoBindingSource1, "Fllname", true));
            this.comboBox1.DataSource = this.brrwersPInfoBindingSource;
            this.comboBox1.DisplayMember = "Fllname";
            this.comboBox1.FormattingEnabled = true;
            this.comboBox1.Location = new System.Drawing.Point(128, 4);
            this.comboBox1.Name = "comboBox1";
            this.comboBox1.Size = new System.Drawing.Size(121, 21);
            this.comboBox1.TabIndex = 0;
            this.comboBox1.ValueMember = "Fllname";
            // 
            // brrwersPInfoBindingSource1
            // 
            this.brrwersPInfoBindingSource1.DataMember = "BrrwersPInfo";
            //this.brrwersPInfoBindingSource1.DataSource = this.db2DataSet2;
            // 
            // db2DataSet2
            // 
            //this.db2DataSet2.DataSetName = "db2DataSet2";
            //this.db2DataSet2.SchemaSerializationMode = System.Data.SchemaSerializationMode.IncludeSchema;
            // 
            // brrwersPInfoBindingSource
            // 
            this.brrwersPInfoBindingSource.DataMember = "BrrwersPInfo";
            //this.brrwersPInfoBindingSource.DataSource = this.db2DataSet2;
            // 
            // brrwersPInfoTableAdapter
            // 
            //this.brrwersPInfoTableAdapter.ClearBeforeFill = true;
            // 
            // Loan
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(569, 261);
            this.Controls.Add(this.comboBox1);
            this.Controls.Add(this.label7);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.dateTimePicker2);
            this.Controls.Add(this.dateTimePicker1);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.penalty);
            this.Controls.Add(this.balance);
            this.Controls.Add(this.interest);
            this.Controls.Add(this.loanamount);
            this.Name = "Loan";
            this.Text = "Loan";
            this.Load += new System.EventHandler(this.Loan_Load);
            ((System.ComponentModel.ISupportInitialize)(this.brrwersPInfoBindingSource1)).EndInit();
            //((System.ComponentModel.ISupportInitialize)(this.db2DataSet2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.brrwersPInfoBindingSource)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox loanamount;
        private System.Windows.Forms.TextBox interest;
        private System.Windows.Forms.TextBox balance;
        private System.Windows.Forms.TextBox penalty;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.DateTimePicker dateTimePicker1;
        private System.Windows.Forms.DateTimePicker dateTimePicker2;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.ComboBox comboBox1;
        //private db2DataSet2 db2DataSet2;
        private System.Windows.Forms.BindingSource brrwersPInfoBindingSource;
        //private db2DataSet2TableAdapters.BrrwersPInfoTableAdapter brrwersPInfoTableAdapter;
        private System.Windows.Forms.BindingSource brrwersPInfoBindingSource1;
    }
}