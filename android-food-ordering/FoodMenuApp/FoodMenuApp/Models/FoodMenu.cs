using System;
using System.Collections.Generic;
using System.Text;

namespace FoodMenuApp
{
    public class FoodMenu
    {
        public string FoodId { get; set; }
        public string FoodName { get; set; }
        public decimal? FoodPrice { get; set; }
        public decimal? FoodVAT { get; set; }
        public decimal? FoodSRP { get; set; }
        public string FoodCategory { get; set; }
        public string FoodAvailability { get; set; }
        public byte[] FoodPic { get; set; }
        public string PesoFoodPrice { get; set; }
    }
}
