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
    public partial class Userlist : Form
    {
        SqlConnection conn;
        SqlCommand cmd;
        SqlDataAdapter da;
        SqlDataReader dr;

        public Userlist()
        {
            InitializeComponent();
        }

        private void Userlist_Load(object sender, EventArgs e)
        {
            conn = Class1.Connectdb1();
            da = new SqlDataAdapter("Select * from Users ", conn);
            DataTable dt = new DataTable();
            da.Fill(dt);

            dataGridView1.Rows.Clear();

            foreach (DataRow item in dt.Rows)
            {
                int n = dataGridView1.Rows.Add();
                dataGridView1.Rows[n].Cells[0].Value = item["Accid"].ToString();
                dataGridView1.Rows[n].Cells[1].Value = item["FirstName"].ToString();
                dataGridView1.Rows[n].Cells[2].Value = item["MiddleI"].ToString();
                dataGridView1.Rows[n].Cells[3].Value = item["LastName"].ToString();
                dataGridView1.Rows[n].Cells[4].Value = item["Address"].ToString();
                dataGridView1.Rows[n].Cells[5].Value = item["ContactNo"].ToString();
                dataGridView1.Rows[n].Cells[6].Value = item["Landline"].ToString();
                dataGridView1.Rows[n].Cells[7].Value = item["Birthday"].ToString();
                dataGridView1.Rows[n].Cells[8].Value = item["Birthage"].ToString();
                dataGridView1.Rows[n].Cells[9].Value = item["Position"].ToString();
                dataGridView1.Rows[n].Cells[10].Value = item["Usename"].ToString();
                dataGridView1.Rows[n].Cells[11].Value = item["Pword"].ToString();
                dataGridView1.Rows[n].Cells[12].Value = item["Role"].ToString();
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.Hide();
        }
    }
}
