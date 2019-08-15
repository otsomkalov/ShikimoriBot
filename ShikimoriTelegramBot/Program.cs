using System;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ShikimoriNET;
using ShikimoriNET.Enums;
using ShikimoriNET.Helpers;
using ShikimoriNET.Models.Anime;
using ShikimoriNET.Params.Anime;
using Telegram.Bot;
using Telegram.Bot.Args;
using Telegram.Bot.Types;
using Telegram.Bot.Types.Enums;
using Telegram.Bot.Types.InlineQueryResults;
using Telegram.Bot.Types.ReplyMarkups;

namespace ShikimoriTelegramBot
{
    internal static class Program
    {
        private const string Url = "https://shikimori.org";
        private static TelegramBotClient _bot;

        private static readonly ShikimoriApi Api = new ShikimoriApi();

        private static async Task Main(string[] args)
        {
            if (args.Length == 0)
            {
                Console.WriteLine("Token cannot be null");

                return;
            }

            _bot = new TelegramBotClient(args[0]);

            _bot.OnInlineQuery += OnInlineQueryAsync;
            _bot.OnMessage += OnOnMessageAsync;
            _bot.StartReceiving();

            Console.WriteLine("Bot started");

            while (true) await Task.Delay(int.MaxValue);
        }

        private static async void OnOnMessageAsync(object sender, MessageEventArgs messageEventArgs)
        {
            var message = messageEventArgs.Message;

            if (message.Text.StartsWith("/start"))
                await _bot.SendTextMessageAsync(new ChatId(message.From.Id),
                    "С помощью этого бота можно искать и делиться аниме. Он работает в любом чате, просто " +
                    "напишите @ShikiAnimeBot в поле для сообщения",
                    replyMarkup: new InlineKeyboardMarkup(new[]
                    {
                        InlineKeyboardButton.WithSwitchInlineQueryCurrentChat("🔍 Поиск аниме"),
                        InlineKeyboardButton.WithSwitchInlineQuery("🔗 Найти и поделиться аниме")
                    }));
        }

        private static async void OnInlineQueryAsync(object sender, InlineQueryEventArgs inlineQueryEventArgs)
        {
            var inlineQuery = inlineQueryEventArgs.InlineQuery;

            var animes = await Api.Anime.SearchAsync(new SearchParams
            {
                Search = inlineQuery.Query,
                Limit = 10
            });

            var response = animes.Select(anime =>
            {
                var resultArticle =
                    new InlineQueryResultArticle(anime.Id.ToString(), anime.Russian ?? anime.Name,
                        new InputTextMessageContent(GetMarkdown(anime))
                        {
                            ParseMode = ParseMode.Html
                        })
                    {
                        ThumbUrl = Url + anime.Image.Preview,
                        Description = anime.Name
                    };

                return resultArticle;
            });

            await _bot.AnswerInlineQueryAsync(inlineQuery.Id, response);
        }

        private static string GetMarkdown(Anime anime)
        {
            var markdownStringBuilder = new StringBuilder()
                .AppendLine($"<a href=\"{Url + anime.Url}\">{anime.Russian ?? anime.Name}</a>")
                .AppendLine($"Тип: {AttributeHelpers.GetDescriptionAttributeData(anime.Kind)}")
                .AppendLine($"Статус: {AttributeHelpers.GetDescriptionAttributeData(anime.Status)}");

            switch (anime.Status)
            {
                case Status.Released:
                    markdownStringBuilder.Append($", {anime.Episodes.ToString()} эп.");

                    break;

                case Status.Ongoing:
                    markdownStringBuilder.Append($", {anime.EpisodesAired.ToString()}/${anime.Episodes.ToString()}");

                    break;
            }

            return markdownStringBuilder.ToString();
        }
    }
}