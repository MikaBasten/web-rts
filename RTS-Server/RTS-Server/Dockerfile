# Use ASP.NET base image for runtime
FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
USER app
WORKDIR /app
EXPOSE 8080
EXPOSE 8081

# Use the SDK image to build the app
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src

# Copy the necessary project files to the build context
COPY ["RTS-Server/RTS-Server/RTS-Server.csproj", "RTS-Server/"]
COPY ["RTS-Server/Core/Core.csproj", "Core/"]
COPY ["RTS-Server/DAL/DAL.csproj", "DAL/"]

# Restore dependencies
RUN dotnet restore "./RTS-Server/RTS-Server.csproj"

# Copy the rest of the source code
COPY . . 

# Set the working directory to the RTS-Server project
WORKDIR "/src/RTS-Server"

# Make sure the /app/build directory is writable by everyone (for CI/CD)
RUN mkdir -p /app/build && chmod -R 777 /app/build

# Build the project
RUN dotnet build "./RTS-Server.csproj" -c $BUILD_CONFIGURATION -o /app/build

# Publish the app
FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "./RTS-Server.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

# Final image to run the app
FROM base AS final
WORKDIR /app

# Copy the published output from the publish stage
COPY --from=publish /app/publish .

# Set the entry point for the container
ENTRYPOINT ["dotnet", "RTS-Server.dll"]
