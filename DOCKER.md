# Docker

[← Back to Home](./README.md)

### Build docker services:

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
docker compose build web-app
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

### Docker Run

For `docker-compose.yml`:

```
docker compose build docker-compose.yml --no-catch --pull
docker compose up -d
docker compose down -v
```

For `docker-compose.local.yml`:

```
docker compose -f docker-compose.local.yml build --no-catch --pull
docker compose -f docker-compose.local.yml up -d
docker compose -f docker-compose.yml down -v
```

### SSL (ONLY for NOT LOCAL)

[mkcert](./devcerts/README.md)