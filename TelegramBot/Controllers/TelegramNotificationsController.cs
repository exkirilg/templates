using Microsoft.AspNetCore.Mvc;
using Telegram.Bot.Types;
using telegram_bot.Services;

namespace telegram_bot.Controllers;

[Route("api/[controller]")]
[ApiController]
public class TelegramNotificationsController : ControllerBase
{
    private readonly ILogger<TelegramNotificationsController> _logger;
    private readonly ITelegramBotMessageHandler _telegramBotMessageHandler;

    public TelegramNotificationsController(
        ILogger<TelegramNotificationsController> logger,
        ITelegramBotMessageHandler telegramBotMessageHandler)
    {
        _logger = logger;
        _telegramBotMessageHandler = telegramBotMessageHandler;
    }

    [HttpPost]
    public IActionResult PostUpdate(
        [FromBody] Update update,
        CancellationToken cancellationToken)
    {
        _logger.LogInformation(
            "Received update from chat {chatId}: {message}",
            update.Message?.Chat.Id,
            update.Message?.Text);

        try
        {
            _telegramBotMessageHandler.HandleUpdateAsync(update);
        }
        catch (Exception ex)
        {
            _logger.LogError(
                "Exception thrown while handling update from chat {chatId}: {message} - {exception}",
                update.Message?.Chat.Id,
                update.Message?.Text,
                ex.Message);
            return Problem();
        }

        return Ok();
    }
}