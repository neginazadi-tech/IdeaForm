using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;

namespace IdeaSubmisionSystem.Application.Contracts.Persistence;

public interface IApplicationDbContext
{
    public DatabaseFacade Database { get; }
    public DbSet<Domain.Entities.Idea> Ideas { get; }

    public Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);
}