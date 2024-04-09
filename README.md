# Inter.Metronome

## Summary
This microservice is in charge of announcing the passage of seconds and minutes to schedule regular events.

# Overview

A brief description of each process.

## Clock

This process gets exclusive publishing access then announces the passage of time.

# How to run

Clone this repository and run with `dotnet run --project Application/Application.csproj`

## General information

This project requires dotnet 8 sdk to run (install link [here](https://dotnet.microsoft.com/en-us/download/dotnet/8.0)).

When running locally, I have the rabbit password replaced using the dotnet user-secrets tool. 
Please follow Microsoft's [guide](https://learn.microsoft.com/en-us/aspnet/core/security/app-secrets?view=aspnetcore-6.0&tabs=linux) to set the value of rabbit_pass to your configured rabbit account's password for the Applications.csproj.

This project uses the MelbergFramework nuget package, please see [its github repo](https://github.com/MelbergFramework) for more info.

## Required Infrastructure
|Product|Details|Database Install Link|
|-|-|-|
|Redis| Update the Uri value of the DefaultContext section of [appsettings.json](Application/appsettings.json).| Docker installation guide for redis [here](https://github.com/bitnami/containers/blob/main/bitnami/redis/README.md).|
|RabbitMQ| The code will create the exchanges, queues, and bindings for you, just update the Rabbit:ClientDeclarations:Connections:0 details in [appsettings.json](Application/appsettings.json).| Docker installation guide for RabbitMQ [here](https://hub.docker.com/_/rabbitmq).|
