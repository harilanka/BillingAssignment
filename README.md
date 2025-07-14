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

---

## Futher Extensions

- Unit tests can be included in future if required.
- Singleton on the JSON Serializer can be implemented and can leverage DI.
- Better FluentValidations would help in validating Models
- This solution doesnt follow 100% DDD (Domain Driven Design) since its a simple use case. If a real logistics solution is implemented, a better way to implement would be a Domain layer that will have entities and their sets linked with multple bounded contexts, Application layer will have all the business logic and presentation layer will have basic DTO operations and some input validations and transformations. This can be nearer step for Clean Architecture.
- Caching of rules can improve performance instead of everytime reading from disc

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
