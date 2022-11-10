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

    public partial class Form1 : Form
    {
        Constring cs= new Constring();
        SqlCommand cmd;
        SqlConnection conn;
        SqlDataReader dr;
        SqlDataAdapter da;
        private static string passingText;//


        public Form1()
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

        private void toolStripButton6_Click(object sender, EventArgs e)
        {
            
        }

        private void toolStripButton4_Click(object sender, EventArgs e)
        {
            this.Hide();
            Cashier frm = new Cashier();
            frm.Show();
        }

        private void toolStripButton10_Click(object sender, EventArgs e)
        {
            panel1.Show();
        }

        private void toolStripButton9_Click(object sender, EventArgs e)
        {
            Application.Exit();
            this.Close();     
        }

        public void Form1_Load(object sender, EventArgs e)
        {
            // TODO: This line of code loads data into the 'pOSDataSet3.login' table. You can move, or remove it, as needed.
            this.loginTableAdapter.Fill(this.pOSDataSet3.login);
            panel1.Hide();
            panel2.Hide();
            textBox1.Text = Login.sendtext;
        }

        private void toolStripButton10_MouseLeave(object sender, EventArgs e)
        {
           
        }

        private void button3_Click(object sender, EventArgs e)
        {
            panel1.Hide();
            panel2.Show();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            try
            {
                DialogResult result = MessageBox.Show("You want to delete this User?", "Confirmation Message", MessageBoxButtons.YesNo);
                if (result == DialogResult.Yes)
                {
                    this.connectdatabase();
                    cmd = new SqlCommand("delete from login where Username='" + dataGridView1.CurrentRow.Cells[2].Value+ "'", conn);
                    cmd.ExecuteNonQuery();
                    conn.Close();

                    
                    MessageBox.Show("User successfully deleted.", "Message");
                    refresh();
                }
                else
                {
                    MessageBox.Show("User does not exist");
                }
            }
            catch
            {
            }
        }

        private void refresh()
        {
            connectdatabase();
            da = new SqlDataAdapter("Select * from login ", conn);
            DataTable dt = new DataTable();
            da.Fill(dt);
            dataGridView1.DataSource = dt;

        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.Hide();
            Register reg = new Register();
            reg.Show();
        }

        private void button5_Click(object sender, EventArgs e)
        {
            panel1.Hide();
        }

        private void button6_Click(object sender, EventArgs e)
        {
            panel2.Hide();
        }

        private void panel1_Paint(object sender, PaintEventArgs e)
        {
           
        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.Close();
            Login log = new Login();
            log.Show();
        }

        private void toolStripButton5_Click(object sender, EventArgs e)
        {
            this.Hide();
            DailySales dls = new DailySales();
            dls.Show();
        }

        private void toolStripButton3_Click(object sender, EventArgs e)
        {
            this.Hide();
            POS pos = new POS();
            pos.Show();
        }

        private void accountsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.Hide();
            POS pos = new POS();
            pos.Show();
        }

        private void customerToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.Hide();
            Cashier frm = new Cashier();
            frm.Show();
        }

        private void supplierToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.Hide();
            DailySales dls = new DailySales();
            dls.Show();
        }

        private void logoutToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.Close();
            Login log = new Login();
            log.Show();
        }

        private void registerToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.Hide();
            Register reg = new Register();
            reg.Show();
        }

        private void removeToolStripMenuItem_Click(object sender, EventArgs e)
        {
            panel1.Hide();
            panel2.Show();
        }
    }
}
