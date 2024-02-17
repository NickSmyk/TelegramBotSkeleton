using Telegram.Bot;
using TelegramBotSkeleton.Models;
using TelegramBotSkeleton.Services.Interfaces;

namespace TelegramBotSkeleton.Services.BotClientServices;

public class TelegramBotClientService : ITelegramBotClientService
{
    private readonly IChatDataProvider _chatDataProvider;
    public TelegramBotClient TelegramBotClient { get; }

    public TelegramBotClientService(BotToken botToken, IChatDataProvider chatDataProvider)
    {
        _chatDataProvider = chatDataProvider;
        this.TelegramBotClient = new TelegramBotClient(botToken.Token);
    }

    public async Task<bool> SendMessage(long chatId, string message)
    {
        bool chatIsRegistered = await _chatDataProvider.IsChatRegistered(chatId);
        if (!chatIsRegistered)
        {
            return false;
        }
        
        await this.TelegramBotClient.SendTextMessageAsync(chatId, message);
        return true;
    }
    
    public async Task<bool> SendMessage(string chatName, string message)
    {
        long chatId = await _chatDataProvider.GetChatIdByChatName(chatName);
        await this.TelegramBotClient.SendTextMessageAsync(chatId, message);
        return true;
    }
}