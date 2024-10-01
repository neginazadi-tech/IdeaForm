using AutoMapper;
using IdeaSystem.Application.Contracts.Dtos;
using IdeaSystem.Domain.Entities;

namespace IdeaSystem.Application.Mappings;

public class MappingProfile : Profile
{
    public MappingProfile()
    {
        CreateMap<Idea, IdeaDto>().ReverseMap();
    }
}
