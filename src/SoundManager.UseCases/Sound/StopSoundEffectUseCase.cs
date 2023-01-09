using Ardalis.Result;
using SoundManager.Core.Interfaces;
using SoundManager.UseCases.Interfaces;

namespace SoundManager.UseCases.Sound;

public class StopSoundEffectUseCase : IStopSoundEffectUseCase
{
    private readonly ISoundPlayer _soundPlayer;

    public StopSoundEffectUseCase(ISoundPlayer soundPlayer)
    {
        _soundPlayer = soundPlayer;
    }

    public async Task<Result> StopSoundEffect(Guid token, int fadeDurationMillis = -1)
    {
        var stopped = await _soundPlayer.StopSound(token, fadeDurationMillis);
        return stopped ? Result.Success() : Result.Error("Token not valid anymore");
    }
}