using FastEndpoints;
using SoundManager.UseCases.Interfaces;

namespace SoundManager.Endpoints.Sound;

public class CreateSoundEffect : Endpoint<CreateSoundEffectRequest, CreateSoundEffectResponse>
{
    private readonly IUploadSoundEffectUseCase _uploadSoundEffectUseCase;

    public CreateSoundEffect(IUploadSoundEffectUseCase uploadSoundEffectUseCase)
    {
        _uploadSoundEffectUseCase = uploadSoundEffectUseCase;
    }
    
    public override void Configure()
    {
        AllowAnonymous();
        Post("/sound");
        AllowFileUploads();
    }

    public override async Task HandleAsync(CreateSoundEffectRequest req, CancellationToken ct)
    {
        var soundEffect = await _uploadSoundEffectUseCase.AddSoundEffectAsync(req.File.OpenReadStream(), req.Name,
            req.VolumePercent, req.Offset);
        await SendAsync(new CreateSoundEffectResponse
        {
            Id = soundEffect.Id,
            Name = soundEffect.Name,
            TotalMilliseconds = soundEffect.TotalMilliseconds,
            VolumePercent = soundEffect.VolumePercent,
        }, cancellation: ct);
    }
}