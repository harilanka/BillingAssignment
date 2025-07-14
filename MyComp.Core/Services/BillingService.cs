using System;
using RulesEngine.Models;
using System.Text.Json;
using System.IO;
using MyComp.Core.Models;
using System.Collections.Generic;
using System.Threading.Tasks;
using MyComp.Core.Interfaces;
using MyComp.Core.Validators;
using System.Text.Json.Serialization;

namespace MyComp.Core.Services
{
    public class BillingService : IBillingService
    {
        private readonly RulesEngine.RulesEngine _rulesEngine;
        private readonly ShipmentValidator _validator;
        private readonly IJsonConverterService _jsonConverter;
        public BillingService()
        {
            _jsonConverter = new JsonConverterService();
            var rulesJson = File.ReadAllText("Rules/shipment_rules.json");
            var workflows = JsonSerializer.Deserialize<Workflow[]>(rulesJson, new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true,
                Converters = { new JsonStringEnumConverter() }
            });
            _rulesEngine = new RulesEngine.RulesEngine(workflows);
            _validator = new ShipmentValidator();
        }

        public async Task<string> GenerateInvoicesAsync(string shipmentJson)
        {
            var shipments = await _validator.ParseAndValidateAsync(shipmentJson);
            var invoices = new List<Invoice>();

            foreach (var shipment in shipments)
            {
                var context = new ChargeContext();
                var inputs = new RuleParameter[]
                {
                    new RuleParameter("input1", shipment),
                    new RuleParameter("input2", context)
                };

                context.BaseCharge = await GetWorkflowOutputAsync("ShipmentCharges", "CalculateBase", inputs);
                context.Surcharge =
                    await GetWorkflowOutputAsync("ShipmentCharges", "ExpressSurcharge", inputs) +
                    await GetWorkflowOutputAsync("ShipmentCharges", "FragileSurcharge", inputs);
                context.Discount = Math.Round(
                    await GetWorkflowOutputAsync("ShipmentCharges", "ClientDiscount", inputs)
                );

                invoices.Add(new Invoice
                {
                    ShipmentId = shipment.ShipmentId,
                    BaseCharge = context.BaseCharge,
                    Surcharge = context.Surcharge,
                    Discount = context.Discount,
                    FinalCharge = context.BaseCharge + context.Surcharge - context.Discount
                });

            }

            return await _jsonConverter.SerializeAsync(invoices);
        }
        private async Task<double> GetWorkflowOutputAsync(string workflowName, string ruleName, RuleParameter[] inputs)
        {
            var result = await _rulesEngine.ExecuteActionWorkflowAsync(workflowName, ruleName, inputs);
            return double.TryParse(Convert.ToString(result?.Output), out double value) ? value : 0.0;
        }

    }
}

