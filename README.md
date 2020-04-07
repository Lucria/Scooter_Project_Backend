# Beam Software Engineering Intern Exercise
![.NET Core Build](https://github.com/Lucria/beam_intern_test/workflows/.NET%20Core%20Build/badge.svg)

This is a system that is capable of fetching a set number of 
scooters specified by a user for a given latitude, longitude within 
a certain distance, and plot it onto a map. 

The goal of building this system is for Beam's Software Engineering Internship
Exercise. 

This repository will house both the frontend and backend for this system.
The database system will be hosted using a Docker file.

## Rough Architecture Diagram
![](Rough%20Architecture%20Diagram.png) 

## Getting Started
### Prerequisites
* .NET Core v3.0 minimum

You may type `dotnet --version` to verify that the downloaded version is 3.0 or above.

* Docker

The application is designed to be run as a Docker container, especially the database.
Docker will also bring further convenience during development.  

### Installing

* Clone the repository to your local machine using Git.  

Using HTTPS:
```
git clone https://github.com/Lucria/beam_intern_test.git
```

Using SSH:

```
git clone git@github.com:Lucria/beam_intern_test.git
```

* Run `dotnet restore` in the root directory to restore NuGet dependencies.

### Running
From within the root folder, run `docker-compose up`. This will
build and deploy the backend API server as well as the PostgresSQL
database.

## Testing

Automated Testing to be added in the future

## Deployment

TODO: To be added in the future
