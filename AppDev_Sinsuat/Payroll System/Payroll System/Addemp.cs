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
    public partial class Addemp : Form
    {
        //Declare these
        Constring cs = new Constring();
        SqlConnection conn;
        SqlCommand cmd;
        SqlDataReader dr;
        String imgLoc = "";
        public static string passingText;//

        public Addemp()
        {
            InitializeComponent();
        }
        //Declare this function
        private void connectdatabase()
        {
           try
           {
               conn = new SqlConnection(cs.connection);
               conn.Open();
            }
            catch
           {
               MessageBox.Show("Sql Server Not Respnding");
           }
        }

        //Form Load
        private void Addemp_Load(object sender, EventArgs e)
        {
            // TODO: This line of code loads data into the 'database1DataSet1.tblpos' table. You can move, or remove it, as needed.
            this.tblposTableAdapter1.Fill(this.database1DataSet1.tblpos);

            this.fillist();
            clear();
            textBox5.Hide();

        }
      

        //Declare this function
        private void clear()
        {
            this.tblposTableAdapter.Fill(this.database1DataSet.tblpos);
            textBox1.Text = "";
            textBox2.Text = "";
            textBox3.Text = "0.00";
            textBox4.Text = "0.00";
            textBox5.Text="photo.jpg";
            textBox6.Text="";
            comboBox1.Text="Select";
            pictureBox1.Image = Payroll_System.Properties.Resources.Photo;
            textBox1.Select();
            textBox1.ReadOnly = false;
            button3.Enabled = false;
            button4.Enabled = false;
            button7.Enabled = false;
            fillist();
        }

        //Declare this function
        private void fillist()
        {
            try
            {
                this.connectdatabase();
                cmd = new SqlCommand("select*from tbladdemp", conn);
                dr = cmd.ExecuteReader();
                listView1.Items.Clear();

                while (dr.Read())
                {
                    ListViewItem lv = new ListViewItem(dr.GetString(0));
                    lv.SubItems.Add(dr.GetString(1));
                    lv.SubItems.Add(dr.GetString(2));
                    lv.SubItems.Add(Convert.ToString(dr.GetValue(3)));
                    lv.SubItems.Add(Convert.ToString(dr.GetValue(4)));
                    listView1.Items.Add(lv);
                }
                dr.Close();
            }
            catch
            {
            }
        }

        //Declare this function
        private void format()
        {
            try
            {
                double a;
                a = Convert.ToDouble(textBox3.Text);
                textBox3.Text = a.ToString("###,###,##0.00");
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
                a = Convert.ToDouble(textBox4.Text);
                textBox4.Text = a.ToString("###,###,##0.00");
            }
            catch
            {
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            try
            {
                this.connectdatabase();
                cmd=new SqlCommand("select*from tbladdemp where empid='"+textBox1.Text+"'",conn);
                dr = cmd.ExecuteReader();
                    if(dr.Read())
                    {
                        MessageBox.Show("Employee already exist");
                        textBox1.Focus();
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
                else if (textBox2.Text == "")
                {
                    MessageBox.Show("Pls. fill up all");
                    textBox2.Select();
                }
                else if (comboBox1.Text == "Select") 
                {
                    MessageBox.Show("Pls. fill up all");
                    comboBox1.Select();
                }
                else if (textBox3.Text == "")
                {
                    MessageBox.Show("Pls. fill up all");
                    textBox4.Select();
                }
                else
                {
                    FileStream fs = new FileStream(textBox5.Text, System.IO.FileMode.Open, System.IO.FileAccess.Read);
                    byte[] image = new byte
                    [fs.Length];
                    fs.Read(image, 0, Convert.ToInt32(fs.Length));
                    fs.Close();

                    this.connectdatabase();
                    cmd=new SqlCommand("insert into tbladdemp(empid, name, pos, rate, bsalary, imageloc, photo)values('"+textBox1.Text+"','"+textBox2.Text+"','"+comboBox1.Text+"','"+Convert.ToDouble(textBox3.Text)+"','"+Convert.ToDouble(textBox4.Text)+"','"+textBox5.Text+"',@pic)",conn);
                    SqlParameter prm = new SqlParameter("@pic", SqlDbType.VarBinary, image.Length, ParameterDirection.Input, false, 0, 0, null, DataRowVersion.Current, image);
                    cmd.Parameters.Add(prm);
                    cmd.ExecuteNonQuery();
                    conn.Close();

                    this.clear();
                    MessageBox.Show("Record Save", "Message...");
                    textBox1.Focus();
                }
                
           }
           catch
            {
                MessageBox.Show("Please check all the data you entered if typed correctly.", "MESSAGE");
            }
        }

        //Button Clear Codes
        private void button6_Click(object sender, EventArgs e)
        {
            clear();
        }

        //Button Search Codes
        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                if (textBox1.Text == "")
                {
                    MessageBox.Show("Please input Employee No.");
                    textBox1.Select();
                }
                else
                {
                    this.connectdatabase();
                    cmd = new SqlCommand("select empid, name, pos, rate, bsalary, imageloc from tbladdemp where empid='" + textBox1.Text + "'", conn);
                    dr = cmd.ExecuteReader();
                    dr.Read();
                    if (dr.HasRows)
                    {
                        textBox1.Text = dr[0].ToString();
                        textBox2.Text = dr[1].ToString();
                        comboBox1.Text = dr[2].ToString();
                        textBox3.Text = dr[3].ToString();
                        textBox4.Text = dr[4].ToString();
                        textBox5.Text = dr[5].ToString();
                        textBox1.ReadOnly = true;
                        image();
                        format();
                        format1();
                        button3.Enabled = true;
                        button4.Enabled = true;
                        button7.Enabled = true;
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

        //Declare this function
        private void image()
        {
            try
            {
                this.connectdatabase();
                SqlDataAdapter sda = new SqlDataAdapter("select*from tbladdemp where empid='" + textBox1.Text + "'", conn);
                DataTable dt = new DataTable();
                sda.Fill(dt);
                byte[] MyData = new byte[0];
                MyData = (byte[])dt.Rows[0][6];
                MemoryStream str = new MemoryStream(MyData);
                pictureBox1.Image = Image.FromStream(str);
            }
            catch
            {
                textBox5.Text = "photo.jpg";
                pictureBox1.Image = Payroll_System.Properties.Resources.Photo;
            }
        }

        private void button5_Click(object sender, EventArgs e)
        {
            try
            {
                OpenFileDialog dlg = new OpenFileDialog();
                dlg.Filter="JPG Files(*.jpg)|GIF files(*.gif)|PNG Files(*.png)|*.png|All Files(*.*)|*.*";
                dlg.Title = "Select Picture";
                if (dlg.ShowDialog() == DialogResult.OK)
                {
                    imgLoc = dlg.FileName.ToString();
                    pictureBox1.ImageLocation = imgLoc;
                    textBox5.Text=imgLoc;
                }
            }
            catch
            {
                MessageBox.Show("Invalid Photo");
            }
        }

        //Textbox3 Codes-Leave Events
        private void textBox3_Leave(object sender, EventArgs e)
        {
            try
            {
                if (textBox3.Text == "")
                {
                    textBox3.Text="0.00";
                }

                else
                {
                    double a;
                    a=Convert.ToDouble(textBox3.Text);
                    textBox3.Text=a.ToString("###,###,##0.00");
                }
            }
            catch
            {
            }
        }

        //Textbox3 codes=TextChanged Events
        private void textBox3_TextChanged(object sender, EventArgs e)
        {
            try
            {
                double a, b;
                a = Convert.ToDouble(textBox3.Text);
                b = a * 26;
                textBox4.Text = b.ToString("###,###,##0.00");
            }
            catch
            {
            }
        }

        //Textbox3 Codes-Click Events
        private void textBox3_Click(object sender, EventArgs e)
        {
            textBox3.SelectAll();
        }

        //Button Update Codes
        private void button3_Click(object sender, EventArgs e)
        {
          try
         {
                if(textBox1.Text=="")
                {
                    MessageBox.Show("Pls. fill up all");
                    textBox1.Select();
                }
                else if(textBox2.Text=="")
                {
                    MessageBox.Show("Pls. fill up all");
                    textBox2.Select();
                }
                else if (comboBox1.Text == "Select")
                {
                    MessageBox.Show("Pls. fill up all");
                    comboBox1.Select();
                }
                else if (textBox3.Text == "")
                {
                    MessageBox.Show("Pls. fill up all");
                    textBox4.Select();
                }
                else
                {
                    FileStream fs = new FileStream(textBox5.Text, System.IO.FileMode.Open, System.IO.FileAccess.Read);
                    byte[] image = new byte[fs.Length];
                    fs.Read(image, 0, Convert.ToInt32(fs.Length));
                    fs.Close();

                    cmd = new SqlCommand("update tbladdemp set empid='" + textBox1.Text + "', name='" + textBox2.Text + "',pos='" + comboBox1.Text + "',rate='" + Convert.ToDouble(textBox3.Text) + "',bsalary='" + Convert.ToDouble(textBox4.Text) + "',imageloc='" + textBox5.Text + "',photo=@pic where empid='" + textBox1.Text + "'", conn);
                    SqlParameter prm = new SqlParameter("@pic", SqlDbType.VarBinary, image.Length, ParameterDirection.Input, false, 0, 0, null, DataRowVersion.Current, image);
                    cmd.Parameters.Add(prm);
                    cmd.ExecuteNonQuery();
                    conn.Close();
                    

                    this.clear();
                    MessageBox.Show("Record has been updated");
                }
            }
         catch
           {
          }
        }

        //Button Delete Codes
        private void button4_Click(object sender, EventArgs e)
        {
            try
            {
                DialogResult result = MessageBox.Show("You want to delete this record?", "Confirmation Message", MessageBoxButtons.YesNo);
                if (result == DialogResult.Yes)
                {
                    this.connectdatabase();
                    cmd = new SqlCommand("delete from tbladdemp where empid='" + textBox1.Text + "'", conn);
                    cmd.ExecuteNonQuery();
                    conn.Close();

                    this.clear();
                    MessageBox.Show("Record successfully deleted.", "Message");
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

        //Textbox6 Codes-TextChanged Events
        private void textBox6_TextChanged(object sender, EventArgs e)
        {
            try
            {
                if (textBox6.Text == "")
                {
                    fillist();
                }
                else
                {
                    this.connectdatabase();
                    cmd=new SqlCommand("select empid, name, pos, rate, bsalary from tbladdemp where empid like'"+textBox6.Text+"%'or name like '%"+textBox6.Text+"%'",conn);
                    dr = cmd.ExecuteReader();
                    listView1.Items.Clear();
                    if (dr.Read())
                    {
                        ListViewItem lv = new ListViewItem(dr.GetString(0));
                        lv.SubItems.Add(dr.GetString(1));
                        lv.SubItems.Add(dr.GetString(2));
                        lv.SubItems.Add(Convert.ToString(dr.GetValue(3)));
                        lv.SubItems.Add(Convert.ToString(dr.GetValue(4)));
                        listView1.Items.Add(lv);
                    }
                    dr.Close();
                }
            }
            catch
            {
            }
        }
        //Listview1 Codes
        private void listView1_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                if (listView1.SelectedItems.Count > 0)
                {
                    textBox1.Text = listView1.SelectedItems[0].Text;
                    listsearch();
                }
            }
            catch
            {
            }

        }


        //Declare this function
        private void listsearch()
        {
            this.connectdatabase();
            cmd = new SqlCommand("select empid, name, pos, rate, bsalary, imageloc from tbladdemp where empid='" +textBox1.Text + "'", conn);
            dr = cmd.ExecuteReader();
                dr.Read();
                if (dr.HasRows)
                {
                    textBox1.Text = dr[0].ToString();
                    textBox2.Text = dr[1].ToString();
                    comboBox1.Text = dr[2].ToString();
                    textBox3.Text = dr[3].ToString();
                    textBox4.Text = dr[4].ToString();
                    textBox5.Text = dr[5].ToString();

                    textBox1.ReadOnly = true;
                    image();
                    format();
                    format1();

                    button3.Enabled = true;
                    button4.Enabled = true;
                    button7.Enabled = true;


                }
                else
                {
                }
        }

        private void button7_Click(object sender, EventArgs e)
        {
                passingText = textBox1.Text;
                Form frm = new Printemp();
                frm.ShowDialog();
                this.clear();
            
          
        }

        //Button Payroll Codes
        private void button8_Click(object sender, EventArgs e)
        {
           this.clear();
           Form frm = new Payroll();
           frm.ShowDialog();
        }       
    }
}