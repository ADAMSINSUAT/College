using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace Exer1
{
    public partial class Confirmation : Form
    {
        public Confirmation()
        {
            InitializeComponent();
        }

        private void Confirmation_Load(object sender, EventArgs e)
        {
            label2.Text = Login.sendtext;
        }

        private void textBox1_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                if (textBox1.Text == label2.Text)
                {
                    Printpayment pt = new Printpayment();
                    pt.Show();
                    textBox1.Clear();
                    this.Hide();
                }
                else
                {
                    MessageBox.Show("Incorrect password", "Please type the correct password");
                    textBox1.Clear();
                }
            }
        }
    }
}
