using Telegram.Bot.Types.Enums;
using TelegramBotSkeleton.Commands.Interfaces;

namespace TelegramBotSkeleton.Commands.CommandTypes;

public class GenericSupportedTypeInformation : ISupportedTypeInformation
{
    public UpdateType UpdateType { get; }
    public ChatType? ChatType { get; }
    
    public GenericSupportedTypeInformation(UpdateType updateType, ChatType? chatType = null)
    {
        this.ChatType = chatType;
        this.UpdateType = updateType;
    }
}