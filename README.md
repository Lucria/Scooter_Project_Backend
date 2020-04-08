# Beam Software Engineering Intern Exercise
![.NET Core Build](https://github.com/Lucria/beam_intern_test/workflows/.NET%20Core%20Build/badge.svg)

This is a system that is capable of fetching a set number of 
scooters specified by a user for a given latitude, longitude within 
a certain distance, and plot it onto a map. 

The goal of building this system is for Beam's Software Engineering Internship
Exercise. 

This repository will house both the backend and the database for this system. Both the backend and 
the database system will be hosted from Docker containers. The backend API server is done in C# 
utilizing .NET Core, while the database is a PostgresSQL database. 

## Rough Architecture Diagram
![Architecture Diagram](Rough%20Architecture%20Diagram.png) 

## Prerequisites
* .NET Core v3.1

    You may type `dotnet --version` to verify that the downloaded version is 3.1 or above. 
    Follow the instructions [here](https://dotnet.microsoft.com/download) to download .NET Core.

* Docker **(Essential)**

    The application is designed to be run within a Docker container, especially the database.
Docker will also bring further convenience during development. Therefore, it is essential that
Docker is installed in your computer for testing.

    Follow the instructions [here](https://www.docker.com/products/docker-desktop) to download Docker.

## Installing

* Clone the repository to your local machine using Git.  

Using HTTPS:
```
git clone https://github.com/Lucria/beam_intern_test.git
```

Using SSH:

```
git clone git@github.com:Lucria/beam_intern_test.git
```

### Web API Server and Database
1) Run `dotnet restore` in the root directory to restore NuGet dependencies.

## Running
From within the root folder, run 
```
docker volume create test_db
docker-compose up -d --build
```
The first command will create a docker volume called `test_db`. This directory will
be used by our PostgresSQL database later on.

The second command will rebuild all images 
and deploy the backend API server as well as the PostgresSQL
database.

## Testing

Automated Testing to be added in the future

## Deployment

To be added in the future
