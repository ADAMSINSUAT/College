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
using System.IO;
using System.Drawing.Imaging;

namespace Android_Menu_Selection_System__Admin_
{
    public partial class AdminProfileForm : Form
    {
        SqlConnection conn;
        SqlDataAdapter da;
        SqlDataReader dr;
        SqlCommand cmd;
        public AdminProfileForm()
        {
            InitializeComponent();
        }

        private void AdminProfileForm_Load(object sender, EventArgs e)
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
            txtUserName.Text = "";
            txtPassword.Text = "";
            txtRePassword.Text = "";
            cmbStatus.Text = "";
            pictureBox1.Image= Android_Menu_Selection_System__Admin_.Properties.Resources.Userpic;
            lblpicpath.Text = "";
        }

        private void refresh()
        {
            this.Database();
            cmd = new SqlCommand("Select * from AdminTable", conn);
            dr = cmd.ExecuteReader();
            if (dr.Read())
            {
                lblAdminID.Text = dr["AdminID"].ToString();
                txtUserName.Text = dr["AdminUsername"].ToString();
                txtPassword.Text = dr["AdminPassword"].ToString();
                lblRole.Text = dr["Role"].ToString();
                cmbStatus.Text = dr["Status"].ToString();
                byte[] photo_array = (byte[])dr[5];
                MemoryStream ms = new MemoryStream(photo_array);
                ms.Position = 0;
                ms.Read(photo_array, 0, photo_array.Length);
                pictureBox1.Image = Image.FromStream(ms);
            }
            else
            {
                lblAdminID.Text = "180110";
                lblRole.Text = "Admin";
            }
            dr.Close();
            conn.Close();

        }

        private void btnAddPhoto_Click(object sender, EventArgs e)
        {
            OFD.Title = "Select an icon...";
            OFD.FileName = "";
            OFD.Filter = "jpeg|*.jpeg|png|*.png|bmp|*.bmp|All Files|*.*";
            OFD.FilterIndex = 1;

            OFD.ShowDialog();
            lblpicpath.Text = OFD.FileName;
            pictureBox1.ImageLocation = lblpicpath.Text;
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            if (txtUserName.Text == "")
            {
                MessageBox.Show("Username field must not be empty");
            }
            else if (txtPassword.Text == "")
            {
                MessageBox.Show("Password field must not be empty");
            }
            else if (txtRePassword.Text != txtPassword.Text)
            {
                MessageBox.Show("Re-password does not match");
            }
            else if (cmbStatus.Text == "")
            {
                MessageBox.Show("You must set a status");
            }
            else
            {
                this.Database();
                cmd = new SqlCommand("Select * from AdminTable", conn);
                dr = cmd.ExecuteReader();
                if (!dr.Read())
                {
                    lblAdminID.Text = "180110";
                    dr.Close();

                    MemoryStream ms = new MemoryStream();
                    pictureBox1.Image.Save(ms, ImageFormat.Jpeg);
                    byte[] pic_array = new byte[ms.Length];
                    ms.Position = 0;
                    ms.Read(pic_array, 0, pic_array.Length);
                    Database();
                    String insert = "Insert into AdminTable(AdminID, AdminUsername, AdminPassword, Role, Status, AdminPhoto)VALUES(@AdminID, @AdminUsername, @AdminPassword, @Role, @Status, @AdminPhoto)";
                    
                    dr.Close();
                    cmd = new SqlCommand(insert, conn);
                    cmd.Parameters.AddWithValue("@AdminID", lblAdminID.Text);
                    cmd.Parameters.AddWithValue("@AdminUsername", txtUserName.Text);
                    cmd.Parameters.AddWithValue("@AdminPassword", txtPassword.Text);
                    cmd.Parameters.AddWithValue("@Role", lblRole.Text);
                    cmd.Parameters.AddWithValue("@Status", cmbStatus.Text);
                    cmd.Parameters.AddWithValue("@AdminPhoto", pic_array);
                    cmd.ExecuteNonQuery();
                    if (System.Windows.Forms.Application.OpenForms["UserListForm"] != null)
                    {
                        (System.Windows.Forms.Application.OpenForms["UserListForm"] as UserListForm).refresh();
                    }
                    String insertadmin = "Insert into UserList(UserID, UserName, UserPassword, UserRole, UserStatus)VALUES(@UserID, @UserName, @UserPassword, @UserRole, @UserStatus)";

                    cmd = new SqlCommand(insertadmin, conn);
                    cmd.Parameters.AddWithValue("@UserID", lblAdminID.Text);
                    cmd.Parameters.AddWithValue("@UserName", txtUserName.Text);
                    cmd.Parameters.AddWithValue("@UserPassword", txtPassword.Text);
                    cmd.Parameters.AddWithValue("@UserRole", lblRole.Text);
                    cmd.Parameters.AddWithValue("@UserStatus", cmbStatus.Text);
                    cmd.ExecuteNonQuery();
                    conn.Close();
                    MessageBox.Show("Admin is added");
                    if (System.Windows.Forms.Application.OpenForms["UserListForm"] != null)
                    {
                        (System.Windows.Forms.Application.OpenForms["UserListForm"] as UserListForm).refresh();
                    }
                    clear();
                }
                else
                {
                    MessageBox.Show("Only one admin account can exist at a time");
                }
            }
        }

        private void btnUpdate_Click(object sender, EventArgs e)
        {
            MemoryStream ms = new MemoryStream();
            pictureBox1.Image.Save(ms, ImageFormat.Jpeg);
            byte[] pic_array = new byte[ms.Length];
            ms.Position = 0;
            ms.Read(pic_array, 0, pic_array.Length);
            Database();
            string update = "Update AdminTable set AdminUsername = @AdminUsername, AdminPassword = @AdminPassword, Role=@Role, Status=@Status, AdminPhoto=@AdminPhoto where AdminID ='" + lblAdminID.Text + "'";
           
            cmd = new SqlCommand(update, conn);
            cmd.Parameters.AddWithValue("@AdminID", lblAdminID.Text);
            cmd.Parameters.AddWithValue("@AdminUsername", txtUserName.Text);
            cmd.Parameters.AddWithValue("@AdminPassword", txtPassword.Text);
            cmd.Parameters.AddWithValue("@Role", lblRole.Text);
            cmd.Parameters.AddWithValue("@Status", cmbStatus.Text);
            cmd.Parameters.AddWithValue("@AdminPhoto", pic_array);
            cmd.ExecuteNonQuery();
            conn.Close();

            string updateuserlist = "Update UserList set UserName = @UserName, UserPassword = @UserPassword, UserRole=@UserRole, UserStatus=@UserStatus where UserID ='" + lblAdminID.Text + "'";
            Database();
            cmd = new SqlCommand(updateuserlist, conn);
            cmd.Parameters.AddWithValue("@UserName", txtUserName.Text);
            cmd.Parameters.AddWithValue("@UserPassword", txtPassword.Text);
            cmd.Parameters.AddWithValue("@UserRole", lblRole.Text);
            cmd.Parameters.AddWithValue("@UserStatus", cmbStatus.Text);
            cmd.ExecuteNonQuery();
            conn.Close();
            if (System.Windows.Forms.Application.OpenForms["UserListForm"] != null)
            {
                (System.Windows.Forms.Application.OpenForms["UserListForm"] as UserListForm).refresh();
            }
            MessageBox.Show("Admin info has been updated");
            clear();
        }

        private void btnClear_Click(object sender, EventArgs e)
        {
            clear();
        }

        private void btnExit_Click(object sender, EventArgs e)
        {
            if (System.Windows.Forms.Application.OpenForms["Main_Menu_Form"] != null)
            {
                (System.Windows.Forms.Application.OpenForms["Main_Menu_Form"] as Main_Menu_Form).panelSettingsFormLoader.Visible=false;
                this.Close();
            }
        }
    }
}
