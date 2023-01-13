using SoundManager.Core.Models;
using SoundManager.UseCases.Interfaces;
using Ardalis.ApiEndpoints;
using Microsoft.AspNetCore.Mvc;

namespace SoundManager.Endpoints.Sound;

public class PlaySoundEffect : EndpointBaseAsync.WithRequest<PlaySoundEffectRequest>.WithResult<SoundPlayResult>
{
    private readonly IPlaySoundEffectUseCase _playSoundEffectUseCase;

    public PlaySoundEffect(IPlaySoundEffectUseCase playSoundEffectUseCase)
    {
        _playSoundEffectUseCase = playSoundEffectUseCase;
    }

    [HttpPost("api/v1/sounds/{id:guid}/play")]
    public override async Task<SoundPlayResult> HandleAsync(PlaySoundEffectRequest req, CancellationToken ct = default)
    {
        var result = await _playSoundEffectUseCase.PlaySoundEffectAsync(req.Id, ct);
        return result;
    }
}