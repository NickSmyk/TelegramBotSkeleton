using TelegramBotSkeleton.Dialog.Interfaces;
using TelegramBotSkeleton.Models.Interfaces;

namespace TelegramBotSkeleton.Services.DialogHandling.Interfaces;

public interface IDialogHandlerService
{
    Task StartDialog<TDialog>(IMessageProperties messageProperties) where TDialog : IDialog;
    Task<bool> TryHandleTheDialog(IMessageProperties messageProperties);
}