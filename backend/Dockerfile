# Set the base image for the build (SDK image)
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build

# Set the working directory inside the container
WORKDIR /src

# Copy the .csproj files separately to restore dependencies first (this optimizes caching)
COPY API/API.csproj API/

# Restore dependencies based on the .csproj file
RUN dotnet restore API/API.csproj

# Copy the entire backend folder to the container
COPY . .

# Build and publish the API project
RUN dotnet publish API/API.csproj -c Release -o /app/out

# Set the base image for runtime (ASP.NET Core image)
FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS runtime

# Set the working directory in the runtime container
WORKDIR /app

# Copy the published app from the build stage
COPY --from=build /app/out .

# Expose the port the app will run on
EXPOSE 8080

# Run the application
ENTRYPOINT ["dotnet", "API.dll"]
