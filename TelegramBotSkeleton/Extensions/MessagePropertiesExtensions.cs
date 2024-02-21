using TelegramBotSkeleton.Models.Interfaces;

namespace TelegramBotSkeleton.Extensions;

public static class MessagePropertiesExtensions
{
    public static long GetMessageChatId(this IMessageProperties messageProperties)
    {
        long? chatId = messageProperties.Update.Message?.Chat.Id;
        if (chatId is null)
        {
            //TODO:WORK -> change this to custom
            throw new Exception();
        }

        return chatId.Value;
    }
}