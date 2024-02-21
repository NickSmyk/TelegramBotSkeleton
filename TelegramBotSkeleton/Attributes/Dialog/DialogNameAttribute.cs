namespace TelegramBotSkeleton.Attributes.Dialog;

public class DialogNameAttribute : Attribute
{
    public readonly IEnumerable<string> DialogNames;


    public DialogNameAttribute(params string [] dialogs)
    {
        DialogNames = dialogs;
    }

    public virtual bool Contains(string dialog)
    {
        foreach (string dialogName in DialogNames)
        {
            if (dialogName.ToLower() == dialog.ToLower())
            {
                return true;
            }
        }
        
        return false;
    }
}