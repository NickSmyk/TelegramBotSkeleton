using TelegramBotSkeleton.Commands.Interfaces;
using TelegramBotSkeleton.Models.Interfaces;
using TelegramBotSkeleton.Services.CommandHandling.Interfaces;
using TelegramBotSkeleton.Services.Interfaces;

namespace TelegramBotSkeleton.Services.CommandHandling.UpdateHandlers;

public abstract class BaseHandler : IUpdateHandler
{
    protected readonly IMessageProperties MessageProperties;
    protected readonly ICommandService CommandService;
    protected abstract ISupportedTypeInformation SupportedTypeInformation { get; }

    public BaseHandler(IMessageProperties messageProperties, ICommandService commandService)
    {
        MessageProperties = messageProperties;
        CommandService = commandService;
    }

    public abstract IEnumerable<ISupportedTypeInformation> GetSupportedTypes();

    public virtual async Task Handle()
    {
        string? message = MessageProperties.Update.Message?.Text;
        if (String.IsNullOrEmpty(message))
        {
            return;
        }
        
        await CommandService.TryExecutingCommand(message, this.SupportedTypeInformation, MessageProperties);
    }
}