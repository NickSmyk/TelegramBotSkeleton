using Telegram.Bot.Types.Enums;
using TelegramBotSkeleton.Commands.Interfaces;

namespace TelegramBotSkeleton.Commands.CommandTypes;

public class GroupMessage : ISupportedTypeInformation
{
    public UpdateType UpdateType => UpdateType.Message;
    public ChatType ChatType => ChatType.Group;
}