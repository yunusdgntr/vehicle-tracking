#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:5.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443
ENV ASPNETCORE_URLS=http://*:80

FROM mcr.microsoft.com/dotnet/sdk:5.0 AS build
WORKDIR /src
COPY Vehicle.Tracking.WebApi ./Vehicle.Tracking.WebApi
COPY Vehicle.Tracking.Core ./Vehicle.Tracking.Core
COPY Vehicle.Tracking.Entities ./Vehicle.Tracking.Entities
COPY Vehicle.Tracking.Business ./Vehicle.Tracking.Business
COPY Vehicle.Tracking.DataAccess ./Vehicle.Tracking.DataAccess
RUN dotnet restore "Vehicle.Tracking.WebApi/Vehicle.Tracking.WebApi.csproj"

WORKDIR "/src/Vehicle.Tracking.WebApi"
RUN dotnet build "Vehicle.Tracking.WebApi.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "Vehicle.Tracking.WebApi.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Vehicle.Tracking.WebApi.dll","--server.urls", "http://+:80;https://+:443"]