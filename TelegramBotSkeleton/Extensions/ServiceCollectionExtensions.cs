using Microsoft.Extensions.DependencyInjection;
using TelegramBotSkeleton.Builder;
using TelegramBotSkeleton.Commands.Interfaces;
using TelegramBotSkeleton.Services.CommandHandling.Interfaces;

namespace TelegramBotSkeleton.Extensions;

//TODO:WORK -> maybe move this into builder directly??
public static class ServiceCollectionExtensions
{
    public static IServiceCollection Build(this IServiceCollection services)
    {
        BotBuilder builder = new(services);
        builder.Build();
        return services;
    }
    
    public static IServiceCollection RegisterCommands(this IServiceCollection services)
    {
        Type interfaceType = typeof(ICommand);
        IEnumerable<Type> types = AppDomain.CurrentDomain.GetAssemblies()
            .SelectMany(s => s.GetTypes())
            .Where(assembly => interfaceType.IsAssignableFrom(assembly) && !assembly.IsInterface && !assembly.IsAbstract);

        foreach (Type command in types)
        {
            services.AddScoped(typeof(ICommand), command);
        }

        return services;
    }
    
    public static IServiceCollection RegisterHandlers(this IServiceCollection services)
    {
        Type interfaceType = typeof(IUpdateHandler);
        IEnumerable<Type> types = AppDomain.CurrentDomain.GetAssemblies()
            .SelectMany(s => s.GetTypes())
            .Where(assembly => interfaceType.IsAssignableFrom(assembly) && !assembly.IsInterface && !assembly.IsAbstract);

        foreach (Type command in types)
        {
            services.AddScoped(typeof(IUpdateHandler), command);
        }

        return services;
    }
}