# car-auctions

### Create Project

inside the `root` folder:

```
dotnet new sln
dotnet new gitignore
```

### Microservices

[Auction Service Details](src/AuctionService/README.md)

### Add Projects into Solution

inside the `root` folder:

```
dotnet sln add src/AuctionService
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
