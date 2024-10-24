# Set the base image for .NET SDK
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build

# Set the working directory to /app in the container
WORKDIR /app

# Copy the entire backend folder
COPY backend/ .

# Restore dependencies for the API project
RUN dotnet restore API/API.csproj

# Build and publish the API project
RUN dotnet publish API/API.csproj -c Release -o out

# Set up the runtime image
FROM mcr.microsoft.com/dotnet/aspnet:8.0

# Set the working directory for the final runtime image
WORKDIR /app

# Copy the build output from the previous step
COPY --from=build /app/out .

# Expose the port
EXPOSE 8080

# Run the application
ENTRYPOINT ["dotnet", "API.dll"]
