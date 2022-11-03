using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.SqlClient;
using System.IO;
using System.Drawing.Imaging;

namespace Android_Menu_Selection_System__Admin_
{
    public partial class ManageCashier : Form
    {
        SqlConnection conn;
        SqlCommand cmd;
        SqlDataAdapter da;
        SqlDataReader dr;

        public ManageCashier()
        {
            InitializeComponent();
        }

        private void AddUserForm_Load(object sender, EventArgs e)
        {
            Database();
            CashierIDGenerator();
            AutoCompleSearch();
        }

        private void Database() //Code for connecting to the database
        {
            conn = ConnectDB.ConnectDatabase();
        }

        private void CheckItemsForSaveData() //Checks if the inputted data is correct or not empty/null
        {
            if ((CashierID.Text == "0" || CashierID.Text == ""))
            {
                MessageBox.Show("Cashier ID is invalid", "Error");
            }
            else if (txtUsername.Text == "")
            {
                MessageBox.Show("Username field should not be empty");
            }
            else if (txtPassword.Text == "")
            {
                MessageBox.Show("Password field should not be empty");
            }
            else if (txtRePassword.Text != txtPassword.Text)
            {
                MessageBox.Show("The password does not match");
            }
            else if (picPath.Text == "")
            {
                MessageBox.Show("Picture is empty");
            }
            else
            {
                SaveUser();
            }
        }
        private void CheckItemsForUpdateData() //Checks if the data to be updated is empty or not
        {
            if ((CashierID.Text == "0" || CashierID.Text == ""))
            {
                MessageBox.Show("Cashier ID is invalid", "Error");
            }
            else if (txtUsername.Text == "")
            {
                MessageBox.Show("Username field should not be empty");
            }
            else if (txtPassword.Text == "")
            {
                MessageBox.Show("Password field should not be empty");
            }
            else if (txtRePassword.Text != txtPassword.Text)
            {
                MessageBox.Show("The password does not match");
            }
            else if (picPath.Text == "")
            {
                MessageBox.Show("Picture field is empty");
            }
            else
            {
                UpdateCashierData();
            }
        }
        private void SaveUser() //Code for saving the inputted data
        {
            try
            {
                conn.Close();
                this.Database();
                cmd = new SqlCommand("Select * from CashierLoginCredentials where Username='" + txtUsername.Text + "' OR Password='" + txtPassword.Text + "'", conn);
                dr = cmd.ExecuteReader();
                if (dr.Read().Equals(txtUsername.Text))
                {
                    MessageBox.Show("Username is already taken");
                }
                else if (dr.Read().Equals(txtPassword.Text))
                {
                    MessageBox.Show("Password is already taken");
                }
                else
                {
                    conn.Close();
                    this.Database();
                    MemoryStream ms = new MemoryStream();
                    pictureBox1.Image.Save(ms, ImageFormat.Jpeg);
                    byte[] pic_arr = new byte[ms.Length];
                    ms.Position = 0;
                    ms.Read(pic_arr, 0, pic_arr.Length);
                    cmd = new SqlCommand("Insert into CashierInfoList(CashierID, Userpic)VALUES(@CashierID, @Userpic)", conn);
                    cmd.Parameters.AddWithValue("@CashierID", CashierID.Text);
                    cmd.Parameters.AddWithValue("@Userpic", pic_arr);
                    cmd.ExecuteNonQuery();
                    MessageBox.Show("Cashier is now registered");
                    conn.Close();
                    SaveIDIntoCashierIDList();
                    SaveIntoCashierLoginCredentials();
                    SaveIntoUserList();
                    CashierIDGenerator();
                    clear();
                }
            }
            catch (Exception e)
            {
                MessageBox.Show(e.ToString());
            }
        }
        //Add ID into the CashierIDList
        private void SaveIDIntoCashierIDList()
        {
            try
            {
                this.Database();
                cmd = new SqlCommand("Insert into CashierIDList(CashierID)VALUES(@CashierID)", conn);
                cmd.Parameters.AddWithValue("@CashierID", Convert.ToInt32(CashierID.Text));
                cmd.ExecuteNonQuery();
                //MessageBox.Show("CashierID is now registered");
                conn.Close();
            }
            catch (Exception e)
            {
                MessageBox.Show(e.ToString());
            }
        }
        //Save Cashier credentials into the CashierLoginCredentials
        private void SaveIntoCashierLoginCredentials()
        {
            string Status = "Active";
            string Role = "Cashier";
            this.Database();
            cmd = new SqlCommand("Insert into CashierLoginCredentials(EmpID, Username, Password, Status, Role)VALUES(@EmpID, @Username, @Password, @Status, @Role)", conn);
            cmd.Parameters.AddWithValue("@EmpID", CashierID.Text);
            cmd.Parameters.AddWithValue("@Username", txtUsername.Text);
            cmd.Parameters.AddWithValue("@Password", txtPassword.Text);
            cmd.Parameters.AddWithValue("@Status", Status);
            cmd.Parameters.AddWithValue("@Role", Role);
            cmd.ExecuteNonQuery();
            conn.Close();
        }

        private void SaveIntoUserList()
        {
            string Status = "Active";
            string Role = "Cashier";
            this.Database();
            cmd = new SqlCommand("Insert into UserList(UserID, UserName, UserPassword, UserStatus, UserRole)VALUES(@UserID, @UserName, @UserPassword, @UserStatus, @UserRole)", conn);
            cmd.Parameters.AddWithValue("@UserID", CashierID.Text);
            cmd.Parameters.AddWithValue("@UserName", txtUsername.Text);
            cmd.Parameters.AddWithValue("@Password", txtPassword.Text);
            cmd.Parameters.AddWithValue("@UserStatus", Status);
            cmd.Parameters.AddWithValue("@UserRole", Role);
            cmd.ExecuteNonQuery();
            conn.Close();
        }

        //Used for clearing the Form
        private void clear()
        {
            txtUsername.Text = "";
            txtPassword.Text = "";
            txtRePassword.Text = "";
            picPath.Text = "";
            pictureBox1.Image = Android_Menu_Selection_System__Admin_.Properties.Resources.Userpic;
        }

        private void btnAddPhoto_Click(object sender, EventArgs e) //Code for opening and selecting the photo for
        {                                                          //the pictureBox
            OFD.FileName = "";
            OFD.Title = "Select picture...";
            OFD.Filter = "jpeg|*.jpg|bmp|*.bmp|png|*.png|all files|*.*";
            OFD.FilterIndex = 1;

            OFD.ShowDialog();
            picPath.Text = OFD.FileName;
            pictureBox1.ImageLocation = picPath.Text;
        }
        
        //Add +1 to the ID to create a new one
        private void CashierIDGenerator()
        {
            Database();
            cmd = new SqlCommand("Select DefaultID from DfltCashierID", conn);
            dr = cmd.ExecuteReader();
            dr.Read();
            int ID = Convert.ToInt32(dr["DefaultID"].ToString());
            conn.Close();

            //Check if the ID number exists within the database
            for (int i = ID; i <= ID; i++)
            {
                Database();
                cmd = new SqlCommand("Select * from CashierIDList where CashierID ='" + i + "'", conn);
                dr = cmd.ExecuteReader();
            }
            if (dr.Read())
            {
                CashierID.Text = Convert.ToString(Convert.ToInt32(dr[0].ToString()) + 1);
            }
            else
            {
                CashierID.Text = Convert.ToString(ID);
            }
            dr.Close();
            conn.Close();
        }
        //Saves the data from the form
        private void btnSave_Click(object sender, EventArgs e)
        {
            CheckItemsForSaveData();
        }
        
        //To update the CashierInfo w/ Username and Password or w/o
        private void UpdateCashierData()
        {
            this.Database();//Update for Cashier Info
            MemoryStream ms = new MemoryStream();
            pictureBox1.Image.Save(ms, ImageFormat.Jpeg);
            byte[] pic_arr = new byte[ms.Length];
            ms.Position = 0;
            ms.Read(pic_arr, 0, pic_arr.Length);
            cmd = new SqlCommand("Update CashierInfoList set Userpic = @Userpic where CashierID ='" + CashierID.Text + "'", conn);
            cmd.Parameters.AddWithValue("@Userpic", pic_arr);
            cmd.ExecuteNonQuery();
            conn.Close();

            this.Database();//Update for  CashierLoginCredentials
            cmd = new SqlCommand("Update CashierLoginCredentials set Username = @Username, Password = @Password where EmpID = '" + CashierID.Text + "'", conn);
            cmd.Parameters.AddWithValue("@Username", txtUsername.Text);
            cmd.Parameters.AddWithValue("@Password", txtPassword.Text);
            cmd.ExecuteNonQuery();
            conn.Close();

            this.Database();//Update for UserList
            cmd = new SqlCommand("Update UserList set UserName = @UserName, UserPassword = @UserPassword where UserID = '" + CashierID.Text + "'", conn);
            cmd.Parameters.AddWithValue("@UserName", txtUsername.Text);
            cmd.Parameters.AddWithValue("@UserPassword", txtPassword.Text);
            cmd.ExecuteNonQuery();
            conn.Close();
            MessageBox.Show("Cashier info is updated!");
            clear();
        }

        private void SearchID()
        {
            txtUsername.Enabled = true;
            txtPassword.Enabled = true;
            txtRePassword.Enabled = true;
            this.Database();
            cmd = new SqlCommand("Select CashierLoginCredentials.EmpID, CashierInfoList.Userpic, CashierLoginCredentials.Username, CashierLoginCredentials.Password  from CashierInfoList INNER JOIN CashierLoginCredentials ON CashierInfoList.CashierID=CashierLoginCredentials.EmpID where EmpID ='" + txtSearchCashier.Text + "'", conn);
            dr = cmd.ExecuteReader();
            if (dr.Read())
            {
                CashierID.Text = dr[0].ToString();
                byte[] photo_array = (byte[])dr[1];
                MemoryStream ms = new MemoryStream(photo_array);
                ms.Position = 0;
                ms.Read(photo_array, 0, photo_array.Length);
                pictureBox1.Image = Image.FromStream(ms);
                txtUsername.Text = dr[2].ToString();
                txtPassword.Text = dr[3].ToString();
            }
            else
            {
                MessageBox.Show("User does not exist!");
            }
        }

        private void btnClear_Click(object sender, EventArgs e)
        {
            clear();
            CashierIDGenerator();
            MessageBox.Show("All items cleared");
        }

        private void btnUpdate_Click(object sender, EventArgs e)
        {
            CheckItemsForUpdateData();
        }

        private void btnSearch_Click(object sender, EventArgs e)
        {
            panel1.Show();
            txtSearchCashier.Focus();
        }

        private void btnCancelSearch_Click(object sender, EventArgs e)
        {
            txtSearchCashier.Text = "";
            panel1.Visible = false;
        }

        private void btnSearchUser_Click(object sender, EventArgs e)
        {
            SearchID();
        }

        void AutoCompleSearch()
        {
            this.Database();
            cmd = new SqlCommand("Select EmpID from CashierLoginCredentials where EmpID LIKE @EmpID", conn);
            cmd.Parameters.Add(new SqlParameter("@EmpID", "%" + txtSearchCashier.Text + "%"));
            //cmd = new SqlCommand("Select productname from POS where productname LIKE @productname", conn);


            cmd.ExecuteNonQuery();
            dr = cmd.ExecuteReader();

            AutoCompleteStringCollection coll = new AutoCompleteStringCollection();

            while (dr.Read())
            {
                coll.Add(dr.GetValue(0).ToString());
                coll.Add(dr.GetValue(1).ToString());
            }
            txtSearchCashier.AutoCompleteCustomSource = coll;
            dr.Close();
        }

        private void btnExit_Click(object sender, EventArgs e)
        {
            btnExit.Visible = false;
            this.Width = 764;
            this.Close();
        }
    }
}
