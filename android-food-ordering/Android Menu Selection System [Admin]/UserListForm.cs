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
    public partial class UserListForm : Form
    {
        SqlConnection conn;
        SqlDataAdapter da;
        SqlCommand cmd;
        public UserListForm()
        {
            InitializeComponent();
        }

        private void UserListForm_Load(object sender, EventArgs e)
        {
            Database();
            refresh();
        }

        private void Database()
        {
            conn = ConnectDB.ConnectDatabase();
        }

        public void refresh()
        {
            Database();
            da = new SqlDataAdapter("Select * from UserList", conn);
            DataTable dt = new DataTable();
            da.Fill(dt);
            dataGridView1.Rows.Clear();

            foreach (DataRow item in dt.Rows)
            {
                int n = dataGridView1.Rows.Add();
                dataGridView1.Rows[n].Cells[0].Value = item["UserID"].ToString();
                dataGridView1.Rows[n].Cells[1].Value = item["UserName"].ToString();
                dataGridView1.Rows[n].Cells[2].Value = item["UserPassword"].ToString();
                dataGridView1.Rows[n].Cells[3].Value = item["UserRole"].ToString();
                dataGridView1.Rows[n].Cells[4].Value = item["UserStatus"].ToString();
            }
        }

        private void dataGridView1_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            if (dataGridView1.CurrentRow.Cells[3].Value.ToString() == "Cashier")
            {
                pnlMessagePrompt.Visible = true;
            }
            else if (dataGridView1.CurrentRow.Cells[3].Value.ToString() == "Admin")
            {
                pnlMessagePrompt.Visible = true;
            }
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            pnlMessagePrompt.Visible = false;
        }

        private void btnYes_Click(object sender, EventArgs e)
        {
            pnlMessagePrompt.Visible = false;
            ManageCashier mc = new ManageCashier();
            mc.Width = 816;
            mc.Height = 459;
            mc.btnExit.Visible = true;
            mc.Show();
            mc.CashierID.Text = "";
            mc.CashierID.Text = dataGridView1.CurrentRow.Cells[0].Value.ToString();
        }

        private void btnNo_Click(object sender, EventArgs e)
        {
            pnlMessagePrompt.Visible = false;
            ManageCashier mc = new ManageCashier();
            mc.Width = 816;
            mc.Height = 459;
            mc.btnExit.Visible = true;
            mc.Show();
            mc.CashierID.Text = "";
            mc.CashierID.Text = dataGridView1.CurrentRow.Cells[0].Value.ToString();
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            pnlMessagePrompt.Visible = false;
            if (System.Windows.Forms.Application.OpenForms["Main_Menu_Form"] != null)
            {
                (System.Windows.Forms.Application.OpenForms["Main_Menu_Form"] as Main_Menu_Form).panelSettingsFormLoader.Visible=false;
                (System.Windows.Forms.Application.OpenForms["Main_Menu_Form"] as Main_Menu_Form).panelSettingsFormLoader.Location = new Point(220, 200);
                this.Close();
            }
        }
    }
}
