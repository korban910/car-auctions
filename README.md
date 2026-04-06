# car-auctions

### Create Project

inside the `root` folder:

```
dotnet new sln
dotnet new gitignore
```
### ClassLib
[Contacts](src/Contracts/README.md)

### Microservices

[Auction Service Details](src/AuctionService/README.md)
[Search Service Details](src/SearchService/README.md)

### Add Projects into Solution

inside the `root` folder:

```
dotnet sln add src/AuctionService
dotnet sln add src/SearchService
dotnet sln add src/Contracts
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
docker compose up -d
```