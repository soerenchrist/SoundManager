using Microsoft.AspNetCore.Mvc;
using SoundManager.UseCases.Interfaces;
using Group = SoundManager.Core.Models.Group;

namespace SoundManager.Endpoints.Groups;

public class GetGroup : EndpointBaseAsync.WithRequest<Guid>.WithActionResult<Group>
{
    private readonly IGetGroupUseCase _getGroupUseCase;

    public GetGroup(IGetGroupUseCase getGroupUseCase)
    {
        _getGroupUseCase = getGroupUseCase;
    }

    [HttpGet("api/v1/groups/{groupId:guid}")]
    public override async Task<ActionResult<Group>> HandleAsync([FromRoute]Guid groupId, CancellationToken cancellationToken = default)
    {
        var result = await _getGroupUseCase.GetGroupAsync(groupId, cancellationToken);
        if (result.IsSuccess)
        {
            return result.Value;
        }

        return NotFound();
    }
}