using Telegram.Bot.Types.Enums;

namespace TelegramBotSkeleton.Commands.Interfaces;

//TODO:WORK -> I dont like how its called
public interface ISupportedTypeInformation
{
    UpdateType UpdateType { get; }
    
    /// <summary>
    /// Type of the chat, in some cases this type can be null
    /// </summary>
    ChatType? ChatType { get; }
}