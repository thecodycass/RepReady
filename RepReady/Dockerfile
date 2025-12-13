FROM mcr.microsoft.com/dotnet/aspnet:10.0 AS base
WORKDIR /app

# HTTP-only container: bind Kestrel to all interfaces on port 8080
ENV ASPNETCORE_URLS=http://+:8080

EXPOSE 8080

# If you later want to run as non-root, uncomment and set APP_UID at build/run time
# USER $APP_UID

FROM mcr.microsoft.com/dotnet/sdk:10.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src
COPY ["RepReady/RepReady.csproj", "RepReady/"]
RUN dotnet restore "RepReady/RepReady.csproj"
COPY . .
WORKDIR "/src/RepReady"
RUN dotnet build "./RepReady.csproj" -c $BUILD_CONFIGURATION -o /app/build

FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "./RepReady.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false /p:PublishStaticWebAssets=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "RepReady.dll"]
