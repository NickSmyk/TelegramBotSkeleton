using TelegramBotSkeleton.Attributes.Dialog;
using TelegramBotSkeleton.Dialog.Interfaces;
using TelegramBotSkeleton.Extensions;
using TelegramBotSkeleton.Models.Dtos;
using TelegramBotSkeleton.Models.Interfaces;
using TelegramBotSkeleton.Services.DialogHandling.Interfaces;
using TelegramBotSkeleton.Services.Interfaces;

namespace TelegramBotSkeleton.Services.DialogHandling;

public sealed class DialogHandlerService : IDialogHandlerService
{
    private readonly IChatDataProviderService _chatDataProviderService;
    private readonly IEnumerable<IDialog> _dialogs;

    public DialogHandlerService(IChatDataProviderService chatDataProviderService, IEnumerable<IDialog> dialogs)
    {
        _chatDataProviderService = chatDataProviderService;
        _dialogs = dialogs;
    }

    public async Task StartDialog<TDialog>(IMessageProperties messageProperties) where TDialog : IDialog
    {
        IDialog dialog = GetDialog<TDialog>();
        long chatId = messageProperties.GetMessageChatId();
        int firstStageNumber = dialog.GetNumberOfTheFirstStage();
        await _chatDataProviderService.CreateDialog(chatId, typeof(TDialog).Name, firstStageNumber);
        await dialog.Next(messageProperties);
    }

    public async Task<bool> TryHandleTheDialog(IMessageProperties messageProperties)
    {
        long chatId = messageProperties.GetMessageChatId();
        bool hasActiveDialog = await _chatDataProviderService.HasActiveDialog(chatId);
        if (!hasActiveDialog)
        {
            return false;
        }

        DialogDto dialogDto = await _chatDataProviderService.GetDialog(chatId);
        IDialog dialog = GetDialog(dialogDto.Name);
        await dialog.Next(messageProperties, dialogDto.Stage);
        int lastStageNumber = dialog.GetNumberOfTheLastStage();
        int nextStageNumber = dialog.GetNumberOfTheNextStage(dialogDto.Stage);
        
        if (lastStageNumber <= nextStageNumber)
        {
            await _chatDataProviderService.DeleteDialog(chatId);
            return true;
        }

        await _chatDataProviderService.UpdateDialogStage(chatId, nextStageNumber);
        return true;
    }

    private IDialog GetDialog(string dialogName)
    {
        foreach (IDialog dialog in _dialogs)
        {
            bool wasMentionedInTheAttribute = CheckDialogNameByAttribute(dialog, dialogName);
            bool dialogNameMatchesClassName = CompareDialogToClassName(dialog, dialogName);
            
            if (wasMentionedInTheAttribute || dialogNameMatchesClassName)
            {
                return dialog;
            }
        }
        
        //TODO:WORK -> change this to custom
        throw new Exception();
    }

    private bool CompareDialogToClassName(IDialog dialog, string commandName)
    {
        string commandFullClassName = dialog.GetType().Name;
        string commandClassName = commandFullClassName.Replace("Dialog", "");
        return commandName.ToLower() == commandClassName.ToLower();
    }

    //TODO:WORK -> this looks like command thing
    private static bool CheckDialogNameByAttribute(IDialog dialog, string dialogName)
    {
        DialogNameAttribute? commandAttribute =
            dialog.GetType().GetCustomAttributes(typeof(DialogNameAttribute), true).FirstOrDefault() as DialogNameAttribute;

        if (commandAttribute is null)
        {
            return false;
        }

        return commandAttribute.Contains(dialogName);
    }

    private IDialog GetDialog<TDialog>() where TDialog : IDialog
    {
        foreach (IDialog dialog in _dialogs)
        {
            if (dialog.GetType() == typeof(TDialog))
            {
                return dialog;
            }
        }
        
        //TODO:WORK -> change this to custom
        throw new Exception();
    }
}