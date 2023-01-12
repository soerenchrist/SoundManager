using Ardalis.Result;
using Microsoft.EntityFrameworkCore;
using SoundManager.Infrastructure.Database;
using SoundManager.UseCases.Interfaces;

namespace SoundManager.UseCases.Groups;

public class DeleteGroupUseCase : IDeleteGroupUseCase, IUseCase
{
    private readonly AppDbContext _context;

    public DeleteGroupUseCase(AppDbContext context)
    {
        _context = context;
    }
    
    public async Task<Result> DeleteGroupAsync(Guid groupId, CancellationToken cancellationToken = default)
    {
        var existing = await _context.Groups.FirstOrDefaultAsync(x => x.Id == groupId, cancellationToken);
        if (existing == null) return Result.NotFound();
        
        _context.Groups.Remove(existing);
        await _context.SaveChangesAsync(cancellationToken);
        return Result.Success();
    }
}