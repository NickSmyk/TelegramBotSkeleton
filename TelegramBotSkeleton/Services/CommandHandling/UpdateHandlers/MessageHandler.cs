using Telegram.Bot.Types.Enums;
using TelegramBotSkeleton.Commands.CommandTypes;
using TelegramBotSkeleton.Commands.Interfaces;
using TelegramBotSkeleton.Models.Interfaces;
using TelegramBotSkeleton.Services.Interfaces;

namespace TelegramBotSkeleton.Services.CommandHandling.UpdateHandlers;

public class MessageHandler : BaseHandler
{
    protected override ISupportedTypeInformation SupportedTypeInformation => new PrivateMessage();

    //TODO:WORK -> I need another handler inside this one because message has a Type ffs
    public MessageHandler(IMessageProperties messageProperties, ICommandService commandService) :
        base(messageProperties, commandService) { }

    public override IEnumerable<ISupportedTypeInformation> GetSupportedTypes()
    {
        yield return new MessageSupportedTypeInformation(ChatType.Private);
    }

    public override async Task Handle()
    {
        string? message = MessageProperties.Update.Message?.Text;
        if (String.IsNullOrEmpty(message))
        {
            return;
        }

        ChatType? chatType = MessageProperties.Update.Message?.Chat.Type;
        if (chatType is null)
        {
            return;
        }

        //TODO:WORK -> this is potential issue You duplicate types for no apparent reason
        ISupportedTypeInformation supportedTypeInformation = new MessageSupportedTypeInformation(chatType.Value);
        await CommandService.TryExecutingCommand(message, supportedTypeInformation, MessageProperties);
    }
}