using SoundManager.Core.Models;

namespace SoundManager.UseCases.Interfaces;

public interface IGetGroupsUseCase
{
    Task<List<Group>> GetGroupsAsync(CancellationToken cancellationToken = default);
}