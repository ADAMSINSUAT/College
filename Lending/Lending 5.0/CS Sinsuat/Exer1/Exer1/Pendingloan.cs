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
    public partial class Pendingloan : Form
    {
        SqlConnection conn;
        SqlCommand cmd;
        SqlDataAdapter da;
        SqlDataReader dr;

        public Pendingloan()
        {
            InitializeComponent();
        }

        private void Pendingloan_Load(object sender, EventArgs e)
        {
            data();
        }

        private void data()
        {
            conn = Class1.Connectdb1();
            da = new SqlDataAdapter("Select*from Loaninfo", conn);
            DataTable dt = new DataTable();
            da.Fill(dt);

            dataGridView1.Rows.Clear();

            foreach (DataRow item in dt.Rows)
            {
                int n = dataGridView1.Rows.Add();
                dataGridView1.Rows[n].Cells[0].Value = item["LonID"].ToString();
                dataGridView1.Rows[n].Cells[1].Value = item["Transactno"].ToString();
                dataGridView1.Rows[n].Cells[2].Value = item["Lastnim"].ToString();
                dataGridView1.Rows[n].Cells[3].Value = item["Firstnim"].ToString();
                dataGridView1.Rows[n].Cells[4].Value = item["Amntlnd"].ToString();
                dataGridView1.Rows[n].Cells[5].Value = item["Intrst"].ToString();
                dataGridView1.Rows[n].Cells[6].Value = item["Pnalty"].ToString();
                dataGridView1.Rows[n].Cells[7].Value = item["Intiallon"].ToString();
                dataGridView1.Rows[n].Cells[8].Value = item["Mnthly"].ToString();
                dataGridView1.Rows[n].Cells[9].Value = item["Amntinwords"].ToString();
                dataGridView1.Rows[n].Cells[10].Value = item["Dtoflon"].ToString();
                dataGridView1.Rows[n].Cells[11].Value = item["Blance"].ToString();
                dataGridView1.Rows[n].Cells[12].Value = item["Dration"].ToString();
                dataGridView1.Rows[n].Cells[13].Value = item["Amntpaidthismonth"].ToString();
                dataGridView1.Rows[n].Cells[14].Value = item["Neardate"].ToString();
                dataGridView1.Rows[n].Cells[15].Value = item["Duedate"].ToString();
            }
        }

        private void Back_Click(object sender, EventArgs e)
        {
            this.Hide();
        }
    }
}
