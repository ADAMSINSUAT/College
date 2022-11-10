using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Runtime.InteropServices;
using System.Data.SqlClient;
using System.Text.RegularExpressions;

namespace Android_Menu_Selection_System__Admin_
{
    public partial class Main_Menu_Form : Form
    {
        SqlCommand cmd;
        SqlConnection conn;
        SqlDataReader dr;
        int i;
        AddFoodForm aff;
        ManageFoodMenu mfm;
        ManageCashier mc;
        DisableCashierForm dcf;
        Login_List ll;
        DailySalesForm dsf;

         [DllImport("Gdi32.dll", EntryPoint = "CreateRoundRectRgn")]

        private static extern IntPtr CreateRoundRectRgn
        (
        int nLeftRect,
        int nTopRect,
        int nRightRect,
        int nBottomRect,
        int nWidthEllipse,
        int nHeightEllipse
        );

        public Main_Menu_Form()
        {
            InitializeComponent();
            Region = System.Drawing.Region.FromHrgn(CreateRoundRectRgn(0, 0, Width, Height, 25, 25));
            panelNav.Height = btnDashBoard.Height; //Code for navigation color/panel
            panelNav.Top = btnDashBoard.Top;
            panelNav.Left = btnDashBoard.Left;
            //btnDashBoard.BackColor = Color.FromArgb(46, 51, 73);
            HidePanelFormSettings();
            //lblTitle.Text = "Dashboard";
            //this.panelFormLoader.Controls.Clear();
            //ManageCashier mc = new ManageCashier() { Dock = DockStyle.Fill, TopLevel = false, TopMost = true };
            //mc.FormBorderStyle = FormBorderStyle.None;
            //this.panelFormLoader.Controls.Add(mc);
            //mc.Show();
        }

        private void Main_Menu_Form_Load(object sender, EventArgs e)
        {
            Database();
            LoadForms();
        }
        //Functions list
        public void HidePanelFormSettings()
        {
            while(panelSettingsFormLoader.Controls.Count<0)
            {
                panelSettingsFormLoader.Visible = false;
            }
        }
        //End of functions list

        //Methods list
        private void LoadForms()
        {
        }

        private void Disable_EnableControl()
        {
            if(panelSettings.Visible==true)
            {
                foreach (Control child in this.Controls)
                {
                    if (child != panelSettings && child!=panelSettingsFormLoader)
                    {
                        child.Enabled = false;
                    }
                }
            }
            else
            {
                foreach (Control child in this.Controls)
                {
                    if (child != panelSettings && child != panelSettingsFormLoader)
                    {
                        child.Enabled = true;
                    }
                }
            }
            if(panelSettingsFormLoader.Visible==true)
            {
                foreach (Control child in this.Controls)
                {
                    if (child != panelSettingsFormLoader)
                    {
                        child.Enabled = false;
                    }
                }
            }
            else
            {
                foreach (Control child in this.Controls)
                {
                    if (child == panelSettings)
                    {
                        child.Enabled = true;
                    }
                }
            }
        }
        private void DisableControl()
        {
            foreach (Control child in this.Controls)
            {
                if (child != panelSettings)
                {
                    child.Enabled = false;
                }
                //if (child == panelSettingsFormLoader)
                //{
                //    child.Enabled = false;
                //}
                //if (child != panelSettingsFormLoader)
                //{
                //    child.Enabled = false;
                //}
            }
        }
        private void Database()
        {
            conn = ConnectDB.ConnectDatabase();
        }
        //End of the methods list

        //Dashboard buttons
        private void btnDashBoard_Click(object sender, EventArgs e)
        {
            panelNav.Height = btnDashBoard.Height;
            panelNav.Top = btnDashBoard.Top;
            panelNav.Left = btnDashBoard.Left;
            //btnDashBoard.BackColor = Color.FromArgb(46, 51, 73);

            //lblTitle.Text = "Dashboard";
            //this.panelFormLoader.Controls.Clear();
            //ManageCashier mc = new ManageCashier() { Dock = DockStyle.Fill, TopLevel = false, TopMost = true };
            //mc.FormBorderStyle = FormBorderStyle.None;
            //this.panelFormLoader.Controls.Add(mc);
            //mc.Show();
        }

        private void btnMenuItem_Click(object sender, EventArgs e)
        {
            btnDashBoard.BackColor = Color.FromArgb(0, 139, 139);
            panelNav.Height = btnMenuItem.Height;
            panelNav.Top = btnMenuItem.Top;
            panelNav.Left = btnMenuItem.Left;
            int i = 1;
            //btnMenuItem.BackColor = Color.FromArgb(46, 51, 73);
            lblTitle.Text = "Add new food";
            this.panelFormLoader.Controls.Clear();
            aff = new AddFoodForm() { Dock = DockStyle.Fill, TopLevel = false, TopMost = true };
            aff.FormBorderStyle = FormBorderStyle.None;
            this.panelFormLoader.Controls.Add(aff);
            this.panelFormLoader.Size = aff.Size;
            aff.Show();
        }

        private void btnManageMenu_Click(object sender, EventArgs e)
        {
            btnDashBoard.BackColor = Color.FromArgb(0, 139, 139);
            panelNav.Height = btnManageMenu.Height;
            panelNav.Top = btnManageMenu.Top;
            panelNav.Left = btnManageMenu.Left;

            lblTitle.Text = "Manage Food Menu";
            this.panelFormLoader.Controls.Clear();
            mfm = new ManageFoodMenu() { Dock = DockStyle.Fill, TopLevel = false, TopMost = true };
            mfm.flowlayoutpanel();
            mfm.toolstripbuttons();
            mfm.FormBorderStyle = FormBorderStyle.None;
            this.panelFormLoader.Controls.Add(mfm);
            this.panelFormLoader.Size = mfm.Size;
            mfm.Show();
        }

        private void btnDailySales_Click(object sender, EventArgs e)
        {
            btnDashBoard.BackColor = Color.FromArgb(0, 139, 139);
            panelNav.Height = btnDailySales.Height;
            panelNav.Top = btnDailySales.Top;
            panelNav.Left = btnDailySales.Left;
            lblTitle.Text = "Daily Sales";
            this.panelFormLoader.Controls.Clear();
            dsf = new DailySalesForm() { Dock = DockStyle.Fill, TopLevel = false, TopMost = true };
            dsf.FormBorderStyle = FormBorderStyle.None;
            this.panelFormLoader.Controls.Add(dsf);
            this.panelFormLoader.Size = dsf.Size;
            dsf.Show();
            //btnDailySales.BackColor = Color.FromArgb(46, 51, 73);
        }

        private void btnManageCashierAccount_Click(object sender, EventArgs e)
        {
            btnDashBoard.BackColor = Color.FromArgb(0, 139, 139);
            panelNav.Height = btnManageCashierAccount.Height;
            panelNav.Top = btnManageCashierAccount.Top;
            panelNav.Left = btnManageCashierAccount.Left;
            //btnManageCashierAccount.BackColor = Color.FromArgb(46, 51, 73);

            lblTitle.Text = "Manage Cashier Account";
            this.panelFormLoader.Controls.Clear();
            mc = new ManageCashier() { Dock = DockStyle.Fill, TopLevel = false, TopMost = true };
            mc.FormBorderStyle = FormBorderStyle.None;
            this.panelFormLoader.Controls.Add(mc);
            this.panelFormLoader.Size = mc.Size;
            mc.Show();
        }

        private void btnDisableCashierAccount_Click(object sender, EventArgs e)
        {
            btnDashBoard.BackColor = Color.FromArgb(0, 139, 139);
            panelNav.Height = btnDisableCashierAccount.Height;
            panelNav.Top = btnDisableCashierAccount.Top;
            panelNav.Left = btnDisableCashierAccount.Left;
            //btnDisableCashierAccount.BackColor = Color.FromArgb(46, 51, 73);

            lblTitle.Text = "Change Cashier Status";
            this.panelFormLoader.Controls.Clear();
            dcf = new DisableCashierForm() { Dock = DockStyle.Fill, TopLevel = false, TopMost = true };
            dcf.FormBorderStyle = FormBorderStyle.None;
            this.panelFormLoader.Controls.Add(dcf);
            this.panelFormLoader.Size = dcf.Size;
            dcf.Show();
        }

        private void btnLoginList_Click(object sender, EventArgs e)
        {
            btnDashBoard.BackColor = Color.FromArgb(0, 139, 139);
            panelNav.Height = btnLoginList.Height;
            panelNav.Top = btnLoginList.Top;
            panelNav.Left = btnLoginList.Left;
            //btnLoginList.BackColor = Color.FromArgb(46, 51, 73);

            lblTitle.Text = "Change Cashier Status";
            this.panelFormLoader.Controls.Clear();
            ll = new Login_List() { Dock = DockStyle.Fill, TopLevel = false, TopMost = true };
            ll.FormBorderStyle = FormBorderStyle.None;
            this.panelFormLoader.Controls.Add(ll);
            this.panelFormLoader.Size = ll.Size;
            ll.Show();
        }

        private void bnLogout_Click(object sender, EventArgs e)
        {
            btnDashBoard.BackColor = Color.FromArgb(0, 139, 139);
            panelNav.Height = bnLogout.Height;
            panelNav.Top = bnLogout.Top;
            panelNav.Left = bnLogout.Left;
            bnLogout.BackColor = Color.FromArgb(46, 51, 73);
            this.Database();
            String update = "Update LoginTable set LogoutTime=@LogoutTime where LoginID='"+lblID.Text+"' AND LoginTime= (Select MAX(LoginTime) from LoginTable)";
            String parameters = "@LogoutTime";
            cmd = new SqlCommand(update, conn);
            cmd.Parameters.AddWithValue(parameters, DateTime.Now.ToString("h:mm:ss:tt"));
            MessageBox.Show("Log out Successfully");
            cmd.ExecuteNonQuery();
            conn.Close();
            LoginForm lgf = new LoginForm();
            this.Hide();
            lgf.Show();
        }

        private void btnDashBoard_Leave(object sender, EventArgs e)
        {
            btnDashBoard.BackColor = Color.FromArgb(0, 139, 139);
        }

        private void btnMenuItem_Leave(object sender, EventArgs e)
        {
            btnMenuItem.BackColor = Color.FromArgb(0, 139, 139);
        }

        private void btnManageMenu_Leave(object sender, EventArgs e)
        {
            btnManageMenu.BackColor = Color.FromArgb(0, 139, 139);
        }

        private void btnDailySales_Leave(object sender, EventArgs e)
        {
            btnDailySales.BackColor = Color.FromArgb(0, 139, 139);
        }

        private void btnManageCashierAccount_Leave(object sender, EventArgs e)
        {
            btnManageCashierAccount.BackColor = Color.FromArgb(0, 139, 139);
        }

        private void btnDisableCashierAccount_Leave(object sender, EventArgs e)
        {
            btnDisableCashierAccount.BackColor = Color.FromArgb(0, 139, 139);
        }

        private void btnLoginList_Leave(object sender, EventArgs e)
        {
            btnLoginList.BackColor = Color.FromArgb(0, 139, 139);
        }

        private void bnLogout_Leave(object sender, EventArgs e)
        {
            bnLogout.BackColor = Color.FromArgb(0, 139, 139);
        }

        private void txtSearchControls_TextChanged(object sender, EventArgs e)
        {
            
        }
        private void txtSearchControls_KeyDown(object sender, KeyEventArgs e)
        {

            if(e.KeyCode == Keys.Enter)
            {
                aff = new AddFoodForm() { Dock = DockStyle.Fill, TopLevel = false, TopMost = true };
                mfm = new ManageFoodMenu() { Dock = DockStyle.Fill, TopLevel = false, TopMost = true };
                mc = new ManageCashier() { Dock = DockStyle.Fill, TopLevel = false, TopMost = true };
                dcf = new DisableCashierForm() { Dock = DockStyle.Fill, TopLevel = false, TopMost = true };
                ll = new Login_List() { Dock = DockStyle.Fill, TopLevel = false, TopMost = true };

                Form[] forms = { aff, mfm, mc, dcf, ll };
                List<Form> formcollection = new List<Form>();

                if (txtSearchControls.Text == "")
                {

                }
                else
                {
                    foreach(var form in forms)
                    {
                        foreach(Control control in form.Controls)
                        {
                            string controltext = Regex.Replace(control.Text, @"[^0-9a-zA-Z]+", "");
                            //string newtext;
                            //control.Text.Replace(@"[^0-9a-zA-Z]+", "");
                            if (control.Text == txtSearchControls.Text || control.Name == txtSearchControls.Text || controltext == txtSearchControls.Text || controltext.Split().Any(a=> a.Contains(txtSearchControls.Text)) || controltext.Equals(txtSearchControls.Text) || txtSearchControls.Text == controltext.Trim())
                            {
                                // MessageBox.Show("Control is found");
                                Form newform = control.FindForm();
                                this.panelFormLoader.Controls.Clear();
                                //newform = new Form() { Dock = DockStyle.Fill, TopLevel = false, TopMost = true };
                                newform.FormBorderStyle = FormBorderStyle.None;
                                this.panelFormLoader.Controls.Add(newform);
                                //this.panelFormLoader.Size = newform.Size;
                                newform.Show();
                                return;
                            }
                            else
                            {
                                //MessageBox.Show("Control is not found");
                            }
                        }
                    }
                }
            }
        }

        private void txtSearchControls_MouseClick(object sender, MouseEventArgs e)
        {
            txtSearchControls.Text = "";
        }
        //End of the Dashboard buttons

        //Buttons for the Settings panel
        private void btnAddCategory_Click(object sender, EventArgs e)
        {
            panelSettingsFormLoader.Location = new Point(10, 10);
            panelSettingsFormLoader.BringToFront();
            panelSettingsFormLoader.Visible = true;
            this.panelSettingsFormLoader.Controls.Clear();
            AddFoodCategory adc = new AddFoodCategory() { Dock = DockStyle.Fill, TopLevel = false, TopMost = true };
            adc.FormBorderStyle = FormBorderStyle.None;
            this.panelSettingsFormLoader.Controls.Add(adc);
            //Panel panel = new Panel();
            //panel.Height = 10;
            //panel.Width = 10;
            this.panelSettingsFormLoader.Size = adc.Size;
            adc.Show();
            //btnHidePanelSettingsFormLoader.ForeColor = adc.btnExit.ForeColor;
            //btnHidePanelSettingsFormLoader.Font = adc.btnExit.Font;
            //btnHidePanelSettingsFormLoader.Size = adc.btnExit.Size;
            //btnHidePanelSettingsFormLoader.BringToFront();
            //btnHidePanelSettingsFormLoader.Location = adc.btnExit.Location;
        }

        private void btnManageDiscounts_Click(object sender, EventArgs e)
        {
            panelSettingsFormLoader.Location = new Point(10, 10);
            panelSettingsFormLoader.BringToFront();
            panelSettingsFormLoader.Visible = true;
            this.panelSettingsFormLoader.Controls.Clear();
            ManageDiscountForm mc = new ManageDiscountForm() { Dock = DockStyle.Fill, TopLevel = false, TopMost = true };
            mc.FormBorderStyle = FormBorderStyle.None;
            this.panelSettingsFormLoader.Controls.Add(mc);
            //Panel panel = new Panel();
            //panel.Height = 10;
            //panel.Width = 10;
            this.panelSettingsFormLoader.Size = mc.Size;
            mc.Show();
        }

        private void btnUserlist_Click(object sender, EventArgs e)
        {
            panelSettingsFormLoader.Location = new Point(10, 10);
            panelSettingsFormLoader.BringToFront();
            panelSettingsFormLoader.Visible = true;
            this.panelSettingsFormLoader.Controls.Clear();
            UserListForm ulf = new UserListForm() { Dock = DockStyle.Fill, TopLevel = false, TopMost = true };
            ulf.FormBorderStyle = FormBorderStyle.None;
            this.panelSettingsFormLoader.Controls.Add(ulf);
            //Panel panel = new Panel();
            //panel.Height = 10;
            //panel.Width = 10;
            this.panelSettingsFormLoader.Size = ulf.Size;
            ulf.Show();
        }

        private void btnAdminProfile_Click(object sender, EventArgs e)
        {
            panelSettingsFormLoader.Location = new Point(10, 10);
            panelSettingsFormLoader.BringToFront();
            panelSettingsFormLoader.Visible = true;
            this.panelSettingsFormLoader.Controls.Clear();
            AdminProfileForm apf = new AdminProfileForm() { Dock = DockStyle.Fill, TopLevel = false, TopMost = true };
            apf.FormBorderStyle = FormBorderStyle.FixedSingle;
            this.panelSettingsFormLoader.Controls.Add(apf);
            //Panel panel = new Panel();
            //panel.Height = 10;
            //panel.Width = 10;
            this.panelSettingsFormLoader.Size = apf.Size;
            apf.Show();
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            panelSettings.Visible = false;
        }

        private void btnSettings_Click(object sender, EventArgs e)
        {
            panelSettings.Focus();
            panelSettings.Visible=true;
        }

        private void panelSettingsFormLoader_ControlRemoved(object sender, ControlEventArgs e)
        {
            if(panelSettingsFormLoader.Controls.Count>0)
            {
                MessageBox.Show("False");
            }
            else
            {
                MessageBox.Show("True");
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {

        }

        private void btnHidePanelSettingsFormLoader_Click(object sender, EventArgs e)
        {
            panelSettingsFormLoader.Visible = false;
        }

        private void panelSettingsFormLoader_VisibleChanged(object sender, EventArgs e)
        {
            Disable_EnableControl();
        }

        private void panelSettings_VisibleChanged(object sender, EventArgs e)
        {
            Disable_EnableControl();
        }
        //End of the Settings panel buttons
    }
}
