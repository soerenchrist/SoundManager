using Ardalis.Result;
using Microsoft.EntityFrameworkCore;
using SoundManager.Infrastructure.Database;
using SoundManager.UseCases.Interfaces;

namespace SoundManager.UseCases.Sound;

public class AddToGroupUseCase : IAddToGroupUseCase, IUseCase
{
    private readonly AppDbContext _context;

    public AddToGroupUseCase(AppDbContext context)
    {
        _context = context;
    }

    public async Task<Result> AddSoundEffectToGroupAsync(Guid soundEffectId, Guid groupId,
        CancellationToken cancellationToken = default)
    {
        var group = await _context.Groups.FirstOrDefaultAsync(x => x.Id == groupId, cancellationToken);
        if (group == null)
        {
            return Result.Error($"Group with id {groupId} not found");
        }

        var soundEffect =
            await _context.SoundEffects.FirstOrDefaultAsync(x => x.Id == soundEffectId, cancellationToken);
        if (soundEffect == null)
        {
            return Result.Error($"Sound effect with id {soundEffectId} not found");
        }

        group.AddSoundEffect(soundEffect);
        await _context.SaveChangesAsync(cancellationToken);
        return Result.Success();
    }
}