# Set base image for the runtime
FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
USER app
WORKDIR /app
EXPOSE 8080
EXPOSE 8081

# Set up build environment using the .NET SDK image
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src

# Copy the necessary .csproj files for the projects
COPY RTS-Server/RTS-Server.csproj RTS-Server/
COPY DAL/DAL.csproj DAL/
COPY Core/Core.csproj Core/

# Restore the .NET dependencies
RUN dotnet restore "RTS-Server/RTS-Server.csproj"

# Copy all other source files
COPY . .

WORKDIR "/src/RTS-Server"
RUN dotnet build "RTS-Server.csproj" -c $BUILD_CONFIGURATION -o /app/build

# Publish the application (ready to deploy)
FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "RTS-Server.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

# Final image (runtime environment)
FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "RTS-Server.dll"]
