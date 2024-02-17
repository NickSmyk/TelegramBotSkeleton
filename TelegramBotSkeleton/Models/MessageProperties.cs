using Telegram.Bot;
using Telegram.Bot.Types;
using TelegramBotSkeleton.Models.Interfaces;

namespace TelegramBotSkeleton.Models;

public class MessageProperties : IMessageProperties
{
    public ITelegramBotClient TelegramBotClient { get; set; }
    public Update Update { get; set; }
    
    public MessageProperties(ITelegramBotClient telegramBotClient, Update update)
    {
        this.TelegramBotClient = telegramBotClient;
        this.Update = update;
    }
}