using FastEndpoints;
using SoundManager.Core.Models;
using SoundManager.UseCases.Interfaces;
using SoundManager.Util;

namespace SoundManager.Endpoints.Sound;

public class CreateSoundEffect : Endpoint<CreateSoundEffectRequest, SoundEffect>
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
            await SendAsync(result, cancellation: ct);
        }
        else
        {
            await SendStringAsync(result.ErrorMessages(), cancellation: ct);
        }
    }
}