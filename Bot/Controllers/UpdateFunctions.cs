using System.Threading.Tasks;
using Bot.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Telegram.Bot.Types;
using Telegram.Bot.Types.Enums;

namespace Bot.Controllers
{
    [ApiController]
    public class UpdateFunctions : ControllerBase
    {
        private readonly IInlineQueryService _inlineQueryService;
        private readonly IMessageService _messageService;

        public UpdateFunctions(IMessageService messageService, IInlineQueryService inlineQueryService)
        {
            _messageService = messageService;
            _inlineQueryService = inlineQueryService;
        }

        public async Task<IActionResult> ProcessUpdateAsync(Update update)
        {
            switch (update.Type)
            {
                case UpdateType.Unknown:

                    break;

                case UpdateType.Message:

                    await _messageService.HandleAsync(update.Message);

                    break;

                case UpdateType.InlineQuery:

                    await _inlineQueryService.HandleAsync(update.InlineQuery);

                    break;
            }

            return new OkResult();
        }
    }
}