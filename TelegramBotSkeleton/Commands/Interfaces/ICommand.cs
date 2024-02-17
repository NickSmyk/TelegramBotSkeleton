using TelegramBotSkeleton.Models.Dtos;

namespace TelegramBotSkeleton.Commands.Interfaces;

public interface ICommand
{
    Task Execute(CommandExecutionDto executionDto);
    IEnumerable<ISupportedTypeInformation> GetCommandTypes();
}