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

namespace Android_Menu_Selection_System__Admin_
{
    public partial class LoginForm : Form
    {
        SqlCommand cmd;
        SqlConnection conn;
        SqlDataReader dr;
        string ID, Username, UserPassword;
        public LoginForm()
        {
            InitializeComponent();
        }

        private void LoginForm_Load(object sender, EventArgs e)
        {
            Database();
            lblDate.Text = DateTime.Now.ToShortDateString();
            lblTime.Text = DateTime.Now.ToLongTimeString();
            timer1.Start();
        }

        private void Database()
        {
            conn = ConnectDB.ConnectDatabase();
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            lblTime.Text = DateTime.Now.ToLongTimeString();
        }

        private void btnLogin_Click(object sender, EventArgs e)
        {
            try
            {
                this.Database();
                cmd = new SqlCommand("Select UserID, UserName, UserPassword, UserRole, UserStatus from UserList where UserName ='" + txtUsername.Text + "' AND UserPassword ='" + txtPassword.Text + "'", conn);
                dr = cmd.ExecuteReader();
                if (dr.Read())
                {
                    ID = dr["UserID"].ToString();
                    Username = dr["Username"].ToString();
                    if (dr["UserStatus"].ToString() == "Active")
                    {
                        if (dr["UserRole"].ToString() == "Admin")
                        {
                            Main_Menu_Form mmf = new Main_Menu_Form();
                            conn.Close();
                            this.Database();
                            cmd = new SqlCommand("Insert into LoginTable(LoginID, LoginUserName, LoginDate, LoginTime)VALUES(@LoginID, @LoginUserName, @LoginDate, @LoginTime)", conn);
                            cmd.Parameters.AddWithValue("@LoginID", ID);
                            cmd.Parameters.AddWithValue("@LoginUserName", Username);
                            cmd.Parameters.AddWithValue("@LoginDate", lblDate.Text);
                            cmd.Parameters.AddWithValue("@LoginTime", lblTime.Text);
                            cmd.ExecuteNonQuery();
                            conn.Close();
                            mmf.lblID.Text = ID;
                            Database();
                            cmd = new SqlCommand("Select AdminPhoto from AdminTable where AdminID='" + ID + "'", conn);
                            dr = cmd.ExecuteReader();
                            dr.Read();
                            byte[] photo_array = (byte[])dr["AdminPhoto"];
                            MemoryStream ms = new MemoryStream(photo_array);
                            ms.Position = 0;
                            ms.Read(photo_array, 0, photo_array.Length);
                            mmf.lblUsername.Text = Username;
                            mmf.pctrBoxAdminID.Image= Image.FromStream(ms);
                            conn.Close();
                            this.Hide();
                            mmf.Show();
                        }
                        else if (dr["UserRole"].ToString() == "Cashier")
                        {
                            CashierForm cfm = new CashierForm(); 
                            //ID = dr["UserID"].ToString();
                            //Username = dr["Username"].ToString();

                            conn.Close();
                            this.Database();
                            cmd = new SqlCommand("Insert into LoginTable(LoginID, LoginUserName, LoginDate, LoginTime)VALUES(@LoginID, @LoginUserName, @LoginDate, @LoginTime)", conn);
                            cmd.Parameters.AddWithValue("@LoginID", ID);
                            cmd.Parameters.AddWithValue("@LoginUserName", Username);
                            cmd.Parameters.AddWithValue("@LoginDate", lblDate.Text);
                            cmd.Parameters.AddWithValue("@LoginTime", lblTime.Text);
                            cmd.ExecuteNonQuery();
                            conn.Close();
                            this.Hide();
                            cfm.logintime = lblTime.Text;
                            cfm.logindate = lblDate.Text;
                            cfm.lblCashierName.Text = Username;
                            cfm.Show();
                        }
                    }
                    else
                    {
                        MessageBox.Show("This account has been suspended or deactivated", "Error Message");
                    }
                }
                else
                {
                    MessageBox.Show("Incorrect username or password");
                }
            }
            catch(Exception b)
            {
                MessageBox.Show(b.ToString());
            }
        }

        private void btnExit_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }
    }
}
