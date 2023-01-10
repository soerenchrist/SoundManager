using Ardalis.Result;
using Microsoft.EntityFrameworkCore;
using SoundManager.Core.Models;
using SoundManager.Infrastructure.Database;
using SoundManager.UseCases.Interfaces;

namespace SoundManager.UseCases.Groups;

public class CreateGroupUseCase : ICreateGroupUseCase, IUseCase
{
    private readonly AppDbContext _context;

    public CreateGroupUseCase(AppDbContext context)
    {
        _context = context;
    }
    
    public async Task<Result<Group>> CreateGroupAsync(string name, CancellationToken cancellationToken = default)
    {
        var existingGroup = await _context.Groups.FirstOrDefaultAsync(g => g.Name == name, cancellationToken: cancellationToken);
        if (existingGroup != null)
        {
            return Result<Group>.Error("Group already exists");
        }

        var group = new Group
        {
            Id = Guid.NewGuid(),
            Name = name
        };
        await _context.AddAsync(group, cancellationToken);
        await _context.SaveChangesAsync(cancellationToken);
        return Result.Success(group);
    }
}