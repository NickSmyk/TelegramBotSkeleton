using Telegram.Bot.Types.Enums;
using TelegramBotSkeleton.Commands.Interfaces;
using TelegramBotSkeleton.Models.Interfaces;
using TelegramBotSkeleton.Services.CommandHandling.Interfaces;
using TelegramBotSkeleton.Services.Interfaces;

namespace TelegramBotSkeleton.Services.CommandHandling.UpdateHandlers;

public class DefaultHandler : IUpdateHandler
{
    private readonly ICommandService commandService;
    public UpdateType HandlerResponsibilityType => UpdateType.Unknown;

    public DefaultHandler(ICommandService commandService)
    {
        this.commandService = commandService;
    }


    public IEnumerable<ISupportedTypeInformation> GetSupportedTypes()
    {
        throw new NotImplementedException();
    }

    public async Task Handle(IMessageProperties messageProperties)
    {
        throw new NotImplementedException();
    }
}