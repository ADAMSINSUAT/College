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
    public partial class POS : Form
    {
        Constring cs = new Constring();
        SqlConnection conn;
        SqlCommand cmd;
        SqlDataReader dr;
        SqlDataAdapter da;
        public static string passingText;//

        public POS()
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

        private void POS_Load(object sender, EventArgs e)
        {
            // TODO: This line of code loads data into the 'pOSDataSet6.POS' table. You can move, or remove it, as needed.
            this.pOSTableAdapter2.Fill(this.pOSDataSet6.POS);
            connectdatabase();
            refresh();
            clear();
            // TODO: This line of code loads data into the 'pOSDataSet1.POS' table. You can move, or remove it, as needed.
           //this.pOSTableAdapter1.Fill(this.pOSDataSet1.POS);
            // TODO: This line of code loads data into the 'pOSDataSet.POS' table. You can move, or remove it, as needed.
          //  this.pOSTableAdapter.Fill(this.pOSDataSet.POS);
        }

        private void clear()
        {
            textBox1.Text = "";
            textBox2.Text = "";
            textBox3.Text = "00.00";
            textBox4.Text = "0";
            DateTime date= DateTime.Now;
            dateTimePicker1.Text = date.ToString();
            dateTimePicker2.Text = date.ToString();

            textBox1.ReadOnly = false;
            button3.Enabled = false;
            button2.Enabled = false;

        }
        private void savedata()
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
                    MessageBox.Show("Pls.fill up all");
                    textBox2.Select();
                }
                else if (textBox3.Text == "")
                {
                    MessageBox.Show("Pls. fill up all");
                    textBox3.Select();
                }
                else if (textBox4.Text == "")
                {
                    MessageBox.Show("Pls. fill up all");
                    textBox4.Select();
                }

                else
                {
                    this.connectdatabase();
                    cmd = new SqlCommand("insert into POS(barcodeid, productname, price, stocks, dateofproduction, dateofexpiry)values('" + textBox1.Text + "','" + textBox2.Text + "','" + Convert.ToDecimal(textBox3.Text) + "','" + Convert.ToDouble(textBox4.Text) + "','" + dateTimePicker1.Text + "','" + dateTimePicker2.Text + "')", conn);
                    cmd.ExecuteNonQuery();
                    this.clear();
                    MessageBox.Show("Payroll Save", "MESSAGE...");
                    refresh();
                    conn.Close();
                    textBox1.Select();                   
                }
            }
            catch
            {
                MessageBox.Show("Please check all the data if type correctly.", "MESSAGE");
            }
        }
        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                this.connectdatabase();
                cmd = new SqlCommand("select*from POS where barcodeid='" + textBox1.Text +"' or productname='"+textBox2.Text+"'", conn);
                dr = cmd.ExecuteReader();
                if (dr.Read())
                {
                        MessageBox.Show("Barcode ID/Product Name already exist");
                        textBox1.Focus();
                        dr.Close();
                }
                else
                {
                    this.savedata();
                    refresh();
                }
                
                
            }
            catch
            {
                MessageBox.Show("Error");
            }
        }

        private void button2_Click(object sender, EventArgs e)
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
                    MessageBox.Show("Pls fill up all");
                    textBox3.Select();
                }
                else if (textBox4.Text == "")
                {
                    MessageBox.Show("Pls. fill up all");
                    textBox4.Select();
                }
                else
                {
                    cmd = new SqlCommand("update POS set barcodeid='" + textBox1.Text + "', productname='" + textBox2.Text + "', price='" + Convert.ToDecimal(textBox3.Text) + "', stocks='" + Convert.ToDouble(textBox4.Text) + "', dateofproduction='" + dateTimePicker1.Text + "', dateofexpiry='" + dateTimePicker2.Text +"' where barcodeid='"+textBox1.Text+"'", conn);
                    cmd.ExecuteNonQuery();
                    conn.Close();


                    this.clear();
                    MessageBox.Show("Record has been updated");
                    refresh();
                   
                  
                }
                
            }
            catch
            {
            }
        }

        private void button5_Click(object sender, EventArgs e)
        {
            try
            {
                if (textBox1.Text == "")
                {
                    MessageBox.Show("Pls. input barcode id");
                    textBox1.Select();
                }
                else
                {
                    this.connectdatabase();
                    cmd = new SqlCommand("select barcodeid, productname, price, stocks, dateofproduction, dateofexpiry from POS where barcodeid='" + textBox1.Text + "'", conn);
                    dr = cmd.ExecuteReader();
                    dr.Read();
                    if (dr.HasRows)
                    {
                        textBox1.Text = dr[0].ToString();
                        textBox2.Text = dr[1].ToString();
                        textBox3.Text = dr[2].ToString();
                        textBox4.Text = dr[3].ToString();
                        dateTimePicker1.Text = dr[4].ToString();
                        dateTimePicker2.Text = dr[5].ToString();
                        textBox1.ReadOnly = true;
                        button2.Enabled = true;
                        button3.Enabled = true;
                        dr.Close();
                    }
                    else
                    {
                        MessageBox.Show("No record found");
                        this.clear();
                    }
                }
            }
            catch
            {
            }
        }

        private void refresh()
        {
            connectdatabase();
            da = new SqlDataAdapter("Select * from POS ", conn);
            DataTable dt = new DataTable();
            da.Fill(dt);
            dataGridView1.DataSource = dt;

        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            refresh();
        }

        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            
            
        }

        private void dataGridView1_MouseDoubleClick(object sender, MouseEventArgs e)
        {

            textBox1.Text = dataGridView1.CurrentRow.Cells[0].Value.ToString();
            textBox2.Text = dataGridView1.CurrentRow.Cells[1].Value.ToString();
            textBox3.Text = dataGridView1.CurrentRow.Cells[2].Value.ToString();
            textBox4.Text = dataGridView1.CurrentRow.Cells[3].Value.ToString();
            dateTimePicker1.Text = dataGridView1.CurrentRow.Cells[4].Value.ToString();
            dateTimePicker2.Text = dataGridView1.CurrentRow.Cells[4].Value.ToString();

            button2.Enabled = true;
            button3.Enabled = true;
                
        }

        private void button4_Click(object sender, EventArgs e)
        {
            clear();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            try
            {
                DialogResult result = MessageBox.Show("You want to delete this product?", "Confirmation Message", MessageBoxButtons.YesNo);
                if (result == DialogResult.Yes)
                {
                    this.connectdatabase();
                   SqlCommand cmd1 = new SqlCommand("delete from POS where barcodeid='" + textBox1.Text + "'", conn);
                    cmd1.ExecuteNonQuery();
                    conn.Close();

                    this.clear();
                    MessageBox.Show("Product successfully deleted.", "Message");
                    refresh();
                }
                else
                {
                    this.clear();
                }
            }
            catch
            {
            }
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void dataGridView1_CellContentDoubleClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void textBox1_KeyPress(object sender, KeyPressEventArgs e)
        {
            try
            {
                char ch = e.KeyChar;

                if (!Char.IsDigit(ch) && ch != 8 && ch != 46)
                {
                    e.Handled = true;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
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
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }

        private void textBox4_KeyUp(object sender, KeyEventArgs e)
        {

        }

        private void textBox4_KeyPress(object sender, KeyPressEventArgs e)
        {
            try
            {
                char ch = e.KeyChar;

                if (!Char.IsDigit(ch) && ch != 8 && ch != 46)
                {
                    e.Handled = true;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }

        private void button6_Click(object sender, EventArgs e)
        {
            this.Hide();
            Form1 form = new Form1();
            form.Show();
        }
    }
}
