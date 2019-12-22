using System.Linq;
using System.Threading.Tasks;
using ShikimoriNET;
using ShikimoriNET.Params.Anime;
using ShikimoriTelegramBot.Helpers;
using ShikimoriTelegramBot.Services.Interfaces;
using Telegram.Bot;
using Telegram.Bot.Types;

namespace ShikimoriTelegramBot.Services
{
    public class InlineQueryService : IInlineQueryService
    {
        private readonly ShikimoriApi _api;
        private readonly ITelegramBotClient _bot;

        public InlineQueryService(ITelegramBotClient bot, ShikimoriApi api)
        {
            _bot = bot;
            _api = api;
        }

        public async Task HandleAsync(InlineQuery inlineQuery)
        {
            // _logger.Information("Got inline query {@InlineQuery}", inlineQuery);

            var animes = await _api.Anime.SearchAsync(new SearchParams
            {
                Search = inlineQuery.Query,
                Limit = 10
            });

            var response = animes.Select(InlineQueryResultArticleHelpers.GetInlineQueryResultArticleForAnime);

            await _bot.AnswerInlineQueryAsync(inlineQuery.Id, response);
        }
    }
}