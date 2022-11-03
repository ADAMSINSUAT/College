using System;
using System.Collections.Generic;

namespace FoodMenuAPITest.Models
{
    public partial class FoodMenu
    {
        public string FoodId { get; set; }
        public string FoodName { get; set; }
        public decimal? FoodPrice { get; set; }
        public decimal? FoodVat { get; set; }
        public decimal? FoodSrp { get; set; }
        public string FoodCategory { get; set; }
        public string FoodAvailability { get; set; }
        public byte[] FoodPic { get; set; }
        public TimeSpan? TimetoCook { get; set; }
    }
}
