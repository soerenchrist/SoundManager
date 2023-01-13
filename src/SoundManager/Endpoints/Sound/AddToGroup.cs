using Microsoft.AspNetCore.Mvc;
using SoundManager.UseCases.Interfaces;
using Group = SoundManager.Core.Models.Group;

namespace SoundManager.Endpoints.Sound;

public class AddToGroup : EndpointBaseAsync.WithRequest<AddToGroupRequest>.WithResult<AddToGroupResponse>
{
    private readonly IAddToGroupUseCase _addToGroupUseCase;

    public AddToGroup(IAddToGroupUseCase addToGroupUseCase)
    {
        _addToGroupUseCase = addToGroupUseCase;
    }

    [HttpPut("api/v1/groups/add")]
    public override async Task<AddToGroupResponse> HandleAsync(AddToGroupRequest req, CancellationToken ct = default)
    {
        var result = await _addToGroupUseCase.AddSoundEffectToGroupAsync(req.SoundEffectId, req.GroupId, ct);
        return new AddToGroupResponse(result.IsSuccess);
    }
}