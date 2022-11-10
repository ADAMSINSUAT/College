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
    public partial class Practiceduedate : Form
    {
        SqlConnection conn;
        SqlCommand cmd;
        SqlDataAdapter da;
        SqlDataReader dr;

        public Practiceduedate()
        {
            InitializeComponent();
        }

        private void Practiceduedate_Load(object sender, EventArgs e)
        {
            Class1.Connectdb1();
            refresh();
        }

        private void refresh()
        {
            conn = Class1.Connectdb1();
            da = new SqlDataAdapter("Select*from Loaninfo where Neardate<='"+DateTime.Now+"' or Duedate<='"+DateTime.Now+"'", conn);
            DataTable dt = new DataTable();
            da.Fill(dt);
            dataGridView1.Rows.Clear();
            conn.Close();
            foreach (DataRow item in dt.Rows)
            {
                double h = Convert.ToDouble(item["Blance"].ToString());
                
                int n = dataGridView1.Rows.Add();
                dataGridView1.Rows[n].Cells[0].Value = item["LonID"].ToString();
                dataGridView1.Rows[n].Cells[1].Value = item["Lastnim"].ToString();
                dataGridView1.Rows[n].Cells[2].Value = item["Firstnim"].ToString();
                dataGridView1.Rows[n].Cells[3].Value = item["Mnthly"].ToString();
                dataGridView1.Rows[n].Cells[4].Value = item["Duedate"].ToString();
                dataGridView1.Rows[n].Cells[5].Value = item["Pnalty"].ToString();
                dataGridView1.Rows[n].Cells[6].Value = item["Blance"].ToString();
                dataGridView1.Rows[n].Cells[7].Value = item["Neardate"].ToString();
                dataGridView1.Rows[n].Cells[8].Value = item["Duedate"].ToString();
                dataGridView1.Rows[n].Cells[9].Value = item["Intrst"].ToString();
                conn.Close();
                conn.Open();
            //    cmd = new SqlCommand("Select*from Loaninfo where Duedate<='" + DateTime.Now + "' or Neardate<='" + DateTime.Now + "'", conn);
            //dr=cmd.ExecuteReader();
            //if (dr.Read())
            //{
                
                DateTime p2 = Convert.ToDateTime(dr["Duedate"].ToString());
                double p3 = Convert.ToDouble(dr["Blance"].ToString());
                double amt = Convert.ToDouble(dr["Amntlnd"].ToString());
                double drat = Convert.ToDouble(dr["Dration"].ToString());
                double final;
                double f2 = p3;
                DateTime t= DateTime.Now;
                dr.Close();
                if (p2 <= t)
                {
                    final= f2-amt;
                    final = final / drat;
                    final = final * 0.02;
                    final = final*drat;
                    final = final + f2;

                    cmd = new SqlCommand("Update Loaninfo set Blance=@Blance where LonID='" + dataGridView1.Rows[n].Cells[0].Value + "'", conn);
                    cmd.Parameters.AddWithValue("@Blance", final);
                    cmd.ExecuteNonQuery();
                    dr.Close();
                }
            //}
            }

            //int p = dataGridView1.Rows.Add();
            //int p2=Convert.ToInt16(dataGridView1.Rows[p].Cells[6].Value);
            //int p3 = Convert.ToInt16(dataGridView1.Rows[p].Cells[9].Value);
            //int f2 = p2;
            //int f3 = p3;
            //double final;
            //final = f3 * 2;
            //final = final / 2;
            //final = final * 0.02;
            //final = f3+final;
            //dataGridView1.Rows[p].Cells[6].Value = final.ToString(); ;

            //cmd=new SqlCommand("Update Loaninfo set Blance=@Blance where Blance='"+p2+"'",conn);
            //cmd.Parameters.AddWithValue("@Blance", p2);
            //cmd.ExecuteNonQuery();
            //conn.Close();
        }

        private void penalty()
        {
        }
    }
}
