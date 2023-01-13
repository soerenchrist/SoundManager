using Microsoft.AspNetCore.Mvc;
using SoundManager.UseCases.Interfaces;

namespace SoundManager.Endpoints.Sound;

public class DeleteSoundEffect : EndpointBaseAsync.WithRequest<DeleteSoundEffectRequest>.WithActionResult
{
    private readonly IDeleteSoundEffectUseCase _deleteSoundEffectUseCase;

    public DeleteSoundEffect(IDeleteSoundEffectUseCase deleteSoundEffectUseCase)
    {
        _deleteSoundEffectUseCase = deleteSoundEffectUseCase;
    }

    [HttpDelete("/api/v1/sounds/{id:guid}")]
    public override async Task<ActionResult> HandleAsync([FromRoute]DeleteSoundEffectRequest request, CancellationToken cancellationToken = default)
    {
        var result = await _deleteSoundEffectUseCase.DeleteSoundEffectAsync(request.Id, cancellationToken);
        if (result.IsSuccess)
        {
            return NoContent();
        }

        return NotFound();
    }
}