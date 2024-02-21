namespace TelegramBotSkeleton.Attributes.Dialog;

[AttributeUsage(AttributeTargets.Method)]
public class DialogStageAttribute : Attribute
{
    public int StageNumber { get; }

    public DialogStageAttribute(int stageNumber)
    {
        this.StageNumber = stageNumber;
    }
}