
# Shopping Trolley

## Functional Overview
This is an implementation of Shopping trolley API for a customer that supports the following features:
- Add an item to the shopping trolley
- Removing an item to the shopping trolley
- View the items in the shopping trolley including total price and discounted price

## Design considerations
- To express that a shopping cart _may not_ exist without a customer entity, customerId is an required attribute for adding or removing items from a shopping cart while this could have been done simply by supplying the shopping cart id in realistic scenario

## Demo
The application has been deployed to an AWS ECS cluster behind AWS ALB.
Open API docs available here: http://shopping-trolley-alb-89335762.ap-southeast-2.elb.amazonaws.com/index.html
### Usage
In order to use the api, we would need to seed the data via the following command
```
curl --location --request POST 'http://shopping-trolley-alb-89335762.ap-southeast-2.elb.amazonaws.com/api/SeedData' \
--header 'Content-Type: application/json' \
--data-raw '{}'
```
example payload response: 
```
{
    "productIds": [
        1,
        2,
        3,
        4
    ],
    "customerIds": [
        "84972bea-808f-4d7e-81cd-f8e9fae93221",
        "b2c0e594-2968-4cd0-aa46-a90ab0a2cde4",
        "bd029fa9-ae39-4ecc-8900-1ee78c90a90c",
        "dacbe782-faf8-4759-aa30-146991cfabd8",
        "422d360a-ce07-46ec-a010-3b408ef185cf"
    ]
}
```

## Monitoring
Monitoring dashboard available: [here](https://cloudwatch.amazonaws.com/dashboard.html?dashboard=Shopping-Trolley-Dashboard&context=eyJSIjoidXMtZWFzdC0xIiwiRCI6ImN3LWRiLTEwNTIxNDAyNTIwOSIsIlUiOiJ1cy1lYXN0LTFfa0xjeWxINVhJIiwiQyI6IjNob2tvZWhudmJhbGtzZWJsNWpnamdlZjhhIiwiSSI6InVzLWVhc3QtMToyNzE4N2QzMS0yODY3LTRlOTktYjE4NC04MTQwNjA2MDUwOTkiLCJNIjoiUHVibGljIn0=)

## Technical Overview
The solution follows a Domain Driven Design ([DDD](https://docs.microsoft.com/en-us/dotnet/architecture/microservices/microservice-ddd-cqrs-patterns/ddd-oriented-microservice)) and is decoupled into the following layers:
- Application
- Domain
- Infrastructure
- API

Unit tests have been added for:
- Commands
- Command Validation

Integration test have been added for the API controllers

Build pipeline has been put in place for every code push for demonstration purpose
