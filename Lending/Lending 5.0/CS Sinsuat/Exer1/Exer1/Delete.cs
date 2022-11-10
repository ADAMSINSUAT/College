using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Data.SqlClient;

namespace Exer1
{
    public partial class Delete : Form
    {
        SqlConnection conn;
        SqlDataAdapter da;
        SqlDataReader dr;
        SqlCommand cmd;
        public Delete()
        {
            InitializeComponent();
        }

        private void Delete_Load(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            conn = Class1.Connectdb1();

        }
    }
}
