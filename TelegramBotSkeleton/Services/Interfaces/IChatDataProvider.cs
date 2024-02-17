namespace TelegramBotSkeleton.Services.Interfaces;

public interface IChatDataProvider
{
    Task<bool> IsChatRegistered(long chatId);
    Task<long> GetChatIdByChatName(string chatName);
}