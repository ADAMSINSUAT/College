using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;

namespace FoodMenuApp.Models
{
    public class FoodMenuList
    {
        private static ObservableCollection<FoodMenu> _foodMenuList;

        public static ObservableCollection<FoodMenu> menulist
        {
            get
            {
                if (_foodMenuList == null)
                {
                    _foodMenuList = new ObservableCollection<FoodMenu>();
                }
                return _foodMenuList;
            }
            set
            {
                _foodMenuList = value;
            }
        }
    }
}
