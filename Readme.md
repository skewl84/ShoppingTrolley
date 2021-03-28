
# Shopping Trolley

## Functional Overview
This is an implementation of Shopping trolley API for a customer that supports the following features:
- Add an item to the shopping trolley
- Removing an item to the shopping trolley
- View the items in the shopping trolley including total price and discounted price

## Demo
The application has been deployed to an AWS ECS cluster behind AWS ALB.
Open API docs available here: http://shopping-trolley-alb-89335762.ap-southeast-2.elb.amazonaws.com/index.html

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
