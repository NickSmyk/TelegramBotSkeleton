using Microsoft.Extensions.Hosting;
using Telegram.Bot;
using TelegramBotSkeleton.Services.Interfaces;

namespace TelegramBotSkeleton.Services.BotClientServices;

public class BotHostedService : IHostedService
{
    private readonly ErrorsHandlerService _errorsHandlerService;
    private readonly IMessagesReceiverService _messagesReceiverService;
    private readonly ITelegramBotClientService _telegramBotClient;
    
    public BotHostedService(
        IMessagesReceiverService messagesReceiverService,
        ErrorsHandlerService errorsHandlerService,
        ITelegramBotClientService telegramBotClient)
    {
        _messagesReceiverService = messagesReceiverService;
        _errorsHandlerService = errorsHandlerService;
        _telegramBotClient = telegramBotClient;
    }

    public async Task StartAsync(CancellationToken cancellationToken)
    {
        _telegramBotClient.TelegramBotClient.StartReceiving(
            _messagesReceiverService.ProcessMessage, 
            _errorsHandlerService.HandleError,
            cancellationToken: cancellationToken);
    }

    public async Task StopAsync(CancellationToken cancellationToken)
    {
        Console.WriteLine("Something went wrong");
    }
}