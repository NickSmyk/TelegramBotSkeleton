using TelegramBotSkeleton.Models.Interfaces;

namespace TelegramBotSkeleton.Dialog.Interfaces;

public interface IDialog
{ 
    Task Next(IMessageProperties messageProperties, int? lastExecutedStageNumber = null);
}