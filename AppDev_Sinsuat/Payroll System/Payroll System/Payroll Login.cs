using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace Payroll_System
{
    public partial class Payroll_Login : Form
    {
        public Payroll_Login()
        {
            InitializeComponent();
        }

        private void Payroll_Login_Load(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            if((textBox1.Text == "User" && textBox2.Text== "Password"))
            {
                this.Hide();
                Form frm = new Addemp();
                frm.ShowDialog();
            }
            else
            {
                MessageBox.Show("Invalid Account.");
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            Close();
        }
    }
}
