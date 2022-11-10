using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Data.SqlClient;

namespace Exer1
{
    public partial class Login : Form
    {
        SqlConnection conn;
        SqlCommand cmd;
        SqlDataAdapter da;
        SqlDataReader dr;
        public static string passingText;//

        public Login()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            conn = Class1.Connectdb1();
                cmd = new SqlCommand("Select Usename, Pword, Role from Users where Usename='" + Username.Text + "' and Pword='" + Password.Text + "'", conn);
                dr = cmd.ExecuteReader();
                if (dr.Read())
                {
                    if (dr["Role"].ToString() != "Admin")
                    {
                        this.Hide();
                        Mainform mf = new Mainform();
                        mf.Show();
                        logfunction();
                    }
                    else if (dr["Role"].ToString() == "Admin")
                    {
                        MessageBox.Show("Admin");

                        Form1 frm1 = new Form1();
                        frm1.textBox2.Text = Username.Text;
                        frm1.textBox3.Text = label3.Text;
                        frm1.textBox4.Text = label4.Text;

                        frm1.Show();
                        this.Hide();
                        logfunction();
                    }
                }
                else
                {
                    MessageBox.Show("Please register first... Contact the admin for further instructions", "User does not exist...");
                }
                conn.Close();
                //logfunction();
                Username.Text = "";
                Password.Text = "";
        }

        private void Login_Load(object sender, EventArgs e)
        {
            label3.Text = DateTime.Now.ToLongTimeString();
            label4.Text = DateTime.Now.ToShortDateString();
            timer1.Start();
        }

        public static string sendtext = "";

        private void timer1_Tick(object sender, EventArgs e)
        {
            label3.Text = DateTime.Now.ToLongTimeString();
        }

        private void logfunction()
        {
            conn = Class1.Connectdb1();
            cmd = new SqlCommand("Insert into Loginf(Username, timein, date, accesslvl)VALUES(@Username, @timein, @date,@Role)", conn);
            cmd.Parameters.AddWithValue("@Username", Username.Text);
            cmd.Parameters.AddWithValue("@timein", label3.Text);
            cmd.Parameters.AddWithValue("@date", label4.Text);
            cmd.Parameters.AddWithValue("@Role", dr["Role"].ToString());
            cmd.ExecuteNonQuery();
            conn.Close();
        }

        private void Exit_Click(object sender, EventArgs e)
        {
            this.Close();
            Application.Exit();
        }

        private void textBox2_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                conn = Class1.Connectdb1();
                cmd = new SqlCommand("Select Usename, Pword, Role from Users where Usename='" + Username.Text + "' and Pword='" + Password.Text + "'", conn);
                dr = cmd.ExecuteReader();
                if (dr.Read())
                {
                    if (dr["Role"].ToString() != "Admin")
                    {
                        sendtext = Password.Text;
                        this.Hide();
                        Mainform mf = new Mainform();
                        mf.Show();
                        logfunction();
                    }
                    else if (dr["Role"].ToString() == "Admin")
                    {
                        sendtext = Password.Text;
                        this.Hide();
                        Form1 frm1 = new Form1();
                        frm1.textBox2.Text = Username.Text;
                        frm1.textBox3.Text = label3.Text;
                        frm1.textBox4.Text = label4.Text;

                        
                        frm1.Show();
                        logfunction();
                    }
                }
                else
                {
                    MessageBox.Show("Please register first... Contact the admin for further instructions", "User does not exist...");
                }
                conn.Close();
                //logfunction();
                Username.Text = "";
                Password.Text = "";
            }
        }
    }
}
