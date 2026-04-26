# Tests

[← Back to Home](../README.md)

### Create Project from Scratch

inside the `root` folder of `car-actions`:

```
dotnet new xunit -o tests/AuctionService.UnitTests
dotnet sln add tests/AuctionService.UnitTests
```

### Build and Run
inside the `root` folder of `car-auctions`:

```
dotnet build
dotnet test
```