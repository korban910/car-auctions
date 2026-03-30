# AuctionService

[← Back to Home](../../README.md)

### Create Project from Scratch

inside the `root` folder of the `car-auction` project:

```
dotnet new webapi -o src/AuctionService -controllers
dotnet add package Scalar.AspNetCore
```

### Run the project

inside the `AuctionService` folder:

```
dotnet watch
```

open the [link](http://localhost:7001/scalar/v1).

### Common Error

If your program does not run or gives some unreasonable errors:

```
dotnet clean
rm -rf bin/ obj/
dotnet restore
```
