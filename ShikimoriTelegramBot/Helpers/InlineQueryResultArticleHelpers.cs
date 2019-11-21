using ShikimoriNET.Models.Anime;
using Telegram.Bot.Types.InlineQueryResults;

namespace ShikimoriTelegramBot.Helpers
{
    public static class InlineQueryResultArticleHelpers
    {
        public static InlineQueryResultArticle GetInlineQueryResultArticleForAnime(Anime anime)
        {
            return new InlineQueryResultArticle(anime.Id.ToString(), anime.Russian ?? anime.Name,
                InputTextMessageContentHelpers.GetInputTextMessageContent(anime))
            {
                ThumbUrl = Configuration.ShikimoriUrl + anime.Image.Preview,
                Description = anime.Name
            };
        }
    }
}
