using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

//import
using System.Data.SqlClient;
using CrystalDecisions.Shared;
using CrystalDecisions.CrystalReports.Engine;

namespace Payroll_System
{
    public partial class Printemp : Form
    {
        Constring cs = new Constring();
        SqlConnection conn;
        SqlCommand cmd;
        SqlDataAdapter da;

        public static string printtext;

        public Printemp()
        {
            InitializeComponent();
        }

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

        private void Printemp_Load(object sender, EventArgs e)
        {
            this.connectdatabase();
            this.print();
        }

        private void print()
        {
            printtext = Addemp.passingText;

            this.connectdatabase();
            ReportDocument crypt = new ReportDocument();
            DataSet ds;

            cmd = new SqlCommand("select * from tbladdemp where empid='" + printtext + "'", conn);

            da = new SqlDataAdapter(cmd);
            ds = new DataSet();
            da.Fill(ds, "tbladdemp");
            crypt.Load("Printemp.rpt");
            crypt.SetDataSource(ds);
            crystalReportViewer1.ReportSource = crypt;
            crystalReportViewer1.Visible = true;
        }
    }
}
