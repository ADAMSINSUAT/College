﻿using System;
using System.Collections.Generic;

namespace FoodMenuAPITest.Models
{
    public partial class CashierLoginCredentials
    {
        public int EmpId { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
        public string Role { get; set; }
        public string Status { get; set; }
    }
}
