using Bogus;
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

    public List<SoundEffect> CreateSoundEffects(int amount)
    {
        var faker = new Faker<SoundEffect>()
            .RuleFor(x => x.Id, f => Guid.NewGuid())
            .RuleFor(x => x.TotalMilliseconds, f => f.Random.Int(0, 1000))
            .RuleFor(x => x.VolumePercent, f => f.Random.Double(0, 1))
            .RuleFor(x => x.Offset, f => f.Random.Int(0, 1000))
            .RuleFor(x => x.Name, f => f.Lorem.Word())
            .RuleFor(x => x.FilePath, f => f.System.FilePath());

        var soundEffects = faker.Generate(amount);
        _context.SoundEffects.AddRange(soundEffects);
        _context.SaveChanges();
        return soundEffects;
    }

    public List<Group> CreateGroups(int amount)
    {
        var faker = new Faker<Group>()
            .RuleFor(x => x.Id, Guid.NewGuid)
            .RuleFor(x => x.Name, f => f.Lorem.Word());
        var groups = faker.Generate(amount);
        
        _context.Groups.AddRange(groups);
        _context.SaveChanges();
        return groups;
    }
}