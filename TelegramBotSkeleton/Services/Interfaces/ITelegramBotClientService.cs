using Telegram.Bot;

namespace TelegramBotSkeleton.Services.Interfaces;

public interface ITelegramBotClientService
{
    TelegramBotClient TelegramBotClient { get; }
    Task<bool> SendMessage(long chatId, string message);
    Task<bool> SendMessage(string chatName, string message);
}