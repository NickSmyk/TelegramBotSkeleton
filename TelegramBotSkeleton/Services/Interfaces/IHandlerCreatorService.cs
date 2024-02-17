using TelegramBotSkeleton.Models.Interfaces;
using TelegramBotSkeleton.Services.CommandHandling.Interfaces;

namespace TelegramBotSkeleton.Services.Interfaces;

public interface IHandlerCreatorService
{
    IUpdateHandler GetHandler(IMessageProperties messageProperties);
}