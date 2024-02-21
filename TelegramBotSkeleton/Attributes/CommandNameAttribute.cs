namespace TelegramBotSkeleton.Attributes;

public class CommandNameAttribute : Attribute
{
    public readonly IEnumerable<string> CommandNames;


    public CommandNameAttribute(params string [] commands)
    {
        CommandNames = commands;
    }

    public virtual bool Contains(string command)
    {
        foreach (string commandName in CommandNames)
        {
            if (commandName.ToLower() == command.ToLower())
            {
                return true;
            }
        }
        
        return false;
    }
}