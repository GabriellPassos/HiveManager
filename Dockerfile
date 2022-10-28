#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY ["HiveManager/HiveManager.csproj", "HiveManager/"]
RUN dotnet restore "HiveManager/HiveManager.csproj"
COPY . .
WORKDIR "/src/HiveManager"
RUN dotnet build "HiveManager.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "HiveManager.csproj" -c Release -o /app/out 

FROM base AS final
WORKDIR /app
COPY --from=build-env /app/out .
ENTRYPOINT ["dotnet", "HiveManager.dll"]
