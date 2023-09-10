using Microsoft.Extensions.Options;
using Telegram.Bot;
using Telegram.Bot.Types.Enums;
using telegram_bot.ConfigurationOptions;

namespace telegram_bot.HostedServices;

public class WebhooksConfigurationService : IHostedService
{
    private readonly ILogger<WebhooksConfigurationService> _logger;
    private readonly IServiceProvider _serviceProvider;
    private readonly TelegramBotOptions _telegramBotOptions;

    public WebhooksConfigurationService(
        ILogger<WebhooksConfigurationService> logger,
        IServiceProvider serviceProvider,
        IOptions<TelegramBotOptions> options)
    {
        _logger = logger;
        _serviceProvider = serviceProvider;
        _telegramBotOptions = options.Value;
    }

    public async Task StartAsync(CancellationToken cancellationToken)
    {
        using var scope = _serviceProvider.CreateScope();
        var telegramBotClient = scope.ServiceProvider.GetRequiredService<ITelegramBotClient>();

        var webhookAddress = $"{_telegramBotOptions.HostAddress}{_telegramBotOptions.Route}";
        _logger.LogInformation("Setting webhook: {WebhookAddress}", webhookAddress);

        await telegramBotClient.SetWebhookAsync(
            url: webhookAddress,
            allowedUpdates: Array.Empty<UpdateType>(),
            secretToken: _telegramBotOptions.SecretToken,
            cancellationToken: cancellationToken);
    }

    public async Task StopAsync(CancellationToken cancellationToken)
    {
        using var scope = _serviceProvider.CreateScope();
        var telegramBotClient = scope.ServiceProvider.GetRequiredService<ITelegramBotClient>();

        _logger.LogInformation("Deleting webhook");
        await telegramBotClient.DeleteWebhookAsync(cancellationToken: cancellationToken);
    }
}