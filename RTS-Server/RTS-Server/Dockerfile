# Use the ASP.NET base image
FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
USER app
WORKDIR /app
EXPOSE 8080
EXPOSE 8081

# Use the .NET SDK for building the application
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src

# Copy all .csproj files (adjusted for your project structure)
COPY ["RTS-Server/RTS-Server.csproj", "RTS-Server/"]
COPY ["RTS-Server/DAL/Dal.csproj", "RTS-Server/DAL/"]
COPY ["RTS-Server/Core/Core.csproj", "RTS-Server/Core/"]

# Restore dependencies for all projects
RUN dotnet restore "RTS-Server/RTS-Server.csproj"

# Copy the rest of the application code
COPY . .

# Build the application
WORKDIR "/src/RTS-Server"
RUN dotnet build "./RTS-Server.csproj" -c $BUILD_CONFIGURATION -o /app/build

# Publish the application
FROM build AS publish
RUN dotnet publish "./RTS-Server.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

# Final image with the published app
FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "RTS-Server.dll"]
