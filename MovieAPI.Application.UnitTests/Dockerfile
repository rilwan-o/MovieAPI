#See https://aka.ms/customizecontainer to learn how to customize your debug container and how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/runtime:8.0 AS base
USER app
WORKDIR /app
EXPOSE 8088
EXPOSE 8087
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src
COPY ["MovieAPI.Application.UnitTests/MovieAPI.Application.UnitTests.csproj", "MovieAPI.Application.UnitTests/"]
COPY ["MovieAPI.Application/MovieAPI.Application.csproj", "MovieAPI.Application/"]
COPY ["MovieAPI.Domin/MovieAPI.Domin.csproj", "MovieAPI.Domin/"]
COPY ["MovieAPI.Persistence/MovieAPI.Persistence.csproj", "MovieAPI.Persistence/"]
RUN dotnet restore "./MovieAPI.Application.UnitTests/MovieAPI.Application.UnitTests.csproj"
COPY . .
WORKDIR "/src/MovieAPI.Application.UnitTests"
RUN dotnet build "./MovieAPI.Application.UnitTests.csproj" -c $BUILD_CONFIGURATION -o /app/build

FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "./MovieAPI.Application.UnitTests.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "MovieAPI.Application.UnitTests.dll"]