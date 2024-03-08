using System.Text.RegularExpressions;
using Telegram.Bot.Types.Enums;
using TelegramBotSkeleton.Attributes;
using TelegramBotSkeleton.Commands.Interfaces;
using TelegramBotSkeleton.Models.Dtos;
using TelegramBotSkeleton.Models.Interfaces;
using TelegramBotSkeleton.Services.Interfaces;

namespace TelegramBotSkeleton.Services.CommandHandling;

public class CommandService : ICommandService
{
    private readonly IEnumerable<ICommand> _commands;

    //TODO:WORK -> move this into a helper
    private const string COMMAND_SYMBOL = "/";
    private static readonly string _commandRecognitionPatternCommand = $"^([\\w\\-\\{COMMAND_SYMBOL}]+)";
    private readonly Regex _commandRecognitionPattern = new Regex(_commandRecognitionPatternCommand);

    public CommandService(IEnumerable<ICommand> commands)
    {
        _commands = commands;
    }

    public async Task TryExecutingCommand(string message, ISupportedTypeInformation supportedTypeInformation, IMessageProperties messageProperties)
    {
        foreach (ICommand command in _commands)
        {
            //TODO:WORK -> you need to refactor this method due to it's size
            IEnumerable<UpdateType> updateTypes = command.GetCommandTypes().Select(o => o.UpdateType);
            if (!updateTypes.Contains(supportedTypeInformation.UpdateType))
            {
                continue;
            }

            IEnumerable<ChatType?> supportedChatTypes = command.GetCommandTypes().Select(o => o.ChatType);
            if (!supportedChatTypes.Contains(supportedTypeInformation.ChatType))
            {
                continue;
            }

            string messageWithNoWhiteSpaces = message.Trim();
            if (!messageWithNoWhiteSpaces.StartsWith(COMMAND_SYMBOL))
            {
                continue;
            }

            string commandName = GetCommandNameFromTheMessage(message);
            bool wasMentionedInTheAttribute = CheckCommandNameByAttribute(command, commandName);
            bool commandNameMatchesClassName = CompareCommandToClassName(commandName, command);
            
            if (wasMentionedInTheAttribute || commandNameMatchesClassName)
            {
                string? messageData = GetMessageData(message);
                CommandExecutionDto commandExecutionDto = new(messageProperties, messageData);
                await command.Execute(commandExecutionDto);
            }
        }
    }

    private string? GetMessageData(string message)
    {
        string messageData = _commandRecognitionPattern.Replace(message, "");
        string messageDataNoWhiteSpaces = messageData.Trim();
        return messageDataNoWhiteSpaces;
    }

    private string GetCommandNameFromTheMessage(string message)
    {
        string commandWithSymbol = _commandRecognitionPattern.Match(message).Value;
        string command = commandWithSymbol.Replace(COMMAND_SYMBOL, "");
        return command;
    }

    private bool CompareCommandToClassName(string commandName, ICommand command)
    {
        string commandFullClassName = command.GetType().Name;
        string commandClassName = commandFullClassName.Replace("Command", "");
        return commandName.ToLower() == commandClassName.ToLower();
    }

    private static bool CheckCommandNameByAttribute(ICommand command, string commandName)
    {
        CommandNameAttribute? commandAttribute =
            command.GetType().GetCustomAttributes(typeof(CommandNameAttribute), true).FirstOrDefault() as CommandNameAttribute;

        if (commandAttribute is null)
        {
            return false;
        }

        return commandAttribute.Contains(commandName);
    }
}