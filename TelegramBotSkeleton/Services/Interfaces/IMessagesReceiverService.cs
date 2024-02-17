using Telegram.Bot;
using Telegram.Bot.Types;

namespace TelegramBotSkeleton.Services.Interfaces;

public interface IMessagesReceiverService
{
    Task ProcessMessage(ITelegramBotClient botClient, Update update, CancellationToken cancellationToken);
}