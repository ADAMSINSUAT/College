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
    public partial class DailySales : Form
    {
        Constring cs = new Constring();
        SqlConnection conn;
        SqlCommand cmd;
        SqlDataReader dr;
        SqlDataAdapter da;

        public static string passingText;//

        public DailySales()
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

        private void DailySales_Load(object sender, EventArgs e)
        {
            // TODO: This line of code loads data into the 'pOSDataSet5.DailySales' table. You can move, or remove it, as needed.
            this.dailySalesTableAdapter.Fill(this.pOSDataSet5.DailySales);
            // TODO: This line of code loads data into the 'pOSDataSet12.DailySales' table. You can move, or remove it, as needed.
            //this.dailySalesTableAdapter4.Fill(this.pOSDataSet12.DailySales);
            // TODO: This line of code loads data into the 'pOSDataSet11.DailySales' table. You can move, or remove it, as needed.
            //this.dailySalesTableAdapter3.Fill(this.pOSDataSet11.DailySales);
            // TODO: This line of code loads data into the 'pOSDataSet10.DailySales' table. You can move, or remove it, as needed.
            //this.dailySalesTableAdapter2.Fill(this.pOSDataSet10.DailySales);  
            button2.Enabled = false;
            button4.Enabled = false;
            refresh();
        }

        private void button2_Click(object sender, EventArgs e)
        {

            this.connectdatabase();
                DialogResult result = MessageBox.Show("Are you sure you want to delete this record?", "Confirmation Message", MessageBoxButtons.YesNo);
                if (result == DialogResult.Yes)
                {
                    this.connectdatabase();
                    

                 
                    cmd = new SqlCommand("delete from DailySales where transactionnumber= '" + textBox1.Text+ "'", conn);
                    cmd.ExecuteNonQuery();
                    conn.Close();
                    

                    this.clear();
                    MessageBox.Show("Record Successfully Deleted");
                    refresh();

                }
                else
                {
                    this.clear();
                }
        }

        private void clear()
        {
            textBox1.Text = "";
            textBox2.Text = "";
            textBox3.Text = "";
            textBox4.Text = "";
            textBox5.Text = "";
            textBox6.Text = "";
            textBox7.Text="";
            textBox8.Text="";
            textBox9.Text = "";
            textBox10.Text = "";
            button2.Enabled = false;
        }

        private void refresh()
        {
            this.connectdatabase();
            da = new SqlDataAdapter("select * from DailySales" , conn);
            DataTable dt = new DataTable();
            da.Fill(dt);
            dataGridView1.DataSource = dt;
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
           
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
            textBox5.Text = dataGridView1.CurrentRow.Cells[4].Value.ToString();
            textBox6.Text = dataGridView1.CurrentRow.Cells[5].Value.ToString();
            textBox7.Text = dataGridView1.CurrentRow.Cells[6].Value.ToString();
            textBox8.Text = dataGridView1.CurrentRow.Cells[7].Value.ToString();
            textBox9.Text = dataGridView1.CurrentRow.Cells[8].Value.ToString();
            textBox10.Text = dataGridView1.CurrentRow.Cells[9].Value.ToString();
            button2.Enabled = true;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.connectdatabase();
            cmd = new SqlCommand("Select * from DailySales where transactionnumber='" + textBox1.Text + "'", conn);
            dr=cmd.ExecuteReader();
            dr.Read();
            if (dr.HasRows)
            {
                textBox1.Text = dr[9].ToString();
                textBox2.Text = dr[0].ToString();
                textBox3.Text = dr[1].ToString();
                textBox4.Text = dr[2].ToString();
                textBox5.Text = dr[3].ToString();
                textBox6.Text = dr[4].ToString();
                textBox7.Text = dr[5].ToString();
                textBox8.Text = dr[6].ToString();
                textBox9.Text = dr[7].ToString();
                textBox10.Text = dr[8].ToString();
                button2.Enabled = true;
                button3.Enabled = true;
                dr.Close();
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            this.Hide();
            Form1 form = new Form1();
            form.Show();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            clear();
            button4.Enabled = false;
        }
    }
}
