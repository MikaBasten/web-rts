FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
USER app
WORKDIR /app
EXPOSE 8080
EXPOSE 8081

FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src
COPY ["RTS-Server/RTS-Server.csproj", "RTS-Server/"]
COPY ["RTS-Server/DAL/Dal.csproj", "RTS-Server/DAL/"]
COPY ["RTS-Server/Core/Core.csproj", "RTS-Server/Core/"]
RUN dotnet restore "./RTS-Server/RTS-Server.csproj"


COPY . .
WORKDIR "/src/RTS-Server"
RUN dotnet build "./RTS-Server.csproj" -c $BUILD_CONFIGURATION -o /app/build

FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "./RTS-Server.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "RTS-Server.dll"]