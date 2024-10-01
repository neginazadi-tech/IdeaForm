using IdeaSubmisionSystem.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace IdeaSubmisionSystem.Infrastructure.Persistence.Data;

public class IdeaDbContext : DbContext
{
    public IdeaDbContext(DbContextOptions<IdeaDbContext> options) : base(options) { }

    public DbSet<Idea> Ideas { get; set; }
}
