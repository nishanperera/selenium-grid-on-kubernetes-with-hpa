## Selenium Grid on Kubernetes with Auto scaling nodes

Setting up a Selenium Grid with single Chrome node and auto scale number of nodes depend on node CPU usage.

## Prerequisites 
* Docker desktop with kubernetes
* Enable Metrics Server for Kubernetes on Docker Desktop 
    https://blog.codewithdan.com/enabling-metrics-server-for-kubernetes-on-docker-desktop/


## Setup Selenium hub

```
kubectl apply -f hub-deployment.yaml
```

Create service to access Selenum hub externally

```
kubectl apply -f hub-service.yaml
```

Access hub using following requests

http://localhost:30001/wd/hub/status

http://localhost:30001/grid/api/hub

## Create Hub configurations

```
kubectl apply -f hub-config.yaml
```

## Create Chrome node 

```
kubectl apply -f chrome-node-deployment.yaml
```

To view the node http://localhost:30001/grid/console

## Setup Pod Autoscaler

```
kubectl autoscale deployment chrome-node-rc --cpu-percent=40 --min=1 --max=5

```

```
kubectl get hpa -w
````


### Get Resources
kubectl exec selenium-hub-6459dd965b-9jt57 -- ps aux
kubectl exec chrome-node-rc-6b44d5855d-mw7bw -- ps aux

### inspect the selenium config file.
kubectl exec selenium-hub-3216163580-7pqtx -- cat /opt/selenium/config.json

### see if selenium is really listening on port 4444.
kubectl exec selenium-hub-6459dd965b-9jt57 -- wget 127.0.0.1:4444 -O -



### links
https://github.com/kubernetes-sigs/metrics-server


