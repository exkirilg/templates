using telegram_bot.Extensions;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers().AddNewtonsoftJson();

builder.ConfigureTelegramBotServices();
builder.ConfigureHostedServices();

var app = builder.Build();

app.UseHttpsRedirection();

app.MapControllers();

app.Run();
