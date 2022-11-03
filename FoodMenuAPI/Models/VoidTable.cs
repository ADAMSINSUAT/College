using System;
using System.Collections.Generic;

namespace FoodMenuAPITest.Models
{
    public partial class VoidTable
    {
        public int? VoidDeviceId { get; set; }
        public int? VoidOrderNumber { get; set; }
        public string VoidInvoiceItem { get; set; }
        public int? VoidInvoiceQuantity { get; set; }
        public decimal? VoidOrderPrice { get; set; }
        public decimal? VoidOrderPriceAmount { get; set; }
        public decimal? VoidInvoiceVatTotal { get; set; }
        public string VoidInvoiceTime { get; set; }
        public string VoidInvoiceMonth { get; set; }
        public string VoidInvoiceDay { get; set; }
        public string VoidInvoiceYear { get; set; }
        public decimal? VoidInvoiceItemVat { get; set; }
    }
}
