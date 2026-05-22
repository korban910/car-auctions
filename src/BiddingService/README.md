#BiddingService

[← Back to Home](../../README.md)

### Create Project from Scratch

inside the `root` folder of the `car-auctions` project:

```
dotnet new webapi -o src/BiddingService -controllers
```

### Packages

inside the `BiddingService`:

```
dotnet add reference ../src/Contracts
```

### Run the project

inside the `BiddingService` folder:

```
dotnet watch
```

open the [link](http://localhost:7001/scalar/v1).

### Migrations

inside the `BiddingService` folder:

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

### Docker
inside the `BiddingService` folder:
```
docker build -f src/BiddingService/Dockerfile -t test-bid-service .
docker run test-bid-service
```
