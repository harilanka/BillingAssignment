using System;
namespace MyComp.Core.Models
{
    public class Invoice
    {
        public string ShipmentId { get; set; }
        public double BaseCharge { get; set; }
        public double Surcharge { get; set; }
        public double Discount { get; set; }
        public double FinalCharge { get; set; }
    }
}

