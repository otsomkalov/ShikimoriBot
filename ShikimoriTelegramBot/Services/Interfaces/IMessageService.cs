using System.Threading.Tasks;
using Telegram.Bot.Types;

namespace ShikimoriTelegramBot.Services.Interfaces
{
    public interface IMessageService
    {
        Task HandleAsync(Message message);
    }
}