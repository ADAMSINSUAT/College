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
    public partial class UpdateFoodMenu : Form
    {
        SqlConnection conn;
        SqlDataReader dr;
        SqlCommand cmd;
        decimal tax, vatprice, price, srp;

        public UpdateFoodMenu()
        {
            InitializeComponent();
        }

        private void UpdateFoodMenu_Load(object sender, EventArgs e)
        {
            Database();
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
            if (txtPrice.Text != "")
            {
                price = Convert.ToDecimal(txtPrice.Text);
            }
            vatprice = tax * price;
            srp = vatprice + price;
            lblVaT.Text = Convert.ToString(Math.Round(vatprice, 2));
            srp = Math.Round(srp, 1, MidpointRounding.ToEven);
            lblPricewithVAT.Text = Convert.ToString(Math.Round(srp, MidpointRounding.ToEven));
            dr.Close();
            conn.Close();
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
        }
        private void clear()
        {
            txtFoodID.Text = "";
            txtFoodName.Text = "";
            txtPrice.Text = "";
            lblVaT.Text = "0.00";
            lblPricewithVAT.Text = "0.00";
            cmbCategory.Text = "";
            toggleAvailability.Checked = false;
            pictureBox1.Image = Android_Menu_Selection_System__Admin_.Properties.Resources.Foodmenu_placeholder;
            txtpicpath.Text = "";
        }
        private void Database()
        {
            conn = ConnectDB.ConnectDatabase();
        }

        private void btnUpdate_Click(object sender, EventArgs e)
        {
            
        }

        private void txtPrice_TextChanged(object sender, EventArgs e)
        {
            GetTax();
        }

        private void btnExit_Click(object sender, EventArgs e)
        {
            clear();
            (System.Windows.Forms.Application.OpenForms["Main_Menu_Form"] as Main_Menu_Form).panelFormLoader.Controls.Clear();
            ManageFoodMenu mfm = new ManageFoodMenu() { Dock = DockStyle.Fill, TopLevel = false, TopMost = true };
            mfm.FormBorderStyle = FormBorderStyle.None;
            (System.Windows.Forms.Application.OpenForms["Main_Menu_Form"] as Main_Menu_Form).panelFormLoader.Controls.Add(mfm);
            (System.Windows.Forms.Application.OpenForms["Main_Menu_Form"] as Main_Menu_Form).lblTitle.Text = "Manage Food Menu";
            mfm.flowlayoutpanel();
            mfm.toolstripbuttons();
            //this.panelFormLoader.Controls.Add(mfm);
            (System.Windows.Forms.Application.OpenForms["Main_Menu_Form"] as Main_Menu_Form).panelFormLoader.Size = mfm.Size;
            mfm.Show();
            //this.Close();
        }

        private void btnAddphoto_Click(object sender, EventArgs e)
        {
            OFD.FileName = "";
            OFD.Title = "Select Picture...";
            OFD.Filter = "jpeg|*.jpg|bmp|*.bmp|png|*.png|all files|*.*";
            OFD.FilterIndex = 1;

            OFD.ShowDialog();
            txtpicpath.Text = OFD.FileName;
            pictureBox1.ImageLocation = txtpicpath.Text;
        }

        private void CheckControlFields()
        {
            if (txtFoodID.Text == "")
            {
                MessageBox.Show("Food ID field cannot be empty");
                txtFoodID.Focus();
            }
            else if (txtFoodName.Text == "")
            {
                MessageBox.Show("Food Name field cannot be empty");
            }
            else if (Convert.ToDouble(txtPrice.Text) <= 0)
            {
                MessageBox.Show("Food Price field cannot be empty");
                txtPrice.Focus();
            }
            else if (cmbCategory.Text == "")
            {
                MessageBox.Show("Food Category field cannot be empty");
                cmbCategory.Focus();
            }
            else if (txtpicpath.Text == "")
            {
                MessageBox.Show("Food Picture field cannot be empty");
            }
            else if (lblVaT.Text == "0.00")
            {
                MessageBox.Show("Food vat field cannot be empty");
            }
            else if (dTPTimeToCook.Text == "")
            {
                MessageBox.Show("Food time to cook field cannot be empty");
            }
            else
            {
                checkToggleSwitch();
            }
        }
        private void checkToggleSwitch() //Checks if the toggleswitch is turned on 
        {
            if (toggleAvailability.Checked == true)
            {
                //parameterAvailability = cmd.Parameters.AddWithValue("@FoodAvailability", toggleAvailability.OnText);
                UpdateFood();
                //MessageBox.Show("Working");
            }
            else
            {
                UpdateFood();
                //MessageBox.Show("Error");
            }
        }

        private void UpdateFood()
        {
            MemoryStream ms = new MemoryStream();
            pictureBox1.Image.Save(ms, ImageFormat.Jpeg);
            byte[] pic_array = new byte[ms.Length];
            ms.Position = 0;
            ms.Read(pic_array, 0, pic_array.Length);
            Database();
            cmd = new SqlCommand("Update FoodMenu set FoodID=@FoodID, FoodName=@FoodName, FoodPrice=@FoodPrice, FoodVAT=@FoodVAT, FoodSRP=@FoodSRP, FoodCategory=@FoodCategory, FoodAvailability=@FoodAvailability, FoodPic=@FoodPic, TimetoCook=@TimetoCook where FoodID='" + txtFoodID.Text + "' or FoodName='" + txtFoodName.Text + "'", conn);
            cmd.Parameters.AddWithValue("@FoodID", txtFoodID.Text);
            cmd.Parameters.AddWithValue("@FoodName", txtFoodName.Text);
            cmd.Parameters.AddWithValue("@FoodPrice", txtPrice.Text);
            cmd.Parameters.AddWithValue("@FoodVAT", lblVaT.Text);
            cmd.Parameters.AddWithValue("@FoodSRP", lblPricewithVAT.Text);
            cmd.Parameters.AddWithValue("@FoodCategory", cmbCategory.Text);
            if (toggleAvailability.Checked == true)
            {
                cmd.Parameters.AddWithValue("@FoodAvailability", toggleAvailability.OnText);
            }
            else
            {
                cmd.Parameters.AddWithValue("@FoodAvailability", toggleAvailability.OffText);
            }
            cmd.Parameters.AddWithValue("@FoodPic", pic_array);
            cmd.Parameters.AddWithValue("@TimetoCook", dTPTimeToCook.Value);
            nudformat();
            cmd.ExecuteNonQuery();
            conn.Close();
            if (System.Windows.Forms.Application.OpenForms["ManageFoodMenu"] != null)
            {
                (System.Windows.Forms.Application.OpenForms["ManageFoodMenu"] as ManageFoodMenu).flowlayoutpanel();
            }
            (System.Windows.Forms.Application.OpenForms["Main_Menu_Form"] as Main_Menu_Form).panelFormLoader.Controls.Clear();
            ManageFoodMenu mfm = new ManageFoodMenu() { Dock = DockStyle.Fill, TopLevel = false, TopMost = true };
            mfm.FormBorderStyle = FormBorderStyle.None;
            (System.Windows.Forms.Application.OpenForms["Main_Menu_Form"] as Main_Menu_Form).panelFormLoader.Controls.Add(mfm);
            (System.Windows.Forms.Application.OpenForms["Main_Menu_Form"] as Main_Menu_Form).lblTitle.Text = "Manage Food Menu";
            mfm.flowlayoutpanel();
            mfm.toolstripbuttons();
            //this.panelFormLoader.Controls.Add(mfm);
            (System.Windows.Forms.Application.OpenForms["Main_Menu_Form"] as Main_Menu_Form).panelFormLoader.Size = mfm.Size;
            mfm.Show();
            MessageBox.Show("Food is updated");
            clear();
            this.Close();
        }
        //End of Methods List
    }
}
