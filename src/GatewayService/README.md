# GateService

[← Back to Home](../../README.md)

### YARP
Yet another reverse proxy

### Create Project from Scratch

inside the `root` folder of the `car-auctions` project:

```
dotnet new web -o src/GatewayService
```

### Packages

inside the `GateService`:

```
dotnet add package Microsoft.AspNetCore.Authentication.JwtBearer
dotnet add package Yarp.ReverseProxy
```

### Run the project

inside the `GateService` folder:

```
dotnet watch
```

open the [link](http://localhost:6001).

### Common Error

If your program does not run or gives some unreasonable errors:

```
dotnet clean
rm -rf bin/ obj/
dotnet restore
```
