#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:5.0 AS base
WORKDIR /app
EXPOSE 80

FROM mcr.microsoft.com/dotnet/sdk:5.0 AS build
WORKDIR /src

COPY ["src/Mre.Visas.Multa.Api/Mre.Visas.Multa.Api.csproj", "./Mre.Visas.Multa.Api/"]
COPY ["src/Mre.Visas.Multa.Application/Mre.Visas.Multa.Application.csproj", "./Mre.Visas.Multa.Application/"]
COPY ["src/Mre.Visas.Multa.Domain/Mre.Visas.Multa.Domain.csproj", "./Mre.Visas.Multa.Domain/"]
COPY ["src/Mre.Visas.Multa.Infrastructure/Mre.Visas.Multa.Infrastructure.csproj", "./Mre.Visas.Multa.Infrastructure/"]
RUN dotnet restore "Mre.Visas.Multa.Api/Mre.Visas.Multa.Api.csproj"

COPY ["src/Mre.Visas.Multa.Api", "./Mre.Visas.Multa.Api/"]
COPY ["src/Mre.Visas.Multa.Application", "./Mre.Visas.Multa.Application/"]
COPY ["src/Mre.Visas.Multa.Domain", "./Mre.Visas.Multa.Domain/"]
COPY ["src/Mre.Visas.Multa.Infrastructure", "./Mre.Visas.Multa.Infrastructure/"]
RUN dotnet build "Mre.Visas.Multa.Api/Mre.Visas.Multa.Api.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "Mre.Visas.Multa.Api/Mre.Visas.Multa.Api.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Mre.Visas.Multa.Api.dll"]