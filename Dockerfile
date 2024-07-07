#See https://aka.ms/customizecontainer to learn how to customize your debug container and how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
WORKDIR /app

# Api
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /
# Copy solution files
COPY ["src/Api/Api.csproj", "Api/"]
COPY ["src/Application/Application.csproj", "Application/"]
COPY ["src/Domain/Domain.csproj", "Domain/"]
COPY ["src/Infrastructure/Infrastructure.csproj", "Infrastructure/"]
RUN dotnet restore "Api/Api.csproj"
COPY ./src ./

WORKDIR /Api
RUN dotnet build --no-restore "Api.csproj" -c Release -o /app/build

# Publish Api release
FROM build AS publish
WORKDIR /Api
RUN dotnet publish "Api.csproj" -c Release -o /app/publish

# Build api runtime image
FROM base AS final
WORKDIR /app
ENV ASPNETCORE_URLS=http://*:8080
COPY --from=publish /app/publish .
EXPOSE 8080/tcp
ENTRYPOINT ["dotnet", "Api.dll"]
