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
    public partial class AddFoodCategory : Form
    {
        SqlConnection conn;
        SqlCommand cmd;
        SqlDataReader dr;
        SqlDataAdapter da;

        PictureBox picturebox = new PictureBox();
        Label pic_path = new Label();
        Button addphoto = new Button();
        TextBox UpdatetxtSearchCategory;
        AutoCompleteStringCollection collect;

        public AddFoodCategory()
        {
            InitializeComponent();
        }

        private void FoodCategory_Load(object sender, EventArgs e)
        {
            panelSearch.Visible = false;
            refresh();
            AutoCompleSearch();
        }

        //Beginning of functions
        public bool CheckClick = false;
        //End of functions

        //Beginning of methods
        private void refresh()
        {
            Database();
            String select = "Select * from FoodCategory ORDER BY Category ASC";
            da = new SqlDataAdapter(select, conn);
            DataTable dt = new DataTable();
            da.Fill(dt);

            dataGridView1.Rows.Clear();

            foreach (DataRow item in dt.Rows)
            {
                int n = dataGridView1.Rows.Add();
                dataGridView1.Rows[n].Cells[0].Value = item["Category"].ToString();
                dataGridView1.Rows[n].Cells[1].Value = item["Image"];
            }
        }
        private void clear()
        {
            txtCategoryName.Text = "";
            txtSearchCategory.Text = "";
            UpdatetxtSearchCategory.Text = "";
            txtpic_path.Text = "";
            pictureBox1.Image= Android_Menu_Selection_System__Admin_.Properties.Resources.Foodmenu_placeholder;
            pic_path.Text = "";
            //UpdatetxtSearchCategory.Text = "";
            picturebox.Image= Android_Menu_Selection_System__Admin_.Properties.Resources.Foodmenu_placeholder;
        }
        private void DefaulPanelSearchControlLocation()
        {
            panelSearch.Controls.Remove(picturebox);
            panelSearch.Controls.Remove(pic_path);
            panelSearch.Controls.Remove(addphoto);
            panelSearch.Controls.Remove(UpdatetxtSearchCategory);
            panelSearch.Controls.Add(txtSearchCategory);
            panelSearch.Height = 350;
            panelSearch.Width = 622;
            panelSearch.Location = new Point(193, 12);
            lblCategory.Location = new Point(17, 2);
            label3.Location = new Point(16, 78);
            txtSearchCategory.Location = new Point(183, 82);
            btnAction.Location = new Point(397, 78);
            btnCancel.Location = new Point(489, 2);
            dataGridView1.Location = new Point(19, 116);
        }
        private void UpdatePanelControl() //For the controls of the update of food category
        {
            panelSearch.Location = new Point(12, 12);
            panelSearch.Height = 350;
            panelSearch.Width = 896;
            lblCategory.Location = new Point(312, 3);
            label3.Location = new Point(311, 79);
            UpdatetxtSearchCategory = new TextBox();
            UpdatetxtSearchCategory.Location = new Point(478, 83);
            UpdatetxtSearchCategory.Size = txtSearchCategory.Size;
            UpdatetxtSearchCategory.KeyDown += UpdatetxtSearchCategory_KeyDown;
            UpdatetxtSearchCategory.TextChanged += UpdatetxtSearchCategory_TextChanged;
            UpdatetxtSearchCategory.AutoCompleteMode = AutoCompleteMode.SuggestAppend;
            //UpdatetxtSearchCategory.AutoCompleteSource = AutoCompleteSource.CustomSource;
            UpdatetxtSearchCategory.AutoCompleteSource = AutoCompleteSource.CustomSource;
            //UpdatetxtSearchCategory.AutoCompleteCustomSource = new AutoCompleteStringCollection();
            //UpdatetxtSearchCategory.AutoCompleteCustomSource = collect;
            UpdatetxtSearchCategory.AllowDrop = true;
            UpdatetxtSearchCategory.Text = "";
            //txtSearchCategory.Location = new Point(402, 56);
            btnAction.Location = new Point(692, 79);
            btnCancel.Location = new Point(784, 3);

            picturebox.Name = "picturebox";
            pic_path.Name = "pic_path";
            addphoto.Name = "addphoto";

            pic_path.Text = txtpic_path.Text;
            addphoto.Text = "Add photo";
            addphoto.Font = btnAddphoto.Font;
            addphoto.BackColor = Control.DefaultBackColor;
            addphoto.FlatStyle = btnAddphoto.FlatStyle;
            addphoto.FlatAppearance.BorderColor = btnAddphoto.FlatAppearance.BorderColor;
            addphoto.FlatAppearance.BorderSize = btnAddphoto.FlatAppearance.BorderSize;
            addphoto.FlatAppearance.MouseDownBackColor = btnAddphoto.FlatAppearance.MouseDownBackColor;
            addphoto.FlatAppearance.MouseOverBackColor = btnAddphoto.FlatAppearance.MouseOverBackColor;

            picturebox.Image = Android_Menu_Selection_System__Admin_.Properties.Resources.Foodmenu_placeholder;
            picturebox.Size = new Size(242, 206);
            picturebox.SizeMode = PictureBoxSizeMode.StretchImage;
            pic_path.Size = txtpic_path.Size;
            addphoto.Size = new Size(242, 52);
            addphoto.Click+= addphoto_Click;
            panelSearch.Controls.Add(picturebox);
            panelSearch.Controls.Add(pic_path);
            panelSearch.Controls.Add(addphoto);
            panelSearch.Controls.Add(UpdatetxtSearchCategory);
            panelSearch.Controls.Remove(txtSearchCategory);

            picturebox.Location = new Point(25, 3);
            pic_path.Location = new Point(22, 218);
            addphoto.Location = new Point(25, 218);
            dataGridView1.Location = new Point(314, 117);
            addphoto.BringToFront();
        }

        void AutoCompleSearch()
        {
            this.Database();
            String select = "Select Category from FoodCategory where Category LIKE @Category";
            String paramcategory = "@Category";
            cmd = new SqlCommand(select, conn);
            cmd.Parameters.Add(new SqlParameter(paramcategory, "%" + txtSearchCategory.Text + "%"));
            cmd.ExecuteNonQuery();
            dr = cmd.ExecuteReader();

            AutoCompleteStringCollection coll = new AutoCompleteStringCollection();

            while (dr.Read())
            {
                coll.Add(dr.GetValue(0).ToString());
            }
            txtSearchCategory.AutoCompleteCustomSource = coll;
            dr.Close();
        }

        void AutoComplete()
        {
            this.Database();
            String select = "Select Category from FoodCategory where Category LIKE @Category";
            String paramcategory = "@Category";
            cmd = new SqlCommand(select, conn);
            cmd.Parameters.Add(new SqlParameter(paramcategory, "%" + UpdatetxtSearchCategory.Text + "%"));
            cmd.ExecuteNonQuery();
            dr = cmd.ExecuteReader();

            collect = new AutoCompleteStringCollection();

            while (dr.Read())
            {
                collect.Add(dr.GetValue(0).ToString());
            }
            UpdatetxtSearchCategory.AutoCompleteCustomSource = collect;
            dr.Close();
        }
    private void EnableControl() //To enable controls from behind when panel is no longer focused
        {
            foreach (Control child in this.Controls)
            {
                if (child != panelSearch)
                {
                    child.Enabled = true;
                }
            }

        }
        private void DisableControl() //To disable controls from behind when panel is focused
        {
            foreach(Control child in this.Controls)
            {
                if(child != panelSearch)
                {
                    child.Enabled = false;
                }
            }
        }

        private void Database() // For connection
        {
            conn = ConnectDB.ConnectDatabase();
        }

        private void AddCategory() //For saving a category
        {
            if (txtCategoryName.Text == "")
            {
                MessageBox.Show("Category Name field is empty!", "Error...");
            }
            else if(pictureBox1 == null || pictureBox1.Image == null)
            {
                MessageBox.Show("Picture Box field is empty!", "Error...");
            }
            else
            {
                MemoryStream ms = new MemoryStream();
                pictureBox1.Image.Save(ms, ImageFormat.Jpeg);
                byte[] pic_array = new byte[ms.Length];
                ms.Position = 0;
                ms.Read(pic_array, 0, pic_array.Length);
                Database();
                String select = "Select Category from FoodCategory where Category = '" + txtCategoryName.Text + "'";
                String insert = "Insert into FoodCategory(Category, Image)VALUES(@Category, @Image)";
                String paramCategory = "@Category";
                String paramImage = "@Image";
                cmd = new SqlCommand(select, conn);
                dr = cmd.ExecuteReader();
                if (dr.Read())
                {
                    MessageBox.Show("A category with that name already exists!");
                }
                else
                {
                    dr.Close();
                    cmd = new SqlCommand(insert, conn);
                    cmd.Parameters.AddWithValue(paramCategory, txtCategoryName.Text);
                    cmd.Parameters.AddWithValue(paramImage,pic_array);
                    cmd.ExecuteNonQuery();
                    conn.Close();
                    MessageBox.Show("Category is added");
                    txtCategoryName.Text = "";
                    if (System.Windows.Forms.Application.OpenForms["AddFoodForm"] != null)
                    {
                        (System.Windows.Forms.Application.OpenForms["AddFoodForm"] as AddFoodForm).UpdateFoodCategory();
                    }
                    if (System.Windows.Forms.Application.OpenForms["ManageFoodMenu"] != null)
                    {
                        (System.Windows.Forms.Application.OpenForms["ManageFoodMenu"] as ManageFoodMenu).toolstripbuttons();
                        (System.Windows.Forms.Application.OpenForms["ManageFoodMenu"] as ManageFoodMenu).flowlayoutpanel();
                    }
                }
            }
            clear();
        }

        private void DeleteCategory() //For deleting a category
        {
            try
            {
                this.Database();
                string search = "Select * from FoodCategory where Category ='" + txtSearchCategory.Text + "'";
                string delete = "Delete from FoodCategory where Category='" + txtSearchCategory.Text + "'";
                cmd = new SqlCommand(search, conn);
                dr = cmd.ExecuteReader();
                if (dr.Read())
                {
                    dr.Close();
                    cmd = new SqlCommand(delete, conn);
                    cmd.ExecuteNonQuery();
                    conn.Close();
                    if (System.Windows.Forms.Application.OpenForms["AddFoodForm"] != null)
                    {
                        (System.Windows.Forms.Application.OpenForms["AddFoodForm"] as AddFoodForm).UpdateFoodCategory();
                    }
                    if (System.Windows.Forms.Application.OpenForms["ManageFoodMenu"] != null)
                    {
                        (System.Windows.Forms.Application.OpenForms["ManageFoodMenu"] as ManageFoodMenu).toolstripbuttons();
                        (System.Windows.Forms.Application.OpenForms["ManageFoodMenu"] as ManageFoodMenu).flowlayoutpanel();
                    }
                    MessageBox.Show("Food Category is deleted");
                    refresh();
                    clear();
                }
                else
                {
                    conn.Close();
                }
            }
            catch(Exception ex)
            {
                MessageBox.Show("Status: "+ ex);
            }
        }

        private void UpdateCategory()
        {
            try
            {
                MemoryStream ms = new MemoryStream();
                picturebox.Image.Save(ms, ImageFormat.Jpeg);
                byte[] pic_array = new byte[ms.Length];
                ms.Position = 0;
                ms.Read(pic_array, 0, pic_array.Length);
                string update = "Update FoodCategory set Category = @Category, Image = @Image where Category ='" + UpdatetxtSearchCategory.Text + "'";
                this.Database();
                cmd = new SqlCommand(update, conn);
                cmd.Parameters.AddWithValue("@Category", UpdatetxtSearchCategory.Text);
                cmd.Parameters.AddWithValue("@Image", pic_array);
                cmd.ExecuteNonQuery();
                conn.Close();
                if (System.Windows.Forms.Application.OpenForms["AddFoodForm"] != null)
                {
                    (System.Windows.Forms.Application.OpenForms["AddFoodForm"] as AddFoodForm).UpdateFoodCategory();
                }
                if (System.Windows.Forms.Application.OpenForms["ManageFoodMenu"] != null)
                {
                    (System.Windows.Forms.Application.OpenForms["ManageFoodMenu"] as ManageFoodMenu).toolstripbuttons();
                    (System.Windows.Forms.Application.OpenForms["ManageFoodMenu"] as ManageFoodMenu).flowlayoutpanel();
                }
                MessageBox.Show("Food category has been updated");
                refresh();
                clear();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Status:" + ex);
            }
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
            UpdatePanelControl();
            AutoComplete();
            panelSearch.Visible = true;
        }

        private void btnAction_Click(object sender, EventArgs e)
        {
            DisableControl();
            if (btnAction.Text == "Delete") //If the text for the btnAction is Delete Category, it will perform
            {
                panelSearch.Focus();                                      //a delete method
                DeleteCategory();
            }
            else if (btnAction.Text == "Update") //If the text for the btnAction is Update Category, it will perform an
            {
                panelSearch.Focus();                                     // update method
                UpdateCategory();
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

        private void btnCancel_Click(object sender, EventArgs e)
        {
            EnableControl();
            txtSearchCategory.Text = "";
            btnAction.Text = "Delete Category";
            btnCancel.Focus();
            panelSearch.Visible = false;
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            DisableControl();
            DefaulPanelSearchControlLocation();
            panelSearch.Focus();
            lblCategory.Text = "Delete Category";
            panelSearch.Visible = true;

        }

        private void btnAddphoto_Click(object sender, EventArgs e)
        {
            OFD.Title = "Select an icon...";
            OFD.FileName = "";
            OFD.Filter = "jpeg|*.jpeg|png|*.png|bmp|*.bmp|All Files|*.*";
            OFD.FilterIndex = 1;

            OFD.ShowDialog();
            txtpic_path.Text = OFD.FileName;
            pictureBox1.ImageLocation = txtpic_path.Text;
        }

        private void addphoto_Click(object sender, EventArgs e)
        {
            OFD.Title = "Select an icon...";
            OFD.FileName = "";
            OFD.Filter = "jpeg|*.jpeg|png|*.png|bmp|*.bmp|All Files|*.*";
            OFD.FilterIndex = 1;

            OFD.ShowDialog();
            pic_path.Text = OFD.FileName;
            picturebox.ImageLocation = pic_path.Text;
        }

        private void txtSearchCategory_KeyDown(object sender, KeyEventArgs e)
        {
            Database();
            String searchcategory = "Select Category from FoodCategory where Category='" + txtSearchCategory.Text + "'";
            cmd = new SqlCommand(searchcategory, conn);
            dr = cmd.ExecuteReader();

            if (dr.Read())
            {
                txtSearchCategory.Text = dr[0].ToString();
            }
            conn.Close();
        }

        private void UpdatetxtSearchCategory_KeyDown(object sender, EventArgs e)
        {
            Database();
            String searchcategory = "Select * from FoodCategory where Category='" + UpdatetxtSearchCategory.Text + "'";
            cmd = new SqlCommand(searchcategory, conn);
            dr = cmd.ExecuteReader();

            if (dr.Read())
            {
                UpdatetxtSearchCategory.Text = dr[0].ToString();
                System.Drawing.ImageConverter converter = new System.Drawing.ImageConverter();
                byte[] photo_array = (byte[])dr[1];
                Image img = (Image)converter.ConvertFrom(photo_array);
                MemoryStream ms = new MemoryStream();
                ms.Position = 0;
                ms.Read(photo_array, 0, photo_array.Length);
                picturebox.Image = img;
                picturebox.SizeMode = PictureBoxSizeMode.StretchImage;
            }
            conn.Close();
        }

        private void dataGridView1_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            if(lblCategory.Text == "Delete Category")
            {
                txtSearchCategory.Text = dataGridView1.CurrentRow.Cells[0].ToString();
            }
            else if(lblCategory.Text == "Update Category")
            {
                txtSearchCategory.Text = dataGridView1.CurrentRow.Cells[0].ToString();
                var data = (Byte[])dataGridView1.CurrentRow.Cells[1].Value;
                var stream = new MemoryStream(data);
                picturebox.Image = Image.FromStream(stream);
            }
        }

        private void txtSearchCategory_TextChanged(object sender, EventArgs e)
        {
            if(txtSearchCategory.Text == "")
            {
                pictureBox1.Image = Android_Menu_Selection_System__Admin_.Properties.Resources.Foodmenu_placeholder;
            }
        }

        private void UpdatetxtSearchCategory_TextChanged(object sender, EventArgs e)
        {
            if (UpdatetxtSearchCategory.Text == "")
            {
                picturebox.Image = Android_Menu_Selection_System__Admin_.Properties.Resources.Foodmenu_placeholder;
            }
        }
    }
}
