using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Android_Menu_Selection_System__Admin_
{
    public partial class ManageDiscountForm : Form
    {
        SqlConnection conn;
        SqlDataAdapter da;
        SqlCommand cmd;
        SqlDataReader dr;
        public ManageDiscountForm()
        {
            InitializeComponent();
        }

        private void ManageDiscountForm_Load(object sender, EventArgs e)
        {
            clear();
            priceformat();
            refresh();
        }

        //Beginning of methods
        private void refresh()
        {
            Database();
            string getDiscounts = "Select * from DiscountTable";
            da = new SqlDataAdapter(getDiscounts, conn);

            DataTable dt = new DataTable();

            da.Fill(dt);

            dGVDiscount.Rows.Clear();

            foreach (DataRow item in dt.Rows)
            {
                int n = dGVDiscount.Rows.Add();

                dGVDiscount.Rows[n].Cells[0].Value = item["DiscountName"].ToString();
                dGVDiscount.Rows[n].Cells[1].Value = item["DiscountPrice"].ToString();
                dGVDiscount.Rows[n].Cells[2].Value = item["DiscountType"].ToString();
            }
        }
        private void Database()
        {
            conn = ConnectDB.ConnectDatabase();
        }

        private void priceformat()
        {
            double discountprice;
            if(txtDiscountPrice.Text!="")
            {
                discountprice = Convert.ToDouble(txtDiscountPrice.Text);
                txtDiscountPrice.Text = discountprice.ToString("###,###,##0.00");
            }
        }

        private void clear()
        {
            txtDiscountName.Text = "";
            txtDiscountPrice.Text = "";
            cmbDiscountType.Text = "";
        }

        //Ending of methods

        private void btnSaveDiscount_Click(object sender, EventArgs e)
        {
            Database();
            String discountname = txtDiscountName.Text;
            discountname = discountname.Replace("'", @"\");
            String selectifexist = "Select * from DiscountTable where DiscountName LIKE '%"+ discountname +"%'";
            String savediscount = "Insert into DiscountTable (DiscountName, DiscountPrice, DiscountType)VALUES(@DiscountName, @DiscountPrice, @DiscountType)";
            if (txtDiscountName.Text == "" && txtDiscountPrice.Text == "" && cmbDiscountType.Text == "")
            {
                MessageBox.Show("All fields are empty");
            }
            else if (txtDiscountName.Text == "")
            {
                MessageBox.Show("Disount Name field is empty");
                txtDiscountName.Focus();
            }
            else if (txtDiscountPrice.Text == "")
            {
                MessageBox.Show("Discount Price field is empty");
                txtDiscountPrice.Focus();
            }
            else if (cmbDiscountType.Text == "")
            {
                MessageBox.Show("Discount Type field is empty");
                cmbDiscountType.Focus();
            }
            else
            {
                using (cmd = new SqlCommand(selectifexist, conn))
                {
                    dr = cmd.ExecuteReader();
                    if (dr.Read())
                    {
                        MessageBox.Show("A discount with this name already exists!");
                        dr.Close();
                    }
                    else
                    {
                        dr.Close();
                        cmd = new SqlCommand(savediscount, conn);
                        cmd.Parameters.AddWithValue("@DiscountName", txtDiscountName.Text);
                        cmd.Parameters.AddWithValue("@DiscountPrice", txtDiscountPrice.Text);
                        cmd.Parameters.AddWithValue("@DiscountType", cmbDiscountType.Text);
                        priceformat();
                        cmd.ExecuteNonQuery();
                        conn.Close();
                        refresh();
                        clear();
                        MessageBox.Show("Discount is saved!");
                    }
                }
            }
        }

        private void ManageDiscountForm_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            txtDiscountName.Text = dGVDiscount.CurrentRow.Cells[0].Value.ToString();
            txtDiscountPrice.Text = dGVDiscount.CurrentRow.Cells[1].Value.ToString();
            priceformat();
            cmbDiscountType.Text = dGVDiscount.CurrentRow.Cells[2].Value.ToString();
        }

        private void btnClear_Click(object sender, EventArgs e)
        {
            clear();
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            Database();
            String deletediscount = "Delete from DiscountTable where DiscountName=@DiscountName";
            if(txtDiscountName.Text=="")
            {
                MessageBox.Show("Discount Name field is empty, nothing to delete!");
                txtDiscountName.Focus();
            }
            else
            {
                using (cmd = new SqlCommand(deletediscount, conn))
                {
                    cmd.Parameters.AddWithValue("@DiscountName", txtDiscountName.Text);
                    cmd.ExecuteNonQuery();
                    MessageBox.Show("Discount has been deleted");
                    refresh();
                    clear();
                    conn.Close();
                }
            }
        }

        private void btnUpdate_Click(object sender, EventArgs e)
        {
            Database();
            String updatediscount = "Update DiscountTable set DiscountName=@DiscountName, DiscountPrice=@DiscountPrice, DiscountType=@DiscountType";
            if (txtDiscountName.Text == "" && txtDiscountPrice.Text == "" && cmbDiscountType.Text == "")
            {
                MessageBox.Show("All fields are empty");
            }
            else if (txtDiscountName.Text == "")
            {
                MessageBox.Show("Disount Name field is empty");
                txtDiscountName.Focus();
            }
            else if (txtDiscountPrice.Text == "")
            {
                MessageBox.Show("Discount Price field is empty");
                txtDiscountPrice.Focus();
            }
            else if (cmbDiscountType.Text == "")
            {
                MessageBox.Show("Discount Type field is empty");
                cmbDiscountType.Focus();
            }
            else
            {
                using (cmd = new SqlCommand(updatediscount, conn))
                {
                    cmd.Parameters.AddWithValue("@DiscountName", txtDiscountName.Text);
                    cmd.Parameters.AddWithValue("@DiscountPrice", txtDiscountPrice.Text);
                    priceformat();
                    cmd.Parameters.AddWithValue("@DiscountType", cmbDiscountType.Text);
                    cmd.ExecuteNonQuery();
                    refresh();
                    MessageBox.Show("Discount has been update!");
                    conn.Close();
                }
            }
        }

        private void btnExit_Click(object sender, EventArgs e)
        {
            if (System.Windows.Forms.Application.OpenForms["Main_Menu_Form"] != null)
            {
                //MessageBox.Show("Form is open");
                (System.Windows.Forms.Application.OpenForms["Main_Menu_Form"] as Main_Menu_Form).panelSettingsFormLoader.Visible = false;
                this.Close();
            }
        }
    }
}
