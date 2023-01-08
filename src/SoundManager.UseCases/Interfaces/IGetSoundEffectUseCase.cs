using Ardalis.Result;
using SoundManager.Core.Models;

namespace SoundManager.UseCases.Interfaces;

public interface IGetSoundEffectUseCase
{
   Task<Result<SoundEffect>> GetSoundEffectAsync(Guid id); 
}