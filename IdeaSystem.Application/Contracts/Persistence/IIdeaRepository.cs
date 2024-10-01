using IdeaSystem.Domain.Entities;

namespace IdeaSystem.Application.Contracts.Persistence;

public interface IIdeaRepository
{
    Task AddAsync(Idea idea);
    Task DeleteAsync(int id);
    Task<IEnumerable<Idea>> GetAllAsync();
    Task<Idea> GetByIdAsync(int id);
    Task UpdateAsync(Idea idea);
}
