using FoodMenuAPI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FoodMenuAPI
{
    public class FoodMenuAppService : IFoodMenuApp
    {
        private List<AvailableFoodMenu> _foodmenu;

        public FoodMenuAppService()
        {
            _foodmenu = new List<AvailableFoodMenu>();
        }

        public List<AvailableFoodMenu> GetFoodMenu()
        {
            return _foodmenu;
        }

        public AvailableFoodMenu AddFoodOrder(AvailableFoodMenu foodmenuitem)
        {
            _foodmenu.Add(foodmenuitem);
            return foodmenuitem;
        }

        public AvailableFoodMenu UpdateFoodOrder(string foodid, AvailableFoodMenu foodmenuitem)
        {
            for (var index = _foodmenu.Count - 1; index >= 0; index--)
            {
                if (_foodmenu[index].AvailableFoodId == foodid)
                {
                    _foodmenu[index] = foodmenuitem;
                }
            }
            return foodmenuitem;
        }

        public string DeleteOrder(string foodid)
        {
            for (var index = _foodmenu.Count - 1; index >= 0; index--)
            {
                if (_foodmenu[index].AvailableFoodId == foodid)
                {
                    _foodmenu.RemoveAt(index);
                }
            }

            return foodid;
        }

    }
}
