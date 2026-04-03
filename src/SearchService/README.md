# SearchService

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
```

### Run the project

inside the `SearchService` folder:

```
dotnet watch
```

open the [link](http://localhost:7002/scalar/v1).

### Migrations

inside the `SearchService` folder:

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
