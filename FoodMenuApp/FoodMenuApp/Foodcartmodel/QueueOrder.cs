using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;

namespace FoodMenuApp.Foodcartmodel
{
    public class QueueOrder
    {
        private static ObservableCollection<FoodOrder> _QueueOrder;

        public static ObservableCollection<FoodOrder> Instance
        {
            get
            {
                if (_QueueOrder == null)
                {
                    _QueueOrder = new ObservableCollection<FoodOrder>();
                }
                return _QueueOrder;
            }
            set
            {
                _QueueOrder = value;
            }
        }
    }
}
