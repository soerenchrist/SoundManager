using FastEndpoints;
using SoundManager.UseCases.Interfaces;

namespace SoundManager.Endpoints.Sound;

public class DeleteSoundEffect : Endpoint<DeleteSoundEffectRequest>
{
    private readonly IDeleteSoundEffectUseCase _deleteSoundEffectUseCase;

    public DeleteSoundEffect(IDeleteSoundEffectUseCase deleteSoundEffectUseCase)
    {
        _deleteSoundEffectUseCase = deleteSoundEffectUseCase;
    }

    public override void Configure()
    {
        AllowAnonymous();
        Delete("/sounds/{id}");
    }

    public override async Task HandleAsync(DeleteSoundEffectRequest request, CancellationToken cancellationToken)
    {
        var result = await _deleteSoundEffectUseCase.DeleteSoundEffectAsync(request.Id, cancellationToken);
        if (result.IsSuccess)
        {
            await SendNoContentAsync(cancellationToken);
        }
        else
        {
            await SendNotFoundAsync(cancellationToken);
        }
    }
}