# HotelApp
This repository contains 2 separate projects: ASP.NET Core API and ASP.NET Core + Angular + TypeScript. Also it has been deployed on Azure and GCP throught CI/CD pipelines. The project represents a basic hotel app for making reservations. There is still work to be done on the application, it is not mobile friendly yet.

Preview https://hp-weu-hotelappweb-dev-01.azurewebsites.net/ 

## Technical expectations
* Database created with EntityFramework Core
* JWT token for login, registration, authorization and authentication
* For model validation FluentValidation is used
* AutoMapper for data transfer
* Logging using Serilog package
* Added a global exception handler, soft delete, paging, integration with an external system...

## Use cases
* Registration
* Login
* Hotel registration and editing info
* Adding and editing hotel rooms
* Hotel approval and rejection
* Room search and reservation
* Cancellation of reservation
* Reservation processing
* Creating and deleting Administrators
* Configuration editing
