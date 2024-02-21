using TelegramBotSkeleton.Models.Dtos;

namespace TelegramBotSkeleton.Services.Interfaces;

public interface IChatDataProviderService
{
    Task<bool> IsChatRegistered(long chatId);
    Task<long> GetChatIdByChatName(string chatName);
    Task<bool> HasActiveDialog(long chatId);
    Task<DialogDto> GetDialog(long chatId);
}