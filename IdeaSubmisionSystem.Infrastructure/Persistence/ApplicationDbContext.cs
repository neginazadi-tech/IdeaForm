using Microsoft.EntityFrameworkCore;
using System.Reflection;
using IdeaSubmisionSystem.Application.Contracts.Persistence;

namespace IdeaSubmisionSystem.Infrastructure.Persistence;
public sealed class ApplicationDbContext : DbContext, IApplicationDbContext
{
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
    {
        //ChangeTracker.LazyLoadingEnabled = false;
    }

    public ApplicationDbContext()
    {
        ChangeTracker.LazyLoadingEnabled = false;
    }
    public DbSet<Domain.Entities.Idea> Ideas => Set<Domain.Entities.Idea>();

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());

        base.OnModelCreating(modelBuilder);
    }
}