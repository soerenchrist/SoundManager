using Ardalis.Result;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using NAudio.Wave;
using SoundManager.Core.Models;
using SoundManager.Infrastructure.Database;
using SoundManager.UseCases.Interfaces;

namespace SoundManager.UseCases.Sound;

public class UploadSoundEffectUseCase : IUploadSoundEffectUseCase, IUseCase
{
    private readonly AppDbContext _appDbContext;
    private readonly string _directoryPath;

    public UploadSoundEffectUseCase(IConfiguration configuration,
        AppDbContext appDbContext)
    {
        var path = configuration["Sounds:Directory"];
        if (path == null) throw new ArgumentException("Path to sound files is not set in configuration");
        if (!Directory.Exists(path)) Directory.CreateDirectory(path);
        _directoryPath = path;
        _appDbContext = appDbContext;
    }

    public async Task<Result<SoundEffect>> AddSoundEffectAsync(Stream file, string name, double volumePercent, int offset, CancellationToken cancellationToken = default)
    {
        var guid = Guid.NewGuid();
        var filePath = Path.Combine(_directoryPath, $"{guid}.mp3");

        var existing = await _appDbContext.SoundEffects.FirstOrDefaultAsync(x => x.Name == name, cancellationToken: cancellationToken);
        if (existing != null) return Result.Error("Sound effect with this name already exists");
        try
        {
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
            };
            await _appDbContext.AddAsync(soundEffect, cancellationToken);
            await _appDbContext.SaveChangesAsync(cancellationToken);
            return soundEffect;
        }
        catch (Exception e)
        {
            return Result.Error(e.Message);
        }
    }
}