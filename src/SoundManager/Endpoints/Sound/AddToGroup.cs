using FastEndpoints;
using SoundManager.UseCases.Interfaces;
using Group = SoundManager.Core.Models.Group;

namespace SoundManager.Endpoints.Sound;

public class AddToGroup : Endpoint<AddToGroupRequest, AddToGroupResponse>
{
    private readonly IAddToGroupUseCase _addToGroupUseCase;

    public AddToGroup(IAddToGroupUseCase addToGroupUseCase)
    {
        _addToGroupUseCase = addToGroupUseCase;
    }

    public override void Configure()
    {
        AllowAnonymous();
        Put("/groups/add");
    }

    public override async Task HandleAsync(AddToGroupRequest req, CancellationToken ct)
    {
        var result = await _addToGroupUseCase.AddSoundEffectToGroupAsync(req.SoundEffectId, req.GroupId, ct);
        await SendAsync(new AddToGroupResponse(result.IsSuccess), cancellation: ct);
    }
}