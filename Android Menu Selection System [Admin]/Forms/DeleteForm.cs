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
    public partial class DeleteForm : Form
    {
        SqlConnection conn;
        SqlCommand cmd;
        SqlDataReader dr;

        public DeleteForm()
        {
            InitializeComponent();
        }

        private void DeleteForm_Load(object sender, EventArgs e)
        {
            AutoCompleSearch();
            Database();
        }

        private void Database()
        {
            conn = ConnectDB.ConnectDatabase();
        }

        void AutoCompleSearch()
        {
            this.Database();
            cmd = new SqlCommand("Select UserID, UserName from UserList where UserID LIKE @UserID OR UserName LIKE @UserName", conn);
            cmd.Parameters.Add(new SqlParameter("@UserID", "%" + txtSearch.Text + "%"));
            cmd.Parameters.Add(new SqlParameter("@UserName", "%" + txtSearch.Text + "%"));
            //cmd = new SqlCommand("Select productname from POS where productname LIKE @productname", conn);


            cmd.ExecuteNonQuery();
            dr = cmd.ExecuteReader();

            AutoCompleteStringCollection coll = new AutoCompleteStringCollection();

            while (dr.Read())
            {
                coll.Add(dr.GetValue(0).ToString());
                coll.Add(dr.GetValue(1).ToString());
            }
            txtSearch.AutoCompleteCustomSource = coll;
            dr.Close();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.Database();
            cmd = new SqlCommand("Delete from AdminTable where AdminID = '" + txtSearch.Text + "'", conn);
            cmd.ExecuteNonQuery();
            conn.Close();
            MessageBox.Show("User is deleted");
        }
    }
}
