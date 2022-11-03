using System;
using System.Collections.Generic;
using System.Text;

namespace FoodMenuApp.Services
{
    public class OrderReferenceNumber
    {
        private static int referencenumber;

        public static int orderreferencenumber
        {
            get
            {
                if (referencenumber == 0)
                {
                    referencenumber = new int();
                }
                return referencenumber;
            }
            set
            {
                referencenumber = value;
            }
        }
    }
}
