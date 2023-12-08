# Dockerfile
FROM mcr.microsoft.com/dotnet/sdk:7.0 AS build-env
WORKDIR /app

EXPOSE 5179

# Copy all files
COPY . .

# Restore dependencies and publish
RUN dotnet restore
RUN dotnet publish ./Inventory_Management_System.csproj -c Release -o out

# Build runtime image
FROM mcr.microsoft.com/dotnet/aspnet:7.0
WORKDIR /app
COPY --from=build-env /app/out .

ENTRYPOINT ["dotnet", "Inventory_Management_System.dll"]

ENV ASPNETCORE_URLS=http://+:5179/

