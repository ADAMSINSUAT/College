using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.SqlClient;

namespace Android_Menu_Selection_System__Admin_
{
    public partial class Test_Connection : Form
    {
        SqlConnection conn;
        SqlCommand cmd;
        SqlDataReader dr;
        public Test_Connection()
        {
            InitializeComponent();
        }
        private void Database()
        {
            conn = ConnectDB.ConnectDatabase();
        }

        private void btnMonth_Click(object sender, EventArgs e)
        {
            Database();
            cmd = new SqlCommand("Select * from InvoiceTable where Month(InvoiceDate)='" + Convert.ToInt32(DateTime.Now.Month) + "'", conn);
            dr = cmd.ExecuteReader();
            if (dr.Read())
            {
                DateTime month = Convert.ToDateTime(dr["InvoiceDate"].ToString());
                MessageBox.Show("Month:" + month.ToString("MM/dd/yyyy"));
            }
        }

        private void Test_Connection_Load(object sender, EventArgs e)
        {
            this.Database();
        }

        private void btnTestConnection_Click(object sender, EventArgs e)
        {
            Database();
        }
    }
}
