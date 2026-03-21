FROM mcr.microsoft.com/dotnet/sdk:8.0 AS restore-layer
WORKDIR /src

COPY src ./src

RUN find . -type f -not -name "*.csproj" -not -name "*.config" -not -name "*.json" -delete


FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src

COPY --from=restore-layer /src .

RUN dotnet restore "web/api/OrchestrationPlatform.Api/OrchestrationPlatform.Api.csproj"

COPY src ./
WORKDIR "/src/web/api/OrchestrationPlatform.Api"
RUN dotnet build "OrchestrationPlatform.Api.csproj" -c Release -o /app/build


FROM build AS publish
RUN dotnet publish "OrchestrationPlatform.Api.csproj" -c Release -o /app/publish /p:UseAppHost=false


FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS final
WORKDIR /app
EXPOSE 8080

COPY sources.list /etc/apt/sources.list 
RUN apt-get update && apt-get install -y \
    curl \
    iputils-ping \
    && rm -rf /var/lib/apt/lists/*

COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "OrchestrationPlatform.Api.dll"]