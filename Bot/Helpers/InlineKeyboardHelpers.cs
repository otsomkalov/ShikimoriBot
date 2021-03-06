using Telegram.Bot.Types.ReplyMarkups;

namespace Bot.Helpers
{
    public static class InlineKeyboardHelpers
    {
        public static InlineKeyboardMarkup GetStartKeyboardMarkup()
        {
            return new(new[]
            {
                InlineKeyboardButton.WithSwitchInlineQueryCurrentChat("🔍 Поиск аниме"),
                InlineKeyboardButton.WithSwitchInlineQuery("🔗 Найти и поделиться аниме")
            });
        }
    }
}