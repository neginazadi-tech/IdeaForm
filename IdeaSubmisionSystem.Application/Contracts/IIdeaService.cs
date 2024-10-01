using IdeaSubmisionSystem.Application.Contracts.Dtos;

namespace IdeaSubmisionSystem.Application.Contracts;

public interface IIdeaService
{
    Task<IEnumerable<IdeaDto>> GetAllAsync();
    Task<IdeaDto> GetByIdAsync(int id);
    Task AddAsync(IdeaDto idea);
    Task UpdateAsync(IdeaDto idea);
    Task DeleteAsync(int id);
}
