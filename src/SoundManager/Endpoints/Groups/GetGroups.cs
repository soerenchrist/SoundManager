using FastEndpoints;
using SoundManager.UseCases.Interfaces;
using Group = SoundManager.Core.Models.Group;

namespace SoundManager.Endpoints.Groups;

public class GetGroups : EndpointWithoutRequest<List<Group>>
{
    private readonly IGetGroupsUseCase _getGroupsUseCase;

    public GetGroups(IGetGroupsUseCase getGroupsUseCase)
    {
        _getGroupsUseCase = getGroupsUseCase;
    }

    public override void Configure()
    {
        AllowAnonymous();
        Get("/groups");
    }

    public override async Task HandleAsync(CancellationToken ct)
    {
        var groups = await _getGroupsUseCase.GetGroupsAsync(ct);
        await SendAsync(groups, cancellation: ct);
    }
}