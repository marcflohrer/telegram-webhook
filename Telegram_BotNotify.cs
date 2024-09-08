using Microsoft.Azure.Functions.Worker;
using Microsoft.Extensions.Logging;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace telegram_webhook
{
    public class Telegram_BotNotify
    {
        private readonly ILogger<Telegram_BotNotify> _logger;

        public Telegram_BotNotify(ILogger<Telegram_BotNotify> logger)
        {
            _logger = logger;
        }

        [Function("Telegram_BotNotify")]
        public IActionResult Run([HttpTrigger(AuthorizationLevel.Anonymous, "get", "post")] HttpRequest req)
        {
            _logger.LogInformation("C# HTTP trigger function processed a request.");
            return new OkObjectResult("Welcome to Azure Functions!");
        }
    }
}
