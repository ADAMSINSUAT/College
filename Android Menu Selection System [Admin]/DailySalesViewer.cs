using CrystalDecisions.CrystalReports.Engine;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Android_Menu_Selection_System__Admin_
{
    public partial class DailySalesViewer : Form
    {
        SqlCommand cmd;
        SqlDataAdapter da;
        SqlConnection conn;
        public string print;
        public DailySalesViewer()
        {
            InitializeComponent();
        }

        private void DailySalesViewer_Load(object sender, EventArgs e)
        {
            GetDailySale();
        }

        private void Database()
        {
            conn = ConnectDB.ConnectDatabase();
        }

        private void DailySalesViewer_KeyDown(object sender, KeyEventArgs e)
        {
            if(e.KeyCode == Keys.Escape)
            {
                this.Close();
            }
        }

        private void GetDailySale()
        {
            Database();
            ReportDocument crypt = new ReportDocument();
            DataSet ds;
            //print = "select * from InvoiceTable where InvoiceTransactionNumber='" + Convert.ToInt32(txtTransactionNumber.Text) + "'";
            da = new SqlDataAdapter(print, conn);
            ds = new DataSet();
            da.Fill(ds, "DailySalesTable");
            crypt.Load(@"C:\Users\asus\source\repos\Android Menu Selection System [Admin]\NewDailySalesCrystalReport.rpt");
            crypt.SetDataSource(ds);
            crystalReportViewer1.ReportSource = crypt;
        }
    }
}
