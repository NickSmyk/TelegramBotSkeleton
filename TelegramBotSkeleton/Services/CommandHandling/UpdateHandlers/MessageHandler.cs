using Telegram.Bot.Types.Enums;
using TelegramBotSkeleton.Commands.CommandTypes;
using TelegramBotSkeleton.Commands.Interfaces;
using TelegramBotSkeleton.Models.Interfaces;
using TelegramBotSkeleton.Services.CommandHandling.Interfaces;
using TelegramBotSkeleton.Services.DialogHandling.Interfaces;
using TelegramBotSkeleton.Services.Interfaces;

namespace TelegramBotSkeleton.Services.CommandHandling.UpdateHandlers;

public class MessageHandler : IUpdateHandler
{
    private readonly ICommandService _commandService;
    private readonly IDialogHandlerService _dialogHandlerService;

    //TODO:WORK -> I need another handler inside this one because message has a Type ffs
    public MessageHandler(ICommandService commandService, IDialogHandlerService dialogHandlerService)
    {
        _commandService = commandService;
        _dialogHandlerService = dialogHandlerService;
    }

    public IEnumerable<ISupportedTypeInformation> GetSupportedTypes()
    {
        yield return new MessageSupportedTypeInformation(ChatType.Private);
    }

    public async Task Handle(IMessageProperties messageProperties)
    {
        bool dialogWasProcessed = await _dialogHandlerService.TryHandleTheDialog(messageProperties);
        if (dialogWasProcessed)
        {
            return;
        }
        
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