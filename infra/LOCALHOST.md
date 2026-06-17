# LocalHost

[← Back to K8S](./README.md)

There is no way to make localhost:PORT work purely from the YAML. Binding a port on your host's localhost (127.0.0.1) is a host-machine concern. Your manifests only describe what happens inside the cluster — Services, NodePorts, etc. all live on the minikube node, which (with the docker driver) is a container with its own IP, not your laptop. So reaching it from the host always needs something on the host side: port-forward, minikube tunnel, or minikube service.

So the goal isn't "do it in YAML" — it's "pick the host-side mechanism that needs the least manual fiddling." Here are the real options, best first:

## Option 1 — minikube tunnel + type: LoadBalancer (closest to declarative)

Change your *-np services from NodePort to LoadBalancer, then run one background process that handles all of them at once:

minikube tunnel        # one process, covers every LoadBalancer service

With the docker driver, your LoadBalancer services then become reachable on 127.0.0.1 at their port:. One command instead of N port-forwards, and the port mapping lives in the YAML. Downside: minikube tunnel needs sudo and must stay running.

## Option 2 — minikube service (no port-forward, keeps NodePort)

Keep your NodePort services exactly as they are and run:

minikube service rabbit-np --url      # prints a 127.0.0.1:<random-port> URL and tunnels it

With the docker driver this opens a tunnel and gives y the local port is random each run, so it's not a fixed
30002.

## Option 3 — connect to the minikube IP directly (zero e

Keep NodePort, don't tunnel anything, just connect to

minikube ip            # e.g. 192.168.49.2 # then: 192.168.49.2:30002 for rabbit mgmt, etc.

No localhost, but no background process either. Note: r gives the node a routable IP. The docker driver on
its default network often does not route to NodePorts exactly why your nodePort "doesn't work" and you fell
back to port-forward.

## Option 4 — keep port-forward, but make it one command

If you like port-forward's fixed localhost ports, justot manual each time. A Makefile in infra/K8S/:

forward:
kubectl port-forward svc/postgres-clusterip 5432
kubectl port-forward svc/rabbit-np 15672:15672 &
kubectl port-forward svc/rabbit-clusterip 5672:5
kubectl port-forward svc/mongo-... 27017:27017 &
wait

Then make forward once. Stable ports, lives in the repo.

## My recommendation

For local dev where your services (Postgres/Rabbit/Mone the cluster by your other pods, you don't actuallyneed host access at all — pods reach each other via thst access is only for you (a DB client, the RabbitMQmanagement UI in a browser).

Given that, I'd go with Option 1 (minikube tunnel + Loclarative and stable, or Option 4 (Makefile) if you
want fixed ports and zero minikube quirks. Both are "s

Want me to convert your *-np services to LoadBalancer,the port-forwards wired up? Tell me which driver you're on (minikube profile list shows it) and I'll tailor it