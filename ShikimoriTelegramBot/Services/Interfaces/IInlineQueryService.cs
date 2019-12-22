using System.Threading.Tasks;
using Telegram.Bot.Types;

namespace ShikimoriTelegramBot.Services.Interfaces
{
    public interface IInlineQueryService
    {
        Task HandleAsync(InlineQuery inlineQuery);
    }
}