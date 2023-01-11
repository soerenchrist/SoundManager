using Ardalis.Result;
using Microsoft.EntityFrameworkCore;
using SoundManager.Infrastructure.Database;
using SoundManager.UseCases.Interfaces;

namespace SoundManager.UseCases.Sound;

public class DeleteSoundEffectUseCase : IDeleteSoundEffectUseCase, IUseCase
{
    private readonly AppDbContext _context;

    public DeleteSoundEffectUseCase(AppDbContext context)
    {
        _context = context;
    }

    public async Task<Result> DeleteSoundEffectAsync(Guid id, CancellationToken cancellationToken = default)
    {
        var soundEffect = await _context.SoundEffects.FirstOrDefaultAsync(x => x.Id == id, cancellationToken);
        if (soundEffect is null)
        {
            return Result.NotFound();
        }

        var filepath = soundEffect.FilePath;
        if (File.Exists(filepath))
        {
            File.Delete(filepath);
        }

        _context.SoundEffects.Remove(soundEffect);
        await _context.SaveChangesAsync(cancellationToken);
        return Result.Success();
    }
}