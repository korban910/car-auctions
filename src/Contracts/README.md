# Contracts

[← Back to Home](../../README.md)

### Create Project from Scratch

inside the `root` folder of the `car-auction` project:

```
dotnet new webapi -o src/SearchService -controllers
```

### Packages

inside the `SearchSerivce`:

```
dotnet add package Scalar.AspNetCore
dotnet add package Ardalis.SmartEnum
dotnet add package Mongodb.Entities
dotnet add package AutoMapper
dotnet add package Microsoft.Extensions.Http.Polly
dotnet add package MassTransit.RabbitMQ
```

### Run the project

inside the `SearchService` folder:

```
dotnet watch
```

open the [link](http://localhost:7002/scalar/v1).

### Common Error

If your program does not run or gives some unreasonable errors:

```
dotnet clean
rm -rf bin/ obj/
dotnet restore
```
