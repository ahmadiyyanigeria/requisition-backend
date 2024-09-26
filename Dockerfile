# Base image with ASP.NET Core runtime
FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
WORKDIR /app

# Build image with .NET SDK for building the app
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src

# Copy the project files and restore dependencies
COPY ["src/Api/Api.csproj", "Api/"]
COPY ["src/Application/Application.csproj", "Application/"]
COPY ["src/Domain/Domain.csproj", "Domain/"]
COPY ["src/Infrastructure/Infrastructure.csproj", "Infrastructure/"]
RUN dotnet restore "Api/Api.csproj"

# Copy the entire source code into the container and build it
COPY ./src ./
WORKDIR /src/Api
RUN dotnet build "Api.csproj" -c Release -o /app/build

# Publish the application
FROM build AS publish
RUN dotnet publish "Api.csproj" -c Release -o /app/publish /p:UseAppHost=false

# Final image with only the necessary runtime dependencies
FROM base AS final
WORKDIR /app
ENV ASPNETCORE_URLS=http://*:8080

# Copy the published application into the runtime image
COPY --from=publish /app/publish .

# Expose the application port
EXPOSE 8080/tcp

# Set the entry point to run the application
ENTRYPOINT ["dotnet", "Api.dll"]
