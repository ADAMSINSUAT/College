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
using CrystalDecisions.Shared;
using CrystalDecisions.CrystalReports.Engine;

namespace Main_Form
{
    public partial class Cashier : Form
    {
        Constring cs = new Constring();
        SqlConnection conn;
        SqlCommand cmd;
        SqlDataReader dr;
        SqlDataAdapter da;
        public static string passingText;

        public Cashier()
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

        private void Cashier_Load(object sender, EventArgs e)
        {
            button1.Enabled = false;
            label11.Text = Login.sendtext;
            timer1.Start();
            label10.Text = DateTime.Now.ToString("MM/dd/yyy");
            label9.Text= DateTime.Now.ToString("MM/dd/yyy");
            connectdatabase();
            AutoCompleSearch();
            panel1.Hide();
            panel2.Hide();
           // clear();
            count();
            button2.Enabled = false;
            button4.Enabled = false;
         }
        private void clear()
        {
            textBox1.ReadOnly = false;
            textBox2.Text = "";
            textBox2.ReadOnly = true;
            textBox3.Text = "";
            textBox4.Text = "";
            textBox5.Text = "";
         }
        private void savedata()
        {
            try
            
            {
                this.connectdatabase();
                    cmd = new SqlCommand("insert into DailySales(transactionnumber)values('" + label6.Text +"')", conn);
                    cmd.ExecuteNonQuery();
                    this.clear();           
                    conn.Close();
                    textBox1.Select();
            }
            catch
            {
                MessageBox.Show("Please check all the data if typed correctly.", "MESSAGE");
            }
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            if (textBox1.Text == "" || textBox3.Text == "")
            {
                button1.Enabled = false;
            }
            else
            {
                button1.Enabled = true;
            }

            if (textBox1.Text == "")
                {
                    clear();
                }
                else
                {
                    connectdatabase();
                    cmd = new SqlCommand("Select * from POS where barcodeid ='" + textBox1.Text + "' OR productname ='" + textBox1.Text + "'", conn);
                    dr = cmd.ExecuteReader();
                    if (dr.Read())
                    {
                        textBox1.Text = dr.GetValue(0).ToString();
                        textBox2.Text = dr.GetValue(1).ToString();
                        textBox4.Text = dr.GetValue(2).ToString();
                        textBox11.Text = dr.GetValue(3).ToString();                     
                    }
                    else
                    {
                        clear();
                        dr.Close();
                    }
            }
        }

        private void textBox1_KeyDown(object sender, KeyEventArgs e)
        {
                connectdatabase();
                cmd = new SqlCommand("Select * from POS where barcodeid ='" + textBox1.Text + "' OR productname ='" + textBox1.Text + "'", conn);
                dr = cmd.ExecuteReader();
                if (dr.Read())
                {
                    textBox1.Text = dr.GetValue(0).ToString();
                    textBox2.Text = dr.GetValue(1).ToString();
                    textBox4.Text = dr.GetValue(2).ToString();
                    textBox11.Text = dr.GetValue(3).ToString();

                    
                }
                dr.Close();
        } 
        void AutoCompleSearch()
        {
            connectdatabase();
            cmd = new SqlCommand("Select barcodeid from POS where barcodeid LIKE @barcodeid And productname LIKE @productname" ,conn);
            cmd.Parameters.Add(new SqlParameter("@barcodeid","%"+ textBox1.Text +"%"));
            cmd.Parameters.Add(new SqlParameter("@productname", "%" + textBox1.Text + "%"));
            //cmd = new SqlCommand("Select productname from POS where productname LIKE @productname", conn);


            cmd.ExecuteNonQuery();
            dr = cmd.ExecuteReader();

            AutoCompleteStringCollection coll = new AutoCompleteStringCollection();

            while (dr.Read())
            {
                coll.Add(dr.GetString(0));

            }
            textBox1.AutoCompleteCustomSource = coll;
            dr.Close();
        }
        private void button5_Click(object sender, EventArgs e)
        {
           
        }

        private void button7_Click(object sender, EventArgs e)
        {
            //try
            //{
                    if (textBox1.Text == "")
                    {
                        MessageBox.Show("Pls. input barcode id");
                        textBox1.Select();
                    }
                    else if (textBox2.Text == "")
                    {
                        MessageBox.Show("Pls. input the product name");
                        textBox1.Select();
                    }
                    else if (textBox3.Text == "")
                    {
                        MessageBox.Show("Pls. input amount");
                        textBox3.Select();
                    }
                    else
                    {
                        decimal z, x;
                        z = Convert.ToDecimal(textBox3.Text);
                        x = Convert.ToDecimal(textBox11.Text);
                        if (z > x)
                        {
                            MessageBox.Show("Quantity exceeds that of the stocks");
                            textBox3.Focus();
                        }

                        else
                        {
                            int n = dataGridView1.Rows.Add();
                            dataGridView1.Rows[n].Cells[0].Value = textBox1.Text;
                            dataGridView1.Rows[n].Cells[1].Value = textBox2.Text;
                            dataGridView1.Rows[n].Cells[2].Value = textBox3.Text;
                            dataGridView1.Rows[n].Cells[3].Value = textBox4.Text;
                            dataGridView1.Rows[n].Cells[4].Value = textBox5.Text;
                            //dataGridView1.Rows[n].Cells[5].Value = textBox7.Text;

                            double b = 0;
                            for (int i = 0; i < dataGridView1.RowCount; i++)
                            {



                                double c = Convert.ToDouble(dataGridView1.Rows[i].Cells[4].Value);
                                b += c;

                            }

                            textBox6.Text = b.ToString();
                            textBox8.Text = textBox6.Text;
                            clear();
                            textBox1.Text = "";
                            textBox1.Focus();
                            button2.Enabled = true;
                            button4.Enabled = true;
                        }
                    }
                  
            //}
            //catch
            //{
            //    MessageBox.Show("Please input the correct corresponding items/value");
            //}
        }
         
        

        private void count()
        {
            this.connectdatabase();
            cmd = new SqlCommand("select * from Counter where count='" + label7.Text + "'", conn);
            dr = cmd.ExecuteReader();

            if (dr.Read())
            {
                int a, b;
                label8.Text = dr.GetValue(0).ToString();
                a = Convert.ToInt16(label8.Text);
                b = a + 1;
                label6.Text = b.ToString();
                dr.Close();
            }
            conn.Close();
        }
        private void textBox3_TextChanged(object sender, EventArgs e)
        {
            try
            {
                if (textBox1.Text == "" || textBox3.Text == "")
                {
                    button1.Enabled = false;
                }
                else
                {
                    button1.Enabled = true;
                }
                if (textBox3.Text == "")
                {
                    textBox3.Text = "";
                }
                else
                {
                    double a, b, c;
                    a = Convert.ToDouble(textBox3.Text);
                    b = Convert.ToDouble(textBox4.Text);
                    c = a * b;
                    textBox5.Text = c.ToString();
                    //textBox5.Text = c.ToString("###,###,##0.00");
                }
            }
            catch
            {
            }
        }
        private void button2_Click(object sender, EventArgs e)
        {
            if (dataGridView1.Rows.Count == 0)
            {
                button2.Enabled = false;
            }
            
            else
            {
                panel1.Show();
                textBox9.Focus();
                button2.Enabled=true;
            }
            
           
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            label9.Text = DateTime.Now.ToString("hh:mm:ss");
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
        //    int n = dataGridView1.Rows.Count;
        //    int b = 0;
        //    int c = Convert.ToInt16(dataGridView1.Rows[n].Cells[4].Value);
        //    b += c;


        //    textBox6.Text = b.ToString();
      
        }


        private void button6_Click(object sender, EventArgs e)
        {
            DialogResult result = MessageBox.Show("Are you sure to go back?", "Confirmation Message...", MessageBoxButtons.YesNo);
                if(result==DialogResult.Yes)
                {
            if (label11.Text == "Admin")
            {
                timer1.Stop();
                this.Hide();
                Form1 form = new Form1();
                form.Show();
            }
            else
            {
                timer1.Stop();
                this.Close();
                Login log = new Login();
                log.Show();
            }
                }
        }

        private void button4_Click(object sender, EventArgs e)
        {
            int i;
            i = dataGridView1.SelectedCells[0].RowIndex;

            dataGridView1.Rows.RemoveAt(i);
            MessageBox.Show("Successfully Remove");

            //dataGridView1.Rows[n].Cells[0].Value = textBox1.Text;
            //dataGridView1.Rows[n].Cells[1].Value = textBox2.Text;
            //dataGridView1.Rows[n].Cells[2].Value = textBox3.Text;
            //dataGridView1.Rows[n].Cells[3].Value = textBox4.Text;
            //dataGridView1.Rows[n].Cells[4].Value = textBox5.Text;

            if (dataGridView1.Rows.Count == 0)
            {
                button4.Enabled = false;
                button2.Enabled = false;
            }
            else
            {
                button4.Enabled = true;
            }
        }

        private void button8_Click(object sender, EventArgs e)
        {
            panel1.Show();
            if (textBox9.Text == "")
            {
                MessageBox.Show("Please input payment amount");
            }
            else
            {
            decimal a, b, c;
            a = Convert.ToDecimal(textBox8.Text);
            b = Convert.ToDecimal(textBox9.Text);
            c = b - a;
            if (b >= a)
            {
                DialogResult result = MessageBox.Show("Are you sure to purchase?", "Confirmation Message", MessageBoxButtons.YesNo);
                if (result == DialogResult.Yes)
                {

                    for (int i = 0; i < dataGridView1.Rows.Count; i++)
                    {
                        foreach (DataGridViewRow item in dataGridView1.Rows)
                        {
                            this.connectdatabase();

                            decimal quan = Convert.ToDecimal(dataGridView1.Rows[i].Cells[2].Value);
                            cmd = new SqlCommand("update POS set stocks=stocks-@stocks where barcodeid='" + dataGridView1.Rows[i].Cells[0].Value + "'", conn);
                            cmd.Parameters.AddWithValue("@stocks", quan);
                            cmd.ExecuteNonQuery();
                            this.connectdatabase();
                            textBox10.Text = c.ToString();
                            dr.Close();

                        }

                            this.connectdatabase();
                            SqlCommand cmd2 = new SqlCommand("Insert into DailySales(barcodeid, productname, quantity, price, total, date, time, change, payment, transactionnumber)VALUES('" + dataGridView1.Rows[i].Cells[0].Value + "','" + dataGridView1.Rows[i].Cells[1].Value + "','" + Convert.ToDecimal(dataGridView1.Rows[i].Cells[2].Value) + "','" + Convert.ToDecimal(dataGridView1.Rows[i].Cells[3].Value) + "','" + Convert.ToDecimal(textBox8.Text) + "','" + Convert.ToDateTime(label10.Text) + "','" + Convert.ToDateTime(label9.Text) + "','" +textBox10.Text + "','" + textBox9.Text + "','" + Convert.ToInt16(label6.Text) + "')", conn);
                            cmd2.ExecuteNonQuery();
                        
                    }
                        MessageBox.Show("save");
                        
                        this.clear();
                        conn.Close();

                        dataGridView1.Rows.Clear();
                        textBox1.Select();
                        this.connectdatabase();

                        panel2.Show();
                        print();
                        SqlCommand cmd1 = new SqlCommand("Update Counter set transactionnumber=@transactionnumber where count='" + label7.Text + "'", conn);
                        cmd1.Parameters.AddWithValue("@transactionnumber", label6.Text);
                        cmd1.ExecuteNonQuery();
                        conn.Close();
                        //this.savedata();
                        MessageBox.Show("Transaction successful");
                        textBox6.Clear();
                        textBox8.Clear();
                        textBox9.Clear();
                        textBox10.Clear();
                        button4.Enabled = false;
                        count();
                        panel1.Hide();
                        button2.Enabled = false;
           
                    
                }
                else
                {
                    MessageBox.Show("Payment insufficient");
                }
            }
            
            }
        }

        private void button9_Click(object sender, EventArgs e)
        {
            panel1.Hide();
        }
        private void print()
        {
            try
            {
                this.connectdatabase();
                ReportDocument crypt = new ReportDocument();
                DataSet ds;
               
                    cmd = new SqlCommand("select * from DailySales where transactionnumber='"+label6.Text+"'", conn);
                    da = new SqlDataAdapter(cmd);
                    ds = new DataSet();
                    da.Fill(ds, "DailySales");
                    crypt.Load(@"d:\Users\Dell-pc\Desktop\Main Form1\Main Form\CrystalReport1.rpt");
                    crypt.SetDataSource(ds);
                    crystalReportViewer1.ReportSource = crypt;           
            }
            catch
            {
            }
            panel2.Show();
        }

        private void textBox3_KeyPress(object sender, KeyPressEventArgs e)
        {
            try
            {
                char ch = e.KeyChar;

                if (!Char.IsDigit(ch) && ch != 8 && ch != 46)
                {
                    e.Handled = true;
                }
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }

        private void button10_Click(object sender, EventArgs e)
        {
            panel2.Hide();
        }

        private void panel2_Paint(object sender, PaintEventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            textBox1.Text = "";
            clear();
            button1.Enabled = false;
            button1.Focus();
        }

       
        private void textBox5_TextChanged(object sender, EventArgs e)
        {
            //double a, b, c;
            //a = Convert.ToDouble(textBox3.Text);
            //b = Convert.ToDouble(textBox4.Text);
            //c = a * b;
            //textBox5.Text = c.ToString();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            panel2.Show();
            print();
        }

        private void label6_TextChanged(object sender, EventArgs e)
        {
            if (label6.Text == "101")
            {
                label6.Text = "0";
            }
            else
            {
            }
        }
    }
}
