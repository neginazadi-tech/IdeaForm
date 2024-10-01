using IdeaSubmisionSystem.Application.Contracts.Persistence;
using IdeaSubmisionSystem.Domain.Entities;

namespace IdeaSubmisionSystem.Infrastructure.Persistence.Repositories;
public class IdeaRepository : IIdeaRepository
{
    private readonly IApplicationDbContext _context;

    public IdeaRepository(IApplicationDbContext context)
    {
        _context = context;
    }

    public Task AddAsync(Idea idea)
    {
       // return await _context.Ideas.AddAsync(idea);
        return Task.CompletedTask;
    }

    public Task DeleteAsync(int id)
    {
        return Task.CompletedTask;
    }

    public async Task<IEnumerable<Idea>> GetAllAsync()
    {
        //return await _context.Ideas.ToListAsync();

        await Task.Delay(1);
        return new List<Idea>
                {
            new Idea
                    {
                    Id = 1,
                    Email = "a@gmail.com",
                    Title = "Idea 1!",
                    Message = "Good",
                    Category = "Improvement",
                    SubmissionDate = DateTime.Now
                    },
                new Idea
                    {
                    Id = 2,
                    Email = "b@gmail.com",
                    Title = "Idea 2!",
                    Message = "Perfect",
                    Category = "Improvement",
                    SubmissionDate = DateTime.Now
                },new Idea
                    {
                    Id = 3,
                    Email = "a@gmail.com",
                    Title = "Idea 1!",
                    Message = "Good",
                    Category = "Improvement",
                    SubmissionDate = DateTime.Now
                    },
                new Idea
                    {
                    Id = 4,
                    Email = "b@gmail.com",
                    Title = "Idea 2!",
                    Message = "Perfect",
                    Category = "Improvement",
                    SubmissionDate = DateTime.Now
                },
                new Idea
                    {
                    Id = 5,
                    Email = "a@gmail.com",
                    Title = "Idea 1!",
                    Message = "Good",
                    Category = "Improvement",
                    SubmissionDate = DateTime.Now
                    },
                new Idea
                    {
                    Id = 6,
                    Email = "b@gmail.com",
                    Title = "Idea 2!",
                    Message = "Perfect",
                    Category = "Improvement",
                    SubmissionDate = DateTime.Now
                }
        };
    }

    public Task<Idea> GetByIdAsync(int id)
    {
        //return await _context.Ideas.FirstOrDefaultAsync(x => x.Id == id);
        return Task.FromResult(new Idea { Id = 1, Title = "My Idea", Message = "My Idea is positive", Email = "neginazadi@gmail.com", SubmissionDate = DateTime.Now });
    }

    public Task UpdateAsync(Idea idea)
    {
        return Task.CompletedTask;
    }
}
