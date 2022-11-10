using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Data.SqlClient;
using CrystalDecisions.CrystalReports.Engine;
using CrystalDecisions.Shared;

namespace Exer1
{
    public partial class Loginlists : Form
    {
        SqlConnection conn;
        SqlCommand cmd;
        SqlDataAdapter da;
        SqlDataReader dr;

        public Loginlists()
        {
            InitializeComponent();
        }

        private void Loginlists_Load(object sender, EventArgs e)
        {
            conn = Class1.Connectdb1();
            da = new SqlDataAdapter("Select * from Loginf ", conn);
            DataTable dt = new DataTable();
            da.Fill(dt);

            dataGridView1.Rows.Clear();

            foreach (DataRow item in dt.Rows)
            {
                int n = dataGridView1.Rows.Add();
                dataGridView1.Rows[n].Cells[0].Value = item["Username"].ToString();
                dataGridView1.Rows[n].Cells[1].Value = item["timein"].ToString();
                dataGridView1.Rows[n].Cells[2].Value = item["date"].ToString();
                dataGridView1.Rows[n].Cells[3].Value = item["accesslvl"].ToString();
                dataGridView1.Rows[n].Cells[4].Value = item["timeout"].ToString();
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.Hide();
        }

        private void Print_Click(object sender, EventArgs e)
        {
            panel1.Show();
            ReportDocument crypt = new ReportDocument();
            DataSet ds;
            cmd = new SqlCommand("select * from Loginf", conn);
            da = new SqlDataAdapter(cmd);
            ds = new DataSet();
            da.Fill(ds, "Loginf");
            crypt.Load(@"C:\Users\asus\Desktop\Adam\Important Files\CS Sinsuat\Exer1\Exer1\CrystalReport2.rpt");
            crypt.SetDataSource(ds);
            crystalReportViewer1.ReportSource = crypt;
        }

        private void button2_Click(object sender, EventArgs e)
        {
            panel1.Hide();
        }
    }
}
