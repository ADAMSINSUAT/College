using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Data.SqlClient;
using CrystalDecisions.Shared;
using CrystalDecisions.CrystalReports.Engine;

namespace Exer1
{
    public partial class Loanpaid : Form
    {
        SqlConnection conn;
        SqlCommand cmd;
        SqlDataAdapter da;
        SqlDataReader dr;

        public Loanpaid()
        {
            InitializeComponent();
        }

        private void Back_Click(object sender, EventArgs e)
        {
            this.Hide();
        }

        private void Loanpaid_Load(object sender, EventArgs e)
        {

        }

        private void data()
        {
            conn = Class1.Connectdb1();
            SqlDataAdapter da = new SqlDataAdapter("Select*from LoanPaind", conn);
            DataTable dt = new DataTable();
            da.Fill(dt);

            dataGridView1.Rows.Clear();

            foreach (DataRow item in dt.Rows)
            {
                int n = dataGridView1.Rows.Add();
                dataGridView1.Rows[n].Cells[0].Value = item["LoanID"].ToString();
                dataGridView1.Rows[n].Cells[1].Value = item["Secundname"].ToString();
                dataGridView1.Rows[n].Cells[2].Value = item["Furstname"].ToString();
                dataGridView1.Rows[n].Cells[3].Value = item["Trasnactno"].ToString();
                dataGridView1.Rows[n].Cells[4].Value = item["Interest"].ToString();
                dataGridView1.Rows[n].Cells[5].Value = item["Penalty"].ToString();
                dataGridView1.Rows[n].Cells[6].Value = item["Balance"].ToString();
                dataGridView1.Rows[n].Cells[7].Value = item["Change"].ToString();
                dataGridView1.Rows[n].Cells[8].Value = item["Pyment"].ToString();
                dataGridView1.Rows[n].Cells[9].Value = item["Dtofpayment"].ToString();
                dataGridView1.Rows[n].Cells[10].Value = item["Dudate"].ToString();
                dataGridView1.Rows[n].Cells[11].Value = item["Nirdate"].ToString();
                dataGridView1.Rows[n].Cells[12].Value = item["Amntforthisacct"].ToString();
            }
        }
    }
}
