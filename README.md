# car-auctions

[About Project](./PROJECT.md)

This is the repository for `Car Auctions` written in back-end .NET 10 and Front-end Nextjs 16.

You can run this app locally by following these instructions:

1. clone the repo

```
git clone git@github.com:korban910/car-auctions.git
```

2. Change into the `car-auctions` directory

```
cd car-cities
```

3. Ensure you have Docker installed. For details click [here](https://docs.docker.com/desktop/).

4. Build the services locally (NOTE: this may take some time)

```
docker compose -f docker-compose.local.yml build
```

5. For SSL cerficiate, please follow [CERTIFICATE](./devcerts/README.md).

6. Run the services:

```
docker compose -f docker-compose.local.yml up -d
```
