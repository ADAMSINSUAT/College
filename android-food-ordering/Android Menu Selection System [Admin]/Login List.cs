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
    public partial class Login_List : Form
    {
        SqlConnection conn;
        SqlCommand cmd;
        SqlDataAdapter da;
        SqlDataReader dr;
        public Login_List()
        {
            InitializeComponent();
        }
        private void Login_List_Load(object sender, EventArgs e)
        {
            refresh();
        }

        private void Database()
        {
            conn = ConnectDB.ConnectDatabase();
        }

        private void refresh()
        {
            Database();
            da = new SqlDataAdapter("Select*from LoginTable", conn);
            DataTable dt = new DataTable();
            da.Fill(dt);

            dataGridView1.Rows.Clear();

            foreach(DataRow item in dt.Rows)
            {
                int n = dataGridView1.Rows.Add();
                dataGridView1.Rows[n].Cells[0].Value = item["LoginID"].ToString();
                dataGridView1.Rows[n].Cells[1].Value = item["LoginUserName"].ToString();
                dataGridView1.Rows[n].Cells[2].Value = item["LoginDate"].ToString();
                dataGridView1.Rows[n].Cells[3].Value = item["LoginTime"].ToString();
                dataGridView1.Rows[n].Cells[4].Value = item["LogoutTime"].ToString();
            }
        }
        private void btnFilter_Click(object sender, EventArgs e)
        {
            Database();
            da = new SqlDataAdapter("SELECT * FROM LoginTable where [LoginDate] between '"+dtpFromDate.Value+ "' AND '"+dtpToDate.Value+"'", conn);
            DataTable dt = new DataTable();
            da.Fill(dt);
            dataGridView1.Rows.Clear();
            foreach(DataRow item in dt.Rows)
            {
                int n = dataGridView1.Rows.Add();
                dataGridView1.Rows[n].Cells[0].Value = item["LoginID"].ToString();
                dataGridView1.Rows[n].Cells[1].Value = item["LoginUserName"].ToString();
                dataGridView1.Rows[n].Cells[2].Value = item["LoginDate"].ToString();
                dataGridView1.Rows[n].Cells[3].Value = item["LoginTime"].ToString();
                dataGridView1.Rows[n].Cells[4].Value = item["LogoutTime"].ToString();
            }
            conn.Close();
        }
    }
}
