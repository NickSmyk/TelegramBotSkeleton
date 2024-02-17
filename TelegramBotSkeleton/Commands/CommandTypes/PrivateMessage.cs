using Telegram.Bot.Types.Enums;
using TelegramBotSkeleton.Commands.Interfaces;

namespace TelegramBotSkeleton.Commands.CommandTypes;

public class PrivateMessage : ISupportedTypeInformation
{
    public UpdateType UpdateType => UpdateType.Message;
    public ChatType ChatType => ChatType.Private;
}