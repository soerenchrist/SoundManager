using Ardalis.Result;

namespace SoundManager.UseCases.Interfaces;

public interface IDeleteSoundEffectUseCase
{
    Task<Result> DeleteSoundEffectAsync(Guid id, CancellationToken cancellationToken = default);
}