# Billing Assignment

## Overview

This project leverages the **Microsoft Rules Engine** to interpret business rules for billing. Rules are defined in `Rules/shipment_rules.json` and can be customized or modified at runtime without redeployment. This approach allows for flexible and maintainable business logic.

---

## Assumptions

- If `ClientCategory` is missing in the input, no discount is applied.
- Both **Express Charge** and **Fragile Charge** are considered as **Surcharge**.

---

## Notes

- **Time taken:** ~2 hours to integrate the Microsoft JSON-based rules engine.
- Rulesets can be stored in a database in the future, enabling updates by modifying a single row.
- Followed simple and best-practice design strategies.
- Unit tests can be included in future if required.

---

## Sample Input
```json

        [
          {
            "ShipmentId": "SHP001",
            "WeightKg": 200,
            "DistanceKm": 150,
            "IsExpress": true,
            "IsFragile": false,
            "ClientCategory": "Gold"
          },
          {
            "ShipmentId": "SHP002",
            "WeightKg": 75,
            "DistanceKm": 300,
            "IsExpress": false,
            "ClientCategory": "Silver"
          },
          {
            "ShipmentId": "SHP003",
            "WeightKg": 80,
            "DistanceKm": 100,
            "IsExpress": true,
            "IsFragile": true
          }
        ]
```


## Sample Output

```json
[
  {
    "shipmentId": "SHP001",
    "baseCharge": 1300,
    "surcharge": 130,
    "discount": 214,
    "finalCharge": 1216
  },
  {
    "shipmentId": "SHP002",
    "baseCharge": 975,
    "surcharge": 0,
    "discount": 98,
    "finalCharge": 877
  },
  {
    "shipmentId": "SHP003",
    "baseCharge": 600,
    "surcharge": 210,
    "discount": 0,
    "finalCharge": 810
  }
]
```
