using Telegram.Bot;
using Telegram.Bot.Types;

namespace TelegramBotSkeleton.Models.Interfaces;

public interface IMessageProperties
{
    ITelegramBotClient TelegramBotClient { get; set; }
    Update Update { get; set; }
}