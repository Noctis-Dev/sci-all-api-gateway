# Use the official ASP.NET Core runtime as a base image
FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
WORKDIR /app
EXPOSE 8080

# Copy the build output to the container
COPY ./bin/Release/net8.0/publish/ .

# Set the entry point for the container
ENTRYPOINT ["dotnet", "sci-all-api-gateway.dll"]