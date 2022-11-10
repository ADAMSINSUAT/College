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
    public partial class Register : Form
    {
        SqlConnection conn;
        SqlCommand cmd;
        SqlDataReader dr;
        SqlDataAdapter da;

        public Register()
        {
            InitializeComponent();
        }

        private void Form2_Load(object sender, EventArgs e)
        {

        }

        private void clear()
        {
            textBox1.Text = "";
            textBox2.Text = "";
            textBox3.Text = "";
            textBox4.Text = "";
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (textBox3.Text != textBox2.Text)
                {
                    MessageBox.Show("Password Mismatch");
                }
            else
            {
            conn = Class1.Connectdb1();
            cmd = new SqlCommand("Select * from Users where accid = '" + textBox1.Text + "'", conn);
            dr = cmd.ExecuteReader();
            if (dr.Read())
            {
                MessageBox.Show("Borrower already exists!");
            }
            else
            {
                dr.Close();
  
                    conn = Class1.Connectdb1();
                    cmd = new SqlCommand("insert into Users(accid, name, pword)VALUES(@accid,@name,@pword)", conn);
                    cmd.Parameters.AddWithValue("@accid", textBox1.Text);
                    cmd.Parameters.AddWithValue("@name", textBox4.Text);
                    cmd.Parameters.AddWithValue("@pword", textBox2.Text);
                    cmd.ExecuteNonQuery();
                    MessageBox.Show("Account Saved");
                    clear();
                   
                }
            }
        }
    }
}
