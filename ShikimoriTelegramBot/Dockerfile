FROM microsoft/dotnet:2.2-runtime AS base
WORKDIR /app

FROM microsoft/dotnet:2.2-sdk AS build
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
