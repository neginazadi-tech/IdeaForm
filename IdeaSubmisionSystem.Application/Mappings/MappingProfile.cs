using AutoMapper;
using IdeaSubmisionSystem.Application.Contracts.Dtos;
using IdeaSubmisionSystem.Domain.Entities;

namespace IdeaSubmisionSystem.Application.Mappings;

public class MappingProfile : Profile
{
    public MappingProfile()
    {
        CreateMap<Idea, IdeaDto>().ReverseMap();
    }
}
