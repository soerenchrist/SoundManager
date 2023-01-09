using Ardalis.Result;

namespace SoundManager.UseCases.Interfaces;

public interface IStopSoundEffectUseCase
{
    Result StopSoundEffect(Guid token);
}