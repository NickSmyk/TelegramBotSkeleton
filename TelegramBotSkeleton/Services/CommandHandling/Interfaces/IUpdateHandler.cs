using Telegram.Bot.Types.Enums;
using TelegramBotSkeleton.Commands.Interfaces;
using TelegramBotSkeleton.Models.Interfaces;

namespace TelegramBotSkeleton.Services.CommandHandling.Interfaces;

public interface IUpdateHandler
{
    public IEnumerable<ISupportedTypeInformation> GetSupportedTypes();
    public Task Handle(IMessageProperties messageProperties);
}