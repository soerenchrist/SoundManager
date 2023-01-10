using System.Reflection;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using SoundManager.UseCases.Interfaces;

namespace SoundManager.UseCases;

public static class DependencyInjection
{
    public static void AddUseCases(this IServiceCollection services, IConfiguration configuration)
    {
        RegisterUseCases(services);
    }

    private static void RegisterUseCases(IServiceCollection services)
    {
        var assembly = Assembly.GetExecutingAssembly();
        var useCases = assembly.GetTypes().Where(x => x.GetInterfaces().Contains(typeof(IUseCase)));
        foreach (var useCase in useCases)
        {
            var interfaceType = useCase.GetInterfaces().First(x => x != typeof(IUseCase));
            services.AddScoped(interfaceType, useCase);
        }
    }
}