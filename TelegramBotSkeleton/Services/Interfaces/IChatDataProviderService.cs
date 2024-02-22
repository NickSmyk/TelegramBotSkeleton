using TelegramBotSkeleton.Models.Dtos;

namespace TelegramBotSkeleton.Services.Interfaces;

public interface IChatDataProviderService
{
    Task SaveDialog(long chatId);
    Task DeleteDialog(long chatId);
    Task<bool> IsChatRegistered(long chatId);
    Task<long> GetChatIdByChatName(string chatName);
    Task<bool> HasActiveDialog(long chatId);
    Task<DialogDto> GetDialog(long chatId);
}