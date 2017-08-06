# Shopping Cart

### Requirements
* .NET Core 1.1

### Run App
* cd ShoppingCart
* dotnet restore
* dotnet run

### Run Unit Tests
* cd ShoppingCart.UnitTests
* dotnet restore
* dotnet test

### Run Integration Tests
* cd ShoppingCart.IntegrationTests
* dotnet restore
* dotnet test

### Additional Features (given more time)
* Use real database (rather than in memory) for app and integration tests
* Provide message to client on errors (eg invalid user id, invalid item id, out of stock etc)
* Cover more exhaustive list of cases in unit and integration tests
* Add authenticiation
* Add constraints in db to prevent stock falling below 0
* Use event sourcing architecture for orders

### Why C# / .NET Core
* Experienced with C#
* Role is C# focused
* Easy to set up web api quickly with .NET Core

### Assumptions
* Only one user in system at a time - could be race conditions on check out

### Time Spent
* 4 hours
