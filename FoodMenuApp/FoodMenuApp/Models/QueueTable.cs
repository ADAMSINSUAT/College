using System;
using System.Collections.Generic;
using System.Text;

namespace FoodMenuApp
{
    public class QueueTable
    {
        public int? TableNumber { get; set; }
        public int? OrderNumber { get; set; }
        public string Item { get; set; }
        public int? Quantity { get; set; }
        public decimal? Price { get; set; }
        public decimal? PriceAmount { get; set; }
        public string OrderType { get; set; }
        public int? TransactionNumber { get; set; }
    }
}
