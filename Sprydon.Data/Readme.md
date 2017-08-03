# Sample MVC and API Application

## Building the Solution

The solution was created in Visual Studio 2017 Enterprise but should be possible to build with all versions

There is a single solution - Sprydon.data.sln

The application targets  .Net 4.6.2

## Components

Sprydon.Data - persistance layer using Entity Framework 6 - assumes a local SQL Express and will create the database on first run

Sprydon.Api - Web API OData Rest API - see connection string in web config

Sprydon.Portal - Web portal based on the MVC Boilerplate - does not access the database directly but via the API layer

Unit Tests - starter unit test project using in memory ef db context

Canopy Tests - integration tests using F# and Selenium

## Components used

Bootstrap
Autofac
Effort
Elmah
Entity Framework
Glimpse
Identity Server 3
NWebSec
OData
XUnit

