using System;
using System.Linq;
using System.Threading.Tasks;
using Serilog;
using ShikimoriNET;
using ShikimoriNET.Params.Anime;
using ShikimoriTelegramBot.Helpers;
using Telegram.Bot;
using Telegram.Bot.Args;
using Telegram.Bot.Types;

namespace ShikimoriTelegramBot
{
    internal class Program
    {
        private static TelegramBotClient _bot;
        private static ILogger _logger;

        private static readonly ShikimoriApi Api = new ShikimoriApi();

        private static async Task Main(string[] args)
        {
            if (args.Length == 0)
            {
                Console.WriteLine("Token cannot be null");

                return;
            }

            _logger = Configuration.ConfigureLogger();

            _bot = new TelegramBotClient(args[0]);

            _bot.OnInlineQuery += OnInlineQueryAsync;
            _bot.OnMessage += OnOnMessageAsync;
            _bot.StartReceiving();

            _logger.Information("Bot started");

            await Task.Delay(-1);
        }

        private static async void OnOnMessageAsync(object sender, MessageEventArgs messageEventArgs)
        {
            try
            {
                var message = messageEventArgs.Message;

                _logger.Information("Got message: {@Message}", message);

                if (message.Text.StartsWith("/start"))
                    await _bot.SendTextMessageAsync(new ChatId(message.From.Id),
                        "С помощью этого бота можно искать и делиться аниме. Он работает в любом чате, просто " +
                        "напишите @ShikiAnimeBot в поле для сообщения",
                        replyMarkup: KeyboardHelpers.GetStartKeyboardMarkup());
            }
            catch (Exception e)
            {
                _logger.Error(e, "Error during processing message");
            }
        }

        private static async void OnInlineQueryAsync(object sender, InlineQueryEventArgs inlineQueryEventArgs)
        {
            try
            {
                var inlineQuery = inlineQueryEventArgs.InlineQuery;

                _logger.Information("Got inline query {@InlineQuery}", inlineQuery);

                var animes = await Api.Anime.SearchAsync(new SearchParams
                {
                    Search = inlineQuery.Query,
                    Limit = 10
                });

                var response = animes.Select(InlineQueryResultArticleHelpers.GetInlineQueryResultArticleForAnime);

                await _bot.AnswerInlineQueryAsync(inlineQuery.Id, response);
            }
            catch (Exception e)
            {
                _logger.Error(e, "Error during processing inline query");
            }
        }
    }
}
