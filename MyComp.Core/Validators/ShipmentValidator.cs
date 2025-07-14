using System;
using MyComp.Core.Models;
using System.Collections.Generic;
using System.Text.Json;
using System.Threading.Tasks;
using System.IO;
using System.Text;
using MyComp.Core.Interfaces;
using MyComp.Core.Services;

namespace MyComp.Core.Validators
{
    /// <summary>
    /// This can be further improved with fluent validations
    /// </summary>
    public class ShipmentValidator
    {
        private readonly IJsonConverterService _jsonConverter = new JsonConverterService();

        public async Task<List<Shipment>> ParseAndValidateAsync(string shipmentJson)
        {
            var shipments = await _jsonConverter.DeserializeAsync<List<Shipment>>(shipmentJson);

            if (shipments == null || shipments.Count == 0)
                throw new ArgumentException("Shipment list is empty");

            foreach (var shipment in shipments)
            {
                if (string.IsNullOrEmpty(shipment.ShipmentId))
                    throw new ArgumentException("ShipmentId cannot be null or empty");

                if (shipment.WeightKg <= 0)
                    throw new ArgumentException($"Invalid WeightKg for shipment {shipment.ShipmentId}");

                if (shipment.DistanceKm <= 0)
                    throw new ArgumentException($"Invalid DistanceKm for shipment {shipment.ShipmentId}");
            }

            return shipments;
        }
    }
}
