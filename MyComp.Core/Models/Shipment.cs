using System;
using MyComp.Core.Enums;

namespace MyComp.Core.Models
{
	public class Shipment
	{
        public string ShipmentId { get; set; }
        public double WeightKg { get; set; }
        public double DistanceKm { get; set; }
        public bool IsExpress { get; set; }
        public bool IsFragile { get; set; }
        public ClientCategory ClientCategory { get; set; }
    }
}

