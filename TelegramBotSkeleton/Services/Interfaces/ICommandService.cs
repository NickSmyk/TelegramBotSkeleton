using TelegramBotSkeleton.Commands.Interfaces;
using TelegramBotSkeleton.Models.Interfaces;

namespace TelegramBotSkeleton.Services.Interfaces;

public interface ICommandService
{
    //TODO:WORK -> think about this CommandType handling
    Task TryExecutingCommand(string message, ISupportedTypeInformation supportedTypeInformation,  IMessageProperties messageProperties);
}