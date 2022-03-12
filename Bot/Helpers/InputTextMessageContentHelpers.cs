using ShikimoriNET.Models.Anime;
using Telegram.Bot.Types.Enums;
using Telegram.Bot.Types.InlineQueryResults;

namespace Bot.Helpers;

public static class InputTextMessageContentHelpers
{
    public static InputTextMessageContent GetInputTextMessageContent(Anime anime)
    {
        return new(MarkdownHelpers.GetMarkdown(anime))
        {
            ParseMode = ParseMode.Html
        };
    }
}