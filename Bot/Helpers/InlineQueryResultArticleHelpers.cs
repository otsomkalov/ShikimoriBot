using ShikimoriNET.Models.Anime;
using Telegram.Bot.Types.InlineQueryResults;

namespace Bot.Helpers
{
    public static class InlineQueryResultArticleHelpers
    {
        public static InlineQueryResultArticle GetInlineQueryResultArticleForAnime(Anime anime)
        {
            return new(anime.Id.ToString(), anime.Russian ?? anime.Name,
                InputTextMessageContentHelpers.GetInputTextMessageContent(anime))
            {
                ThumbUrl = Settings.ShikimoriUrl + anime.Image.Preview,
                Description = anime.Name
            };
        }
    }
}