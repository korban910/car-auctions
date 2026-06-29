# Network

### DNS

Go to any `dns` server to register the domain name. For instance, in this case `car-auctions.store`. Pay attention to trap **auto-renew**.

In the **Nameservers**, select **Custom DNS** as:

```
ns1.digitalocean.com
ns2.digitalocean.com
ns3.digitalocean.com
```

### Networking

In the **digital ocean**:
 1. Domain
  1.1 Enter Name: car-auctions.store
  1.2 Add Domain
 2. Create new record
  2.1 HOSTNAME: @
  2.2 WILL DIRECT TO: *loadbalancer* vm
  2.3 TTL(seconds): 30
  2.4 Create Record
 3. CNAME
  3.1 HOSTNAME: app
  3.2 IS AN ALIAS OF: @
  3.3 TTL(seconds): 30
  3.4 Create Record
 4. CNAME
   4.1 HOSTNAME: api
   4.2 IS AN ALIAS OF: @
   4.3 TTL(seconds): 30
   4.4 Create Record
5. CNAME
   5.1 HOSTNAME: id
   5.2 IS AN ALIAS OF: @
   5.3 TTL(seconds): 30
   5.4 Create Record
 6. A
  6.1 HOSTNAME: workaround
  6.2 WILL DIRECT TO: *loadbalancer* vm
  6.3 TTL(seconds): 30
  6.4 Create Record