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
    public partial class DisableCashierForm : Form
    {
        SqlConnection conn;
        SqlCommand cmd;
        SqlDataAdapter da;
        SqlDataReader dr;
        public DisableCashierForm()
        {
            InitializeComponent();
        }

        private void DisableCashierForm_Load(object sender, EventArgs e)
        {
            Database();
            refresh();
        }

        private void Database()
        {
            conn = ConnectDB.ConnectDatabase();
        }

        private void clear()
        {
            txtCashierID.Text = "";
            txtCashierName.Text = "";
            cmbStatus.Text = "";
        }

        private void refresh()
        {
            Database();
            da = new SqlDataAdapter("Select*from CashierInfoList", conn);
            DataTable table = new DataTable();
            da.Fill(table);
            dataGridView1.Rows.Clear();

            foreach (DataRow item in table.Rows)
            {
                int n = dataGridView1.Rows.Add();
                dataGridView1.Rows[n].Cells[0].Value = item["CashierID"].ToString();
                dataGridView1.Rows[n].Cells[1].Value = item["FirstName"].ToString();
                dataGridView1.Rows[n].Cells[2].Value = item["LastName"].ToString();
                dataGridView1.Rows[n].Cells[3].Value = item["MI"].ToString();
                dataGridView1.Rows[n].Cells[4].Value = item["Age"].ToString();
                dataGridView1.Rows[n].Cells[5].Value = item["Birthdate"].ToString();
                dataGridView1.Rows[n].Cells[6].Value = item["Gender"].ToString();
                dataGridView1.Rows[n].Cells[7].Value = item["Address"].ToString();
                dataGridView1.Rows[n].Cells[8].Value = item["City"].ToString();
                dataGridView1.Rows[n].Cells[9].Value = item["Zipcode"].ToString();
                dataGridView1.Rows[n].Cells[10].Value = item["Contactno"].ToString();
                dataGridView1.Rows[n].Cells[11].Value = item["Userpic"];
            }
        }

        private void dataGridView1_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            txtCashierID.Text = dataGridView1.CurrentRow.Cells[0].Value.ToString();
            txtCashierName.Text = dataGridView1.CurrentRow.Cells[1].Value.ToString()+" "+ dataGridView1.CurrentRow.Cells[3].Value.ToString()+" "+ dataGridView1.CurrentRow.Cells[2].Value.ToString();
            Database();
            cmd = new SqlCommand("Select Status from CashierLoginCredentials where EmpID='" + txtCashierID.Text + "'", conn);
            dr = cmd.ExecuteReader();
            if(dr.Read())
            {
                cmbStatus.Text = dr["Status"].ToString();
                dr.Close();
            }
            conn.Close();
        }

        private void btnUpdate_Click(object sender, EventArgs e)
        {
            Database();
            cmd = new SqlCommand("Update CashierLoginCredentials set Status=@Status where EmpID='"+txtCashierID.Text+"'", conn);
            cmd.Parameters.AddWithValue("@Status", cmbStatus.Text);
            cmd.ExecuteNonQuery();
            conn.Close();
            MessageBox.Show("Cashier status has been updated");
            clear();
        }

        private void btnStatus_Click(object sender, EventArgs e)
        {
            clear();
        }
    }
}
