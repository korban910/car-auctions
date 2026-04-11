# IdentityService

[← Back to Home](../../README.md)

### Create Project from Scratch

inside the `root` folder of the `car-auctions` project:

```
dotnet new duende-is-aspid -o src/IdentityService
```

### Packages

inside the `IdentityService`, (optional) remove sqlite package:

```
dotnet add package Npgsql.EntityFrameworkCore.PostgreSQL
```

### Run the project

inside the `IdentityService` folder:

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
