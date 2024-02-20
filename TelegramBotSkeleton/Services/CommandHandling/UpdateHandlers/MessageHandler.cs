using Telegram.Bot.Types.Enums;
using TelegramBotSkeleton.Commands.CommandTypes;
using TelegramBotSkeleton.Commands.Interfaces;
using TelegramBotSkeleton.Models.Interfaces;
using TelegramBotSkeleton.Services.CommandHandling.Interfaces;
using TelegramBotSkeleton.Services.Interfaces;

namespace TelegramBotSkeleton.Services.CommandHandling.UpdateHandlers;

public class MessageHandler : IUpdateHandler
{
    protected ISupportedTypeInformation SupportedTypeInformation => new PrivateMessage();
    private readonly ICommandService _commandService;

    //TODO:WORK -> I need another handler inside this one because message has a Type ffs
    public MessageHandler(ICommandService commandService)
    {
        _commandService = commandService;
    }

    public IEnumerable<ISupportedTypeInformation> GetSupportedTypes()
    {
        yield return new MessageSupportedTypeInformation(ChatType.Private);
    }

    public async Task Handle(IMessageProperties messageProperties)
    {
        string? message = messageProperties.Update.Message?.Text;
        if (String.IsNullOrEmpty(message))
        {
            return;
        }

        ChatType? chatType = messageProperties.Update.Message?.Chat.Type;
        if (chatType is null)
        {
            return;
        }

        //TODO:WORK -> this is potential issue You duplicate types for no apparent reason
        ISupportedTypeInformation supportedTypeInformation = new MessageSupportedTypeInformation(chatType.Value);
        await _commandService.TryExecutingCommand(message, supportedTypeInformation, messageProperties);
    }
}