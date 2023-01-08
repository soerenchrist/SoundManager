using NAudio.Wave;
using SoundManager.Core.Models;
using SoundManager.Infrastructure.Database;
using SoundManager.UseCases.Interfaces;

namespace SoundManager.UseCases.Sound;

public class UploadSoundEffectUseCase : IUploadSoundEffectUseCase
{
    private readonly string _directoryPath;
    private readonly AppDbContext _appDbContext;

    public UploadSoundEffectUseCase(string directoryPath,
        AppDbContext appDbContext)
    {
        _directoryPath = directoryPath;
        _appDbContext = appDbContext;
    }

    public async Task<SoundEffect> AddSoundEffectAsync(Stream file, string name, double volumePercent, int offset)
    {
        var guid = Guid.NewGuid();
        var filePath = Path.Combine(_directoryPath, $"{guid}.mp3");

        await using (var fileStream = File.Create(filePath))
        {
            await file.CopyToAsync(fileStream);
        }

        await using var fileReader = new Mp3FileReader(filePath);
        var duration = fileReader.TotalTime;
        var durationMillis = (int)duration.TotalMilliseconds;

        var soundEffect = new SoundEffect
        {
            Id = guid,
            Name = name,
            VolumePercent = volumePercent,
            Offset = offset,
            FilePath = filePath,
            TotalMilliseconds = durationMillis,
            PlayDurationMilliseconds = durationMillis
        };
        await _appDbContext.AddAsync(soundEffect);
        await _appDbContext.SaveChangesAsync();
        return soundEffect;
    }
}