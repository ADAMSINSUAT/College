using System;
using System.Collections.Generic;
using System.Text;

namespace FoodMenuApp.Models
{
    public class FinalFoodOrder
    {
        public int? FoodOrderDeviceID { get; set; }
        public int? FoodOrderNumber { get; set; }
        public string FoodOrderItem { get; set; }
        public int? FoodOrderQuantity { get; set; }
        public decimal? FoodOrderPrice { get; set; }
        public decimal? FoodOrderPriceAmount { get; set; }
        public decimal? FoodOrderSubTotal { get; set; }
        public string FoodOrderTime { get; set; }
        public string FoodOrderMonth { get; set; }
        public string FoodOrderDay { get; set; }
        public string FoodOrderYear { get; set; }
    }
}
