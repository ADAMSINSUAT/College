using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;
using System.Windows.Forms;
using System.Data;

namespace Android_Menu_Selection_System__Admin_
{

    class UpdateFoodCategoryData
    {
        public class UpdateComboBox
        {
            public void SaveData(DataSet ds)
            {
                SqlConnection conn = ConnectDB.ConnectDatabase();
                String refresh = "Select Category from FoodCategory";
                SqlDataAdapter da = new SqlDataAdapter(refresh, conn);
                da.Update(ds);
            }
        }
    }
}
