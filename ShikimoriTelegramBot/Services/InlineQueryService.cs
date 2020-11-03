using System;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
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
        private readonly ILogger<InlineQueryService> _logger;

        public InlineQueryService(ITelegramBotClient bot, ShikimoriApi api, ILogger<InlineQueryService> logger)
        {
            _bot = bot;
            _api = api;
            _logger = logger;
        }

        public async Task HandleAsync(InlineQuery inlineQuery)
        {
            if (!string.IsNullOrEmpty(inlineQuery.Query))
            {
                var animes = await _api.Anime.SearchAsync(new SearchParams
                {
                    Search = inlineQuery.Query,
                    Limit = 10
                });

                var response = animes.Select(InlineQueryResultArticleHelpers.GetInlineQueryResultArticleForAnime);

                try
                {
                    await _bot.AnswerInlineQueryAsync(inlineQuery.Id, response);
                }
                catch (Exception e)
                {
                    _logger.LogError(e, "Error during AnswerInlineQueryAsync");
                }
            }
        }
    }
}