using System.Reflection;
using TelegramBotSkeleton.Attributes.Dialog;
using TelegramBotSkeleton.Dialog.Interfaces;
using TelegramBotSkeleton.Models.Interfaces;

namespace TelegramBotSkeleton.Dialog;

public abstract class DialogBase : IDialog
{
    public async Task Next(IMessageProperties messageProperties, int? lastExecutedStageNumber = null)
    {
        MethodInfo method = GetMethod(lastExecutedStageNumber);
        if (method.ReturnType == typeof(Task))
        {
            await (Task)method.Invoke(this, new object[] { messageProperties })!;
            return;
        }
            
        method.Invoke(this, new object[] { messageProperties });
    }

    public int GetNumberOfTheLastStage()
    {
        IEnumerable<MethodInfo> methods = GetType().GetMethods()
            .Where(method => method.GetCustomAttributes(typeof(DialogStageAttribute), true).Any());

        IEnumerable<int> methodNumbers = 
            methods
                .Select(method => ((DialogStageAttribute)method.GetCustomAttributes(typeof(DialogStageAttribute), true).First()).StageNumber)
                .OrderByDescending(o => o);
        int? lastStageNumber = methodNumbers.FirstOrDefault();
        if (lastStageNumber is null)
        {
            //TODO:WORK -> change this to custom
            throw new Exception();
        }

        return lastStageNumber.Value;
    }

    protected MethodInfo GetMethod(int? lastExecutedStageNumber)
    {
        IEnumerable<MethodInfo> methods = GetType().GetMethods()
            .Where(method => method.GetCustomAttributes(typeof(DialogStageAttribute), true).Any());

        if (lastExecutedStageNumber.HasValue)
        {
            methods = methods.Where(method =>
                ((DialogStageAttribute)method.GetCustomAttributes(typeof(DialogStageAttribute), true).First())
                .StageNumber > lastExecutedStageNumber.Value);
        }

        methods = methods.OrderBy(method => ((DialogStageAttribute)method.GetCustomAttributes(typeof(DialogStageAttribute), true).First()).StageNumber);
        MethodInfo? method = methods.FirstOrDefault();
        if (method is null)
        {
            //TODO:WORK -> change this to custom
            throw new Exception();
        }

        return method;
    }
}