FROM mcr.microsoft.com/dotnet/core/runtime:latest AS base
WORKDIR /app

FROM mcr.microsoft.com/dotnet/core/sdk:latest AS build
WORKDIR /src
COPY ShikimoriTelegramBot/ShikimoriTelegramBot.csproj ShikimoriTelegramBot/
RUN dotnet restore ShikimoriTelegramBot/ShikimoriTelegramBot.csproj
COPY . .
WORKDIR /src/ShikimoriTelegramBot
RUN dotnet build ShikimoriTelegramBot.csproj -c Release -o /app

FROM build AS publish
RUN dotnet publish ShikimoriTelegramBot.csproj -c Release -o /app

FROM base AS final
WORKDIR /app
COPY --from=publish /app .
ENTRYPOINT ["dotnet", "ShikimoriTelegramBot.dll"]
