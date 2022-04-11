# wcf-netnamedpipe-container
Shows how an external facing WCF service hosted inside windows container can call other service via netnamedpipe binding.

# What it demos

- We can host netNamedPipe binding inside Windows container.
- IIS running inside Windows container can host WCF services using netNamedPipe binding.
- A service/process hosted in Windows container can call another service hosted in same container using netNamedPipe binding.

# Getting started

## CI outside of Docker build
Here we need to compile the application outside of Docker build. The Dockerfile only has the instructions to package binaries.

- Clone the repo and compile the solution
- Run the below command from root folder (the folder where .sln file resides) to build container image.
  - `docker build -t wcf -f Dockerfile.combined.dev .`
- To run container give command `docker run --name testwcf -p 82:80 -d wcf:latest`
- Navigate to [http://localhost:82/FrontEndWCFService/FrontEndService.svc/areaOf/3/delay/1](http://localhost:82/FrontEndWCFService/FrontEndService.svc/areaOf/3/delay/1) to see it in action

## CI via Docker build using SDK container. 
Here we just issue the Docker build command that will copy source to SDK container, compile the application inside of SDK container. Then the runtime imaage is prepared by copying binaries.

- Clone the repo and give the below command from root folder (the folder where .sln file resides) to build container image.
  - `docker build -t wcf -f Dockerfile.combined.devops .`
- To run container give command `docker run --name testwcf -p 82:80 -d wcf:latest`
- Navigate to [http://localhost:82/FrontEndWCFService/FrontEndService.svc/areaOf/3/delay/1](http://localhost:82/FrontEndWCFService/FrontEndService.svc/areaOf/3/delay/1) to see it in action

# Prerequisite

- Docker environment with Windows containers support. Better Docker Desktop.
- Basic knowledge about containers.

# Points to note

- The base WCF container that is provided by MSFT is not enabled with netNamedPipeBinding. We need to enable ourselves.
- While enabling the net.pipe binding, we need to set the IIS bindings at site level and protocols enablement at Web Application level.

# Design notes
- The Docker image is not layered to include internal service in base image and frontend WCF service in other container. It is advised to have a base container that has only internal service and give to other teams that develop front end services.
- No side car pattern at container level - It uses service to host helping component.  

## Hosting your own base container

Currently the application image uses a base container (that enabled netNamedPipe in IIS) that is hosted under user [joymon](https://hub.docker.com/r/joymon/wcfnetnamedpipe). If you want to host the base image, please
- Build the [Dockerfile.base](Dockerfile.base)
- Push to your own docker hub repo or ACR
- Change the Dockerfile.combined.* images to use your newly pushed image. 



