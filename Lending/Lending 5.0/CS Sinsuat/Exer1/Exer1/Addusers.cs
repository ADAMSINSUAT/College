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
using System.Drawing.Imaging;

namespace Exer1
{
    public partial class Addusers : Form
    {
        SqlConnection conn;
        SqlCommand cmd;
        SqlDataAdapter da;
        SqlDataReader dr;

        public Addusers()
        {
            InitializeComponent();
        }

        private void Register_Click(object sender, EventArgs e)
        {
            conn = Class1.Connectdb1();
            if (textBox1.Text == "" || textBox2.Text == "" || textBox3.Text == "" || textBox4.Text == "" || textBox5.Text == "" || textBox6.Text == "" || Agee.Text == "" || textBox7.Text == "" || textBox8.Text == "" || textBox9.Text == "" || comboBox1.Text == "" || picpath.Text == "" || pictureBox1.Text == picpath.Text)
            {
                MessageBox.Show("Please fill up all fields");
            }
            else
            {
                int years = DateTime.Now.Year - dateTimePicker1.Value.Year;
                if (dateTimePicker1.Value.AddYears(years) > DateTime.Now) years--;
                Agee.Text = years.ToString();
                int a = Convert.ToInt16(Agee.Text);
                if (a < 18 || a >= 70)
                {
                    MessageBox.Show("Age requirement not met, Must be 18 and above of age but not exceeding 70");
                }
                else
                {     
                        dr.Close();
                        MemoryStream ms = new MemoryStream();
                        pictureBox1.Image.Save(ms, ImageFormat.Jpeg);
                        byte[] pic_arr = new byte[ms.Length];
                        ms.Position = 0;
                        ms.Read(pic_arr, 0, pic_arr.Length);
                        cmd = new SqlCommand("Insert into Users(Accid, FirstName, MiddleI, LastName, Address, ContactNo, Landline, Birthday, Birthage, Position, Usename, Pword, Role, Userpic)VALUES(@Accid, @FirstName, @MiddleI, @LastName, @Address, @ContactNo, @Landline, @Birthday, @Birthage, @Position, @Usename, @Pword, @Role, @Userpic)", conn);
                        cmd.Parameters.AddWithValue("@Accid", AccountID.Text);
                        cmd.Parameters.AddWithValue("@FirstName", textBox1.Text);
                        cmd.Parameters.AddWithValue("@MiddleI", textBox2.Text);
                        cmd.Parameters.AddWithValue("@LastName", textBox3.Text);
                        cmd.Parameters.AddWithValue("@Address", textBox4.Text);
                        cmd.Parameters.AddWithValue("@ContactNo", textBox5.Text);
                        cmd.Parameters.AddWithValue("@Landline", textBox6.Text);
                        cmd.Parameters.AddWithValue("@Birthday", dateTimePicker1.Text);
                        cmd.Parameters.AddWithValue("@Birthage", Agee.Text);
                        cmd.Parameters.AddWithValue("@Position", textBox7.Text);
                        cmd.Parameters.AddWithValue("@Usename", textBox8.Text);
                        cmd.Parameters.AddWithValue("@Pword", textBox9.Text);
                        cmd.Parameters.AddWithValue("@Role", comboBox1.Text);
                        cmd.Parameters.AddWithValue("@Userpic", pic_arr);
                        cmd.ExecuteNonQuery();
                        MessageBox.Show("User is registered!");
                        conn.Close();
                        clear();
                        refresh();
                }
            }
        }

        private void Addusers_Load(object sender, EventArgs e)
        {
            conn = Class1.Connectdb1();
            cmd = new SqlCommand("Select TOP(1) Accid from Users ORDER BY 1 DESC", conn);
            dr = cmd.ExecuteReader();
            if (dr.Read())
            {
                AccountID.Text = dr["Accid"].ToString();
                int a;
                a = Convert.ToInt16(AccountID.Text);
                a++;
                AccountID.Text = a.ToString();
                dr.Close();
                conn.Close();
            }
            else
            {
                AccountID.Text = "0";
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
            textBox7.Text = "";
            textBox7.Text = "";
            textBox8.Text = "";
            textBox9.Text = "";
            dateTimePicker1.Text = "";
            Agee.Text = "";
            picpath.Text = "";
            comboBox1.Text = "";
            pictureBox1.Image = null;
        }

        private void Addphoto_Click(object sender, EventArgs e)
        {
            OFD.Title = "Select picture...";
            OFD.Filter = "jpeg|*.jpg|bmp|*.bmp|png|*.png|all files|*.*";
            OFD.FilterIndex = 1;

            OFD.ShowDialog();
            picpath.Text = OFD.FileName;
            pictureBox1.ImageLocation = picpath.Text;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            clear();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            Form1 form = new Form1();
            form.Show();
            this.Close();
        }

        private void dateTimePicker1_ValueChanged(object sender, EventArgs e)
        {
            int years = DateTime.Now.Year - dateTimePicker1.Value.Year;
            if (dateTimePicker1.Value.AddYears(years) > DateTime.Now) years--;
            Agee.Text = years.ToString();
            int a = Convert.ToInt16(Agee.Text);
            try
            {
                if (Agee.Text == "")
                {
                    Agee.Text = "0";
                }
                else
                {
                    double age;
                    age = Convert.ToDouble(dateTimePicker1.Text);
                    Agee.Text = a.ToString();
                }
            }
            catch
            {
            }
        }
        private void refresh()
        {
            conn = Class1.Connectdb1();
            cmd = new SqlCommand("Select TOP(1) Accid from Users ORDER BY 1 DESC", conn);
            dr = cmd.ExecuteReader();
            if (dr.Read())
            {

                AccountID.Text = dr["Accid"].ToString();
                int a;
                a = Convert.ToInt16(AccountID.Text);
                a++;
                AccountID.Text = a.ToString();
                dr.Close();
                conn.Close();
            }
        }
    }
}
