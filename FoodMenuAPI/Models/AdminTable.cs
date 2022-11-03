using System;
using System.Collections.Generic;

namespace FoodMenuAPITest.Models
{
    public partial class AdminTable
    {
        public int AdminId { get; set; }
        public string AdminUsername { get; set; }
        public string AdminPassword { get; set; }
        public string Role { get; set; }
        public string Status { get; set; }
        public byte[] AdminPhoto { get; set; }
    }
}
