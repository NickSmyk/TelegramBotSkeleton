using Telegram.Bot.Types.Enums;

namespace TelegramBotSkeleton.Commands.Interfaces;

//TODO:WORK -> I dont like how its called
public interface ISupportedTypeInformation
{
    UpdateType UpdateType { get; }
    ChatType ChatType { get; }
}