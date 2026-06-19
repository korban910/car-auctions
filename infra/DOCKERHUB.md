# Docker Hub

[← Back to K8S](./README.md)

### Build Images

In the `root` folder of the project:

```
docker compose build --no-cache --pull
```

### Push

Push the images into `dockerhub`:

```
docker push korban910/auction-svc:latest
docker push korban910/search-svc:latest
docker push korban910/bid-svc:latest 
docker push korban910/notify-svc:latest 
```