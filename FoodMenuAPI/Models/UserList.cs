using System;
using System.Collections.Generic;

namespace FoodMenuAPITest.Models
{
    public partial class UserList
    {
        public int UserId { get; set; }
        public string UserName { get; set; }
        public string UserPassword { get; set; }
        public string UserRole { get; set; }
        public string UserStatus { get; set; }
    }
}
