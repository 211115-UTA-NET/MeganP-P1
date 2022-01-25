# MeganP-P1

# Overview
This project is a store inventory managment system. There are products, customers and stores. 
As a customer purchases products from a store location the inventory for that store location is updated.

# Technologies Used
.NET 6
ADO.NET
C#
Azure Web Services
SQL Server
SQL Server Managment Studio
RESTful API
HTTP

# Features
* interactive console application with a REST HTTP API backend
* place orders to store locations for customers
* add a new customer
* search customers by name
* display details of an order
* display all order history of a store location
* display all order history of a customer
* input validation (in the console app and also in the server)
* exception handling, including foreseen SQL and HTTP errors
* persistent data; no prices, customers, order history, etc. hardcoded in C#
* use ADO.NET (not Entity Framework)
* use ASP.NET Core Web API
* use an Azure SQL DB in third normal form
* have a SQL script that can set up the database from scratch
* don't use public fields
* define and use at least one interface
* best practices: separation of concerns, OOP principles, SOLID, REST, HTTP
* XML documentation
* the API should own the business logic of the application, not just the data access logic
* doesn't trust that the console app hasn't been tampered with
* able to handle multiple instances of the console app connecting to it at the same time
* use dependency injection for controller dependencies
* separate different concerns into different classes
* use repository pattern for data access
* can contain multiple kinds of product in the same order
* rejects orders with unreasonably high product quantities
* (optional: some additional business rules, like special deals)
* has an inventory
* inventory decreases when orders are accepted
* rejects orders that cannot be fulfilled with remaining inventory
* (optional: for at least one product, more than one inventory item decrements when ordering that product)
* the console app provides a UI, interprets user input, uses the REST API over HTTP, and formats output
* should gracefully handle HTTP error codes from the server, as well as connection errors
* separate different concerns into different classes
* recommended to separate the connection to the API into a separate project
* recommended to keep the console app project for only console interface concerns, not HTTP concerns
* at least 10 test methods

# set up
the API is already deployed on Azure so you just have to download the client and either use dotnet run or if in visual studio the run button will work too.

# Contributors
Megan Postlewait
