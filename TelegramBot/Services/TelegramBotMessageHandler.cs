using Telegram.Bot;
using Telegram.Bot.Types;
using telegram_bot.Exceptions;
using telegram_bot.Models;

namespace telegram_bot.Services;

public class TelegramBotMessageHandler : ITelegramBotMessageHandler
{
    private readonly ILogger<TelegramBotMessageHandler> _logger;
    private readonly ITelegramBotClient _telegramBotClient;

    public TelegramBotMessageHandler(
        ILogger<TelegramBotMessageHandler> logger,
        ITelegramBotClient telegramBotClient)
    {
        _logger = logger;
        _telegramBotClient = telegramBotClient;
    }

    public async Task HandleUpdateAsync(Update update)
    {
        _logger.LogInformation(
            "Handling update for chat {chatId}: {message}",
            update.Message?.Chat.Id,
            update.Message?.Text);

        if (!IncomingMessage.TryParse(update, out var incomingMessage))
        {
            throw new UnknownUpdateType();
        }

        await ReplyToMessageAsync(incomingMessage);
    }

    private async Task ReplyToMessageAsync(IncomingMessage incomingMessage)
    {
        var text = incomingMessage.Text ?? string.Empty;
        await SendTextMessageAsync(incomingMessage.ChatId, text, incomingMessage.MessageId);
    }

    private async Task SendTextMessageAsync(long chatId, string text, int? replyToMessageId = null)
    {
        await _telegramBotClient.SendTextMessageAsync(chatId, text, replyToMessageId: replyToMessageId);
    }
}