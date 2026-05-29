FROM mcr.microsoft.com/dotnet/sdk:10.0 AS build
WORKDIR /app

COPY car-auctions.slnx ./
COPY Directory.Packages.props ./

COPY src/NotificationService/NotificationService.csproj ./src/NotificationService/
COPY src/Contracts/Contracts.csproj ./src/Contracts/
RUN dotnet restore ./src/NotificationService/NotificationService.csproj

COPY src/NotificationService/ ./src/NotificationService/
COPY src/Contracts/ ./src/Contracts/

RUN dotnet publish ./src/NotificationService/NotificationService.csproj -c Release -o /app/publish

FROM mcr.microsoft.com/dotnet/aspnet:10.0 AS runtime
WORKDIR /app
EXPOSE 80
COPY --from=build /app/publish .
ENTRYPOINT ["dotnet", "NotificationService.dll"]