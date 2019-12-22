using System.Threading.Tasks;
using ShikimoriTelegramBot.Helpers;
using ShikimoriTelegramBot.Services.Interfaces;
using Telegram.Bot;
using Telegram.Bot.Types;

namespace ShikimoriTelegramBot.Services
{
    public class MessageService : IMessageService
    {
        private readonly ITelegramBotClient _bot;

        public MessageService(ITelegramBotClient bot)
        {
            _bot = bot;
        }

        public async Task HandleAsync(Message message)
        {
            if (message.Text.StartsWith("/start"))
                await _bot.SendTextMessageAsync(new ChatId(message.From.Id),
                    "С помощью этого бота можно искать и делиться аниме. Он работает в любом чате, просто " +
                    "напишите @ShikiAnimeBot в поле для сообщения",
                    replyMarkup: InlineKeyboardHelpers.GetStartKeyboardMarkup());
        }
    }
}