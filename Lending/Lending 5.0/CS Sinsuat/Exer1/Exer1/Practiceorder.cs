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
    public partial class Practiceorder : Form
    {
        SqlConnection conn;
        SqlCommand cmd;
        SqlDataAdapter da;
        SqlDataReader dr;

        public Practiceorder()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            conn = Class1.Connectdb1();
            cmd = new SqlCommand("Select*from Loaninfo where LonID='"+LOANID.Text+"'", conn);
            dr = cmd.ExecuteReader();
            if (dr.Read())
            {
                cmd = new SqlCommand("Select TOP(1) Transactno from Loaninfo ORDER By 1 DESC", conn);
                //dr = cmd.ExecuteReader();
                //dr.Read();
                LOANID.Text = dr[0].ToString();
                LASTNAME.Text = dr[1].ToString();
                FIRSTNAME.Text = dr[2].ToString();
                AMOUNTLOANED.Text = dr[3].ToString();
                DATEOFLOAN.Text = dr[4].ToString();
                INTEREST.Text = dr[5].ToString();
                PENALTY.Text = dr[6].ToString();
                INITIALLOAN.Text=dr[7].ToString();
                MONTHLY.Text = dr[8].ToString();
                AMNTINWORDS.Text = dr[9].ToString();
                BALANCE.Text = dr[10].ToString();
                DURATION.Text = dr[11].ToString();
                AMOUNTPAIDTHISMONTH.Text = dr[12].ToString();
                TRANSACTNO.Text = dr[13].ToString();
                NEARDATE.Text = dr[14].ToString();
            }
        }

        private void Practiceorder_Load(object sender, EventArgs e)
        {
            Class1.Connectdb1();
        }
    }
}
