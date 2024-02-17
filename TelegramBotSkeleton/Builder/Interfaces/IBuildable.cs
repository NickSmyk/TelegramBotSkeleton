using Microsoft.Extensions.DependencyInjection;

namespace TelegramBotSkeleton.Builder.Interfaces;

public interface IBuildable
{
    public IServiceCollection Build();
}