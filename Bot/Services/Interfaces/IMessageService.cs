using Telegram.Bot.Types;

namespace Bot.Services.Interfaces;

public interface IMessageService
{
    Task HandleAsync(Message message);
}