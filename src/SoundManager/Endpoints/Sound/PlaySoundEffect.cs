using FastEndpoints;
using SoundManager.Core.Models;
using SoundManager.UseCases.Interfaces;

namespace SoundManager.Endpoints.Sound;

public class PlaySoundEffect : Endpoint<PlaySoundEffectRequest, SoundPlayResult>
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
        var result = await _playSoundEffectUseCase.PlaySoundEffectAsync(req.Id, ct);
        if (result.IsSuccess)
        {
            await SendAsync(result.Value, cancellation: ct);
        }
        else
        {
            await SendNotFoundAsync(ct);
        }
    }
}