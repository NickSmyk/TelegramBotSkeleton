using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Microsoft.Extensions.Hosting;
using TelegramBotSkeleton.Builder.Interfaces;
using TelegramBotSkeleton.Extensions;
using TelegramBotSkeleton.Services.BotClientServices;
using TelegramBotSkeleton.Services.CommandHandling;
using TelegramBotSkeleton.Services.DialogHandling;
using TelegramBotSkeleton.Services.DialogHandling.Interfaces;
using TelegramBotSkeleton.Services.Interfaces;

namespace TelegramBotSkeleton.Builder;

public class BotBuilder :
    IBuildable
{
    private readonly IServiceCollection _services;

    public BotBuilder(IServiceCollection services)
    {
        _services = services;
    }
    
    public IServiceCollection Build()
    {
        _services.AddHostedService<BotHostedService>().TryAddSingleton(x => x
            .GetServices<IHostedService>()
            .OfType<BotHostedService>()
            .First());

        _services.TryAddSingleton<IMessagesReceiverService, MessagesReceiverService>();
        _services.TryAddSingleton<ITelegramBotClientService, TelegramBotClientService>();
        _services.TryAddScoped<IDialogHandlerService, DialogHandlerService>();
        _services.TryAddScoped<IHandlerManagerService, HandlerManagerService>();
        _services.TryAddScoped<ICommandService, CommandService>();
        _services.TryAddSingleton<ErrorsHandlerService>();
        _services.RegisterCommands();
        _services.RegisterHandlers();
        _services.RegisterDialogs();
        return _services;
    }
}