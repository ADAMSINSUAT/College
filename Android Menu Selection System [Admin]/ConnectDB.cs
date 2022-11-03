using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;
using System.Windows.Forms;

namespace Android_Menu_Selection_System__Admin_
{
    class ConnectDB
    {
        public static SqlConnection ConnectDatabase()
        {
            try
            {
                SqlConnection conn = new SqlConnection(@"Data Source=DESKTOP-2N78NV2;Initial Catalog=Android Food Menu DB;User ID=Restaurant Admin;Password=foodsotasty1999");
                conn.Open();
                //MessageBox.Show("Connection Successful");
                return conn;
            }
            catch(Exception e)
            {
                MessageBox.Show(e.ToString());
                return null;
            }
        }
    }
}
