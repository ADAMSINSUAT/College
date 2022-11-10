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
    public partial class Form1 : Form
    {
        SqlConnection conn;
        SqlCommand cmd;
        SqlDataAdapter da;
        SqlDataReader dr;
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            timer1.Start();
            Class1.Connectdb1();
            data();
            label2.Text = DateTime.Now.ToLongTimeString();
            label3.Text = DateTime.Now.ToShortDateString();
        }

        private void toolStripButton1_Click(object sender, EventArgs e)
        {
            Mainform mf = new Mainform();
            conn = Class1.Connectdb1();
            cmd = new SqlCommand("Select TOP(1) ApplcntID from BrrwersPInfo ORDER BY 1 DESC", conn);
            dr = cmd.ExecuteReader();
            if (dr.Read())
            {
                mf.BorrowersID.Text = dr["ApplcntID"].ToString();
                int a;
                a = Convert.ToInt16(mf.BorrowersID.Text);
                a++;
                mf.BorrowersID.Text = a.ToString();
                dr.Close();
                conn.Close();
            }
            mf.Show();
           
        }

        private void data()
        {
            conn = Class1.Connectdb1();
            DateTime j = DateTime.Now.AddMonths(1);
            DateTime j1 = DateTime.Now.AddDays(-3);
                da = new SqlDataAdapter("Select*from Loaninfo where Neardate<='"+j+"' OR Neardate<='"+j1+"'", conn);
                DataTable dt = new DataTable();
                da.Fill(dt);
                    dataGridView1.Rows.Clear();

                    foreach (DataRow item in dt.Rows)
                    {                 
                            int n = dataGridView1.Rows.Add();
                            dataGridView1.Rows[n].Cells[0].Value = item["LonID"].ToString();
                            dataGridView1.Rows[n].Cells[1].Value = item["Lastnim"].ToString();
                            dataGridView1.Rows[n].Cells[2].Value = item["Firstnim"].ToString();
                            dataGridView1.Rows[n].Cells[3].Value = item["Mnthly"].ToString();
                            dataGridView1.Rows[n].Cells[4].Value = item["Blance"].ToString();
                            dataGridView1.Rows[n].Cells[5].Value = item["Neardate"].ToString();
                    }
                    
        }

        private void toolStripButton3_Click(object sender, EventArgs e)
        {
            conn = Class1.Connectdb1();
            cmd = new SqlCommand("Update Loginf set timeout=@timeout where Username='" + textBox2.Text + "' and timein='"+textBox3.Text+"' and date='"+textBox4.Text+"'", conn);
            cmd.Parameters.AddWithValue("@timeout", DateTime.Now.ToShortTimeString());
            cmd.ExecuteNonQuery();
            conn.Close();
            this.Close();
            Login lg = new Login();
            lg.Show();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            Loanpaid lp = new Loanpaid();
            lp.Show();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            Addusers adu = new Addusers();
            this.Hide();
            adu.Show();
        }

        private void toolStripButton2_Click(object sender, EventArgs e)
        {
            panel1.Show();
        }

        private void button5_Click(object sender, EventArgs e)
        {
            panel1.Hide();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Userlist ul = new Userlist();
            ul.Show();
        }

        private void button6_Click(object sender, EventArgs e)
        {
            Loginlists lg = new Loginlists();
            lg.ShowDialog();
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
        }

        private void dataGridView1_MouseDoubleClick_1(object sender, MouseEventArgs e)
        {
            Mainform mf = new Mainform();
            mf.label40.Text = dataGridView1.CurrentRow.Cells[0].Value.ToString();
            mf.ShowDialog();
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            label2.Text = DateTime.Now.ToLongTimeString();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            Pendingloan pd = new Pendingloan();
            pd.Show();
        }

        private void button7_Click(object sender, EventArgs e)
        {
            Borrowers_Info bp = new Borrowers_Info();
            bp.Show();
        }

       

    }
}
