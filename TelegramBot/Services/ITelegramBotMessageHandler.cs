using Telegram.Bot.Types;

namespace telegram_bot.Services;

public interface ITelegramBotMessageHandler
{
    Task HandleUpdateAsync(Update update);
}