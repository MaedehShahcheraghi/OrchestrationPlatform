FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
WORKDIR /app

EXPOSE 8080

FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build

WORKDIR /src

COPY . .

RUN dotnet restore \
    "src/web/presentation/OrchestrationPlatform.WebUI/OrchestrationPlatform.WebUI.csproj"

RUN dotnet build \
    "src/web/presentation/OrchestrationPlatform.WebUI/OrchestrationPlatform.WebUI.csproj" \
    -c Release \
    -o /app/build

FROM build AS publish

RUN dotnet publish \
    "src/web/presentation/OrchestrationPlatform.WebUI/OrchestrationPlatform.WebUI.csproj" \
    -c Release \
    -o /app/publish \
    /p:UseAppHost=false

FROM base AS final

ENV ASPNETCORE_URLS=http://+:8080

WORKDIR /app

COPY --from=publish /app/publish .

ENTRYPOINT ["dotnet", "OrchestrationPlatform.WebUI.dll"]