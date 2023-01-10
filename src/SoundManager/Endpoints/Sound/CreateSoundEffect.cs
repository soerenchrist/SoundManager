using FastEndpoints;
using SoundManager.Core.Models;
using SoundManager.Dtos;
using SoundManager.UseCases.Interfaces;
using SoundManager.Util;

namespace SoundManager.Endpoints.Sound;

public class CreateSoundEffect : Endpoint<CreateSoundEffectRequest, SoundEffectDto>
{
    private readonly IUploadSoundEffectUseCase _uploadSoundEffectUseCase;

    public CreateSoundEffect(IUploadSoundEffectUseCase uploadSoundEffectUseCase)
    {
        _uploadSoundEffectUseCase = uploadSoundEffectUseCase;
    }

    public override void Configure()
    {
        AllowAnonymous();
        Post("/sounds");
        AllowFileUploads();
    }

    public override async Task HandleAsync(CreateSoundEffectRequest req, CancellationToken ct)
    {
        var result = await _uploadSoundEffectUseCase.AddSoundEffectAsync(req.File.OpenReadStream(), req.Name,
            req.VolumePercent, req.Offset, ct);
        if (result.IsSuccess)
        {
            var sound = result.Value;
            await SendAsync(new SoundEffectDto
            {
                TotalMilliseconds = sound.TotalMilliseconds,
                Id = sound.Id,
                Name = sound.Name,
                VolumePercent = sound.VolumePercent,
                Offset = sound.Offset
            }, cancellation: ct);
        }
        else
        {
            await SendStringAsync(result.ErrorMessages(), cancellation: ct);
        }
    }
}