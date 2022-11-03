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
using System.Drawing.Imaging;
using System.IO;

namespace Android_Menu_Selection_System__Admin_
{
    public partial class ManageFoodMenu : Form
    {
        SqlConnection conn;
        SqlCommand cmd;
        SqlDataAdapter da;
        SqlDataReader dr;
        PictureBox pictureBox;
        Label foodid;
        Button btnUpdate;

        public ManageFoodMenu()
        {
            InitializeComponent();
        }

        private void ManageFoodMenu_Load(object sender, EventArgs e)
        {
            Database();
            //toolstripbuttons();
            //flowlayoutpanel();
        }

        //Methods list
        private void Database()
        {
            conn = ConnectDB.ConnectDatabase();
        }

        public void flowlayoutpanel()
        {
            flowLayoutPanel1.Controls.Clear();
            try
            {
                Database();
                String selectcategory = "Select * from FoodMenu";
                da = new SqlDataAdapter(selectcategory, conn);
                DataTable dt = new DataTable();
                da.Fill(dt);

                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    System.Drawing.ImageConverter converter = new System.Drawing.ImageConverter();
                    byte[] photo_array = (byte[])dt.Rows[i][7];
                    Image img = (Image)converter.ConvertFrom(photo_array);
                    MemoryStream ms = new MemoryStream();
                    ms.Position = 0;
                    ms.Read(photo_array, 0, photo_array.Length);
                    pictureBox = new PictureBox();
                    pictureBox.Image = img;
                    pictureBox.SizeMode = PictureBoxSizeMode.Zoom;
                    pictureBox.Height = 150;
                    pictureBox.Width = 150;
                    pictureBox.Tag = i;

                    foodid = new Label();
                    foodid.Text = dt.Rows[i][0].ToString();
                    foodid.Tag = i;
                    foodid.Name = dt.Rows[i][0].ToString();

                    Label foodname = new Label();
                    foodname.Text = dt.Rows[i][1].ToString();
                    foodname.AutoSize = true;
                    foodname.ForeColor = Color.White;
                    foodid.Visible = false;

                    Label foodprice = new Label();
                    foodprice.Text = Convert.ToString("P"+dt.Rows[i][4].ToString());
                    foodprice.ForeColor = Color.White;

                    Label foodcategory = new Label();
                    foodcategory.Text = dt.Rows[i][5].ToString();
                    foodcategory.Visible = false;

                    btnUpdate = new Button();
                    btnUpdate.Text = "Update";
                    btnUpdate.BackColor = Color.FromName("ControlLight");
                    btnUpdate.Name = foodid.Text;
                    btnUpdate.Width = 75;
                    btnUpdate.Height = 25;
                    btnUpdate.Location = new Point(0, 0);
                    btnUpdate.Tag = i;
                    btnUpdate.Click += new EventHandler(btnUpdate_Click);

                    Button btnRemove = new Button();
                    btnRemove.Text = "Remove";
                    btnRemove.BackColor = Color.FromName("ControlLight");
                    btnRemove.Name = foodid.Text;
                    btnRemove.Width = 75;
                    btnRemove.Height = 25;
                    btnRemove.Location = new Point(80, 0);
                    btnRemove.Tag = i;
                    btnRemove.Click += new EventHandler(btnRemove_Click);

                    Panel panel = new Panel();
                    panel.Height = 25;
                    panel.Width = 155;
                    panel.BackColor = SystemColors.ControlLight;
                    panel.Controls.Add(btnUpdate);
                    btnUpdate.Dock = DockStyle.Left;
                    panel.Controls.Add(btnRemove);
                    btnRemove.Dock = DockStyle.Right;
                    //panel.AutoSize = true;
                    //panel.AutoSizeMode = AutoSizeMode.GrowAndShrink;


                    pictureBox.Controls.Add(foodname);
                    pictureBox.Controls.Add(foodprice);
                    foodprice.Dock = DockStyle.Bottom;
                    foodname.Dock = DockStyle.Bottom;
                    //btnUpdate.Anchor = AnchorStyles;
                    //btnRemove.Dock = DockStyle.Bottom;

                    flowLayoutPanel1.Controls.Add(pictureBox);
                    flowLayoutPanel1.Controls.Add(panel);
                    flowLayoutPanel1.Controls.Add(foodid);
                    flowLayoutPanel1.Controls.Add(foodcategory);
                    //btnUpdate.Dock = DockStyle.Bottom;
                }
            }
            catch (Exception e)
            {
                MessageBox.Show(e.ToString());
            }
        }

        public void toolstripbuttons()
        {
            btnFoodCategory.Items.Clear();
            try
            {
                Database();
                String selectcategory = "Select * from FoodCategory";
                da = new SqlDataAdapter(selectcategory, conn);
                DataTable dt = new DataTable();
                da.Fill(dt);
                for(int i = 0; i < dt.Rows.Count; i++)
                {
                    ToolStripButton filterbutton = new ToolStripButton();
                    filterbutton.AutoSize = false;
                    filterbutton.Width = 100;
                    filterbutton.Name = "btn" + dt.Rows[i][0].ToString();
                    filterbutton.Text = dt.Rows[i][0].ToString();
                    filterbutton.TextImageRelation = TextImageRelation.ImageAboveText;
                    System.Drawing.ImageConverter converter = new System.Drawing.ImageConverter();
                    byte[] photo_array = (byte[])dt.Rows[i][1];
                    Image img = (Image)converter.ConvertFrom(photo_array);
                    MemoryStream ms = new MemoryStream();
                    ms.Position = 0;
                    ms.Read(photo_array, 0, photo_array.Length);
                    filterbutton.Image = img;
                    filterbutton.Dock = DockStyle.Left;
                    btnFoodCategory.Items.Add(filterbutton);
                }
            }
            catch
            {

            }
        }

        private void btnUpdate_Click(object sender, EventArgs e)
        {
            Button ID = sender as Button;
            String id;
            foreach (Label lb in flowLayoutPanel1.Controls.OfType<Label>())
            {
                if (lb.Name == ID.Name)
                {
                    id = lb.Name;
                    //MessageBox.Show("Successful" + ID.Name);
                    (System.Windows.Forms.Application.OpenForms["Main_Menu_Form"] as Main_Menu_Form).panelFormLoader.Controls.Clear();
                    UpdateFoodMenu ufm = new UpdateFoodMenu() { Dock = DockStyle.Fill, TopLevel = false, TopMost = true };
                    ufm.FormBorderStyle = FormBorderStyle.None;
                    (System.Windows.Forms.Application.OpenForms["Main_Menu_Form"] as Main_Menu_Form).panelFormLoader.Controls.Add(ufm);
                    //(System.Windows.Forms.Application.OpenForms["Main_Menu_Form"] as Main_Menu_Form).panelFormLoader.Size = ufm.Size;
                    String select = "Select*from FoodMenu where FoodID='" + id + "'";
                    Database(); cmd = new SqlCommand(select, conn);
                    dr = cmd.ExecuteReader();
                    if (dr.Read())
                    {
                        ufm.txtFoodID.Text = dr[0].ToString();
                        ufm.txtFoodName.Text = dr[1].ToString();
                        ufm.txtPrice.Text = dr[2].ToString();
                        if (dr[5].ToString() == "Available")
                        {
                            ufm.toggleAvailability.Checked = true;
                        }
                        else if (dr[5].ToString() == "Unavailable")
                        {
                            ufm.toggleAvailability.Checked = false;
                        }
                        ufm.lblVaT.Text = dr[3].ToString();
                        ufm.cmbCategory.Text = dr[4].ToString();
                        byte[] photo_array = (byte[])dr[6];
                        MemoryStream ms = new MemoryStream(photo_array);
                        ms.Position = 0;
                        ms.Read(photo_array, 0, photo_array.Length);
                        ufm.pictureBox1.Image = Image.FromStream(ms);
                        ufm.dTPTimeToCook.Text = Convert.ToString(dr[7].ToString());
                    }
                    ufm.Show();
                    break;
                }
            }
        }
        private void btnRemove_Click(object sender, EventArgs e)
        {
            Button ID = sender as Button;
            String id;
            flowLayoutPanel1.SuspendLayout();
            DialogResult check = MessageBox.Show("Are you sure you want to delete this menu item?", "Delete message...", MessageBoxButtons.YesNo);
            if(check==DialogResult.Yes)
            {
                foreach (Label lb in flowLayoutPanel1.Controls.OfType<Label>())
                {
                    if (lb.Name == ID.Name)
                    {
                        id = lb.Name;
                        Database();
                        String delete = "Delete from FoodMenu where FoodID='" + id + "'";
                        cmd = new SqlCommand(delete, conn);
                        cmd.ExecuteNonQuery();
                        conn.Close();
                        MessageBox.Show("Food is deleted");
                        flowlayoutpanel();
                    }
                }
            }
            else if (check == DialogResult.No)
            {

            }
            flowLayoutPanel1.ResumeLayout();
        }
    }
}
