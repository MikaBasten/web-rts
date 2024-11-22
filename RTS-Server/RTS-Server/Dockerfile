# Use the ASP.NET base image
FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
USER app
WORKDIR /app
EXPOSE 8080
EXPOSE 8081

FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src

# Copy the .csproj files for all projects and restore dependencies
COPY ["RTS-Server/Core/Core.csproj", "Core/"]
COPY ["RTS-Server/DAL/Dal.csproj", "DAL/"]
COPY ["RTS-Server/RTS-Server/RTS-Server.csproj", "RTS-Server/"]



RUN dotnet restore "RTS-Server/RTS-Server.csproj"

# Copy all source code to the container
COPY . .

# Build the application
WORKDIR "/src/RTS-Server"
RUN dotnet build "RTS-Server.csproj" -c $BUILD_CONFIGURATION -o /app/build

FROM build AS publish
RUN dotnet publish "RTS-Server.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "RTS-Server.dll"]
