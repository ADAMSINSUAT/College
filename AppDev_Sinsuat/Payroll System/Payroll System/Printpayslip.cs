using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

//Import libraries
using System.Data.SqlClient;
using System.IO;
using CrystalDecisions.Shared;
using CrystalDecisions.CrystalReports.Engine;

namespace Payroll_System
{
    public partial class Printpayslip : Form
    {
        //Declare these
        Constring cs = new Constring();
        SqlConnection conn;
        SqlCommand cmd;
        SqlDataAdapter da;
        SqlDataReader dr;

        public Printpayslip()
        {
            InitializeComponent();
        }

        //Declare this function for database connection
        private void connectdatabase()
        {
            try
            {
                conn = new SqlConnection(cs.connection);
                conn.Open();
            }
            catch
            {
                MessageBox.Show("Sql Server Not Responding");
            }
        }

        //Form Load
        private void Printpayslip_Load(object sender, EventArgs e)
        {
            this.connectdatabase();
        }

        //Button Search Codes
        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                if (textBox1.Text == "")
                {
                    MessageBox.Show("Please input Employee No.");
                    textBox1.Select();
                }
                else
                {
                    this.connectdatabase();
                    cmd= new SqlCommand("select * from tblpayroll where datefrom='"+ dateTimePicker1.Text+"' and dateto='"+ dateTimePicker2.Text+"'and empid='" +textBox1.Text+"'",conn);
                    dr = cmd.ExecuteReader();
                    if(dr.Read())
                    {
                        print();
                        dr.Close();
                    }
                    else
                    {
                        MessageBox.Show("Record not found");
                    }
                }
            }
            catch
            {
            }
        }

        //Declare this function
        private void print()
        {
            try
            {
                this.connectdatabase();
                ReportDocument crypt= new ReportDocument();
                DataSet ds;

                cmd= new SqlCommand("select * from tblpayroll where datefrom='"+ dateTimePicker1.Text+"' and dateto='"+ dateTimePicker2.Text+"'and empid='" +textBox1.Text+"'",conn);
                da = new SqlDataAdapter(cmd);
                ds = new DataSet();
                da.Fill(ds, "tblpayroll");
                crypt.Load("Printpayslip.rpt");
                crypt.SetDataSource(ds);
                crystalReportViewer1.ReportSource = crypt;
                crystalReportViewer1.Visible = true;
            }
            catch
            {
            }
        }
    }
}
