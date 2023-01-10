using Microsoft.EntityFrameworkCore;
using SoundManager.Core.Models;
using SoundManager.Infrastructure.Database;
using SoundManager.UseCases.Interfaces;

namespace SoundManager.UseCases.Groups;

public class GetGroupsUseCase : IGetGroupsUseCase, IUseCase
{
    private readonly AppDbContext _context;

    public GetGroupsUseCase(AppDbContext context)
    {
        _context = context;
    }

    public Task<List<Group>> GetGroupsAsync(CancellationToken cancellationToken = default)
    {
        return _context.Groups.ToListAsync(cancellationToken: cancellationToken);
    }
}