using Telegram.Bot.Types.Enums;
using TelegramBotSkeleton.Commands.Interfaces;

namespace TelegramBotSkeleton.Commands.CommandTypes;

public class MessageSupportedTypeInformation : ISupportedTypeInformation
{
    public UpdateType UpdateType => UpdateType.Message;
    public ChatType ChatType { get; }

    public MessageSupportedTypeInformation(ChatType chatType)
    {
        this.ChatType = chatType;
    }
}