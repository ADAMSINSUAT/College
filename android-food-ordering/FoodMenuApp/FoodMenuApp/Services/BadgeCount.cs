using System;
using System.Collections.Generic;
using System.Text;

namespace FoodMenuApp.Services
{
    public class BadgeCount
    {
        private static int count;

        public static int badgecount
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
