using System;
using System.Collections.Generic;

namespace FoodMenuAPITest.Models
{
    public partial class LoginTable
    {
        public string LoginId { get; set; }
        public string LoginUserName { get; set; }
        public DateTime? LoginDate { get; set; }
        public string LoginTime { get; set; }
        public string LogoutTime { get; set; }
    }
}
