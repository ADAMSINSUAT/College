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
    public partial class Register : Form
    {
        Constring cs = new Constring();
        SqlConnection conn;
        SqlCommand cmd;
        SqlDataReader dr;
        public static string passingText;//

        public Register()
        {
            InitializeComponent();
        }

        private void connectdatabase()
        {
            try
            {
                conn = new SqlConnection(cs.connection);
                conn.Open();
            }
            catch
            {
                MessageBox.Show("Sql Server Not Responding");
            }
        }
        private void Register_Load(object sender, EventArgs e)
        {
            clear();
        }

        private void clear()
        {
            textBox1.Text = "";
            textBox2.Text = "";
            textBox3.Text = "";
            textBox4.Text = "";
            comboBox1.Text = "Select";
        }

        private void savedata()
        {
            try
            {
                connectdatabase();
                cmd = new SqlCommand("insert into login(Fullname, Username, Password, Role)values('" + textBox1.Text + "','" + textBox2.Text + "','" + textBox3.Text + "','" + comboBox1.Text + "')", conn);
                cmd.ExecuteNonQuery();
                conn.Close();

                this.clear();
                MessageBox.Show("Account Saved", "MESSAGE...");
                textBox1.Focus();
            }
            catch
            {
                MessageBox.Show("Sql Server Not Responding");
            }

        }

        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                if (textBox1.Text == "")
                {
                    MessageBox.Show("Pls. fill up all");
                    textBox1.Select();
                }
                else if (textBox2.Text == "")
                {
                    MessageBox.Show("Pls. fill up all");
                    textBox2.Select();
                }
                else if (textBox3.Text == "")
                {
                    MessageBox.Show("Pls. fill up all");
                    textBox3.Select();
                }
                else if (textBox4.Text != textBox3.Text)
                {
                    MessageBox.Show("Password Mismatch");
                    textBox3.Select();
                }
                else if (comboBox1.Text == "Select")
                {
                    MessageBox.Show("Pls. fill up all");
                    comboBox1.Select();
                }
                else
                {
                    connectdatabase();
                    cmd = new SqlCommand("select*from login where Username='" + textBox2.Text + "'", conn);
                    dr = cmd.ExecuteReader();
                    if (dr.Read())
                    {
                        MessageBox.Show("User already exist");
                        textBox1.Focus();
                        dr.Close();
                    }
                    else
                    {
                        savedata();
                        Form1 frm = new Form1();
                        frm.Show();
                        this.Hide();
                    }
                }
            }
            catch
            {
            }
            
        }

        private void button2_Click(object sender, EventArgs e)
        {
            DialogResult result = MessageBox.Show("Are you sure to go back?", "Confirmation Message...", MessageBoxButtons.YesNo);
            if (result == DialogResult.Yes)
            {
                this.Hide();
                Form1 form = new Form1();
                form.Show();
            }
        }

    }
}
