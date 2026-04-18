# AuctionService

[← Back to Home](../../README.md)

### Create Project from Scratch

inside the `root` folder of the `car-auctions` project:

```
dotnet new webapi -o src/AuctionService -controllers
```

### Packages

inside the `AuctionService`:

```
dotnet add package Scalar.AspNetCore
dotnet add package Ardalis.SmartEnum
dotnet add package Microsoft.EntityFrameworkCore.Design
dotnet add package Npgsql.EntityFrameworkCore.PostgreSQL
dotnet add package AutoMapper
dotnet add package MassTransit.RabbitMQ
dotnet add package MassTransit.EntityFrameworkCore
dotnet add package Microsoft.AspNetCore.Authentication.JwtBearer
dotnet add reference ../src/Contracts
```

### Run the project

inside the `AuctionService` folder:

```
dotnet watch
```

open the [link](http://localhost:7001/scalar/v1).

### Migrations

inside the `AuctionService` folder:

```
dotnet ef migrations add "InitialCreate" -o Migrations
dotnet ef database update
dotnet ef database drop
```

### Common Error

If your program does not run or gives some unreasonable errors:

```
dotnet clean
rm -rf bin/ obj/
dotnet restore
```
