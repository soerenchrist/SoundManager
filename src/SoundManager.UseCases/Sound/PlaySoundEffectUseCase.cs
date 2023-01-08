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
        var soundEffect =
            await _context.SoundEffects.FindAsync(new object?[] { id }, cancellationToken: cancellationToken);
        if (soundEffect == null)
        {
            return Result.NotFound($"Sound effect with id {id} not found");
        }

        await using var audioFile = new AudioFileReader(soundEffect.FilePath);
        SetOffset(audioFile, soundEffect.Offset);
        using var outputDevice = new WaveOutEvent
        {
            Volume = (float)soundEffect.VolumePercent
        };
        outputDevice.Init(audioFile);
        outputDevice.Play();
        while (outputDevice.PlaybackState == PlaybackState.Playing)
        {
            if (cancellationToken.IsCancellationRequested) return Result.Error("Cancelled");
            await Task.Delay(200, cancellationToken);
        }

        return Result.Success();
    }

    private void SetOffset(AudioFileReader audioFile, int offsetMillis)
    {
        var totalDuration = audioFile.TotalTime.TotalMilliseconds;
        var totalBytes = audioFile.Length;
        var bytesPerMilliseconds = totalBytes / totalDuration;

        var offsetBytes = bytesPerMilliseconds * offsetMillis;
        audioFile.Position = (int)offsetBytes;
    }
}