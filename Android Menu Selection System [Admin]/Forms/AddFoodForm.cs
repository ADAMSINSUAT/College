using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;
using System.Drawing.Imaging;
using System.Data.SqlClient;
using System.Globalization;

namespace Android_Menu_Selection_System__Admin_
{
    public partial class AddFoodForm : Form
    {
        SqlConnection conn;
        SqlCommand cmd;
        SqlDataReader dr;
        SqlDataAdapter da;
        SqlParameter parameterID, parameterName, parameterPrice, parameterVAT, parameterCategory, parameterAvailability, parameterPicture, parameterTimeToCook;
        decimal tax, price, vatprice, srp;
        public AddFoodForm()
        {
            InitializeComponent();
        }

        private void Manage_Food_Menu_Load(object sender, EventArgs e)
        {
            GetTax();
            clear();
            nudformat();
        }


        //Methods list
        private void GetTax()
        {
            Database();
            string Tax = "Select FoodMenuTax from DefaultTaxTable";
            cmd = new SqlCommand(Tax, conn);
            dr = cmd.ExecuteReader();
            if (dr.Read())
            {
                tax = Convert.ToDecimal(dr[0].ToString());
                tax = Math.Round(tax, 2);
            }
            else
            {
                tax = Convert.ToDecimal(0.12);
            }
            if(txtPrice.Text!="")
            {
                price = Convert.ToDecimal(txtPrice.Text);
            }
            vatprice = tax * price;
            srp = vatprice + price;
            lblVaT.Text = Convert.ToString(Math.Round(vatprice, 2));
            srp = Math.Round(srp, MidpointRounding.ToEven);
            lblPricewithVAT.Text = srp.ToString("###,###,##0.00");
            dr.Close();
            conn.Close();
        }
        private void Database()
        {
            conn = ConnectDB.ConnectDatabase();
        }


        public void SaveFood()
        {
            Database();
            string CheckifExist = "Select * from FoodMenu where FoodName='" + txtFoodName.Text + "' AND FoodID='" + txtFoodID.Text + "'";
            cmd = new SqlCommand(CheckifExist, conn);
            dr = cmd.ExecuteReader();
            if(dr.Read())
            {
                MessageBox.Show("Food Name or Food ID already exists!");
            }
            else
            {
                dr.Close();
                String id, name, price, vat, srp, category, availability, pic, timetocok;
                id = "@FoodID";
                name = "@FoodName";
                price = "@FoodPrice";
                vat = "@FoodVAT";
                srp = "@FoodSRP";
                category = "@FoodCategory";
                availability = "@FoodAvailability";
                pic = "@FoodPic";
                timetocok = "@TimetoCook";

                MemoryStream ms = new MemoryStream();
                pictureBox1.Image.Save(ms, ImageFormat.Jpeg);
                byte[] pic_array = new byte[ms.Length];
                ms.Position = 0;
                ms.Read(pic_array, 0, pic_array.Length);
                Database();
                String save = "Insert into FoodMenu(FoodID, FoodName, FoodPrice, FoodVAT, FoodSRP, FoodCategory, FoodAvailability, FoodPic, TimetoCook)VALUES(@FoodID, @FoodName, @FoodPrice, @FoodVAT, @FoodSRP, @FoodCategory, @FoodAvailability, @FoodPic, @TimetoCook)";
                cmd = new SqlCommand(save, conn);
                parameterID = cmd.Parameters.AddWithValue(id, txtFoodID.Text);
                parameterName = cmd.Parameters.AddWithValue(name, txtFoodName.Text);
                parameterPrice = cmd.Parameters.AddWithValue(price, txtPrice.Text);
                parameterCategory = cmd.Parameters.AddWithValue(category, cmbCategory.Text);
                parameterAvailability = cmd.Parameters.AddWithValue(availability, "Available");
                parameterPicture = cmd.Parameters.AddWithValue(pic, pic_array);
                parameterVAT = cmd.Parameters.AddWithValue(vat, lblVaT.Text);
                parameterVAT = cmd.Parameters.AddWithValue(srp, lblPricewithVAT.Text);
                parameterTimeToCook = cmd.Parameters.AddWithValue(timetocok, dTPTimeToCook.Value);
                nudformat();
                cmd.ExecuteNonQuery();
                conn.Close();
                if (System.Windows.Forms.Application.OpenForms["ManageFoodMenu"] as ManageFoodMenu != null)
                {
                    (System.Windows.Forms.Application.OpenForms["ManageFoodMenu"] as ManageFoodMenu).flowlayoutpanel();
                }
                MessageBox.Show("Food item is saved", "Successful");
                clear();
            }
        }

        private void clear()
        {
            txtFoodID.Text = "";
            txtFoodName.Text = "";
            txtPrice.Text = "0.00";
            lblVaT.Text = "0.00";
            lblPricewithVAT.Text = "0.00";
            //GetFoodCategory();
            UpdateFoodCategory();
            cmbCategory.Text = "";
            toggleAvailability.Checked = false;
            txtpicpath.Text = "";
            pictureBox1.Image= Android_Menu_Selection_System__Admin_.Properties.Resources.Foodmenu_placeholder;
            dTPTimeToCook.Text = "00:00:00";
        }

        private void nudformat()
        {
            double price, vat, vatprice;
            price = Convert.ToDouble(txtPrice.Text);
            txtPrice.Text = price.ToString("###,###,##0.00");
            vat = Convert.ToDouble(lblVaT.Text);
            lblVaT.Text = vat.ToString("###,###,##0.00");
            vatprice = Convert.ToDouble(lblPricewithVAT.Text);
            lblPricewithVAT.Text = vatprice.ToString("###,###,##0.00");
            dTPTimeToCook.Format = DateTimePickerFormat.Custom;
            dTPTimeToCook.CustomFormat = "HH:mm:ss";
            //dTPTimeToCook.Value = DateTime.TryParse(txtTimetoCook.Text, "HH:mm:ss", CultureInfo.InvariantCulture, out );
            //TimeSpan timeSpan = TimeSpan.Parse(txtTimetoCook.Text);
        }

        private void CheckControlFields()
        {
            if(txtFoodID.Text =="")
            {
                MessageBox.Show("Food ID field cannot be empty");
                txtFoodID.Focus();
            }
            else if(txtFoodName.Text == "")
            {
                MessageBox.Show("Food Name field cannot be empty");
            }
            else if(Convert.ToDouble(txtPrice.Text)<=0)
            {
                MessageBox.Show("Food Price field cannot be empty");
                txtPrice.Focus();
            }
            else if(cmbCategory.Text =="")
            {
                MessageBox.Show("Food Category field cannot be empty");
                cmbCategory.Focus();
            }
            else if(txtpicpath.Text =="")
            {
                MessageBox.Show("Food Picture field cannot be empty");
            }
            else if(lblVaT.Text == "0.00")
            {
                MessageBox.Show("Food vat field cannot be empty");
            }
            else if (dTPTimeToCook.Text == "")
            {
                MessageBox.Show("Food time to cook field cannot be empty");
            }
            else
            {
                //checkToggleSwitch();
                SaveFood();
            }
        }

        private void txtPrice_TextChanged_1(object sender, EventArgs e)
        {
            GetTax();
        }

        //private void checkToggleSwitch() //Checks if the toggleswitch is turned on 
        //{
        //    if(toggleAvailability.Checked ==true)
        //    {
        //        //parameterAvailability = cmd.Parameters.AddWithValue("@FoodAvailability", toggleAvailability.OnText);
        //        SaveFood();
        //        //MessageBox.Show("Working");
        //    }
        //    else
        //    {
        //        SaveFood();
        //        //MessageBox.Show("Error");
        //    }
        //}

        public void GetFoodCategory()
        {
            Database();
            String select = "Select Category from FoodCategory";
            cmd = new SqlCommand(select, conn);
            dr = cmd.ExecuteReader();
            while(dr.Read())
            {
                cmbCategory.Items.Add(dr["Category"].ToString());
            }
            conn.Close();
        }

        public void UpdateFoodCategory()
        {
            cmbCategory.Items.Clear();
            cmbCategory.Update();
            cmbCategory.Refresh();
            Database();
            String select = "Select Category from FoodCategory";
            cmd = new SqlCommand(select, conn);
            dr = cmd.ExecuteReader();
            while (dr.Read())
            {
                cmbCategory.Items.Add(dr["Category"].ToString());
            }
            conn.Close();
        }

        //End of methods list

        //Form buttons list
        private void btnSave_Click(object sender, EventArgs e)
        {
            CheckControlFields();
        }

        private void btnAddphoto_Click(object sender, EventArgs e)
        {
            OFD.FileName = "";
            OFD.Title = "Select Picture...";
            OFD.Filter = "jpeg|*.jpeg|jpg|*.jpg|bmp|*.bmp|png|*.png|all files|*.*";
            OFD.FilterIndex = 1;

            OFD.ShowDialog();
            txtpicpath.Text = OFD.FileName;
            pictureBox1.ImageLocation = txtpicpath.Text;
        }

        private void txtPrice_TextChanged(object sender, EventArgs e)
        {

        }

        private void txtPrice_Leave(object sender, EventArgs e)
        {
            try
            {
                if (txtPrice.Text == "")
                {
                    txtPrice.Text = "0.00";
                }

                else
                {
                    double a;
                    a = Convert.ToDouble(txtPrice.Text);
                    txtPrice.Text = a.ToString("###,###,##0.00");
                }
            }
            catch
            {
            }
        }

        private void txtPrice_Click(object sender, EventArgs e)
        {
            txtPrice.SelectAll();
        }
        //End of form buttons list
    }
}
