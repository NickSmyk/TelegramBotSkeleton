using Telegram.Bot.Types.Enums;
using TelegramBotSkeleton.Commands.Interfaces;
using TelegramBotSkeleton.Models.Interfaces;
using TelegramBotSkeleton.Services.CommandHandling.Interfaces;

namespace TelegramBotSkeleton.Services.CommandHandling.UpdateHandlers;

public class DefaultHandler : IUpdateHandler
{
    private readonly IMessageProperties _messageProperties;
    public UpdateType HandlerResponsibilityType => UpdateType.Unknown;

    public DefaultHandler(IMessageProperties messageProperties)
    {
        _messageProperties = messageProperties;
    }


    public IEnumerable<ISupportedTypeInformation> GetSupportedTypes()
    {
        throw new NotImplementedException();
    }

    public async Task Handle()
    {
        throw new NotImplementedException();
    }
}