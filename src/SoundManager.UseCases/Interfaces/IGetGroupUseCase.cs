using Ardalis.Result;
using SoundManager.Core.Models;

namespace SoundManager.UseCases.Interfaces;

public interface IGetGroupUseCase
{

    public Task<Result<Group>> GetGroupAsync(Guid groupId, CancellationToken cancellationToken = default);
}