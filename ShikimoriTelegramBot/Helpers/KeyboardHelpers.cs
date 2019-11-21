using Telegram.Bot.Types.ReplyMarkups;

namespace ShikimoriTelegramBot.Helpers
{
    public static class KeyboardHelpers
    {
        public static InlineKeyboardMarkup GetStartKeyboardMarkup()
        {
            return new InlineKeyboardMarkup(new[]
            {
                InlineKeyboardButton.WithSwitchInlineQueryCurrentChat("🔍 Поиск аниме"),
                InlineKeyboardButton.WithSwitchInlineQuery("🔗 Найти и поделиться аниме")
            });
        }
    }
}
