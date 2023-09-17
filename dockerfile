# Use the official image as a parent image.
FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build-env

# Set the working directory.
WORKDIR /app

# Copy csproj and restore dependencies
COPY *.csproj ./
RUN dotnet restore

# Copy everything else and build the project
COPY . ./
RUN dotnet publish -c Release -o out

# Build runtime image
FROM mcr.microsoft.com/dotnet/aspnet:6.0
WORKDIR /app
COPY --from=build-env /app/out .

# Install netcat
RUN apt-get update && apt-get install -y netcat

# Copy the wait script
COPY wait-for-it.sh /wait-for-it.sh
RUN chmod +x /wait-for-it.sh

ENTRYPOINT ["/wait-for-it.sh", "sql_server", "1433", "--", "dotnet", "WebApiBackend.dll"]