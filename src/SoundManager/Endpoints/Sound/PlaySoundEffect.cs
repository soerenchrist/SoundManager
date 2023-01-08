using FastEndpoints;
using SoundManager.UseCases.Interfaces;

namespace SoundManager.Endpoints.Sound;

public class PlaySoundEffect : Endpoint<PlaySoundEffectRequest, PlaySoundEffectResponse>
{
    private readonly IPlaySoundEffectUseCase _playSoundEffectUseCase;

    public PlaySoundEffect(IPlaySoundEffectUseCase playSoundEffectUseCase)
    {
        _playSoundEffectUseCase = playSoundEffectUseCase;
    }

    public override void Configure()
    {
        Post("sounds/{id}/play");
        AllowAnonymous();
    }

    public override async Task HandleAsync(PlaySoundEffectRequest req, CancellationToken ct)
    {
        var result = await _playSoundEffectUseCase.PlaySoundEffectAsync(req.Id);
        await SendAsync(new PlaySoundEffectResponse(result.IsSuccess), cancellation: ct);
    }
}