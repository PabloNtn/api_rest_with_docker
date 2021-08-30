#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:5.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:5.0 AS build
WORKDIR /src
COPY ["ApiDocker/ApiDocker.csproj", "ApiDocker/"]
RUN dotnet restore "ApiDocker/ApiDocker.csproj"
COPY . .
WORKDIR "/src/ApiDocker"
RUN dotnet build "ApiDocker.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "ApiDocker.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "ApiDocker.dll"]
