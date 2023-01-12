using Ardalis.Result;

namespace SoundManager.UseCases.Interfaces;

public interface IDeleteGroupUseCase
{
   Task<Result> DeleteGroupAsync(Guid groupId, CancellationToken cancellationToken = default);
}