using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SoundManager.Core.Models;

namespace SoundManager.Infrastructure.Database.Config;

public class SoundEffectConfig : IEntityTypeConfiguration<SoundEffect>
{
    public void Configure(EntityTypeBuilder<SoundEffect> builder)
    {
        builder.HasKey(x => x.Id);
        builder.Property(x => x.Id).ValueGeneratedOnAdd();
        builder.Property(x => x.Name).IsRequired();
        builder.Property(x => x.FilePath).IsRequired();
        builder.Property(x => x.VolumePercent).IsRequired().HasDefaultValue(1);
    }
}