using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Telegram.Bot;
using Telegram.Bot.Types;
using TelegramBotSkeleton.Models;
using TelegramBotSkeleton.Models.Interfaces;
using TelegramBotSkeleton.Services.CommandHandling.Interfaces;
using TelegramBotSkeleton.Services.Interfaces;

namespace TelegramBotSkeleton.Services.BotClientServices;

public sealed class MessagesReceiverService : IMessagesReceiverService
{
    private readonly ILogger<MessagesReceiverService> _logger;
    private readonly IServiceScopeFactory _scopeFactory;

    public MessagesReceiverService(ILogger<MessagesReceiverService> logger, IServiceScopeFactory scopeFactory)
    {
        _logger = logger;
        _scopeFactory = scopeFactory;
    }

    public async Task ProcessMessage(ITelegramBotClient botClient, Update update, CancellationToken cancellationToken)
    {
        try
        {
            await using (AsyncServiceScope scope = _scopeFactory.CreateAsyncScope())
            {
                IHandlerManagerService? handlerManagerService = scope.ServiceProvider.GetService<IHandlerManagerService>();
                if (handlerManagerService is null)
                {
                    //TODO:WORK -> change this to custom
                    throw new Exception();
                }
                
                IMessageProperties messageProperties = new MessageProperties(botClient, update);
                IUpdateHandler handler = handlerManagerService.GetHandler(messageProperties);
                await handler.Handle(messageProperties);
            }
        }
        catch (Exception e)
        {
            //TODO: WORK -> move it to a different file
            const string message = "Error occured while handling a message";
            _logger.LogError(e, message);
        }
    }
}