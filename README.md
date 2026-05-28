# car-auctions

### Create Project

inside the `root` folder:

```
dotnet new sln
dotnet new gitignore
dotnet new install Duende.Templates
```

### ClassLib

[Contacts](src/Contracts/README.md)

### Microservices

[Auction Service Details](src/AuctionService/README.md)

[Search Service Details](src/SearchService/README.md)

[Identity Service Details](src/IdentityService/README.md)

[Gateway Service Details](src/GatewayService/README.md)

[Bid Service Details](src/BiddingService/README.md)

[Notification Service Details](src/NotificationService/README.md)

### Add Projects into Solution

inside the `root` folder:

```
dotnet sln add src/AuctionService
dotnet sln add src/SearchService
dotnet sln add src/Contracts
dotnet sln add src/IdentityService
dotnet sln add src/GatewayService
dotnet sln add src/BiddingService
dotnet sln add src/NotificationService
```

### Mise (Optional but recommended)

install mise from [mise](https://mise.jdx.dev/)

inside the `root` folder:

```
touch .mise.toml
```

content of `.mise.toml`:

```
[env._]
file = ".env"
```

### Linux resource wall

```
sudo sysctl fs.inotify.max_user_instances=512
```

### Run docker services:

inside the `root` folder:

```
docker compose build
```

or

```
docker compose build --no-cache --pull
```

```
docker compose up -d
docker compose up --build -d
docker compose up postgres -d
docker compose up mongodb -d
docker compose up rabbitmq -d
```

or build each manually:

```
docker compose build postgres
docker compose build mongodb
docker compose build rabbitmq
docker compose build auction-svc
docker compose build search-svc
docker compose build identity-svc
docker compose build gateway-svc
```

for removing the containers and clean up volumes:

```
docker compose down
docker volume ls
docker volume rm [volume_name]
```

or

```
docker compose down -v
```

### Tests

[Test Details](tests/README.md)

### Useful Commands

for removing all `bin` and `obj` folders in the project, run the following command in the root folder:
```
find . -type d -name "bin" -exec rm -rf {} +
find . -type d -name "obj" -exec rm -rf {} +
```
