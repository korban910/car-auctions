# SSL

[Certificate](https://cert-manager.io/)

[Reference](https://cert-manager.io/docs/tutorials/acme/nginx-ingress/)

### Installation

```
kubectl apply -f https://github.com/cert-manager/cert-manager/releases/download/v1.20.3/cert-manager.yaml
```

In **prod-k8s** folder, create `staging-le.yml` file. Inside the `infra/prod-k8s`:

```
kubectl apply -f staging-le.yml
kubectl apply -f ingress-svc.yml
```

### Troubleshooting

```
kubectl get certificates
kubectl get secrets
kubectl get CertificateRequest
kubectl get orders
kubectl get Challenges
```

```
kubectl describe certificate <CERT_NAME>
kubectl describe secret <SEC_NAME>
kubectl describe CertificateRequest <CertificateRequest_NAME>
kubectl describe order <ORDERS_NAME>
kubectl get challenge <CHALLENGES_NAME>
```

### Production

Inside `infra/k8s`, create `prod-le.yml` file. 

```
kubectl apply -f prod-le.yml
```