using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace FoodMenuAPI.Models
{
    public partial class AvailableFoodMenu
    {
        //public Guid AFI { get; set; } = Guid.NewGuid();
        [Key]
        public string AvailableFoodId { get; set; }
        public string AvailableFoodName { get; set; }
        public double? AvailableFoodPrice { get; set; }
        public string AvailableFoodCategory { get; set; }
        public string AvailableFoodAvailability { get; set; }
        public byte[] AvailableFoodPic { get; set; }

        public virtual FoodMenu AvailableFood { get; set; }
    }
}
