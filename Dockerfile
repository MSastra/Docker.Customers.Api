FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build-env
WORKDIR /app

COPY Customers.Api/Customers.Api.csproj .
RUN dotnet restore Customers.Api.csproj

COPY . .
RUN dotnet publish Customers.Api/Customers.Api.csproj -c Release -o out

FROM mcr.microsoft.com/dotnet/aspnet:6.0
WORKDIR /app

COPY --from=build-env /app/out .

ENTRYPOINT ["dotnet", "Customers.Api.dll"]
