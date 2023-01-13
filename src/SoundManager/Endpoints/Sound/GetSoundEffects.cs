using Microsoft.AspNetCore.Mvc;
using SoundManager.Dtos;
using SoundManager.UseCases.Interfaces;

namespace SoundManager.Endpoints.Sound;

public class GetSoundEffects : EndpointBaseAsync.WithRequest<GetSoundEffectsRequest>.WithResult<List<SoundEffectDto>>
{
    private readonly IGetSoundEffectsUseCase _getSoundEffectsUseCase;

    public GetSoundEffects(IGetSoundEffectsUseCase getSoundEffectsUseCase)
    {
        _getSoundEffectsUseCase = getSoundEffectsUseCase;
    }

    [HttpGet("api/v1/sounds")]
    public override async Task<List<SoundEffectDto>> HandleAsync([FromQuery]GetSoundEffectsRequest req, CancellationToken ct = default)
    {
        var soundEffects =
            await _getSoundEffectsUseCase.GetSoundEffectsAsync(req.GroupId, ct);

        return soundEffects.Select(x => new SoundEffectDto
        {
            Name = x.Name,
            Id = x.Id,
            Offset = x.Offset,
            TotalMilliseconds = x.TotalMilliseconds,
            VolumePercent = x.VolumePercent
        }).ToList();
    }
}