# Use the .NET SDK as a base image for building
FROM mcr.microsoft.com/dotnet/sdk:7.0 AS build-env

# Set the working directory inside the container
WORKDIR /app

# Copy the .csproj files to the container
COPY OxygenMeasurementApi/*.csproj ./OxygenMeasurementApi/
COPY OxygenMeasurementMailLibrary/*.csproj ./OxygenMeasurementMailLibrary/

# Restore NuGet packages
RUN dotnet restore ./OxygenMeasurementApi

# Copy the remaining files to the container
COPY . .

# Build the application
RUN dotnet publish -c Release -o out

# Use the .NET runtime as a base image for the final stage
FROM mcr.microsoft.com/dotnet/aspnet:7.0 AS final-env
WORKDIR /app

# Copy the published output from the build stage to the final stage
COPY --from=build-env /app/out .

# Specify the entry point for the application
ENTRYPOINT ["dotnet", "OxygenMeasurementApi.dll"]
