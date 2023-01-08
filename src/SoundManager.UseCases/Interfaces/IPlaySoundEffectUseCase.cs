using Ardalis.Result;

namespace SoundManager.UseCases.Interfaces;

public interface IPlaySoundEffectUseCase
{
    Task<Result> PlaySoundEffectAsync(Guid id, CancellationToken ct = default);
}