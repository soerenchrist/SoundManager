using Ardalis.Result;
using NAudio.Wave;
using SoundManager.Infrastructure.Database;
using SoundManager.UseCases.Interfaces;

namespace SoundManager.UseCases.Sound;

public class PlaySoundEffectUseCase : IPlaySoundEffectUseCase
{
    private readonly AppDbContext _context;

    public PlaySoundEffectUseCase(AppDbContext context)
    {
        _context = context;
    }

    public async Task<Result> PlaySoundEffectAsync(Guid id, CancellationToken cancellationToken = default)
    {
        var soundEffect = await _context.SoundEffects.FindAsync(new object?[] { id }, cancellationToken: cancellationToken);
        if (soundEffect == null)
        {
            return Result.NotFound($"Sound effect with id {id} not found");
        }

        await using var audioFile = new AudioFileReader(soundEffect.FilePath);
        using var outputDevice = new WaveOutEvent();
        outputDevice.Init(audioFile);
        outputDevice.Play();
        while (outputDevice.PlaybackState == PlaybackState.Playing)
        {
            if (cancellationToken.IsCancellationRequested) return Result.Error("Cancelled");
            await Task.Delay(200, cancellationToken);
        }

        return Result.Success();
    }
}