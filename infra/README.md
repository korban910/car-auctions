# K8S

[← Back to Kubernetes](../K8S.md)

### Minikube
```
minikube start
minikube dashboard
```

### Create

Inside the `infra/K8S`:
```
kubectl apply -f local-pvc.yml
kubectl apply -f postgres-depl.yml
```

### Forward

```
kubectl port-forward svc/postgres-np 30001:5432 &
```

### Remove

Inside the `infra/K8S`:
```
kubectl delete -f postgres-depl.yml
kubectl delete -f local-pvc.yml
```

### Useful commands

```
kubectl get pods
kubectl get deployments
kubectl decribe pod <NAME>
kubectl get services
```

In above `<NAME>` would be from results of `kubectl get pods`.