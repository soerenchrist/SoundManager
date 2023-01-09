using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using SoundManager.Infrastructure.Database;
using SoundManager.UseCases.Interfaces;
using SoundManager.UseCases.Output;
using SoundManager.UseCases.Sound;

namespace SoundManager.UseCases;

public static class DependencyInjection
{
    public static void AddUseCases(this IServiceCollection services, IConfiguration configuration)
    {
        var soundDirectory = configuration["Sounds:Directory"];
        if (soundDirectory == null)
        {
            throw new ArgumentException("Sound directory not found");
        }

        if (!Directory.Exists(soundDirectory))
        {
            Directory.CreateDirectory(soundDirectory);
        }

        services.AddScoped<IUploadSoundEffectUseCase>(provider => new UploadSoundEffectUseCase(soundDirectory,
            provider.GetRequiredService<AppDbContext>()));
        services.AddScoped<IPlaySoundEffectUseCase, PlaySoundEffectUseCase>();
        services.AddScoped<IGetSoundEffectUseCase, GetSoundEffectUseCase>();
        services.AddScoped<IGetOutputDevicesUseCase, GetOutputDevicesUseCase>();
        services.AddScoped<IStopSoundEffectUseCase, StopSoundEffectUseCase>();
    }
}