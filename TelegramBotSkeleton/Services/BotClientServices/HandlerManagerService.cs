using Telegram.Bot.Types;
using Telegram.Bot.Types.Enums;
using TelegramBotSkeleton.Commands.Interfaces;
using TelegramBotSkeleton.Models.Interfaces;
using TelegramBotSkeleton.Services.CommandHandling.Interfaces;
using TelegramBotSkeleton.Services.Interfaces;

namespace TelegramBotSkeleton.Services.BotClientServices;

public class HandlerManagerService : IHandlerManagerService
{
    private readonly IEnumerable<IUpdateHandler> _handlers;

    public HandlerManagerService(IEnumerable<IUpdateHandler> handlers)
    {
        _handlers = handlers;
    }

    public IUpdateHandler GetHandler(IMessageProperties messageProperties)
    {
        UpdateType updateType = messageProperties.Update.Type;
        ChatType? chatType = messageProperties.Update.Message?.Chat.Type;
        
        foreach (IUpdateHandler updateHandler in _handlers)
        {
            IEnumerable<UpdateType> supportedTypes = updateHandler.GetSupportedTypes().Select(o => o.UpdateType);
            if (!supportedTypes.Contains(updateType))
            {
                continue;
            }
            
            IEnumerable<ChatType> supportedChatTypes = updateHandler.GetSupportedTypes().Select(o => o.ChatType);
            if (chatType is null || !supportedChatTypes.Contains(chatType.Value))
            {
                continue;
            }

            return updateHandler;
        }
        
        //TODO:QUESTION -> maybe you should return some sort of default handler? Buy why?
        //TODO:WORK -> change this to custom
        throw new Exception($"Couldn't find handler for UpdateType {updateType} and ChatType {chatType}");
    }
}