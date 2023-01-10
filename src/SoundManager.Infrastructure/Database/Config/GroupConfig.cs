using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SoundManager.Core.Models;

namespace SoundManager.Infrastructure.Database.Config;

public class GroupConfig : IEntityTypeConfiguration<Group>
{
    public void Configure(EntityTypeBuilder<Group> builder)
    {
        builder.HasKey(x => x.Id);
        builder.Property(x => x.Name).IsRequired();
        builder.HasMany(x => x.SoundEffects)
            .WithMany(x => x.Groups);
    }
}