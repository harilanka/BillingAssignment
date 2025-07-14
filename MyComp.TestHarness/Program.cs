using System.Xml;
using MyComp.Core.Interfaces;
using MyComp.Core.Models;
using MyComp.Core.Services;
using System.Text.Json;

class Program
{
    static async Task Main()
    {
        IBillingService billingService = new BillingService();

        var shipmentInput = @"
        [
          {
            ""ShipmentId"": ""SHP001"",
            ""WeightKg"": 200,
            ""DistanceKm"": 150,
            ""IsExpress"": true,
            ""IsFragile"": false,
            ""ClientCategory"": ""Gold""
          },
          {
            ""ShipmentId"": ""SHP002"",
            ""WeightKg"": 75,
            ""DistanceKm"": 300,
            ""IsExpress"": false,
            ""ClientCategory"": ""Silver""
          },
          {
            ""ShipmentId"": ""SHP003"",
            ""WeightKg"": 80,
            ""DistanceKm"": 100,
            ""IsExpress"": true,
            ""IsFragile"": true
          }
        ]";

        var invoices = await billingService.GenerateInvoicesAsync(shipmentInput);

        Console.WriteLine(invoices);
    }
}