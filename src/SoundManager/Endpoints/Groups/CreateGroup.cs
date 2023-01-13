using Microsoft.AspNetCore.Mvc;
using SoundManager.UseCases.Interfaces;
using Group = SoundManager.Core.Models.Group;

namespace SoundManager.Endpoints.Groups;

public class CreateGroup : EndpointBaseAsync.WithRequest<CreateGroupRequest>.WithActionResult<Group>
{
    private readonly ICreateGroupUseCase _createGroupUseCase;

    public CreateGroup(ICreateGroupUseCase createGroupUseCase)
    {
        _createGroupUseCase = createGroupUseCase;
    }

    [HttpPost("api/v1/groups")]
    public override async Task<ActionResult<Group>> HandleAsync([FromBody]CreateGroupRequest req, CancellationToken ct = default)
    {
        var result = await _createGroupUseCase.CreateGroupAsync(req.Name, ct);
        if (result.IsSuccess)
        {
            return result.Value;
        }

        return BadRequest(result.Errors);
    }
}