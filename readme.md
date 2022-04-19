Table booking system is a online restaurant table booking system, which allows customers to book table in the restaurants.

# ASP.NET Core MVC - Table Booking System

### Prerequisites

- Visual Studio 2019
- .Net 5

#### Nuget Packages

AutoMapper
Dapper
MediatR
FluentValidation  
NewtonsoftJSON

### Database

Database Server: SQL Server 2014/MySQL

### Project Architecture

Specify your project architecture here. Suppose you are following Repository pattern then mention your all the projects along with short description here.

- TableBookingSystem.Domain (Conatins database model)
- TableBookingSystem.Persistence (Contains repositories of all the entities)
- TableBookingSystem.Services (Contains business logic)
- TableBookingSystem.Application (contains all abstractions values)
- TableBookingSystem.Web
- TableBookingSystem.Tests (Contains all the test methods)

### Deployement Steps

1. Create virtual directory under IIS
2. Enable Inbound and Outbound port for SQL Server(1433) in Firewall(If required)
3. Create security group in your production and stage server if you are using EC2
4. Update the Database Connection string with Production in appsettings.json
5. Publish your project
6. Move your publish files to the stage/production server

## Versioning

Specify the version history
|Version |Descrption |Other Description|
|----------------|-------------------------------|-----------------------------|
|1.0.0.0|First Version | Other Description |
