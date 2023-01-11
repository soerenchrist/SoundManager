using Ardalis.Result;

namespace SoundManager.UseCases.Interfaces;

public interface IStopSoundEffectUseCase
{
    Task<Result> StopSoundEffectAsync(Guid token, int fadeDurationMillis = -1);
}