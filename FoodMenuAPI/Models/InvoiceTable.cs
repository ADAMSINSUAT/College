using System;
using System.Collections.Generic;

namespace FoodMenuAPITest.Models
{
    public partial class InvoiceTable
    {
        public int InvoiceTableNumber { get; set; }
        public int? InvoiceDeviceId { get; set; }
        public string InvoiceItem { get; set; }
        public int? InvoiceQuantity { get; set; }
        public decimal? InvoicePrice { get; set; }
        public decimal? InvoicePriceAmount { get; set; }
        public decimal? InvoiceSubTotal { get; set; }
        public decimal? InvoiceItemTax { get; set; }
        public decimal? InvoiceTotalTax { get; set; }
        public decimal? InvoiceSeniorCitizenPwddiscount { get; set; }
        public decimal? InvoiceHolidayDiscount { get; set; }
        public decimal? InvoiceTotal { get; set; }
        public decimal? InvoicePayment { get; set; }
        public decimal? InvoiceChange { get; set; }
        public TimeSpan? InvoiceTime { get; set; }
        public string InvoiceMonth { get; set; }
        public string InvoiceDay { get; set; }
        public string InvoiceYear { get; set; }
        public DateTime? InvoiceDate { get; set; }
        public string InvoiceOrderCommand { get; set; }
        public string InvoiceCashier { get; set; }
        public TimeSpan? InvoiceTimeToCook { get; set; }
        public int? InvoiceOrderReferenceNumber { get; set; }
    }
}
