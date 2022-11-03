using System;
using System.Collections.Generic;

namespace FoodMenuAPITest.Models
{
    public partial class DailySalesTable
    {
        public string Dsdate { get; set; }
        public string Dstime { get; set; }
        public string Dsitem { get; set; }
        public int? Dsquantity { get; set; }
        public double? Dstax { get; set; }
        public double? Dspayment { get; set; }
        public double? Dstotal { get; set; }
        public double? Dschange { get; set; }
        public int? DsdeviceId { get; set; }
        public int DstransactionNumber { get; set; }
    }
}
