using Microsoft.AspNetCore.Mvc;
using SoundManager.Dtos;
using SoundManager.UseCases.Interfaces;

namespace SoundManager.Endpoints.Sound;

public class GetSoundEffectById : EndpointBaseAsync.WithRequest<GetSoundEffectByIdRequest>.WithActionResult<SoundEffectDto>
{
    private readonly IGetSoundEffectUseCase _getSoundEffectUseCase;

    public GetSoundEffectById(IGetSoundEffectUseCase getSoundEffectUseCase)
    {
        _getSoundEffectUseCase = getSoundEffectUseCase;
    }
    
    [HttpGet("api/v1/sounds/{id}")]
    public override async Task<ActionResult<SoundEffectDto>> HandleAsync([FromRoute] GetSoundEffectByIdRequest req, CancellationToken ct = default)
    {
        var result = await _getSoundEffectUseCase.GetSoundEffectAsync(req.Id);
        if (result.IsSuccess)
        {
            var sound = result.Value;
            return new SoundEffectDto
            {
                VolumePercent = sound.VolumePercent,
                Id = sound.Id,
                Name = sound.Name,
                Offset = sound.Offset,
                TotalMilliseconds = sound.TotalMilliseconds
            };
        }

        return NotFound();
    }
}