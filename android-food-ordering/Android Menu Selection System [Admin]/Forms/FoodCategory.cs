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

namespace Android_Menu_Selection_System__Admin_.Forms
{
    public partial class FoodCategory : Form
    {
        SqlConnection conn;
        SqlCommand cmd;
        SqlDataReader dr;

        public FoodCategory()
        {
            InitializeComponent();
        }

        private void Add_Food_Category_Load(object sender, EventArgs e)
        {
            panelSearch.Visible = false;
        }

        //Beginning of methods

        private void Database() // For connection
        {
            conn = ConnectDB.ConnectDatabase();
        }

        private void AddCategory() //For saving a category
        {
            Database();
            String select = "Select Category from FoodCategory where Category = '" + txtCategoryName.Text + "'";
            String insert = "Insert into FoodCategory(Category)VALUES(@Category)";
            String parameter = "@Category";
            cmd = new SqlCommand(select, conn);
            dr = cmd.ExecuteReader();
            if (dr.Read())
            {
                MessageBox.Show("A category with that name already exists!");
            }
            else
            {
                cmd = new SqlCommand(insert, conn);
                cmd.Parameters.AddWithValue(parameter, txtCategoryName.Text);
                cmd.ExecuteNonQuery();
                conn.Close();
                MessageBox.Show("Category is added");
                txtCategoryName.Text = "";
            }
        }

        private void DeleteCategory() //For deleting a category
        {
            Database();
            string search = "Select * from FoodCategory where Category ='" + txtSearchCategory.Text + "'";
            string delete = "Delete from FoodCategory where Category='"+txtSearchCategory.Text+"'";
            cmd = new SqlCommand(search, conn);
            dr = cmd.ExecuteReader();
            if (dr.Read())
            {
                cmd = new SqlCommand(delete, conn);
                cmd.ExecuteNonQuery();
                conn.Close();
            }
            else
            {
                conn.Close();
            }
        }

        private void UpdateCategory()
        {
            if (txtSearchCategory.Text == "")
            {
            }
            else
            {
                Database();
                string update = "Update FoodCategory set Category = @Category where Category ='" + txtSearchCategory.Text + "'";
                string category = "@Category";
                cmd = new SqlCommand(update, conn);
                cmd.Parameters.AddWithValue(category, txtSearchCategory.Text);
                cmd.ExecuteNonQuery();
                conn.Close();
            }
;
        }
        //End of methods


        private void btnAddCategory_Click(object sender, EventArgs e)
        {
            AddCategory();
        }

        private void btnUpdate_Click(object sender, EventArgs e)
        {
            lblCategory.Text = "Update Category"; //Changes the text of the lblCategory from Delete Category into Update Category"
            btnAction.Text = "Update";   //Changes the text of the btnAction from Delete Category into Update"
        }

        private void btnAction_Click(object sender, EventArgs e)
        {
            if(btnAction.Text == "Delete") //If the text for the btnAction is Delete Category, it will perform
            {                                               //a delete method
                DeleteCategory();
            }
            else if(btnAction.Text == "Update") //If the text for the btnAction is Update Category, it will perform an
            {                                            // update method
                UpdateCategory();
            }
        }

        private void btnExit_Click(object sender, EventArgs e)
        {
            this.Hide();
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            txtSearchCategory.Text = "";
            btnAction.Text = "Delete Category";
            panelSearch.Visible = false;
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            panelSearch.Visible = true;
        }
    }
}
