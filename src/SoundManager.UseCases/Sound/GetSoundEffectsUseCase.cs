using Microsoft.EntityFrameworkCore;
using SoundManager.Core.Models;
using SoundManager.Infrastructure.Database;
using SoundManager.UseCases.Interfaces;

namespace SoundManager.UseCases.Sound;

public class GetSoundEffectsUseCase : IGetSoundEffectsUseCase, IUseCase
{
    private readonly AppDbContext _context;

    public GetSoundEffectsUseCase(AppDbContext context)
    {
        _context = context;
    }

    public async Task<List<SoundEffect>> GetSoundEffectsAsync(Guid? groupId,
        CancellationToken cancellationToken = default)
    {
        if (groupId.HasValue)
        {
            var group = await _context.Groups
                .Include(x => x.SoundEffects)
                .FirstOrDefaultAsync(x => x.Id == groupId, cancellationToken);
            if (group == null) return new List<SoundEffect>(0);
            return group.SoundEffects.ToList();
        }

        return await _context.SoundEffects.Where(x => x.Groups.Count == 0).ToListAsync(cancellationToken);
    }
}