<h1 align="center">
📄<br>French Amortization API
</h1>

## 📚 About project

> This repository contains an API to calculate a Loan using the French Amortization System, the sole purpose for this project was to learn Kubernetes using minikube. Down below I will add some notes / commands to use in order to get this running on minikube.
---


## 💻 Requirements

Before we start, we need to install:
- Docker
- Minikube

And we must have docker running before executing any commands.

## 🚀 Getting started

To run minikube use this command:

```
minikube start --driver=docker
```

Then we need to set up the dashboard (for this command you need to keep the terminal open):

```
minikube dashboard
```

Then we need to build our docker image:

```
docker image build -t french-amortization-api -f French.Amortization.Api/Dockerfile .
```

Then we need to load our local image onto minikube.

```
minikube image load french-amortization-api
```

Then we need to deploy our app to kubernetes:
```
kubectl apply -f kubernetes/deployment.yml
kubectl apply -f kubernetes/service.yml
kubectl apply -f kubernetes/hpa.yml
```

## ☕ Using our app

We must create a tunnel between minikube and the kubernetes container running on our docker desktop app:

```
minikube service french-amortization-api-service
```


## 📄 Useful notes

To test HPA here's a useful loader:

```
kubectl run -i --tty load-generator --rm --image=busybox:1.28 --restart=Never -- /bin/sh -c "while sleep 0.01; do wget -q -O- http://<INTERAL POD IP HERE>/swagger/index.html; done"
```

To see metrics, run this command: 
```
minikube addons enable metrics-server
```