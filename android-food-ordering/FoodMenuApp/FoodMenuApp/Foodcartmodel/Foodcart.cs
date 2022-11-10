using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;

namespace FoodMenuApp
{
    public class Foodcart
    {
        private static ObservableCollection<FoodOrder> _foodMenus;

        public static ObservableCollection<FoodOrder> Instance
        {
            get
            {
                if(_foodMenus==null)
                {
                    _foodMenus = new ObservableCollection<FoodOrder>();
                }
                return _foodMenus;
            }
            set
            {
                _foodMenus = value;
            }
        }
    }
}
