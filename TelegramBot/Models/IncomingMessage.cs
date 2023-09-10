using Telegram.Bot.Types;

namespace telegram_bot.Models;

public class IncomingMessage
{
    public long ChatId { get; set; }

    public int MessageId { get; set; }

    public string? Text { get; set; }

    public static bool TryParse(Update source, out IncomingMessage result)
    {
        result = new IncomingMessage();

        if (source.Message is null)
        {
            return false;
        }

        result.ChatId = source.Message.Chat.Id;
        result.MessageId = source.Message.MessageId;
        result.Text = source.Message.Text;

        return true;
    }
}