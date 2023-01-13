using Ardalis.ApiEndpoints;
using Microsoft.AspNetCore.Mvc;
using SoundManager.UseCases.Interfaces;

namespace SoundManager.Endpoints.Sound;

public class StopSoundEffect : EndpointBaseAsync.WithRequest<StopSoundEffectRequest>.WithResult<StopSoundEffectResponse>
{
    private readonly IStopSoundEffectUseCase _stopSoundEffectUseCase;

    public StopSoundEffect(IStopSoundEffectUseCase stopSoundEffectUseCase)
    {
        _stopSoundEffectUseCase = stopSoundEffectUseCase;
    }

    [HttpPost("api/v1/sounds/{token:guid}/stop")]
    public override async Task<StopSoundEffectResponse> HandleAsync(StopSoundEffectRequest req, CancellationToken ct = default)
    {
        var result = await _stopSoundEffectUseCase.StopSoundEffectAsync(req.Token, req.FadeDurationMillis);
        return new StopSoundEffectResponse(result.IsSuccess);
    }
}