using IdeaSystem.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace IdeaSystem.Infrastructure.Persistence.Data;

public class IdeaDbContext : DbContext
{
    public IdeaDbContext(DbContextOptions<IdeaDbContext> options) : base(options) { }

    public DbSet<Idea> Ideas { get; set; }
}
