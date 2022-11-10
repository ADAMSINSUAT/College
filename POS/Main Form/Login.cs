using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

using System.Data.SqlClient;
using System.IO;

namespace Main_Form
{
    public partial class Login : Form
    {
        Constring cs = new Constring();
        SqlCommand cmd;
        SqlConnection conn;
        SqlDataReader dr;
        SqlDataAdapter da;
        public static string passingText;//

        public Login()
        {
            InitializeComponent();
        }

        public void connectdatabase()
        {
            try
            {
                conn = new SqlConnection(cs.connection);
                conn.Open();
            }
            catch
            {
                MessageBox.Show("Sql Server is not Responding"); 
            }
        }

        public static string sendtext = "";

        private void button1_Click(object sender, EventArgs e)
        {      
                if (textBox1.Text == "" || textBox2.Text=="")
                {
                    MessageBox.Show("Invalid Account");
                    textBox1.Select();
                }
                else
                {
                    this.connectdatabase();
                    cmd = new SqlCommand("select Fullname, Username, Password, Role from login where Username='" + textBox1.Text + "'and Password='" + textBox2.Text + "'", conn);
                    dr = cmd.ExecuteReader();
                    
                    
                    if (dr.Read())
                    {

                        label3.Text = dr.GetString(3);

                        if (label3.Text == "Admin")
                        {
                            sendtext = label3.Text;
                            Form1 frm = new Form1();
                            frm.Show();
                            this.Hide();
                        }
                        else if (label3.Text == "User")
                        {
                            sendtext = label3.Text;
                            Cashier chr = new Cashier();
                            chr.Show();
                            this.Hide();
                        }
                    }
                        else
                        {
                            MessageBox.Show("User does not exist");
                        }
                    
                    dr.Close();
                    conn.Close();

                }
                
        }

        private void Login_Load(object sender, EventArgs e)
        {
            timer1.Start();
            label4.Text = DateTime.Now.ToString();
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            label4.Text = DateTime.Now.ToString();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            //timer1.Stop();
            this.Close();
            Application.Exit();

        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

       
    }
}
