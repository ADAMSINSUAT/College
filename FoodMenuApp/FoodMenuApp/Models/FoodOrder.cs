using System;
using System.Collections.Generic;
using System.Text;

namespace FoodMenuApp
{
    public class FoodOrder
    {
        public byte[] FoodOrderPic { get; set; }
        public int? FoodOrderDeviceID { get; set; }
        public string FoodOrderItem { get; set; }
        public int? FoodOrderQuantity { get; set; }
        public decimal? FoodOrderPrice { get; set; }
        public decimal? FoodOrderPriceAmount { get; set; }
        public string FoodOrderPriceString { get; set; }
        public string FoodOrderAmountString { get; set; }

        public int? TransactionNumber { get; set; }
    }
}
