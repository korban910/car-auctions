# K8S

[LocalHost issues](./LOCALHOST.md)

[Docker Hub](./DOCKERHUB.md)

[← Back to Kubernetes](../K8S.md)

### Minikube
```
minikube start --ports=30001:30001,30002:30002,30003:30003
minikube dashboard &
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
minikube service postgres-np --url
minikube service rabbit-np --url
minikube service mongo-np --url
```
Above sample output `http://127.0.0.1:<PORT_NUMBER>`, instead of `nodePort`, `<PORT_NUMBER>` could be used. Alternative to `port-forward`.

### Claude

```
minikube config set kubernetes-version v1.35.1
minikube delete && minikube start --kubernetes-version=v1.35.1 --ports=30001:30001,30002:30002,30003:30003
```

Above `config set` sets the version.