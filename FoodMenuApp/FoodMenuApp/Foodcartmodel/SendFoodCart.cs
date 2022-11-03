using FoodMenuApp.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;

namespace FoodMenuApp.Foodcartmodel
{
    public class SendFoodCart
    {
        private static ObservableCollection<QueueTable> _foodOrder;

        public static ObservableCollection<QueueTable> Instance
        {
            get
            {
                if (_foodOrder == null)
                {
                    _foodOrder = new ObservableCollection<QueueTable>();
                }
                return _foodOrder;
            }
            set
            {
                _foodOrder = value;
            }
        }
    }
}
