using AutoMapper;
using IdeaSystem.Application.Contracts;
using IdeaSystem.Application.Contracts.Dtos;
using IdeaSystem.Application.Contracts.Persistence;
using IdeaSystem.Domain.Entities;

namespace IdeaSystem.Application.Services;

public class IdeaService : IIdeaService
{
    private readonly IIdeaRepository _ideaRepository;
    private readonly IMapper _mapper;

    public IdeaService(IIdeaRepository ideaRepository, IMapper mapper)
    {
        _ideaRepository = ideaRepository;
        _mapper = mapper;
    }

    public async Task<IEnumerable<IdeaDto>> GetAllAsync()
    {
        var ideas = await _ideaRepository.GetAllAsync();
        return _mapper.Map<IEnumerable<IdeaDto>>(ideas);
    }

    public async Task<IdeaDto> GetByIdAsync(int id)
    {
        var idea = await _ideaRepository.GetByIdAsync(id);
        return _mapper.Map<IdeaDto>(idea);
    }

    public async Task AddAsync(IdeaDto idea)
    {
        var res = _mapper.Map<Idea>(idea);
        await _ideaRepository.AddAsync(res);
    }

    public async Task UpdateAsync(IdeaDto ideaDto)
    {
        var idea = _mapper.Map<Idea>(ideaDto);
        await _ideaRepository.UpdateAsync(idea);
    }
    public async Task DeleteAsync(int id)
    {
        await _ideaRepository.DeleteAsync(id);
    }
}
