using Microsoft.AspNetCore.Mvc;
using SoundManager.UseCases.Interfaces;
using Group = SoundManager.Core.Models.Group;

namespace SoundManager.Endpoints.Groups;

public class GetGroups : EndpointBaseAsync.WithoutRequest.WithResult<List<Group>>
{
    private readonly IGetGroupsUseCase _getGroupsUseCase;

    public GetGroups(IGetGroupsUseCase getGroupsUseCase)
    {
        _getGroupsUseCase = getGroupsUseCase;
    }

    [HttpGet("api/v1/groups")]
    public override async Task<List<Group>> HandleAsync(CancellationToken ct = default)
    {
        var groups = await _getGroupsUseCase.GetGroupsAsync(ct);
        return groups;
    }
}