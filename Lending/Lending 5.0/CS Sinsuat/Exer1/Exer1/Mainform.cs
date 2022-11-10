using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Drawing.Imaging;
using System.Data.SqlClient;
using System.IO;
using CrystalDecisions.CrystalReports.Engine;
using CrystalDecisions.Shared;


namespace Exer1
{
    public partial class Mainform : Form
    {
        SqlConnection conn;
        SqlCommand cmd;
        SqlDataAdapter da;
        SqlDataReader dr;
        public Mainform()
        {
            

            InitializeComponent();
        }
        //string imgLoc = "";

        private void button1_Click(object sender, EventArgs e)
        {
            conn = Class1.Connectdb1();
            if (BorrowersID.Text == "" || LastName.Text=="" || FirstName.Text=="" || MiddleInitial.Text==""|| BirthDate.Text=="" || Age.Text == "" || PlaceofBirth.Text == "" || CivilStatus.Text==""|| Gender.Text==""|| BorrowerStatus.Text==""|| Address.Text == "" || City.Text == "" || Province.Text == "" || Zipcode.Text == "" || Email.Text == "" || Landline.Text=="" || ContactNo.Text==""|| Occupation.Text==""|| pictureBox1.Text==Imagepath.Text)
            {
                MessageBox.Show("Please fill up all fields");
            }
            else
            {
                int years = DateTime.Now.Year - BirthDate.Value.Year;
                if (BirthDate.Value.AddYears(years) > DateTime.Now) years--;
                Age.Text = years.ToString();
                int a = Convert.ToInt16(Age.Text);
                if (a < 18 || a >= 70)
                {
                    MessageBox.Show("Age requirement not met, Must be 18 and above of age but not exceeding 70");
                }
                else
                {
                    cmd = new SqlCommand("Select*from BrrwersPInfo where ApplcntID='" + BorrowersID.Text + "'", conn);
                    dr = cmd.ExecuteReader();
                    if (dr.Read())
                    {
                        MessageBox.Show("Borrower already exists");
                    }
                    else
                    {
                        dr.Close();
                        //byte[] images = null; ;
                        //FileStream fs = new FileStream(imgLoc,FileMode.Open, FileAccess.Read);
                        //BinaryReader br = new BinaryReader(fs);
                        //images = br.ReadBytes((int)fs.Length);
                        //fs.Read(images, 0, Convert.ToInt32(fs.Length));
                        //fs.Close();
                        MemoryStream ms = new MemoryStream();
                        pictureBox1.Image.Save(ms, System.Drawing.Imaging.ImageFormat.Jpeg);
                        byte[] pic_arr = new byte[ms.Length];
                        ms.Position = 0;
                        ms.Read(pic_arr, 0, pic_arr.Length);

                        cmd = new SqlCommand("Insert into BrrwersPInfo(ApplcntID, Lstname, Frstname, MI, Bday, Age, Plcfbrth, CvilStats, Gnder, BrrwersStats, Addrss, Cty, Prvince, Zpcde, Email, Lndlne, Cno, Occpatn, Image)VALUES(@ApplcntID, @Lstname, @Frstname, @MI, @Bday, @Age, @Plcfbrth, @CvilStats, @Gnder, @BrrwersStats, @Addrss, @Cty, @Prvince, @Zpcde, @Email, @Lndlne, @Cno, @Occpatn, @Image)", conn);
                        cmd.Parameters.AddWithValue("@ApplcntID", BorrowersID.Text);
                        cmd.Parameters.AddWithValue("@Lstname", LastName.Text);
                        cmd.Parameters.AddWithValue("@Frstname", FirstName.Text);
                        cmd.Parameters.AddWithValue("@MI", MiddleInitial.Text);
                        cmd.Parameters.AddWithValue("@Bday", BirthDate.Text);
                        cmd.Parameters.AddWithValue("@Age", Age.Text);
                        cmd.Parameters.AddWithValue("@Plcfbrth", PlaceofBirth.Text);
                        cmd.Parameters.AddWithValue("@CvilStats", CivilStatus.Text);
                        cmd.Parameters.AddWithValue("@Gnder", Gender.Text);
                        cmd.Parameters.AddWithValue("@BrrwersStats", BorrowerStatus.Text);
                        cmd.Parameters.AddWithValue("@Addrss", Address.Text);
                        cmd.Parameters.AddWithValue("@Cty", City.Text);
                        cmd.Parameters.AddWithValue("@Prvince", Province.Text);
                        cmd.Parameters.AddWithValue("@Zpcde", Zipcode.Text);
                        cmd.Parameters.AddWithValue("@Email", Email.Text);
                        cmd.Parameters.AddWithValue("@Lndlne", Landline.Text);
                        cmd.Parameters.AddWithValue("@Cno", ContactNo.Text);
                        cmd.Parameters.AddWithValue("@Occpatn", Occupation.Text);
                        cmd.Parameters.AddWithValue("@Image", pic_arr);
                        cmd.ExecuteNonQuery();
                        MessageBox.Show("Saved");
                        BorrowersID.Text = "";
                        LastName.Text = "";
                        FirstName.Text = "";
                        MiddleInitial.Text = "";
                        Age.Text = "";
                        PlaceofBirth.Text = "";
                        Address.Text = "";
                        City.Text = "";
                        Province.Text = "";
                        Zipcode.Text = "";
                        Email.Text = "";
                        Landline.Text = "";
                        ContactNo.Text = "";
                        CivilStatus.Text = "";
                        Gender.Text = "";
                        BorrowerStatus.Text = "";
                        BirthDate.Text = "";
                        button3.Enabled = false;
                        pictureBox1.Image = null;
                        Imagepath.Text = "";
                        Occupation.Text = "";
                        conn.Close();
                        refresh();
                    }
                }
            }
            //conn.Close();
        }
        private void dateTimePicker1_ValueChanged(object sender, EventArgs e)
        {
            int years = DateTime.Now.Year - BirthDate.Value.Year;
            if (BirthDate.Value.AddYears(years) > DateTime.Now) years--;
            Age.Text = years.ToString();

            try
            {
                if (Age.Text == "")
                {
                    Age.Text = "0";
                }
                else
                {
                    double a;
                    a = Convert.ToDouble(BirthDate.Text);
                    Age.Text = a.ToString();
                }
            }
            catch
            {
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            clear();
            BorrowersID.Enabled = true;
        }

        private void Mainform_Load(object sender, EventArgs e)
        {
            label45.Text = Login.sendtext;
            //clear();
            loandata();
            //LoanAmount.Text = "";
            //BorrowersID.Text = "";
            Landline.Text = "N/A";
            ContactNo.Text = "N/A";
            Interestold.Text = "0.08";
            Dateloanmonths.Text = "0";
            button4.Enabled = false;
            button6.Enabled = false;

            conn = Class1.Connectdb1();
            cmd = new SqlCommand("Select TOP(1) Transactno from Loaninfo ORDER BY 1 DESC", conn);
            dr = cmd.ExecuteReader();
            if (dr.Read())
            {
                TransactionID.Text = dr["Transactno"].ToString();
                int a;
                a = Convert.ToInt16(TransactionID.Text);
                a++;
                TransactionID.Text = a.ToString();
                dr.Close();
                conn.Close();
            }
            else
            {
                TransactionID.Text = "1";
            }     
        }

        private void button4_Click(object sender, EventArgs e)
        {
            if (BorrowersID.Text == "")
            {
                MessageBox.Show("Please input borrower's ID");
            }
            else
            {
                conn = Class1.Connectdb1();
                cmd = new SqlCommand("Select ApplcntID, Lstname, Frstname, MI, Bday, Age, Plcfbrth, CvilStats, Gnder, BrrwersStats, Addrss, Cty, Prvince, Zpcde, Email, Lndlne, Cno, Occpatn, Image from BrrwersPInfo where ApplcntID='" + BorrowersID.Text + "'", conn);
                SqlDataReader dr = cmd.ExecuteReader();
                dr.Read();
                if (dr.HasRows)
                {
                    BorrowersID.Text = dr[0].ToString();
                    LastName.Text = dr[1].ToString();
                    FirstName.Text = dr[2].ToString();
                    MiddleInitial.Text = dr[3].ToString();
                    BirthDate.Text = dr[4].ToString();
                    Age.Text = dr[5].ToString();
                    PlaceofBirth.Text = dr[6].ToString();
                    CivilStatus.Text = dr[7].ToString();
                    Gender.Text = dr[8].ToString();
                    BorrowerStatus.Text = dr[9].ToString();
                    Address.Text = dr[10].ToString();
                    City.Text = dr[11].ToString();
                    Province.Text = dr[12].ToString();
                    Zipcode.Text = dr[13].ToString();
                    Email.Text = dr[14].ToString();
                    Landline.Text = dr[15].ToString();
                    ContactNo.Text = dr[16].ToString();
                    Occupation.Text = dr[17].ToString();
                    string a1;
                    a1 = Convert.ToString("pic");
                    if (a1 == "")
                    {
                    }
                    else
                    {
                        byte[] photo_array = (byte[])dr[18];
                        MemoryStream ms = new MemoryStream(photo_array);
                        ms.Position = 0;
                        ms.Read(photo_array, 0, photo_array.Length);

                        pictureBox1.Image = Image.FromStream(ms);
                    }
                    BorrowersID.Enabled = false;
                    button3.Enabled = true;
                    button1.Enabled = false;
                }
                else
                {
                    MessageBox.Show("Applicant ID unrecognizable");
                    button1.Enabled = true;
                    button3.Enabled = true;
                    LastName.Text = "";
                    FirstName.Text = "";
                    MiddleInitial.Text = "";
                    BirthDate.Text = "";
                    Age.Text = "";
                    PlaceofBirth.Text = "";
                    CivilStatus.Text = "";
                    Gender.Text = "";
                    BorrowerStatus.Text = "";
                    Address.Text = "";
                    City.Text = "";
                    Province.Text = "";
                    Zipcode.Text = "";
                    Email.Text = "";
                    Landline.Text = "";
                    ContactNo.Text = "";
                    Occupation.Text = "";
                    pictureBox1.ResetText();
                }
            }
        }

        private void button5_Click(object sender, EventArgs e)
        {
            //try
            //{
                OFD.Title = "Select picture...";
                OFD.Filter = "jpeg|*.jpg|bmp|*.bmp|png|*.png|all files|*.*";
                OFD.FilterIndex = 1;

                OFD.ShowDialog();
                Imagepath.Text = OFD.FileName;
                pictureBox1.ImageLocation = Imagepath.Text;
                //if (dialog.ShowDialog() == DialogResult.OK)
                //{
                //    imgLoc = dialog.FileName.ToString();
                //    pictureBox1.ImageLocation = imgLoc;
                //}
            //}
            //catch(Exception d)
            //{
            //    MessageBox.Show(d.ToString()+"Invalid Photo");
            //}
        }

        private void dateTimePicker2_ValueChanged(object sender, EventArgs e)
        {

        }
        private void button3_Click(object sender, EventArgs e)
        {
            //try
            //{
                conn = Class1.Connectdb1();
                //SqlCommand cmd1 = new SqlCommand();
                //cmd1 = new SqlCommand("Select*from BrrwersPInfo where ApplcntID='" + BorrowersID.Text + "'", conn);
                //dr = cmd1.ExecuteReader();
                //if (dr.Read())
                //{
                //    MessageBox.Show("Borrower already exists!");
                //}
                //else
                //{
                    MemoryStream ms = new MemoryStream();
                    pictureBox1.Image.Save(ms, System.Drawing.Imaging.ImageFormat.Jpeg);
                    byte[] pic_arr = new byte[ms.Length];
                    ms.Position = 0;
                    ms.Read(pic_arr, 0, pic_arr.Length);
                    cmd = new SqlCommand("Update BrrwersPInfo set ApplcntID=@ApplcntID, Lstname=@Lstname, Frstname=@Frstname, MI=@MI, Bday=@Bday, Age=@Age, Plcfbrth=@Plcfbrth, CvilStats=@CvilStats, Gnder=@Gnder, BrrwersStats=@BrrwersStats, Addrss=@Addrss, Cty=@Cty, Prvince=@Prvince, Zpcde=@Zpcde, Email=@Email, Lndlne=@Lndlne, Cno=@Cno, Occpatn=@Occpatn, Image=@Image where ApplcntID='" + BorrowersID.Text + "'", conn);
                    //cmd = new SqlCommand("Update BrrwersPInfo set ApplcntID ='" + BorrowersID.Text + "', Lstname= '" + LastName.Text + "', Frstname='" + FirstName.Text + "', MI='" + MiddleInitial.Text + "', Bday='" + BirthDate.Text + "', Age='" + Age.Text + "', Plcfbrth='" + PlaceofBirth.Text + "', CvilStats='" + CivilStatus.Text + "', Gnder='" + Gender.Text + "', BrrwersStats='" + BorrowerStatus.Text + "', Addrss='" + Address.Text + "', Cty='" + City.Text + "', Prvince='" + Province.Text + "', Zpcde='" + Zipcode.Text + "', Email='" + Email.Text + "', Lndlne='" + Landline.Text + "', Cno='" + ContactNo.Text + "', Image=@pic where ApplcntID='" + BorrowersID.Text + "'", conn);
                    //cmd = new SqlCommand("Update BrrwersPInfo set(ApplcntID, Lstname, Frstname, MI, Bday, Age, Plcfbrth, CvilStats, Gnder, BrrwersStats, Addrss, Cty, Prvince, Zpcde, Email, Lndlne, Cno, Image)VALUES(@ApplcntID, @Lstname, @Frstname, @MI, @Bday, @Age, @Plcfbrth, @CvilStats, @Gnder, @BrrwersStats, @Addrss, @Cty, @Prvince, @Zpcde, @Email, @Lndlne, @Cno, @pic)", conn);    
                    cmd.Parameters.AddWithValue("@ApplcntID", BorrowersID.Text);
                    cmd.Parameters.AddWithValue("@Lstname", LastName.Text);
                    cmd.Parameters.AddWithValue("@Frstname", FirstName.Text);
                    cmd.Parameters.AddWithValue("@MI", MiddleInitial.Text);
                    cmd.Parameters.AddWithValue("@Bday", BirthDate.Text);
                    cmd.Parameters.AddWithValue("@Age", Age.Text);
                    cmd.Parameters.AddWithValue("@Plcfbrth", PlaceofBirth.Text);
                    cmd.Parameters.AddWithValue("@CvilStats", CivilStatus.Text);
                    cmd.Parameters.AddWithValue("@Gnder", Gender.Text);
                    cmd.Parameters.AddWithValue("@BrrwersStats", BorrowerStatus.Text);
                    cmd.Parameters.AddWithValue("@Addrss", Address.Text);
                    cmd.Parameters.AddWithValue("@Cty", City.Text);
                    cmd.Parameters.AddWithValue("@Prvince", Province.Text);
                    cmd.Parameters.AddWithValue("@Zpcde", Zipcode.Text);
                    cmd.Parameters.AddWithValue("@Email", Email.Text);
                    cmd.Parameters.AddWithValue("@Lndlne", Landline.Text);
                    cmd.Parameters.AddWithValue("@Cno", ContactNo.Text);
                    cmd.Parameters.AddWithValue("@Occpatn", ContactNo.Text);
                    cmd.Parameters.AddWithValue("@Image", pic_arr);
                    cmd.ExecuteNonQuery();
                    conn.Close();

                    MessageBox.Show("Info updated!");
                    clear();
                //}
            //}
            //catch (Exception k)
            //{
            //    MessageBox.Show(k.ToString());
            //}
        }



        private void clear()
        {
            BorrowersID.Text="";
            LastName.Text="";
            FirstName.Text="";
            MiddleInitial.Text="";
            Age.Text="";
            PlaceofBirth.Text="";
            Address.Text="";
            City.Text="";
            Province.Text="";
            Zipcode.Text="";
            Email.Text="";
            Landline.Text="";
            ContactNo.Text="";
            CivilStatus.Text="";
            Gender.Text="";
            BorrowerStatus.Text="";
            BirthDate.Text="";
            button3.Enabled = false;
            pictureBox1.Image = null;
            Occupation.Text = "";
        }
        private void BorrowersID_KeyPress(object sender, KeyPressEventArgs e)
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

        private void SearchID_Click(object sender, EventArgs e)
        {
            if (BID.Text == "")
            {
                MessageBox.Show("Please input the correct ID");
            }
            else
            {
                conn = Class1.Connectdb1();
                cmd = new SqlCommand("Select*from BrrwersPInfo where ApplcntID='" + BID.Text + "'", conn);
                dr = cmd.ExecuteReader();
                if (dr.Read())
                {
                    BLastName.Text = dr[1].ToString();
                    B1stName.Text = dr[2].ToString();

                    conn = Class1.Connectdb1();
                    cmd = new SqlCommand("Select*from Loaninfo where LonID='" + BID.Text + "'or Neardate<='" + DateTime.Now + "' and Duedate<='" + DateTime.Now + "'", conn);
                    dr = cmd.ExecuteReader();
                    if (dr.Read())
                    {
                        MessageBox.Show("This user has a pending loan");
                        button6.Enabled = false;
                        LoanAmount.Text = "0";
                        BID.Enabled = false;
                        TransactionID.Text = dr["Transactno"].ToString();
                        Montlypayment.Text = dr["Mnthly"].ToString();
                        textBox1.Text = dr["Amntinwords"].ToString();
                        //label34.Text = dr["Blance"].ToString();
                        dateTimePicker3.Text = Convert.ToDateTime(dr["Neardate"].ToString()).ToShortDateString();
                        label42.Text = dr["Intrst"].ToString();
                        Amountforthisaccount.Text = dr["Blance"].ToString();
                      dateTimePicker4.Text = Convert.ToDateTime(dr["Duedate"].ToString()).ToShortDateString();
                        dr.Close();
                    }
                    else
                    {
                        button6.Enabled = true;
                        SearchID.Enabled = false;
                        BID.Enabled = false;
                        dr.Close();
                    }
                }
                else
                {
                    MessageBox.Show("Borrower's ID not found...");
                    BID.Enabled = false;
                    button6.Enabled = false;
                    B1stName.Text = "";
                    BLastName.Text = "";
                }
                loandata();
                conn.Close();
            }
        }

        private void button6_Click(object sender, EventArgs e)
        {
            if (SearchID.Text == "" || B1stName.Text == "" || BLastName.Text == "" || LoanAmount.Text == "" || comboBox2.Text == "Select" || Interest.Text == "0" || Initialloan.Text == "" || textBox1.Text == "")
            {
                MessageBox.Show("Please fill up all necessary information", "Error, no data present");
            }
            else
            {
                conn = Class1.Connectdb1();
                cmd = new SqlCommand("Select*from Loaninfo where LonID='" + BID.Text + "'", conn);
                dr = cmd.ExecuteReader();
                if (dr.Read())
                {
                    MessageBox.Show("Borrower has a pending loan", "Error...");
                }
                else
                {
                    dr.Close();
                    double p = Convert.ToDouble(TransactionID.Text);
                    for (int i = Convert.ToInt16(comboBox2.Text); i > 0; i--)
                    {
                        cmd = new SqlCommand("Insert into Loaninfo(LonID, Lastnim, Firstnim, Amntlnd, Dtoflon, Intrst, Duedate, Pnalty, Intiallon, Mnthly,  Amntinwords, Blance, Dration, Neardate, Transactno)VALUES(@LonID, @Lastnim, @Firstnim, @Amntlnd, @Dtoflon, @Intrst, @Duedate, @Pnalty, @Intiallon, @Mnthly,  @Amntinwords, @Blance, @Dration, @Neardate, @Transactno)", conn);
                        cmd.Parameters.AddWithValue("@LonID", BID.Text);
                        cmd.Parameters.AddWithValue("@Lastnim", BLastName.Text);
                        cmd.Parameters.AddWithValue("@Firstnim", B1stName.Text);
                        cmd.Parameters.AddWithValue("@Amntlnd", LoanAmount.Text);
                        cmd.Parameters.AddWithValue("@Dtoflon", Convert.ToDateTime(Dateofloan.Value).ToShortDateString());
                        cmd.Parameters.AddWithValue("@Intrst", Interest.Text);
                        cmd.Parameters.AddWithValue("@Duedate", Convert.ToDateTime(Duedateofloan.Value).ToShortDateString());
                        cmd.Parameters.AddWithValue("@Pnalty", Penalty.Text);
                        cmd.Parameters.AddWithValue("@Intiallon", Initialloan.Text);
                        cmd.Parameters.AddWithValue("@Mnthly", Montlypayment.Text);
                        cmd.Parameters.AddWithValue("@Amntinwords", textBox1.Text);
                        cmd.Parameters.AddWithValue("@Blance", Initialloan.Text);
                        cmd.Parameters.AddWithValue("@Dration", comboBox2.Text);
                        cmd.Parameters.AddWithValue("@Neardate", Convert.ToDateTime(dateTimePicker3.Value).ToShortDateString());
                        cmd.Parameters.AddWithValue("@Transactno", TransactionID.Text);
                        p++;
                        DateTime date = dateTimePicker3.Value.AddMonths(1);
                        dateTimePicker3.Value = date;
                        TransactionID.Text = p.ToString();
                        cmd.ExecuteNonQuery();
                        //conn.Close();

                    }
                    MessageBox.Show("Loan saved!...");
                    refresh2();
                    clearloan();
                }
            }
        }
        private void LoanAmount_TextChanged(object sender, EventArgs e)
        {
           
        }

        private void clearloan()
        {
            BID.Text = "";
            LoanAmount.Text = "0";
            Penalty.Text = "0";
            Initialloan.Text = "0";
            B1stName.Text = "";
            BLastName.Text = "";
            textBox1.Text = "";
            Dateofloan.ResetText();
            Duedateofloan.ResetText();
            Interest.Text = "0";
            comboBox2.Text = "Select";
            Montlypayment.Text = "";
            label34.Text = "";
            Penalty.Text = "";
            dateTimePicker3.ResetText();
            label38.Text = "";
            textBox1.Text = "";
        }

        private void BorrowersID_TextChanged(object sender, EventArgs e)
        {
            if (BorrowersID.Text == "")
            {
                button4.Enabled = false;
            }
            else
            {
                button4.Enabled = true;
            }
        }

        private void refresh()
        {
            conn = Class1.Connectdb1();
            cmd = new SqlCommand("Select TOP(1) ApplcntID from BrrwersPInfo ORDER BY 1 DESC", conn);
            dr = cmd.ExecuteReader();
            if (dr.Read())
            {
                BorrowersID.Text = dr["ApplcntID"].ToString();
                int a;
                a = Convert.ToInt16(BorrowersID.Text);
                a++;
                BorrowersID.Text = a.ToString();
                dr.Close();
                conn.Close();
            }
            else
            {
                BorrowersID.Text = "1";
            }      
        }

        private void Check_Click(object sender, EventArgs e)
        {
        }

        private void label29_TextChanged(object sender, EventArgs e)
        {
            conn = Class1.Connectdb1();
            cmd = new SqlCommand("Select * from BrrwersPInfo where ApplcntID = '"+label29.Text+"'",conn);
            dr = cmd.ExecuteReader();
            if (dr.Read())
            {
                BorrowersID.Text = dr.GetString(0);
                LastName.Text = dr.GetString(1);
                FirstName.Text = dr.GetString(2);
                MiddleInitial.Text = dr.GetString(3);
                BirthDate.Text = dr.GetDateTime(4).ToShortDateString();
                Age.Text = dr.GetString(5);
                PlaceofBirth.Text = dr.GetString(6);
                CivilStatus.Text = dr.GetString(7);
                Gender.Text = dr.GetString(8);
                BorrowerStatus.Text = dr.GetString(9);
                Address.Text = dr.GetString(10);
                City.Text = dr.GetString(11);
                Province.Text = dr.GetString(12);
                Zipcode.Text = dr.GetString(13);
                Email.Text = dr.GetString(14);
                Landline.Text = dr.GetString(15);
                ContactNo.Text = dr.GetString(16);
                Occupation.Text = dr.GetString(17);
                BorrowersID.Enabled = false;
                BID.Text = dr.GetString(0);
                BID.Enabled = false;
                SearchID.Enabled = false;
                button2.Enabled = false;
                button3.Enabled = true;
                button5.Enabled = false;
                byte[]up = (byte[])(dr[18]);
                if (up == null)
                {
                    pictureBox1.Image = null;
                }
                else
                {
                    MemoryStream ms = new MemoryStream(up);
                    pictureBox1.Image = Image.FromStream(ms);
                }
            }
        }

        private void button5_Click_1(object sender, EventArgs e)
        {
            SearchID.Enabled = true;
            BID.Enabled = true;
            clearloan();
            paymentclear();
            Calculatepayment.Enabled=true;
        }
        private void loandata()
        {
            conn = Class1.Connectdb1();
            da = new SqlDataAdapter("Select LonID, Lastnim, Firstnim, Amntlnd, Dtoflon, Intrst, Duedate, Pnalty, Intiallon, Mnthly,  Amntinwords, Blance, Dration, Neardate, Transactno  from Loaninfo where LonID='" + BID.Text + "'", conn);
            DataTable dt = new DataTable();
            da.Fill(dt);

            dataGridView1.Rows.Clear();

            foreach (DataRow item in dt.Rows)
            {
                int n = dataGridView1.Rows.Add();
                dataGridView1.Rows[n].Cells[0].Value = item["Transactno"].ToString();
                dataGridView1.Rows[n].Cells[1].Value = item["LonID"].ToString();
                dataGridView1.Rows[n].Cells[2].Value = item["Lastnim"].ToString();
                dataGridView1.Rows[n].Cells[3].Value = item["Firstnim"].ToString();
                dataGridView1.Rows[n].Cells[4].Value = item["Amntlnd"].ToString();
                dataGridView1.Rows[n].Cells[5].Value = Convert.ToDateTime(item["Dtoflon"].ToString()).ToShortDateString();
                dataGridView1.Rows[n].Cells[6].Value = item["Intrst"].ToString();
                dataGridView1.Rows[n].Cells[7].Value = Convert.ToDateTime(item["Duedate"].ToString()).ToShortDateString();
                dataGridView1.Rows[n].Cells[8].Value = item["Pnalty"].ToString();
                dataGridView1.Rows[n].Cells[9].Value = item["Intiallon"].ToString();
                dataGridView1.Rows[n].Cells[10].Value = item["Mnthly"].ToString();
                dataGridView1.Rows[n].Cells[11].Value = item["Amntinwords"].ToString();
                dataGridView1.Rows[n].Cells[12].Value = item["Blance"].ToString();
                dataGridView1.Rows[n].Cells[13].Value = item["Dration"].ToString();

                dataGridView1.Rows[n].Cells[15].Value = Convert.ToDateTime(item["Neardate"].ToString()).ToShortDateString();
            }
        }

        private void LoanAmount_KeyPress(object sender, KeyPressEventArgs e)
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

        private void Calculate_Click(object sender, EventArgs e)
        {
            if (LoanAmount.Text == "")
            {
                MessageBox.Show("Please input amount");
            }
            else if (comboBox2.Text == "Select")
            {
                MessageBox.Show("Please input the duration");
            }
            else
            {
                double a, b, c, d, f1, g;
                a = Convert.ToDouble(LoanAmount.Text);
                b = 0.08;
                c = 0.02;
                d = Convert.ToDouble(Dateloanmonths.Text);
                f1 = a;
                g = 1;
                int monthlydate = Convert.ToInt16(comboBox2.Text);
                Duedateofloan.Value = DateTime.Now.AddMonths(monthlydate);
                dateTimePicker3.Value = Dateofloan.Value.AddMonths(+1);
                //int loan = Duedateofloan.Value.Month - Dateofloan.Value.Month;
                //dateTimePicker1.Value = Dateofloan.Value.AddMonths(loan);

                //int year = Duedateofloan.Value.Year - Dateofloan.Value.Year;

                //int add = year * 12;

                //if (Dateofloan.Value.AddMonths(loan) < Duedateofloan.Value)
                //{
                //    //MessageBox.Show("+1");
                //    loan = loan + add;
                //    Dateloanmonths.Text = loan.ToString();
                //}
                //else if (dateTimePicker1.Value.ToString() == Duedateofloan.Value.ToString())
                //{
                //    loan = loan + add;
                //    //MessageBox.Show("+2");

                //    Dateloanmonths.Text = loan.ToString();
                //}
                //else if (Dateofloan.Value.AddMonths(loan) > Duedateofloan.Value)
                //{
                //    //loan = add+(loan - 1);
                //    loan = 0;
                //    //Dateloanmonths.Text = loan.ToString();
                //    MessageBox.Show("Alon, Amino, Bedz, Buenbrazo, Mac, Moja, Salik is here!!! Za WARUDO!!!!!!! ORA ORA ORA ORA ORA ORA ORA ORA ORA ORA ORA ORA ORA!!!!!!! MUDA MUDA MUDA MUDA MUDA MUDA MUDA MUDA MUDA MUDA MUDA MUDA MUDA MUDA MUDA MUDA MUDA MUDA MUDA MUDA MUDA MUDA MUDA MU...DA!!!!!!....!!!!!!!");
                //}
                //f2 = loan;
                //if (loan == 0)
                //{
                //    Initialloan.Text = "0";
                //}
                //else if (f2 > 2)
                //{
                //    f2--;
                //    f1 = f1 + (f1 * b) + (f1 * b * c);
                //    while (f2 > 0)
                //    {
                //        f2--;
                //        f1 = f1 + (f1 * b);
                //        Initialloan.Text = string.Format("{0:0.00}", f1);
                //    }
                //}
                //else if (f2 <= 2)
                //{
                //    f1 = a * b* 2 +(a/2*2);
                //    Initialloan.Text = f1.ToString();
                //}

                if (LoanAmount.Text == "0")
                {
                    Initialloan.Text = "0";
                }
                else
                {
                    
                    f1 = f1 * b * monthlydate + (f1 / monthlydate * monthlydate);
                    Initialloan.Text = String.Format("{0:0.00}", f1);
                    f1 = f1 / monthlydate;
                    Montlypayment.Text = String.Format("{0:0.00}", f1);
                }
                f1 = a * b;
                Interest.Text = String.Format("{0:0.00}", f1);
            }
        }

        private void comboBox2_SelectedIndexChanged(object sender, EventArgs e)
        {
           
        }

        private void button7_Click(object sender, EventArgs e)
        {
            this.Hide();
        }

        private void Pay_Click(object sender, EventArgs e)
        {
            if (textBox2.Text == "")
            {
                textBox2.Text = "0";
            }
            else if (Convert.ToDouble(textBox2.Text) < Convert.ToDouble(Montlypayment.Text))
            {
                MessageBox.Show("Amount must be equal or above of payment");
            }
            else if (Penalty.Text == "")
            {
                Penalty.Text = "0";
            }
            else if (Convert.ToDouble(Amountforthisaccount.Text) > 0 && Convert.ToDouble(textBox2.Text)==0)
            {
                MessageBox.Show("Please input payment");
            }
            else
            {
                DialogResult result = MessageBox.Show("Would you like to have change?", "Confirmation Message...", MessageBoxButtons.YesNo);
                if (result == DialogResult.Yes)
                {
                        int date = dateTimePicker2.Value.Month-dateTimePicker3.Value.Month;
                        int year = dateTimePicker2.Value.Year - dateTimePicker3.Value.Year;
                        int day = dateTimePicker2.Value.Day - dateTimePicker3.Value.Day;
                        int add = year * 12;
                        date = date + add + day;
                        Dateloanmonths.Text = date.ToString();

                        if (dateTimePicker2.Value.AddMonths(date) > dateTimePicker3.Value)
                        {
                            
                            MessageBox.Show("+1");
                            double date2 = date;
                            double amountpay, amountforthisaccount, penalty, interest, monthlypayment, penaltyformula, penaltyinterestformula, penaltyresult, amountforthisaccountformula, balance, change, montlypaymentformula;
                            amountpay = Convert.ToDouble(textBox2.Text);
                            amountforthisaccount = Convert.ToDouble(Amountforthisaccount.Text);
                            interest = Convert.ToDouble(label42.Text);
                            penalty = Convert.ToDouble(Penalty.Text);
                            monthlypayment = Convert.ToDouble(Montlypayment.Text);
                            penaltyformula = interest * 0.02;
                            //penaltyinterestformula = date * -1;
                            penaltyresult = penaltyformula * date2;
                            Penalty.Text = penaltyresult.ToString();
                            montlypaymentformula = monthlypayment + penaltyresult;
                            Montlypayment.Text = montlypaymentformula.ToString();
                            amountforthisaccountformula = amountforthisaccount + penaltyresult;
                            Amountforthisaccount.Text = amountforthisaccountformula.ToString();
                            balance = amountforthisaccountformula - montlypaymentformula;
                            change = montlypaymentformula - amountpay;
                            label38.Text = change.ToString();
                            label34.Text = balance.ToString();
                            Calculatepayment.Enabled = false;
                        }
                        else
                        {
                            if (Convert.ToDouble(textBox2.Text) > Convert.ToDouble(Amountforthisaccount.Text))
                            {
                                double a, b, c, d, f, g, h;
                                a = Convert.ToDouble(textBox2.Text);
                                b = Convert.ToDouble(Amountforthisaccount.Text);
                                f = Convert.ToDouble(Montlypayment.Text);
                                d = b - a;
                                label38.Text = d.ToString();
                                g = 0;
                                label34.Text = g.ToString();
                            }
                            else
                            {
                                double a, b, c, d, f, g, h;
                                a = Convert.ToDouble(textBox2.Text);
                                b = Convert.ToDouble(Amountforthisaccount.Text);
                                f = Convert.ToDouble(Montlypayment.Text);
                                d = 0;
                                label38.Text = d.ToString();
                                g = b - a;
                                label34.Text = g.ToString();
                            }
                        }
                }
                if (result == DialogResult.No)
                {
                    int date = dateTimePicker2.Value.Month - dateTimePicker3.Value.Month;
                    int year = dateTimePicker2.Value.Year - dateTimePicker3.Value.Year;
                    int day = dateTimePicker2.Value.Day - dateTimePicker3.Value.Day;
                    int add = year * 12;
                    date = date + add + day;
                    Dateloanmonths.Text = date.ToString();

                    if (dateTimePicker2.Value.AddMonths(date) > dateTimePicker3.Value)
                    {
                        MessageBox.Show("+1");
                        if (Convert.ToDouble(textBox2.Text) > Convert.ToDouble(Amountforthisaccount.Text))
                        {
                            double date2 = date;
                            double amountpay, amountforthisaccount, penalty, interest, monthlypayment, penaltyformula, penaltyinterestformula, penaltyresult, amountforthisaccountformula, balance, change, montlypaymentformula;
                            amountpay = Convert.ToDouble(textBox2.Text);
                            amountforthisaccount = Convert.ToDouble(Amountforthisaccount.Text);
                            interest = Convert.ToDouble(label42.Text);
                            penalty = Convert.ToDouble(Penalty.Text);
                            monthlypayment = Convert.ToDouble(Montlypayment.Text);
                            penaltyformula = interest * 0.02;
                            //penaltyinterestformula = date * -1;
                            penaltyresult = penaltyformula * date2;
                            Penalty.Text = penaltyresult.ToString();
                            montlypaymentformula = monthlypayment + penaltyresult;
                            Montlypayment.Text = montlypaymentformula.ToString();
                            amountforthisaccountformula = amountforthisaccount + penaltyresult;
                            Amountforthisaccount.Text = amountforthisaccountformula.ToString();
                            balance = 0;
                            change = amountforthisaccountformula - amountpay;
                            label34.Text = change.ToString();
                            label38.Text = balance.ToString();
                            Calculatepayment.Enabled = false;
                        }
                        else
                        {
                            double date2 = date;
                            double amountpay, amountforthisaccount, penalty, interest, monthlypayment, penaltyformula, penaltyinterestformula, penaltyresult, amountforthisaccountformula, balance, change, montlypaymentformula;
                            amountpay = Convert.ToDouble(textBox2.Text);
                            amountforthisaccount = Convert.ToDouble(Amountforthisaccount.Text);
                            interest = Convert.ToDouble(label42.Text);
                            penalty = Convert.ToDouble(Penalty.Text);
                            monthlypayment = Convert.ToDouble(Montlypayment.Text);
                            penaltyformula = interest * 0.02;
                            //penaltyinterestformula = date * -1;
                            penaltyresult = penaltyformula * date2;
                            Penalty.Text = penaltyresult.ToString();
                            montlypaymentformula = monthlypayment + penaltyresult;
                            Montlypayment.Text = montlypaymentformula.ToString();
                            amountforthisaccountformula = amountforthisaccount + penaltyresult;
                            Amountforthisaccount.Text = amountforthisaccountformula.ToString();
                            balance = amountforthisaccountformula - amountpay;
                            change = 0;
                            label38.Text = change.ToString();
                            label34.Text = balance.ToString();
                            Calculatepayment.Enabled = false;
                        }
                    }
                    else
                    {
                        if (Convert.ToDouble(textBox2.Text) > Convert.ToDouble(Amountforthisaccount.Text))
                        {
                            double a, b, c, d, f, g, h;
                            a = Convert.ToDouble(textBox2.Text);
                            b = Convert.ToDouble(Amountforthisaccount.Text);
                            f = Convert.ToDouble(Montlypayment.Text);
                            d = b-a;
                            label38.Text = d.ToString();
                            g = 0;
                            label34.Text = g.ToString();
                        }
                        else
                        {
                            double a, b, c, d, f, g, h;
                            a = Convert.ToDouble(textBox2.Text);
                            b = Convert.ToDouble(Amountforthisaccount.Text);
                            f = Convert.ToDouble(Montlypayment.Text);
                            d = 0;
                            label38.Text = d.ToString();
                            g = b - a;
                            label34.Text = g.ToString();
                        }
                    }
                }
            }
        }

        private void label40_TextChanged(object sender, EventArgs e)
        {
            conn = Class1.Connectdb1();
            cmd = new SqlCommand("Select*from Loaninfo where LonID='" + label40.Text + "'", conn);
            dr = cmd.ExecuteReader();
            if (dr.Read())
            {
                BID.Text = dr["LonID"].ToString();
                BLastName.Text = dr["Lastnim"].ToString();
                B1stName.Text = dr["Firstnim"].ToString();
            }
            conn = Class1.Connectdb1();
            cmd = new SqlCommand("Select*from Loaninfo where LonID='" + BID.Text + "'", conn);
            dr = cmd.ExecuteReader();
            if (dr.Read())
            {
                MessageBox.Show("This user has a pending loan");
                button6.Enabled = false;
                LoanAmount.Text = "0";
                BID.Enabled = false;
                Calculate.Enabled = false;
                ClearID.Enabled = false;
                Montlypayment.Text = dr["Mnthly"].ToString();
                textBox1.Text = dr["Amntinwords"].ToString();
                label34.Text = dr["Blance"].ToString();
                dateTimePicker3.Text = dr["Neardate"].ToString();
                dr.Close();
            }
            else
            {
                button6.Enabled = true;
                SearchID.Enabled = false;
                BID.Enabled = false;
                dr.Close();
            }
            conn = Class1.Connectdb1();
            cmd = new SqlCommand("Select * from BrrwersPInfo where ApplcntID = '" + label40.Text + "'", conn);
            dr = cmd.ExecuteReader();
            if (dr.Read())
            {
                BorrowersID.Text = dr.GetString(0);
                LastName.Text = dr.GetString(1);
                FirstName.Text = dr.GetString(2);
                MiddleInitial.Text = dr.GetString(3);
                BirthDate.Text = dr.GetDateTime(4).ToShortDateString();
                Age.Text = dr.GetString(5);
                PlaceofBirth.Text = dr.GetString(6);
                CivilStatus.Text = dr.GetString(7);
                Gender.Text = dr.GetString(8);
                BorrowerStatus.Text = dr.GetString(9);
                Address.Text = dr.GetString(10);
                City.Text = dr.GetString(11);
                Province.Text = dr.GetString(12);
                Zipcode.Text = dr.GetString(13);
                Email.Text = dr.GetString(14);
                Landline.Text = dr.GetString(15);
                ContactNo.Text = dr.GetString(16);
                Occupation.Text = dr.GetString(17);
                BorrowersID.Enabled = false;
                BID.Text = dr.GetString(0);
                BID.Enabled = false;
                SearchID.Enabled = false;
                button2.Enabled = false;
                button3.Enabled = true;
                button5.Enabled = false;
                byte[] up = (byte[])(dr[18]);
                if (up == null)
                {
                    pictureBox1.Image = null;
                }
                else
                {
                    MemoryStream ms = new MemoryStream(up);
                    pictureBox1.Image = Image.FromStream(ms);
                }
            }
        }

        private void refresh2()
        {
            conn = Class1.Connectdb1();
            cmd = new SqlCommand("Select TOP(1) Transactno from Loaninfo ORDER BY 1 DESC", conn);
            dr = cmd.ExecuteReader();
            if (dr.Read())
            {
                TransactionID.Text = dr["Transactno"].ToString();
                int a;
                a = Convert.ToInt16(TransactionID.Text);
                a++;
                TransactionID.Text = a.ToString();
                dr.Close();
                conn.Close();
            }
            else
            {
                TransactionID.Text = "1";
            }     
        }

        private void button8_Click(object sender, EventArgs e)
        {
            panel2.Show();
        }

        //private void label38_TextChanged(object sender, EventArgs e)
        //{
        //    double amount1 = Convert.ToDouble(label38.Text);
        //    double amount2 = Convert.ToDouble(label34.Text);
        //    double balance;
        //    balance = amount1 - amount2;
        //    label34.Text = balance.ToString();
        //}
        private void paymentclear()
        {
            textBox2.Text = "";
            Montlypayment.Text = "0";
            Penalty.Text = "0";
            label42.Text = "0";
            Amountforthisaccount.Text = "0";
            dateTimePicker3.ResetText();
            label38.Text = "0";
            label34.Text = "0";
            dateTimePicker4.ResetText();
        }

        private void button9_Click(object sender, EventArgs e)
        {
            panel1.Hide();
        }

        private void pay()
        {

            panel1.Show();
            ReportDocument crypt = new ReportDocument();
            DataSet ds;
            cmd = new SqlCommand("select * from LoanPaid where trasnactno='" + TransactionID.Text + "'", conn);
            da = new SqlDataAdapter(cmd);
            ds = new DataSet();
            da.Fill(ds, "LoanPaid");
            crypt.Load(@"C:\Users\asus\Desktop\Adam\Important Files\CS Sinsuat\Exer1\Exer1\CrystalReport1.rpt");
            crypt.SetDataSource(ds);
            crystalReportViewer1.ReportSource = crypt;
        }

        private void textBox3_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                if (txtpassword.Text == label45.Text)
                {
                    panel2.Hide();
                    conn = Class1.Connectdb1();
                    cmd = new SqlCommand("Select*from LoanPaid where LoanID='" + BID.Text + "' and Trasnactno='" + TransactionID.Text + "'", conn);
                    dr = cmd.ExecuteReader();
                    if (dr.Read())
                    {
                        MessageBox.Show("This user has already paid this");
                        dr.Close();
                    }
                    else
                    {
                        conn = Class1.Connectdb1();
                        cmd = new SqlCommand("Insert into LoanPaid(LoanID, Trasnactno, Pyment, Dtofpayment, Interest, Penalty, Dudate, Nirdate, Change, Balance, Amntforthisacc, Furstname, Secundname) values(@LoanID, @Trasnactno, @Pyment, @Dtofpayment, @Interest, @Penalty, @Dudate, @Nirdate, @Change, @Balance, @Amntforthisacc, @Furstname, @Secundname)", conn);
                        cmd.Parameters.AddWithValue("@LoanID", BID.Text);
                        cmd.Parameters.AddWithValue("@Trasnactno", TransactionID.Text);
                        cmd.Parameters.AddWithValue("@Pyment", textBox2.Text);
                        cmd.Parameters.AddWithValue("@Dtofpayment", Convert.ToDateTime(dateTimePicker1.Value).ToShortDateString());
                        cmd.Parameters.AddWithValue("@Interest", label42.Text);
                        cmd.Parameters.AddWithValue("@Penalty", Penalty.Text);
                        cmd.Parameters.AddWithValue("@Dudate", Convert.ToDateTime(dateTimePicker4.Value).ToShortDateString());
                        cmd.Parameters.AddWithValue("@Nirdate", Convert.ToDateTime(dateTimePicker3.Value).ToShortDateString());
                        cmd.Parameters.AddWithValue("@Change", label38.Text);
                        cmd.Parameters.AddWithValue("@Balance", Convert.ToDouble(label34.Text)).ToString();
                        cmd.Parameters.AddWithValue("@Amntforthisacc", Amountforthisaccount.Text);
                        cmd.Parameters.AddWithValue("@Furstname", B1stName.Text);
                        cmd.Parameters.AddWithValue("@Secundname", BLastName.Text);
                        cmd.ExecuteNonQuery();
                        conn.Close();
                        conn = Class1.Connectdb1();
                        cmd = new SqlCommand("Select Min(Neardate) from Loaninfo where LonID='"+BID.Text+"'", conn);
                        dr = cmd.ExecuteReader();
                        if (dr.Read())
                        {
                            double payment, monthly, result;
                            payment = Convert.ToDouble(textBox2.Text);
                            monthly = Convert.ToDouble(Montlypayment.Text);
                            result = monthly - payment;
                            label48.Text = result.ToString();
                            conn = Class1.Connectdb1();
                            cmd = new SqlCommand("Update Loaninfo set Mnthly=@Mnthly+Mnthly", conn);
                            cmd.Parameters.AddWithValue("@Mnthly", label48.Text);
                            cmd.ExecuteNonQuery();
                            cmd = new SqlCommand("Update Loaninfo set Blance=@Blance, Pnalty=@Pnalty", conn);
                            cmd.Parameters.AddWithValue("@Blance", label34.Text);
                            cmd.Parameters.AddWithValue("@Pnalty", Penalty.Text);
                            cmd.ExecuteNonQuery();
                            if (Convert.ToDouble(label34.Text) <= 0)
                            {
                                cmd = new SqlCommand("Delete from Loaninfo where LonID='" + BID.Text + "'", conn);
                                cmd.ExecuteNonQuery();
                            }
                            else
                            {
                                cmd = new SqlCommand("Delete from Loaninfo where Transactno='" + TransactionID.Text + "'", conn);
                                cmd.ExecuteNonQuery();
                                MessageBox.Show("Payment successful");
                                conn.Close();
                                panel2.Hide();
                                loandata();
                            }
                        }
                    }
                }
                else
                {
                    MessageBox.Show("Incorrect password");
                }
            }
            pay();
            paymentclear();
            clearloan();
        }

        private void button9_Click_1(object sender, EventArgs e)
        {
            panel1.Hide();
        }

        private void Loansearch_Click(object sender, EventArgs e)
        {
            conn = Class1.Connectdb1();
            cmd = new SqlCommand("Select sum (Mnthly) as sum1 from Loaninfo where Transactno>='" + textBox3.Text + "' and Transactno<='" + textBox4.Text + "'", conn);
            dr = cmd.ExecuteReader();
            if (dr.Read())
            {
                textBox6.Text = dr["sum1"].ToString();
                dr.Close();
            }
            else
            {
                MessageBox.Show("Transaction number invalid");
            }
        }

        private void textBox7_TextChanged(object sender, EventArgs e)
        {
            //double a, b, c;
            //a = Convert.ToDouble(textBox5.Text);
            //b = Convert.ToDouble(textBox6.Text);
            //c = b - a;
            //textBox7.Text = c.ToString();
        }

        private void Advancepay_Click(object sender, EventArgs e)
        {
            double a, b, c;
            a = Convert.ToDouble(textBox5.Text);
            b = Convert.ToDouble(textBox6.Text);
            c = b - a;
            textBox7.Text = c.ToString();
            int d, f;
            d = Convert.ToInt16(textBox3.Text);
            f = Convert.ToInt16(textBox4.Text);
            for (int i = d; i <= f; i++)
            {
                conn = Class1.Connectdb1();
                cmd = new SqlCommand("Update Loaninfo set Mnthly=@Mnthly where Transactno='"+  i + "'", conn);
                cmd.Parameters.AddWithValue("@Mnthly", c.ToString());
                cmd.ExecuteNonQuery();
                cmd = new SqlCommand("Update Loaninfo set Blance=Blance-@Blance", conn);
                cmd.Parameters.AddWithValue("@Blance", textBox5.Text);
                cmd.ExecuteNonQuery();
                conn.Close();
            }
            MessageBox.Show("Payment successful");
        }
    }
}
