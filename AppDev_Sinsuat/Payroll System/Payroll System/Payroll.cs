using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

//Import libraries
using System.Data.SqlClient;
using System.IO;

namespace Payroll_System
{
    public partial class Payroll : Form
    {
        //Declare these
        Constring cs = new Constring();
        SqlConnection conn;
        SqlCommand cmd;
        SqlDataReader dr;

        public Payroll()
        {
            InitializeComponent();
        }
        //Declare this for database connection
        private void connectdatabase()
        {
            try
            {
                conn = new SqlConnection(cs.connection);
                conn.Open();
            }
            catch
            {
                MessageBox.Show("Sql Server not Responding");
            }
        }
        
        //Form Load
        private void Payroll_Load(object sender, EventArgs e)
        {
            clear();  
        }

        //Declare this function
        private void clear()
        {
            textBox1.Select();
            textBox1.Text = "";
            textBox2.Text = "";
            textBox3.Text = "";
            textBox4.Text = "0.00";
            textBox5.Text = "0.00";
            textBox6.Text = "0";
            textBox7.Text = "0.00";
            textBox8.Text = "0.00";
            textBox9.Text = "0.00";
            textBox10.Text = "0.00";
            textBox11.Text = "0.00";
            textBox12.Text = "0.00";
            textBox13.Text = "0.00";
            textBox14.Text = "0.00";

            DateTime date = DateTime.Now;
            dateTimePicker1.Text=date.ToString();
            dateTimePicker2.Text=date.ToString();

            textBox1.ReadOnly = false;
            button1.Enabled = false;
        }

        //Declare this function
        private void format()
        {
            try
            {
                double a;
                a = Convert.ToDouble(textBox4.Text);
                textBox4.Text = a.ToString("###,###,##0.00");
            }
            catch
            {
            }
        }

        //Declare this function
        private void format1()
        {
            try
            {
                double a;
                a = Convert.ToDouble(textBox5.Text);
                textBox5.Text = a.ToString("###,###,##0.00");
            }
            catch
            {
            }
        }

        //Declare this function
        private void format2()
        {
            try
            {
                double a;
                a = Convert.ToDouble(textBox7.Text);
                textBox7.Text = a.ToString("###,###,##0.00");
            }
            catch
            {
            }
        }

        //Declare this function
        private void format3()
        {
            try
            {
                double a;
                a = Convert.ToDouble(textBox8.Text);
                textBox8.Text = a.ToString("###,###,##0.00");
            }
            catch
            {
            }
        }

        //Declare this function
        private void format4()
        {
            try
            {
                double a;
                a = Convert.ToDouble(textBox9.Text);
                textBox9.Text = a.ToString("###,###,##0.00");
            }
            catch
            {
            }
        }

        //Declare this function
        private void format5()
        {
            try
            {
                double a;
                a = Convert.ToDouble(textBox10.Text);
                textBox10.Text = a.ToString("###,###,##0.00");
            }
            catch
            {
            }
        }

        //Declare this function
        private void format6()
        {
            try
            {
                double a;
                a = Convert.ToDouble(textBox11.Text);
                textBox11.Text = a.ToString("###,###,##0.00");
            }
            catch
            {
            }
        }

        //Declare this function
        private void format7()
        {
            try
            {
                double a;
                a = Convert.ToDouble(textBox12.Text);
                textBox12.Text = a.ToString("###,###,##0.00");
            }
            catch
            {
            }
        }

        //Declare this function
        private void format8()
        {
            try
            {
                double a;
                a = Convert.ToDouble(textBox13.Text);
                textBox13.Text = a.ToString("###,###,##0.00");
            }
            catch
            {
            }
        }

        //Declare this function
        private void format9()
        {
            try
            {
                double a;
                a = Convert.ToDouble(textBox14.Text);
                textBox14.Text = a.ToString("###,###,##0.00");
            }
            catch
            {
            }
        }

        //TextBox12 Codes-TextChanged Events
        private void textBox12_TextChanged(object sender, EventArgs e)
        {
            try
            {
                if (textBox12.Text == "") 
                {
                    textBox12.Text="0.00";
                }
                else
                {
                    double a,b,c;
                    a=Convert.ToDouble(textBox11.Text);
                    b=Convert.ToDouble(textBox12.Text);
                    c=a+b;
                    textBox13.Text=c.ToString("###,###,##0.00");
                }
            }
            catch
            {
            }
        }



        //Button Clear Code
        private void button3_Click(object sender, EventArgs e)
        {
            clear();
        }

        //Textbox6 Codes-TextChanged Events
        private void textBox6_TextChanged(object sender, EventArgs e)
        {
            try
            {
                if (textBox6.Text == "") 
                {
                    textBox6.Text="0";
                }
                else
                {
                    double a, b, c;
                    a = Convert.ToDouble(textBox4.Text);
                    b = Convert.ToDouble(textBox6.Text);
                    c = a * b;
                    textBox7.Text = c.ToString("###,###,##0.00");
                    textBox14.Text = c.ToString("###,###,##0.00");
                }
            }
            catch
            {
            }
        }

        //Textbox8 Codes-TextChanged Events
        private void textBox8_TextChanged(object sender, EventArgs e)
        {
            try
            {
                if (textBox8.Text == "")
                {
                    textBox8.Text = "0.00";
                }
                else
                {
                    double a, b, c;
                    a = Convert.ToDouble(textBox8.Text);
                    b = Convert.ToDouble(textBox9.Text);
                    c = a + b;
                    textBox10.Text = c.ToString("###,###,##0.00");
                }
            }
            catch
            {
            }
        }

        //Textbox9 Codes-TextChanged Events
        private void textBox9_TextChanged(object sender, EventArgs e)
        {
            try
            {
                if (textBox9.Text == "")
                {
                    textBox9.Text = "0.00";
                }
                else
                {
                    double a, b, c;
                    a = Convert.ToDouble(textBox8.Text);
                    b = Convert.ToDouble(textBox9.Text);
                    c = a + b;
                    textBox10.Text = c.ToString("###,###,##0.00");
                }
            }
            catch
            {
            }
        }

        //Textbox7 Codes-TextChanged Events
        private void textBox7_TextChanged(object sender, EventArgs e)
        {
            try
            {
                double a, b, c, d;
                a=Convert.ToDouble(textBox7.Text);
                b = Convert.ToDouble(textBox10.Text);
                c = Convert.ToDouble(textBox13.Text);
                d = (a + b) - c;
                textBox14.Text = d.ToString("###,###,##0.00");
            }
            catch
            {
            }
        }

        //Textbox10 Codes-TextChanged Evets
        private void textBox10_TextChanged(object sender, EventArgs e)
        {
            try
            {
                double a, b, c, d;
                a = Convert.ToDouble(textBox7.Text);
                b = Convert.ToDouble(textBox10.Text);
                c = Convert.ToDouble(textBox13.Text);
                d = (a + b) - c;
                textBox14.Text = d.ToString("###,###,##0.00");
            }
            catch
            {
            }
        }

        //Textbox13 Codes-TextChanged Events
        private void textBox13_TextChanged(object sender, EventArgs e)
        {
            try
            {
                double a, b, c, d;
                a = Convert.ToDouble(textBox7.Text);
                b = Convert.ToDouble(textBox10.Text);
                c = Convert.ToDouble(textBox13.Text);
                d = (a + b) - c;
                textBox14.Text = d.ToString("###,###,##0.00");
            }
            catch
            {
            }
        }

        private void textBox11_TextChanged(object sender, EventArgs e)
        {
            try
            {
                if (textBox11.Text == "")
                {
                    textBox11.Text = "0.00";
                }
                else
                {
                    double a, b, c;
                    a=Convert.ToDouble(textBox11.Text);
                    b=Convert.ToDouble(textBox12.Text);
                    c = a + b;
                    textBox13.Text = c.ToString("###,###,##0.00");
                }
            }
            catch
            {
            }
        }

        //Declare this function
        private void savedata()
        {
            try
            {
                if (textBox1.Text == "")
                {
                    MessageBox.Show("Pls. fill up all");
                    textBox1.Select();
                }
                else if (textBox7.Text == "0.00")
                {
                    MessageBox.Show("Pls. fill up all");
                    textBox6.Select();
                }
                else
                {
                    this.connectdatabase();
                    cmd = new SqlCommand("insert into tblpayroll( datefrom,dateto,empid,name,pos,rate, bsalary,dayswork,cola,allowance,sss,tardiness,nincome)values('" + dateTimePicker1.Text + "','" + dateTimePicker2.Text + "','" + textBox1.Text + "','" + textBox2.Text + "','" + textBox3.Text + "','" + Convert.ToDouble(textBox4.Text) + "','" + Convert.ToDouble(textBox5.Text) + "','" + Convert.ToDouble(textBox6.Text) + "','" + Convert.ToDouble(textBox8.Text) + "','" + Convert.ToDouble(textBox9.Text) + "','" + Convert.ToDouble(textBox11.Text) + "','" + Convert.ToDouble(textBox12.Text) + "','" + Convert.ToDouble(textBox14.Text) + "')", conn);
                    cmd.ExecuteNonQuery();
                    conn.Close();
                    this.clear();
                    MessageBox.Show("Payroll Save","Message...");
                        textBox1.Select();
                }
            }
            catch
            {
                MessageBox.Show("Please check all the data you entered if typed correctly.", "MESSAGE");
            }
        }

        //Button Print Payslip Codes
        private void button4_Click(object sender, EventArgs e)
        {
            this.clear();
            Form frm = new Printpayslip();
            frm.ShowDialog();
        }

        //Button Print Payroll Codes
        private void button5_Click(object sender, EventArgs e)
        {
            this.clear();
            Form frm = new Printpayroll();
            frm.ShowDialog();
        }

        //Button Search Codes
        private void button2_Click(object sender, EventArgs e)
        {
            try
            {
                if (textBox1.Text == "")
                {
                    MessageBox.Show("Please input Employee No.:");
                    textBox1.Select();
                }
                else
                {
                    this.connectdatabase();
                    cmd = new SqlCommand("select empid, name, pos, rate, bsalary from tbladdemp where empid='" + textBox1.Text + "'", conn);
                    dr = cmd.ExecuteReader();
                    dr.Read();
                    if (dr.HasRows)
                    {
                        textBox1.Text = dr[0].ToString();
                        textBox2.Text = dr[1].ToString();
                        textBox3.Text = dr[2].ToString();
                        textBox4.Text = dr[3].ToString();
                        textBox5.Text = dr[4].ToString();
                        textBox1.ReadOnly = true;

                        format();
                        format1();
                        button1.Enabled = true;
                    }
                    else
                    {
                        MessageBox.Show("No record found");
                        clear();
                    }
                }
            }
            catch
            {
            }
        }

        //Button Save Codes
        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                this.connectdatabase();
                cmd = new SqlCommand("select * from tblpayroll where datefrom='" + dateTimePicker1.Text + "'and dateto='" + dateTimePicker2.Text + "'and empid='" + textBox1.Text + "'", conn);
                dr = cmd.ExecuteReader();
                if (dr.Read())
                {
                    MessageBox.Show("Record already exist");
                    dr.Close();
                }
                else
                {
                    this.savedata();
                }
            }
            catch
            {
            }
        }
   }
}
