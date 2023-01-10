using FastEndpoints;
using SoundManager.UseCases.Interfaces;
using SoundManager.Util;
using Group = SoundManager.Core.Models.Group;

namespace SoundManager.Endpoints.Groups;

public class CreateGroup : Endpoint<CreateGroupRequest, Group>
{
    private readonly ICreateGroupUseCase _createGroupUseCase;

    public CreateGroup(ICreateGroupUseCase createGroupUseCase)
    {
        _createGroupUseCase = createGroupUseCase;
    }

    public override void Configure()
    {
        AllowAnonymous();
        Post("/groups");
    }

    public override async Task HandleAsync(CreateGroupRequest req, CancellationToken ct)
    {
        var result = await _createGroupUseCase.CreateGroupAsync(req.Name, ct);
        if (result.IsSuccess)
        {
            await SendAsync(result.Value, cancellation: ct);
        }
        else
        {
            await SendStringAsync(result.ErrorMessages(), 400, cancellation: ct);
        }
    }
}