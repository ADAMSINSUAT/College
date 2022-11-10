using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.SqlClient;
using System.Windows.Forms;

namespace Exer1
{
    class Class1
    {
        public static SqlConnection Connectdb1()
        {
            try
            {
                SqlConnection conn = new SqlConnection(@"Data Source=DESKTOP-2N78NV2; database=db2; User id=DARKSIDERS; password=deathfurystrifewar;");
                //SqlConnection conn = new SqlConnection(@"Data Source=LAB1PC16-PC;Initial Catalog=db1;Integrated Security=True");
                conn.Open();
              //  MessageBox.Show("Connection Successful");
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
