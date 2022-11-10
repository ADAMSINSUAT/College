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
    public partial class Basic_Info : Form
    {
        SqlConnection conn;
        SqlDataAdapter da;
        SqlDataReader dr;
        SqlCommand cmd;

        public Basic_Info()
        {
            InitializeComponent();
        }
        private void button1_Click(object sender, EventArgs e)
        {
            conn = Class1.Connectdb1();
            if (textBox1.Text == "" && textBox3.Text=="" && textBox4.Text=="")
            {
                MessageBox.Show("Please fill up all fields");
                textBox1.Focus();
            }
            else if (textBox1.Text == "")
            {
                MessageBox.Show("Please input name");
            }
            else if (textBox3.Text == "")
            {
                MessageBox.Show("Please input contact number");
                textBox3.Focus();
            }
            else if (textBox4.Text == "")
            {
                MessageBox.Show("Please input address");
                textBox4.Focus();
            }
            else
            {
                int years = DateTime.Now.Year - dateTimePicker1.Value.Year;
            if (dateTimePicker1.Value.AddYears(years) > DateTime.Now) years--;
            textBox2.Text = years.ToString();
            int a = Convert.ToInt16(textBox2.Text);
                if(a<18 || a >=70)
                {
                    MessageBox.Show("Age requirement not met, Must be 18 or above of age not exceeding 70");
                }
                else
                {
                cmd = new SqlCommand("Select*from BrrwersPInfo where Fllname='" + textBox1.Text +"'",conn);
                dr = cmd.ExecuteReader();
                if (dr.Read())
                {
                    MessageBox.Show("Borrower already exists");
                }
                else
                {
                    dr.Close();

                    cmd = new SqlCommand("Insert into BrrwersPInfo(Addrss, Age, Bday, Cno, CvilStats, Fllname)VALUES(@Addrss, @Age, @Bday, @Cno, @CvilStats, @Fllname)", conn);
                    cmd.Parameters.AddWithValue("@Addrss", textBox4.Text);
                    cmd.Parameters.AddWithValue("@Age", textBox2.Text);
                    cmd.Parameters.AddWithValue("@Bday", dateTimePicker1.Text);
                    cmd.Parameters.AddWithValue("@Cno", textBox3.Text);
                    cmd.Parameters.AddWithValue("@CvilStats", comboBox1.Text);
                    cmd.Parameters.AddWithValue("@Fllname", textBox1.Text);
                    cmd.ExecuteNonQuery();
                    MessageBox.Show("Saved");
                    textBox1.Clear();
                    textBox2.Clear();
                    textBox3.Clear();
                    textBox4.Clear();
                    dateTimePicker1.ResetText();
                    comboBox1.ResetText();
                }
                }
            }
        }

        private void Basic_Info_Load(object sender, EventArgs e)
        {
            textBox2.Text = "0";
            comboBox1.Text = "";

            //int years = DateTime.Now.Year - dateTimePicker1.Value.Year;
            //if (dateTimePicker1.Value.AddYears(years) > DateTime.Now) years--;
            //textBox2.Text = Convert.ToInt16(years).ToString();
 
        }

        private void textBox2_TextChanged(object sender, EventArgs e)
        {
            try
            {
                if (textBox2.Text == "")
                {
                    textBox2.Text = "0";
                }
                else
                {
                    double a;
                    a = Convert.ToDouble(dateTimePicker1.Text);
                    dateTimePicker1.Text = a.ToString();
                }
            }
            catch
            {
            }
        }

        private void dateTimePicker1_ValueChanged(object sender, EventArgs e)
        {
            int years = DateTime.Now.Year - dateTimePicker1.Value.Year;
            if (dateTimePicker1.Value.AddYears(years) > DateTime.Now) years--;
            textBox2.Text = years.ToString();

            try
            {
                if (textBox2.Text == "")
                {
                    textBox2.Text = "0";
                }
                else
                {
                    double a;
                    a = Convert.ToDouble(dateTimePicker1.Text);
                    textBox2.Text = a.ToString();
                }
            }
            catch
            {
            }
        }
    }
}
