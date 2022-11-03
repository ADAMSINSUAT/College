using FoodMenuAPI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FoodMenuAPI
{
    interface IFoodMenuApp
    {
        public List<AvailableFoodMenu> GetFoodMenu();

        public AvailableFoodMenu AddFoodOrder(AvailableFoodMenu order);

        public AvailableFoodMenu UpdateFoodOrder(string foodid, AvailableFoodMenu order);

        public string DeleteOrder(string foodid);
    }
}
