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
docker push korban910/gateway-svc:latest
docker push korban910/identity-svc:latest
docker push korban910/web-app:latest 
```

### Useful Minikube

The default minikube start uses a profile literally named minikube. To create a separate instance for another project, just add -p (or --profile):

#### This project

minikube start -p car-auctions

#### A different project, totally isolated

minikube start -p other-project

```
minikube status   -p car-auctions
minikube stop     -p car-auctions
minikube delete   -p car-auctions      # removes only this instance
minikube dashboard -p car-auctions
minikube ip       -p car-auctions
```

### Current Context

```
minikube profile list
kubectl config current-context
```
