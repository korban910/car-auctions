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

### Remove

Inside the `infra/K8S`:
```
kubectl delete -f postgres-depl.yml
kubectl delete -f local-pvc.yml
```