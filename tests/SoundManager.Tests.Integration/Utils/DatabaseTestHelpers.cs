using Microsoft.Data.Sqlite;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using SoundManager.Core.Models;
using SoundManager.Dtos;
using SoundManager.Infrastructure.Database;

namespace SoundManager.Tests.Integration.Utils;

public class DatabaseTestHelpers : IDisposable
{
    private readonly IServiceScope _scope;
    private readonly AppDbContext _context;

    public DatabaseTestHelpers(IServiceProvider serviceProvider)
    {
        _scope = serviceProvider.CreateScope();
        _context = _scope.ServiceProvider.GetRequiredService<AppDbContext>();
    }


    public bool SoundEffectExists(SoundEffectDto soundEffect)
    {
        var dbEffect = _context.SoundEffects.FirstOrDefault(x => x.Id == soundEffect.Id);
        if (dbEffect == null) return false;
        
        if (dbEffect.Name != soundEffect.Name) return false;
        if (dbEffect.Offset != soundEffect.Offset) return false;
        if (Math.Abs(dbEffect.VolumePercent - soundEffect.VolumePercent) > 0.00001) return false;
        if (dbEffect.TotalMilliseconds != soundEffect.TotalMilliseconds) return false;

        return true;
    }

    private void ClearDatabase()
    {
        _context.Groups.ExecuteDelete();
        _context.SoundEffects.ExecuteDelete();
    }

    public void Dispose()
    {
        ClearDatabase();
    }
}