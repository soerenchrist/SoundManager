using SoundManager.Core.Models;

namespace SoundManager.UseCases.Interfaces;

public interface IGetSoundEffectsUseCase
{
    Task<List<SoundEffect>> GetSoundEffectsAsync(Guid? groupId, CancellationToken cancellationToken = default);
}