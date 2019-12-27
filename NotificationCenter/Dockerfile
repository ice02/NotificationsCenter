FROM mcr.microsoft.com/dotnet/core/aspnet:2.2-stretch-slim AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/core/sdk:2.2-stretch AS build
WORKDIR /src
COPY ["NotificationCenter/NotificationCenter.csproj", "NotificationCenter/"]
RUN dotnet restore "NotificationCenter/NotificationCenter.csproj"
COPY . .
WORKDIR "/src/NotificationCenter"
RUN dotnet build "NotificationCenter.csproj" -c Release -o /app

FROM build AS publish
RUN dotnet publish "NotificationCenter.csproj" -c Release -o /app

FROM base AS final
WORKDIR /app
COPY --from=publish /app .
ENTRYPOINT ["dotnet", "NotificationCenter.dll"]
