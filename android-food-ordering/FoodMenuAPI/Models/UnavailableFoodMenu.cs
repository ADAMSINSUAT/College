using System;
using System.Collections.Generic;

namespace FoodMenuAPI.Models
{
    public partial class UnavailableFoodMenu
    {
        public Guid id { get; set; } = Guid.NewGuid();
        public string UnavailableFoodId { get; set; }
        public string UnavailableFoodName { get; set; }
        public double? UnavailableFoodPrice { get; set; }
        public string UnavailableFoodCategory { get; set; }
        public string UnavailableFoodAvailability { get; set; }
        public byte[] UnavailableFoodPic { get; set; }

        public virtual FoodMenu UnavailableFood { get; set; }
    }
}
