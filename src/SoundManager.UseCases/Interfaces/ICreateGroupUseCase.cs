using Ardalis.Result;
using SoundManager.Core.Models;

namespace SoundManager.UseCases.Interfaces;

public interface ICreateGroupUseCase
{
    Task<Result<Group>> CreateGroupAsync(string name, CancellationToken cancellationToken = default);
}