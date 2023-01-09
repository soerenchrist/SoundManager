using Ardalis.Result;
using SoundManager.Core.Models;

namespace SoundManager.UseCases.Interfaces;

public interface IPlaySoundEffectUseCase
{
    Task<Result<SoundPlayResult>> PlaySoundEffectAsync(Guid id, CancellationToken ct = default);
}