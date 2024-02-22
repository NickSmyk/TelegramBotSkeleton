using TelegramBotSkeleton.Models.Interfaces;
using TelegramBotSkeleton.Services.CommandHandling.Interfaces;

namespace TelegramBotSkeleton.Services.Interfaces;

public interface IHandlerManagerService
{
    IUpdateHandler GetHandler(IMessageProperties messageProperties);
}