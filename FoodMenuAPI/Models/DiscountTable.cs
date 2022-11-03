using System;
using System.Collections.Generic;

namespace FoodMenuAPITest.Models
{
    public partial class DiscountTable
    {
        public string DiscountName { get; set; }
        public string DiscountType { get; set; }
        public decimal? DiscountPrice { get; set; }
    }
}
