#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:5.0-buster-slim AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:5.0-buster-slim AS build
WORKDIR /src
COPY ["CoreCooking.Api/CoreCooking.Api.csproj", "CoreCooking.Api/"]
RUN dotnet restore "CoreCooking.Api/CoreCooking.Api.csproj"
COPY . .
WORKDIR "/src/CoreCooking.Api"
RUN dotnet build "CoreCooking.Api.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "CoreCooking.Api.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "CoreCooking.Api.dll"]