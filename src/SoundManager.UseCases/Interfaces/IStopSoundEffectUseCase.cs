using Ardalis.Result;

namespace SoundManager.UseCases.Interfaces;

public interface IStopSoundEffectUseCase
{
    Task<Result> StopSoundEffect(Guid token, int fadeDurationMillis = -1);
}