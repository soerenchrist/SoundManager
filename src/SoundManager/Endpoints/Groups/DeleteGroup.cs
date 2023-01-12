using FastEndpoints;
using SoundManager.UseCases.Interfaces;

namespace SoundManager.Endpoints.Groups;

public class DeleteGroup : Endpoint<DeleteGroupRequest>
{
    private readonly IDeleteGroupUseCase _deleteGroupUseCase;

    public DeleteGroup(IDeleteGroupUseCase deleteGroupUseCase)
    {
        _deleteGroupUseCase = deleteGroupUseCase;
    }

    public override void Configure()
    {
        Delete("/groups/{groupId}");
        AllowAnonymous();
    }

    public override async Task HandleAsync(DeleteGroupRequest req, CancellationToken ct)
    {
        var result = await _deleteGroupUseCase.DeleteGroupAsync(req.GroupId, ct);
        if (result.IsSuccess)
        {
            await SendNoContentAsync(ct);
        }
        else
        {
            await SendNotFoundAsync(ct);
        }
    }
}