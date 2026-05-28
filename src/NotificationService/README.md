# NotificationService

[← Back to Home](../../README.md)

### Create Project from Scratch

inside the `root` folder of the `car-auctions` project:

```
dotnet new web -o src/NotificationService
```

### Packages

inside the `NotificationService`:

```
dotnet add package MassTransit.RabbitMQ
dotnet add reference ../src/Contracts
```

### Run the project

inside the `NotificationService` folder:

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

### Docker
inside the `AuctionService` folder:
```
docker build -f src/AuctionService/Dockerfile -t test-auction-service .
docker run test-auction-service
```
