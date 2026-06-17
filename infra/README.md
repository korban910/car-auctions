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
kubectl port-forward svc/rabbit-np 30002:15672 &
kubectl port-forward svc/mongo-np 30003:27017 &
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
kubectl get pvc
```

In above `<NAME>` would be from results of `kubectl get pods`.

### IP issues

```
minikube service postgres-np --url
```
Above output `http://127.0.0.1:38045` and `30001` would need to change it `38045`.