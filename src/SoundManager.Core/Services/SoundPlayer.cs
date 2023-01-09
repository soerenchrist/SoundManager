using System.Collections.Concurrent;
using NAudio.Wave;
using SoundManager.Core.Interfaces;
using SoundManager.Core.Models;

namespace SoundManager.Core.Services;

public class SoundPlayer : ISoundPlayer
{
    private readonly ConcurrentDictionary<Guid, WaveOutEvent> _playingSounds = new();

    public SoundPlayResult PlaySound(SoundEffect soundEffect)
    {
        var token = Guid.NewGuid();
        FireSoundEffect(token, soundEffect);

        return new SoundPlayResult(token, soundEffect.Id, soundEffect.TotalMilliseconds);
    }

    public bool StopSound(Guid token)
    {
        if (_playingSounds.TryGetValue(token, out var waveOutEvent))
        {
            waveOutEvent.Stop();
            waveOutEvent.Dispose();
            _playingSounds.TryRemove(token, out _);
            return true;
        }

        return false;
    }

    private void FireSoundEffect(Guid token, SoundEffect soundEffect)
    {
        Task.Run(async () =>
        {
            await using var audioFile = new AudioFileReader(soundEffect.FilePath);
            SetOffset(audioFile, soundEffect.Offset);
            var outputDevice = new WaveOutEvent
            {
                Volume = (float)soundEffect.VolumePercent
            };
            _playingSounds.TryAdd(token, outputDevice);
            outputDevice.Init(audioFile);
            outputDevice.Play();
            while (outputDevice.PlaybackState == PlaybackState.Playing)
            {
                await Task.Delay(200);
            }

            _playingSounds.TryRemove(token, out _);
        });
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