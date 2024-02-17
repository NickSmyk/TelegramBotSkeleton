namespace TelegramBotSkeleton.Models;

public class BotToken
{
    public string Token { get; set; }

    public BotToken(string token)
    {
        this.Token = token;
    }
}