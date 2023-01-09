using Ardalis.Result;
using NAudio.Wave;
using SoundManager.Core.Interfaces;
using SoundManager.Core.Models;
using SoundManager.Infrastructure.Database;
using SoundManager.UseCases.Interfaces;

namespace SoundManager.UseCases.Sound;

public class PlaySoundEffectUseCase : IPlaySoundEffectUseCase
{
    private readonly AppDbContext _context;
    private readonly ISoundPlayer _soundPlayer;

    public PlaySoundEffectUseCase(AppDbContext context, ISoundPlayer soundPlayer)
    {
        _context = context;
        _soundPlayer = soundPlayer;
    }

    public async Task<Result<SoundPlayResult>> PlaySoundEffectAsync(Guid id,
        CancellationToken cancellationToken = default)
    {
        var soundEffect =
            await _context.SoundEffects.FindAsync(new object?[] { id }, cancellationToken: cancellationToken);
        if (soundEffect == null)
        {
            return Result.NotFound($"Sound effect with id {id} not found");
        }

        var result = _soundPlayer.PlaySound(soundEffect);
        return result;
        /*
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
        */
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