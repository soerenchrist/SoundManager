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

    public Result StopSoundEffect(Guid token)
    {
        var stopped = _soundPlayer.StopSound(token);
        return stopped ? Result.Success() : Result.Error("Token not valid anymore");
    }
}