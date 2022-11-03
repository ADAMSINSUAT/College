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

    public partial class CustomerInvoiceViewer : Form
    {
        SqlCommand cmd;
        SqlConnection conn;
        SqlDataAdapter da;
        SqlDataReader dr;
        public CustomerInvoiceViewer()
        {
            InitializeComponent();
        }

        private void CustomerInvoiceViewer_Load(object sender, EventArgs e)
        {
            PrintReceipt();
            Database();
        }

        private void Database()
        {
            conn = ConnectDB.ConnectDatabase();
        }

        private void PrintReceipt()
        {
            Database();
            ReportDocument crypt = new ReportDocument();
            DataSet ds;
            string print = "select * from InvoiceTable where InvoiceTransactionNumber='" + Convert.ToInt32(txtTransactionNumber.Text) + "'";
            da = new SqlDataAdapter(print, conn);
            ds = new DataSet();
            da.Fill(ds, "InvoiceTable");
            crypt.Load(@"C:\Users\asus\source\repos\Android Menu Selection System [Admin]\CustomerReceiptCrystalReport.rpt");
            crypt.SetDataSource(ds);
            crystalReportViewer1.ReportSource = crypt;
            //if (System.Windows.Forms.Application.OpenForms["CashierForm"] != null)
            //{
            //    (System.Windows.Forms.Application.OpenForms["CashierForm"] as CashierForm).Clear();
            //}
        }

        private void CustomerInvoiceViewer_KeyDown(object sender, KeyEventArgs e)
        {
            if(e.KeyCode==Keys.Escape)
            {
                this.Close();
            }
        }
    }
}
