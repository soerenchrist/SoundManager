using FastEndpoints;
using SoundManager.UseCases.Interfaces;

namespace SoundManager.Endpoints.Sound;

public class StopSoundEffect : Endpoint<StopSoundEffectRequest, StopSoundEffectResponse>
{
    private readonly IStopSoundEffectUseCase _stopSoundEffectUseCase;

    public StopSoundEffect(IStopSoundEffectUseCase stopSoundEffectUseCase)
    {
        _stopSoundEffectUseCase = stopSoundEffectUseCase;
    }

    public override void Configure()
    {
        AllowAnonymous();
        Post("/sounds/{token}/stop");
    }

    public override async Task HandleAsync(StopSoundEffectRequest req, CancellationToken ct)
    {
        var result = await _stopSoundEffectUseCase.StopSoundEffect(req.Token, req.FadeDurationMillis);
        await SendAsync(new StopSoundEffectResponse(result.IsSuccess), cancellation: ct);
    }
}