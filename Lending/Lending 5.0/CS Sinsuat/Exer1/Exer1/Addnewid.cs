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
    public partial class Addnewid : Form
    {
        SqlConnection conn;
        SqlCommand cmd;
        SqlDataAdapter da;
        SqlDataReader dr;

        public Addnewid()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            //conn = Class1.Connectdb1();
            //cmd = new SqlCommand("Select TOP(1) ID from practice ORDER BY 1 DESC", conn);
            //dr = cmd.ExecuteReader();
            //dr.Read();
            ////string data = dr["ID"].ToString();
            //textBox1.Text = dr["ID"].ToString();
            ////data = Convert.ToString(textBox1.Text);
            ////textBox1.Text=dr.ToString();
            //dr.Close();
            //conn.Close();
        }

        private void Addnewid_Load(object sender, EventArgs e)
        {
            conn = Class1.Connectdb1();
            cmd = new SqlCommand("Select TOP(1) ID from practice ORDER BY 1 DESC", conn);
            dr = cmd.ExecuteReader();
            dr.Read();
            //string data = dr["ID"].ToString();
            textBox1.Text = dr["ID"].ToString();
            int a;
            a = Convert.ToInt16(textBox1.Text);
            a++;
            textBox1.Text = a.ToString();
            //data = Convert.ToString(textBox1.Text);
            //textBox1.Text=dr.ToString();
            dr.Close();
            conn.Close();
        }
    }
}
