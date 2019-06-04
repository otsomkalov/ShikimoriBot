# Shikimori Telegram Bot

Telegram Bot to explore [Shikimori](https://shikimori.org/). 

## Getting Started
### Prerequisites

- [.NET Core 2.2](https://dotnet.microsoft.com/download) or higher

### Installing

**Telegram:**
1. Contact to [@BotFather](https://t.me/BotFather) in Telegram
2. Create new bot
3. Copy bot token

**Project:**
1. Clone project
2. Run in folder
```
docker-compose build
docker run -d shikimoritelegrambot <telegram_bot_token>
```
or go to **ShikimoriTelegramBot** folder and run:
```
dotnet run <telegram_bot_token>
```

## Usage

You can try this bot in [Telegram](https://t.me/ExploreShikimoriBot)

## Built With

* [Telegram.Bot](https://github.com/TelegramBots/Telegram.Bot) - .NET Client for Telegram Bot API
* [ShikimoriNET](https://github.com/otsomkalov/ShikimoriNET) - .NET wrapper for Shikimori API

## Contributing
Pull requests are welcome. For major changes, please open an issue first to discuss what you would like to change.