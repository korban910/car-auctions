# Digital Ocean

[← Back to K8S](./K8S.md)

### Digital Ocean

Go to [Digital Ocean](https://www.digitalocean.com/) and create an account. Select "Kubernetes" from Pricing then Compute in the menu. In addition, need select Load Balances from Pricing then Networking. Minimum $24/month in total.

### Login
To login, go to [Digital Ocean](https://www.digitalocean.com/) and sign in with your account credentials.

### Setup Project and Kubernetes Cluster

1. + New Project -> Name your project: Car-Auctions -> Create Project
2. Select Car-Auctions under the project, then top right corner, select `Create` then `Kubernetes Cluster`.
 2.1 Choose the region close to yourself
 2.2 Select a version(prefer latest stable)
 2.3 Choose cluster size -> `Fixed size`
 2.4 Node pool name -> car-auctions-np
 2.5 Machine type (Droplet) -> Basic (Regular SSD)
 2.6 Node plan -> $24/month per node (4GB RAM, 2 vCPUs, 80 GB storage) (option to chose different plan)
 2.7 Nodes -> 2
 2.8 Finalize
  2.8.1 Name -> car-auctions
  2.8.2 Project -> Car-Auctions
 2.9 Create Cluster

### Start with Kubernetes

After create cluster, once everything is done, `Create a Kubernetes Cluster` will be done (which is 1st step). Then click `Get Started` to proceed with the Kubernetes dashboard.

First install `doctl`:
```
brew install doctl
```

`Generate New Token ` in the Digital Ocean dashboard with following settings:
- Token Name: car-auctions
- Expiration: 90 days (option to choose different expiration)
- Scope: Full Access
Then click `Generate Token`.

Then copy the token and run the following command:
```
doctl auth init
```
Then enter the token when prompted.

Validate the login by running:
```
doctl account get
```

Copy the command in `Automated (recommended)` and run it to set up the `doctl` cluster kubernetes kubeconfig. Command is similar to:
```
doctl kubernetes cluster kubeconfig save <random-guid>
```

Then run `kubectl config get-contexts` to verify the kubeconfig is set up correctly. Switch between contexts using `kubectl config use-context <context-name>`.
