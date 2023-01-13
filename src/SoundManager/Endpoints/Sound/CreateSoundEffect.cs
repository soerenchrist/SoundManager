using Microsoft.AspNetCore.Mvc;
using SoundManager.Dtos;
using SoundManager.UseCases.Interfaces;
using SoundManager.Util;

namespace SoundManager.Endpoints.Sound;

public class CreateSoundEffect : EndpointBaseAsync.WithRequest<CreateSoundEffectRequest>.WithActionResult<SoundEffectDto>
{
    private readonly IUploadSoundEffectUseCase _uploadSoundEffectUseCase;

    public CreateSoundEffect(IUploadSoundEffectUseCase uploadSoundEffectUseCase)
    {
        _uploadSoundEffectUseCase = uploadSoundEffectUseCase;
    }


    [HttpPost("api/v1/sounds")]
    public override async Task<ActionResult<SoundEffectDto>> HandleAsync([FromForm]CreateSoundEffectRequest req, CancellationToken ct = default)
    {
        var result = await _uploadSoundEffectUseCase.AddSoundEffectAsync(req.File.OpenReadStream(), req.Name,
            req.VolumePercent, req.Offset, ct);
        if (result.IsSuccess)
        {
            var sound = result.Value;
            return new SoundEffectDto
            {
                TotalMilliseconds = sound.TotalMilliseconds,
                Id = sound.Id,
                Name = sound.Name,
                VolumePercent = sound.VolumePercent,
                Offset = sound.Offset
            };
        }

        return BadRequest(result.ErrorMessages()); 
    }
}