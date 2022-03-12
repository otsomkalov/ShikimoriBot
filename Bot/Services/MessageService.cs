using Bot.Helpers;
using Bot.Services.Interfaces;
using Telegram.Bot;
using Telegram.Bot.Types;

namespace Bot.Services;

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
        {
            await _bot.SendTextMessageAsync(new(message.From.Id),
                "С помощью этого бота можно искать и делиться аниме. Он работает в любом чате, просто " +
                "напишите @shkmrbot в поле для сообщения",
                replyMarkup: InlineKeyboardHelpers.GetStartKeyboardMarkup());
        }
    }
}