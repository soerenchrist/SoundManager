using FastEndpoints;
using SoundManager.UseCases.Interfaces;

namespace SoundManager.Endpoints.Sound;

public class GetSoundEffectById : Endpoint<GetSoundEffectByIdRequest, GetSoundEffectByIdResponse>
{
    private readonly IGetSoundEffectUseCase _getSoundEffectUseCase;

    public GetSoundEffectById(IGetSoundEffectUseCase getSoundEffectUseCase)
    {
        _getSoundEffectUseCase = getSoundEffectUseCase;
    }

    public override void Configure()
    {
        AllowAnonymous();
        Get("sounds/{id}");
    }

    public override async Task HandleAsync(GetSoundEffectByIdRequest req, CancellationToken ct)
    {
        var result = await _getSoundEffectUseCase.GetSoundEffectAsync(req.Id);
        if (result.IsSuccess)
        {
            var sound = result.Value;
            await SendAsync(new GetSoundEffectByIdResponse(sound.Id, sound.Name, sound.TotalMilliseconds, sound.Offset,
                sound.PlayDurationMilliseconds), cancellation: ct);
        }
        else
        {
            await SendNotFoundAsync(cancellation: ct);
        }
    }
}