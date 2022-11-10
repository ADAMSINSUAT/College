using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Data.SqlClient;

namespace Exer1
{
    public partial class Loan : Form
    {
        SqlConnection conn;
        SqlCommand cmd;
        SqlDataAdapter da;
        SqlDataReader dr;

        public Loan()
        {
            InitializeComponent();
        }

        private void Loan_Load(object sender, EventArgs e)
        {
            //DateTime date = DateTime.Now;
            //dateTimePicker1.Text = date.ToString();
            //int months = dateTimePicker1.Value.Month + 2;
            //months = Convert.ToDateTime();
            //dateTimePicker2.Value.AddMonths(2);
      
            // TODO: This line of code loads data into the 'db2DataSet2.BrrwersPInfo' table. You can move, or remove it, as needed.
            //this.brrwersPInfoTableAdapter.Fill(this.db2DataSet2.BrrwersPInfo);
            loanamount.Text = "";
            interest.Text = "";
            balance.Text = "";
            penalty.Text = "";
            comboBox1.Text = "Select";
        }
        private void clear()
        { 
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (balance.Text == "" && interest.Text == "" && penalty.Text == "")
            {
                MessageBox.Show("Please fill up all fields");
            }
            else
            {
                conn = Class1.Connectdb1();
                cmd = new SqlCommand("Select*from Loaninfo where Name='" + comboBox1.Text + "'", conn);
                dr = cmd.ExecuteReader();
                dr.Read();
                if (dr.Read())
                {
                    MessageBox.Show("This borrower still has a pending/balance loan");
                }
                else
                {
                    dr.Close();
                    cmd = new SqlCommand("Insert into Loaninfo(Amntlnd, Dtoflon, Intrst, Duedate, Pnalty, Blance, Name)VALUES(@Amntlnd, @Dtoflon, @Intrst, @Duedate, @Pnalty, @Blance, @Name)", conn);
                    cmd.Parameters.AddWithValue("@Amntlnd", loanamount.Text);
                    cmd.Parameters.AddWithValue("@Dtoflon", dateTimePicker1.Text);
                    cmd.Parameters.AddWithValue("@Intrst", interest.Text);
                    cmd.Parameters.AddWithValue("@Duedate", dateTimePicker2.Text);
                    cmd.Parameters.AddWithValue("@Pnalty", penalty.Text);
                    cmd.Parameters.AddWithValue("@Blance", balance.Text);
                    cmd.Parameters.AddWithValue("@Name", comboBox1.Text);
                    DialogResult result = MessageBox.Show("Confirm?", "Confirmation Message...", MessageBoxButtons.YesNo);
                    if (result == DialogResult.Yes)
                    {
                        cmd.ExecuteNonQuery();
                        comboBox1.ResetText();
                        loanamount.Clear();
                        interest.Clear();
                        dateTimePicker1.ResetText();
                        dateTimePicker2.ResetText();
                        penalty.Clear();
                        balance.Clear();
                    }
                }
            }
        }

        private void dateTimePicker2_ValueChanged(object sender, EventArgs e)
        {
            int years = dateTimePicker1.Value.Month+2;
            dateTimePicker2.Text = years.ToString();
        }

        private void dateTimePicker1_ValueChanged(object sender, EventArgs e)
        {
              }
    }
}
