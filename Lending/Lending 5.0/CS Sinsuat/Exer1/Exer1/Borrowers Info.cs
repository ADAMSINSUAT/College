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
    public partial class Borrowers_Info : Form
    {
        SqlConnection conn;
        SqlCommand cmd;
        SqlDataAdapter da;
        SqlDataReader dr;

        public Borrowers_Info()
        {
            InitializeComponent();
        }

        private void Borrowers_Info_Load(object sender, EventArgs e)
        {
            data();
        }

        private void data()
        {
            conn = Class1.Connectdb1();
            da = new SqlDataAdapter("Select*from BrrwersPInfo", conn);
            DataTable dt = new DataTable();
            da.Fill(dt);

            dataGridView1.Rows.Clear();

            foreach (DataRow item in dt.Rows)
            {
                int n = dataGridView1.Rows.Add();
                dataGridView1.Rows[n].Cells[0].Value = item["ApplcntID"].ToString();
                dataGridView1.Rows[n].Cells[1].Value = item["Lstname"].ToString();
                dataGridView1.Rows[n].Cells[2].Value = item["Frstname"].ToString();
                dataGridView1.Rows[n].Cells[3].Value = item["MI"].ToString();
                dataGridView1.Rows[n].Cells[4].Value = item["Age"].ToString();
                dataGridView1.Rows[n].Cells[5].Value = item["Bday"].ToString();
                dataGridView1.Rows[n].Cells[6].Value = item["Plcfbrth"].ToString();
                dataGridView1.Rows[n].Cells[7].Value = item["CvilStats"].ToString();
                dataGridView1.Rows[n].Cells[8].Value = item["Gnder"].ToString();
                dataGridView1.Rows[n].Cells[9].Value = item["BrrwersStats"].ToString();
                dataGridView1.Rows[n].Cells[10].Value = item["Addrss"].ToString();
                dataGridView1.Rows[n].Cells[11].Value = item["Cty"].ToString();
                dataGridView1.Rows[n].Cells[12].Value = item["Prvince"].ToString();
                dataGridView1.Rows[n].Cells[13].Value = item["Zpcde"].ToString();
                dataGridView1.Rows[n].Cells[14].Value = item["Email"].ToString();
                dataGridView1.Rows[n].Cells[15].Value = item["Lndlne"].ToString();
                dataGridView1.Rows[n].Cells[16].Value = item["Cno"].ToString();
                dataGridView1.Rows[n].Cells[17].Value = item["Occpatn"].ToString();
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.Hide();
        }
    }
}
