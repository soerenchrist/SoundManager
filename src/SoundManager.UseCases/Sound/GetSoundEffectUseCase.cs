using Ardalis.Result;
using SoundManager.Core.Models;
using SoundManager.Infrastructure.Database;
using SoundManager.UseCases.Interfaces;

namespace SoundManager.UseCases.Sound;

public class GetSoundEffectUseCase : IGetSoundEffectUseCase, IUseCase
{
    private readonly AppDbContext _context;

    public GetSoundEffectUseCase(AppDbContext context)
    {
        _context = context;
    }

    public async Task<Result<SoundEffect>> GetSoundEffectAsync(Guid id)
    {
        var soundEffect = await _context.SoundEffects.FindAsync(id);
        if (soundEffect == null)
        {
            return Result<SoundEffect>.NotFound();
        }

        return soundEffect;
    }
}