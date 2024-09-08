using Microsoft.Azure.Functions.Worker;
using Microsoft.Extensions.Logging;
using System.IO;
using System.Threading.Tasks;
using Telegram.Bot;
using Telegram.Bot.Types;

public class Telegram_BotNotify
{
    private readonly ILogger _logger;
    private readonly TelegramBotClient _botClient;

     public Telegram_BotNotify(ILogger<Telegram_BotNotify> logger, IConfiguration configuration)
    {
        _logger = logger;
        var botToken = configuration["TELEGRAM_BOT_SECRET"];
        _botClient = new TelegramBotClient(botToken);
    }

    [Function("Telegram_BotNotify")]
    public async Task Run([HttpTrigger(AuthorizationLevel.Anonymous, "post")] HttpRequest req)
    {
        _logger.LogInformation("C# HTTP trigger function processed a request.");

        string requestBody = await new StreamReader(req.Body).ReadToEndAsync();
        Update update = Newtonsoft.Json.JsonConvert.DeserializeObject<Update>(requestBody);

        if (update?.Message != null)
        {
            await _botClient.SendTextMessageAsync(
                chatId: update.Message.Chat.Id,
                text: $"Received your message {update.Message.Chat.Id}"
            );
        }
    }
}
