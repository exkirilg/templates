namespace telegram_bot.ConfigurationOptions;

public class TelegramBotOptions
{
    public const string BotConfigurationSection = "TelegramBotConfiguration";
    public const string BotClientName = "telegram_bot_client";

    public string BotToken { get; init; } = default!;
    public string HostAddress { get; init; } = default!;
    public string Route { get; init; } = default!;
    public string SecretToken { get; init; } = default!;
}