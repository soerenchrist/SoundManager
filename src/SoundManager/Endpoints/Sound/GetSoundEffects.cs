using FastEndpoints;
using SoundManager.Core.Models;
using SoundManager.Dtos;
using SoundManager.UseCases.Interfaces;

namespace SoundManager.Endpoints.Sound;

public class GetSoundEffects : Endpoint<GetSoundEffectsRequest, List<SoundEffectDto>>
{
    private readonly IGetSoundEffectsUseCase _getSoundEffectsUseCase;

    public GetSoundEffects(IGetSoundEffectsUseCase getSoundEffectsUseCase)
    {
        _getSoundEffectsUseCase = getSoundEffectsUseCase;
    }

    public override void Configure()
    {
        AllowAnonymous();
        Get("sounds");
    }

    public override async Task HandleAsync(GetSoundEffectsRequest req, CancellationToken ct)
    {
        var soundEffects =
            await _getSoundEffectsUseCase.GetSoundEffectsAsync(req.GroupId, ct);
        await SendAsync(soundEffects.Select(x => new SoundEffectDto
        {
            Name = x.Name,
            Id = x.Id,
            Offset = x.Offset,
            TotalMilliseconds = x.TotalMilliseconds,
            VolumePercent = x.VolumePercent
        }).ToList(), cancellation: ct);
    }
}