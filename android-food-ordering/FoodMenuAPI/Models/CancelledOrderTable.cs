using System;
using System.Collections.Generic;

namespace FoodMenuAPITest.Models
{
    public partial class CancelledOrderTable
    {
        public int CancelledOrderId { get; set; }
        public int? CancelledOrderDeviceId { get; set; }
        public string CancelledOrderItem { get; set; }
        public int? CancelledOrderQuantity { get; set; }
        public double? CancelledOrderSubTotal { get; set; }
        public double? CancelledOrderTax { get; set; }
        public double? CancelledOrderTotal { get; set; }
        public double? CancelledOrderOgpayment { get; set; }
        public double? CancelledOrderChange { get; set; }
        public string CancelledOrderTime { get; set; }
        public string CancelledOrderMonth { get; set; }
        public string CancelledOrderDay { get; set; }
        public string CancelledOrderYear { get; set; }
        public DateTime? CancelledOrderDate { get; set; }
    }
}
