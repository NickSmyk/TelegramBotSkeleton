using TelegramBotSkeleton.Commands.Interfaces;
using TelegramBotSkeleton.Models.Interfaces;
using TelegramBotSkeleton.Services.CommandHandling.Interfaces;
using TelegramBotSkeleton.Services.Interfaces;

namespace TelegramBotSkeleton.Services.CommandHandling.UpdateHandlers;

public abstract class BaseHandler : IUpdateHandler
{
    protected readonly ICommandService CommandService;
    protected abstract ISupportedTypeInformation SupportedTypeInformation { get; }

    public BaseHandler(ICommandService commandService)
    {
        CommandService = commandService;
    }

    public abstract IEnumerable<ISupportedTypeInformation> GetSupportedTypes();

    public virtual async Task Handle(IMessageProperties messageProperties)
    {
        string? message = messageProperties.Update.Message?.Text;
        if (String.IsNullOrEmpty(message))
        {
            return;
        }
        
        await CommandService.TryExecutingCommand(message, this.SupportedTypeInformation, messageProperties);
    }
}