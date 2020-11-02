using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using ShikimoriNET;
using ShikimoriTelegramBot;
using ShikimoriTelegramBot.Services;
using ShikimoriTelegramBot.Services.Interfaces;
using Telegram.Bot;

[assembly: WebJobsStartup(typeof(Startup))]

namespace ShikimoriTelegramBot
{
    internal class Startup : IWebJobsStartup
    {
        public void Configure(IWebJobsBuilder builder)
        {
            var config = new ConfigurationBuilder()
                .AddEnvironmentVariables()
                .Build();

            ITelegramBotClient bot = new TelegramBotClient(config["Token"]);
            var api = new ShikimoriApi();

            builder.Services
                .AddSingleton(bot)
                .AddSingleton(api)
                .AddScoped<IMessageService, MessageService>()
                .AddScoped<IInlineQueryService, InlineQueryService>();
        }
    }
}