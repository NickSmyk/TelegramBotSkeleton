using TelegramBotSkeleton.Models.Dtos;

namespace TelegramBotSkeleton.Services.Interfaces;

public interface IChatDataProviderService
{
    Task CreateDialog(long chatId, string dialogName, int stageNumber);
    Task UpdateDialogStage(long chatId, int stageNumber);
    Task DeleteDialog(long chatId);
    Task<bool> IsChatRegistered(long chatId);
    Task<long> GetChatIdByChatName(string chatName);
    Task<bool> HasActiveDialog(long chatId);
    Task<DialogDto> GetDialog(long chatId);
}