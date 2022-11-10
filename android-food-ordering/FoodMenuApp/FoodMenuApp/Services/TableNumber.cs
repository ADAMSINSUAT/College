using System;
using System.Collections.Generic;
using System.Text;

namespace FoodMenuApp.Services
{
    public class TableNumber
    {
        private static int count;
        public static int tablenumber
        {
            get
            {
                if(count==0)
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
