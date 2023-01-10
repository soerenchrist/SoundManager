using Ardalis.Result;
using SoundManager.Core.Models;

namespace SoundManager.UseCases.Interfaces;

public interface IUploadSoundEffectUseCase
{
    Task<Result<SoundEffect>> AddSoundEffectAsync(Stream file, string name, double volumePercent, int offset,
        CancellationToken cancellationToken = default);
}