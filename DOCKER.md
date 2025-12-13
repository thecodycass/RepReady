# Docker Setup for RepReady Blazor App

This guide explains how to build and run your .NET Blazor Server app in Docker containers.

## What Was Configured

### 1. Dockerfile (`RepReady/Dockerfile`)
- **Multi-stage build**: Separates build and runtime environments for smaller images
- **HTTP-only configuration**: Runs on port 8080 inside the container
- **Environment variable**: `ASPNETCORE_URLS=http://+:8080` binds Kestrel to all network interfaces
- **No HTTPS**: Simplified for development/learning (add HTTPS via reverse proxy in production)

### 2. Docker Compose (`compose.yaml`)
- **Port mapping**: Maps host port `5100` → container port `8080`
- **Environment**: Sets `ASPNETCORE_ENVIRONMENT=Development`
- **Build context**: Configured to build from the solution root

### 3. Program.cs
- **HTTPS redirection**: Already configured to only redirect in non-Development environments
- This means the container runs HTTP-only in Development mode

## Building the Docker Image

From the solution root directory:

```bash
docker build -f RepReady/Dockerfile -t repready:dev .
```

**What this does:**
- `-f RepReady/Dockerfile`: Specifies the Dockerfile location
- `-t repready:dev`: Tags the image as `repready:dev`
- `.`: Uses current directory as build context

## Running the Container

### Option 1: Using docker run

```bash
docker run --rm -p 5100:8080 --name repready repready:dev
```

**Flags explained:**
- `--rm`: Automatically remove container when stopped
- `-p 5100:8080`: Map host port 5100 to container port 8080
- `--name repready`: Give the container a friendly name
- `repready:dev`: The image to run

**Access the app:** Open your browser to `http://localhost:5100`

**To stop:** Press `Ctrl+C` or run `docker stop repready` in another terminal

### Option 2: Using Docker Compose (Recommended)

```bash
docker compose up --build
```

**To stop:** Press `Ctrl+C` or run `docker compose down`

**Access the app:** Open your browser to `http://localhost:5100`

## Useful Docker Commands

### View running containers
```bash
docker ps
```

### View container logs
```bash
docker logs repready
# or with docker compose
docker compose logs
```

### Stop a running container
```bash
docker stop repready
# or with docker compose
docker compose down
```

### Remove the image
```bash
docker rmi repready:dev
```

### Run container in detached mode (background)
```bash
docker run -d --rm -p 5100:8080 --name repready repready:dev
```

### Execute commands inside running container (debugging)
```bash
docker exec -it repready /bin/bash
```

## Understanding the Container Environment

- **Operating System**: Linux (specified in `RepReady.csproj` as `DockerDefaultTargetOS`)
- **Base Image**: `mcr.microsoft.com/dotnet/aspnet:10.0` (runtime)
- **SDK Image**: `mcr.microsoft.com/dotnet/sdk:10.0` (used only during build)
- **Working Directory**: `/app`
- **Port**: Listens on `8080` inside container, mapped to `5100` on host

## Production Considerations

When deploying to production, consider:

1. **HTTPS/TLS**: Use a reverse proxy (Nginx, Traefik, Caddy) to terminate SSL/TLS
2. **Environment Variables**: Set `ASPNETCORE_ENVIRONMENT=Production`
3. **Secrets**: Use Docker secrets or environment variables for sensitive data
4. **Data Protection Keys**: Mount a volume for `/root/.aspnet/DataProtection-Keys`
5. **Health Checks**: Add Docker health checks to your Dockerfile
6. **Security**: Run as non-root user (uncomment `USER $APP_UID` in Dockerfile)
7. **Logging**: Configure centralized logging (Serilog, ELK stack, etc.)

## Troubleshooting

### Port already in use
If port 5100 is already in use, change it in `compose.yaml` or use a different port:
```bash
docker run --rm -p 5200:8080 --name repready repready:dev
```

### Container exits immediately
Check the logs:
```bash
docker logs repready
```

### Build fails
Ensure you're in the solution root and have internet access for NuGet packages.

### Can't access http://localhost:5100
- Verify container is running: `docker ps`
- Check port mapping: `docker port repready`
- Test with curl: `curl http://localhost:5100`

## Next Steps

- Explore Blazor Server components in the `Components/` directory
- Add database support (consider docker compose with PostgreSQL/SQL Server)
- Set up volume mounts for development hot-reload
- Configure CI/CD pipelines to build/push Docker images
- Learn about Docker networks for multi-container apps
