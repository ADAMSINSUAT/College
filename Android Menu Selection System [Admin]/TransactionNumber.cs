using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Android_Menu_Selection_System__Admin_
{
    public class TransactionNumber
    {
        private static int count;

        public static int transactionnumber
        {
            get
            {
                if (count == 0)
                {
                    count = new int();
                }
                return count;
            }
            set
            {
                count = value;
            }
        }
    }
}
