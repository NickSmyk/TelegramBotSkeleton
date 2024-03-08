using Telegram.Bot.Types.Enums;
using TelegramBotSkeleton.Commands.Interfaces;

namespace TelegramBotSkeleton.Commands.CommandTypes;

public class CallbackQuerySupportedTypeInformation : ISupportedTypeInformation
{
    public UpdateType UpdateType => UpdateType.CallbackQuery;
    public ChatType? ChatType => null;
}