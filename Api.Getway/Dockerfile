FROM mcr.microsoft.com/dotnet/aspnet:5.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:5.0 AS build
WORKDIR /src
COPY ["Api.Getway/Api.Gateway.csproj", "Api.Getway/"]
RUN dotnet restore "Api.Getway/Api.Gateway.csproj"
COPY . .
WORKDIR "/src/Api.Getway"
RUN dotnet build "Api.Gateway.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "Api.Gateway.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Api.Gateway.dll"]