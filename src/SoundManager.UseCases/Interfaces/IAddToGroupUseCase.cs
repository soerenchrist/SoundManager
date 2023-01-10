using Ardalis.Result;

namespace SoundManager.UseCases.Interfaces;

public interface IAddToGroupUseCase
{
    Task<Result> AddSoundEffectToGroupAsync(Guid soundEffectId, Guid groupId, CancellationToken cancellationToken = default);
}