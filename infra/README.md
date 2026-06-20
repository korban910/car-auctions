# K8S

[LocalHost issues](./LOCALHOST.md)

[Docker Hub](./DOCKERHUB.md)

[← Back to Kubernetes](../K8S.md)

### Minikube

```
minikube start -p car-auctions --ports=30001:30001,30002:30002,30003:30003
minikube dashboard -p car-auctions &
```

### Create

Inside the `infra/K8S`:

```
kubectl apply -f local-pvc.yml
kubectl apply -f postgres-depl.yml
kubectl apply -f rabbit-depl.yml
kubectl apply -f mongo-depl.yml
set -a; . ./.env; set +a
envsubst < config.yml | kubectl apply -f -
kubectl apply -f auction-depl.yml
kubectl apply -f search-depl.yml
kubectl apply -f bid-depl.yml
kubectl apply -f notify-depl.yml
kubectl apply -f gateway-depl.yml
kubectl apply -f identity-depl.yml
kubectl apply -f web-app-depl.yml
```

Running after modify `config.yml` or `.env`:

```
set -a; . ./.env; set +a          # ← loads .env (what you asked for)
envsubst < config.yml | kubectl apply -f -   # ← this replaces "kubectl apply -f config.yml"
```

### Restart

```
kubectl rollout restart deployment auction-svc
kubectl rollout restart deployment search-svc
kubectl rollout restart deployment bid-svc
kubectl rollout restart deployment notify-svc
kubectl rollout restart deployment gateway-svc
kubectl rollout restart deployment identity-svc
```

### Forward

If `minikube start --ports` still does NOT work, run below:

```
kubectl port-forward svc/postgres-np 30001:5432 &
kubectl port-forward svc/rabbit-np 30002:15672 &
kubectl port-forward svc/mongo-np 30003:27017 &
```

### Remove

Inside the `infra/K8S`:

```
kubectl delete -f postgres-depl.yml
kubectl delete -f rabbit-depl.yml
kubectl delete -f mongo-depl.yml
kubectl delete -f local-pvc.yml
kubectl delete -f auction-depl.yml
kubectl delete -f search-depl.yml
kubectl delete -f bid-depl.yml
kubectl delete -f notify-depl.yml
kubectl delete -f gateway-depl.yml
kebectl delete -f identity-depl.yml
```

### Useful commands

```
kubectl get pods
kubectl get deployments
kubectl decribe pod <NAME>
kubectl get services
kubectl get pvc
```

In above `<NAME>` would be from results of `kubectl get pods`.

### IP URL

```
minikube service postgres-np --url -p car-auctions
minikube service rabbit-np --url -p car-auctions
minikube service mongo-np --url -p car-auctions
```

Above sample output `http://127.0.0.1:<PORT_NUMBER>`, instead of `nodePort`, `<PORT_NUMBER>` could be used. Alternative to `port-forward`.

### Claude

Set `version` to 1.35.1.

```
minikube config set kubernetes-version v1.35.1
minikube delete && minikube start --kubernetes-version=v1.35.1 --ports=30001:30001,30002:30002,30003:30003
```

Above `config set` sets the version.
