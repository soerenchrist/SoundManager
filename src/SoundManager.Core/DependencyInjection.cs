using Microsoft.Extensions.DependencyInjection;
using SoundManager.Core.Interfaces;
using SoundManager.Core.Services;

namespace SoundManager.Core;

public static class DependencyInjection
{
   public static void AddCoreDependencies(this IServiceCollection services)
   {
      services.AddSingleton<ISoundPlayer, SoundPlayer>();
   } 
}