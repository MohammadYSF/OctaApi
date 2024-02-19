# OctaApi

OctaApi , is the API for the Octa application ( octa is an autoservice-management application)

## Features

- Customer management
- vehicle management
- inventory item management
- service management
- buy invoice management
- sell invoice management
- reportinng 
- ...
## Architecture

CQRS is used for this API (physical CQRS) . Therefor , We have two API's . One for Command , and one for Query . 
In Both API's , we have used Clean-Architecture.
In Command API , we have used DDD (Domain-Driven-Design) for the Domain layer . Talking about data storage and persistence ; We persist the latest version of out write data model into the database  . Also ; we are raising events and publishing them message bus . so that other microservices ( like octa authentication service , or like the QueryApi) can listen for this events and do anything if they want. 

## Concepts Used
- DDD
- Clean Architecture
- CQRS
- Microservice architecture
- Mediator pattern
- Strategy pattern
- Repository pattern
- SOLID
- TDD
- Service communication
- Unit Testing
- Integration Testing
- Api Testing

## Technologies Used

- C# , .NET , MediateR , FluentValidation,FluentAssertion , XUnit,Moq, ...
- RabbitMQ
- Redis
- EF Core
- PostgreSQL

## Getting Started

### Prerequisites

- .NET SDK (8 , 7)
- Docker

### Installation

```bash
git clone https://github.com/yourusername/yourproject.git
