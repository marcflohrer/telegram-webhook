using Microsoft.Azure.Functions.Worker;
using Microsoft.Extensions.Logging;
using System.IO;
using System.Threading.Tasks;
using Telegram.Bot;
using Telegram.Bot.Types;
using Microsoft.Extensions.Configuration;
using Microsoft.Azure.Functions.Worker.Http;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace Telegram.Bot.Web;

public class Telegram_BotNotify
{
    private readonly ILogger _logger;
    private readonly TelegramBotClient _botClient;

     public Telegram_BotNotify(ILogger<Telegram_BotNotify> logger, IConfiguration configuration)
    {
        _logger = logger;
        var botToken = configuration["TELEGRAM_BOT_SECRET"] ?? throw new InvalidDataException("No Telegram Bot Secret found!");
        _botClient = new TelegramBotClient(botToken);
    }

    [Function("Telegram_BotNotify")]
    public async Task Run([HttpTrigger(AuthorizationLevel.Anonymous, "post")] HttpRequestData req)
    {
        _logger.LogInformation("C# HTTP trigger function processed a request.");

        var requestBody = await new StreamReader(req.Body).ReadToEndAsync();
        Update update = JsonConvert.DeserializeObject<Update>(requestBody) ?? throw new InvalidDataException($"Cannot parse telegram message. {requestBody}");

        if (update?.Message != null)
        {
            await _botClient.SendTextMessageAsync(
                chatId: update.Message.Chat.Id,
                text: $"Received your message {update.Message.Chat.Id}"
            );
        }
    }
}
