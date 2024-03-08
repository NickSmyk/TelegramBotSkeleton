using TelegramBotSkeleton.Models.Interfaces;

namespace TelegramBotSkeleton.Extensions;

public static class MessagePropertiesExtensions
{
    public static long GetMessageChatId(this IMessageProperties messageProperties)
    {
        long? chatId = messageProperties.Update.Message?.Chat.Id;
        chatId ??= messageProperties.Update.CallbackQuery?.From.Id;
        if (chatId is null)
        {
            //TODO:WORK -> change this to custom
            throw new Exception("Couldn't retrieve chat id from message properties");
        }

        return chatId.Value;
    }
}