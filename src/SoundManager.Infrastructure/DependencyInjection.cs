using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using SoundManager.Infrastructure.Database;

namespace SoundManager.Infrastructure;

public static class DependencyInjection
{
   public static void AddInfrastructure(this IServiceCollection services)
   {
      services.AddDbContext<AppDbContext>(options =>
      {
         options.UseSqlite("Data Source=SoundManager.db");
      });
   } 
}