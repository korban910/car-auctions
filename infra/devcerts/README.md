# mkcert

[← Back to K8S](../DOCKER.md)

Go to [mkcert](https://github.com/filosottile/mkcert) to follow `mkcert` install.

```
mkcert -install
```

Use `sudo vi /etc/hosts`:
![Hosts](../images/hosts.png)

Inside `devcerts` folder:

```
mkcert -key-file server.key -cert-file server.crt app.car-auctions.local api.car-auctions.local id.car-auctions.local
```

Once `server.crt` and `server.key` is generated, run:

```
kubectl create secret tls car-auctions-app-tls --key server.key --cert server.crt
```

Verify `secrets`:

```
kubectl get secrets
```

Output should be similar to:

```
NAME                   TYPE                DATA   AGE
car-auctions-app-tls   kubernetes.io/tls   2      23s
```