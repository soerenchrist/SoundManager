using Microsoft.EntityFrameworkCore;
using SoundManager.Core.Models;

namespace SoundManager.Infrastructure.Database;

public class AppDbContext : DbContext
{
    public DbSet<Group> Groups => Set<Group>();
    public DbSet<SoundEffect> SoundEffects => Set<SoundEffect>();

    public AppDbContext(DbContextOptions options) : base(options)
    {
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(AppDbContext).Assembly);
    }
}