using FastEndpoints;
using SoundManager.UseCases.Interfaces;
using SoundManager.Util;
using Group = SoundManager.Core.Models.Group;

namespace SoundManager.Endpoints.Groups;

public class GetGroup : Endpoint<GetGroupRequest, Group>
{
    private readonly IGetGroupUseCase _getGroupUseCase;

    public GetGroup(IGetGroupUseCase getGroupUseCase)
    {
        _getGroupUseCase = getGroupUseCase;
    }

    public override void Configure()
    {
        AllowAnonymous();
        Get("/groups/{groupId}");
    }

    public override async Task HandleAsync(GetGroupRequest request, CancellationToken cancellationToken)
    {
        var result = await _getGroupUseCase.GetGroupAsync(request.GroupId, cancellationToken);
        if (result.IsSuccess)
        {
            await SendAsync(result.Value, cancellation: cancellationToken);
        }
        else
        {
            await SendStringAsync(result.ErrorMessages(), 404, cancellation: cancellationToken);
        }
    }
}