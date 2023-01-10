using Ardalis.Result;
using Microsoft.EntityFrameworkCore;
using SoundManager.Core.Models;
using SoundManager.Infrastructure.Database;
using SoundManager.UseCases.Interfaces;

namespace SoundManager.UseCases.Groups;

public class GetGroupUseCase : IGetGroupUseCase, IUseCase
{
    private readonly AppDbContext _context;

    public GetGroupUseCase(AppDbContext context)
    {
        _context = context;
    }

    public async Task<Result<Group>> GetGroupAsync(Guid groupId, CancellationToken cancellationToken = default)
    {
        var group = await _context.Groups.Include(x => x.SoundEffects)
            .FirstOrDefaultAsync(g => g.Id == groupId, cancellationToken);
        if (group == null) return Result<Group>.NotFound();
        return group;
    }
}