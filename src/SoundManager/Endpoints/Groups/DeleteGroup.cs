using Microsoft.AspNetCore.Mvc;
using SoundManager.UseCases.Interfaces;

namespace SoundManager.Endpoints.Groups;

public class DeleteGroup : EndpointBaseAsync.WithRequest<Guid>.WithActionResult
{
    private readonly IDeleteGroupUseCase _deleteGroupUseCase;

    public DeleteGroup(IDeleteGroupUseCase deleteGroupUseCase)
    {
        _deleteGroupUseCase = deleteGroupUseCase;
    }

    [HttpDelete("api/v1/groups/{groupId:guid}")]
    public override async Task<ActionResult> HandleAsync([FromRoute]Guid groupId, CancellationToken ct = default)
    {
        var result = await _deleteGroupUseCase.DeleteGroupAsync(groupId, ct);
        if (result.IsSuccess)
        {
            return NoContent();
        }

        return NotFound();
    }
}