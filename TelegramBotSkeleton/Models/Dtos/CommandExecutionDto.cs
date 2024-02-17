using TelegramBotSkeleton.Models.Interfaces;

namespace TelegramBotSkeleton.Models.Dtos;

public class CommandExecutionDto
{
    public IMessageProperties MessageProperties { get; set; } = null!;
    public string? MessageData { get; set; }

    public CommandExecutionDto()
    { }
    
    public CommandExecutionDto(IMessageProperties messageProperties, string? messageData = null)
    {
        this.MessageProperties = messageProperties;
        this.MessageData = messageData;
    }
}