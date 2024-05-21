FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build-env
WORKDIR /App

COPY . .

RUN dotnet restore /Customers.Api/Customers.Api.csproj

RUN dotnet publish /Customers.Api/Customers.Api.csproj -c Release -o out

FROM mcr.microsoft.com/dotnet/aspnet:6.0
WORKDIR /App
COPY --from=build-env /App/out .
ENTRYPOINT ["dotnet", "Customers.Api.dll"]
