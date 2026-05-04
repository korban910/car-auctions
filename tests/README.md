# Tests

[← Back to Home](../README.md)

### Create Project from Scratch

inside the `root` folder of `car-actions`:

```
dotnet new xunit -o tests/AuctionService.UnitTests
dotnet new xunit -o tests/AuctionService.IntegrationTests
dotnet new xunit -o tests/SearchService.IntegrationTests
```

```
dotnet sln add tests/AuctionService.UnitTests
dotnet sln add tests/AuctionService.IntegrationTests
dotnet sln add tests/SearchService.IntegrationTests
```

### Build and Run
inside the `root` folder of `car-auctions`:

```
dotnet build
dotnet test
```