using TelegramBotSkeleton.Commands.CommandTypes;
using TelegramBotSkeleton.Commands.Interfaces;
using TelegramBotSkeleton.Models.Interfaces;
using TelegramBotSkeleton.Services.Interfaces;

namespace TelegramBotSkeleton.Services.CommandHandling.UpdateHandlers;

public class ChannelPostHandler : BaseHandler
{
    protected override ISupportedTypeInformation SupportedTypeInformation => new GroupMessage();
    
    public ChannelPostHandler(IMessageProperties messageProperties, ICommandService commandService) :
        base(messageProperties, commandService) { }

    public override IEnumerable<ISupportedTypeInformation> GetSupportedTypes()
    {
        throw new NotImplementedException();
    }

    public override async Task Handle()
    {
        string? message = MessageProperties.Update.ChannelPost?.Text;
        if (String.IsNullOrEmpty(message))
        {
            return;
        }
        
        await CommandService.TryExecutingCommand(message, this.SupportedTypeInformation, MessageProperties);
    }
}