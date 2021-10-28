# Vehicle Tracking Api Example
### Features

- N-Tier architecture
- SOLID Design Principles
- Containerized with Docker
- .NET 5.0
- Lightweight, Scalable




### TODO List


- Caching (Redis)
- Loging (Elasticsearch)
- Containerized with Docker
- Ocelot Api Gateway

## Installation

Download the project from github.

```sh
git clone https://github.com/yunusdgntr/vehicle-tracking
cd .\vehicle-tracking\Vehicle.Tracking.WebApi\
```
Change the DB ConnectionString in "appsettings.json-->VehicleTrackMsqlContext" and start the api.
```sh
dotnet run
```
```sh
Browse https://localhost:5001/swagger/index.html
```
You can get token with following payload
```sh
{
  "email": "testadmin@test.com",
  "password": "testpwd!"
}
```
```sh
{
  "email": "testcustomer@test.com",
  "password": "testcustomerpwd!"
}
```
<img  width="600" alt="GIF" src="https://raw.githubusercontent.com/yunusdgntr/vehicle-tracking/develop/token.jpg" />

## Docker Compose
Vehicle Tracking Api is very easy to install and deploy with docker-compose.
It easily runs docker-compose database and application together.
By default, the Docker will expose port 8000 and 8001, so change this within the
docker-compose.yml if necessary. When ready, simply use the docker-compose.yml to
deploy the app.
```sh
cd vehicle-tracking
docker-compose up --build -d
```
Verify the deployment by navigating to your server address in
your preferred browser.
```sh
https://localhost:8001/swagger/index.html
http://localhost:8000/swagger/index.html
```
## Docker

Vehicle Tracking Api is very easy to install and deploy in a Docker container.

By default, the Docker will expose port 80 and 443, so change this within the
Dockerfile if necessary. When ready, simply use the Dockerfile to
build the image.

```sh
cd vehicle-tracking
docker build -t <youruser>/vehicletracking:v1 .
```

This will create the vehicletracking image and pull in the necessary dependencies.


Once done, run the Docker image and map the port to whatever you wish on
your host. In this example, we simply map port 8000 of the host to
port 8080 of the Docker (or whatever port was exposed in the Dockerfile):

```sh
docker run -d -p 8000:80 --restart=always --name=vehicletracking <youruser>/vehicletracking:v1
```

Verify the deployment by navigating to your server address in
your preferred browser.

```sh
http://localhost:8000/swagger/index.html
```
## Scaling
The application is scalable with docker swarm, multiple replicas can be created.
Docker Swarm uses Round Robin algorithm for load balancing therefore We have to use a tool for session persistence.

##### Session Persistence (Sticky Session) in Docker Swarm
We can use Traefik.
Traefik (pronounced traffic) is a modern HTTP reverse proxy and load balancer that makes deploying microservices easy. 