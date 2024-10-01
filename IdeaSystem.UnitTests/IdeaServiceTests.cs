using AutoMapper;
using FluentAssertions;
using IdeaSystem.Application.Contracts.Dtos;
using IdeaSystem.Application.Contracts.Persistence;
using IdeaSystem.Application.Services;
using Moq;

namespace Idea.UnitTests;

public class IdeaServiceTests
{
    private readonly Mock<IIdeaRepository> _ideaRepositoryMock;
    private readonly IMapper _mapper;
    private readonly IdeaService _ideaService;

    public IdeaServiceTests()
    {
        _ideaRepositoryMock = new Mock<IIdeaRepository>();

        var config = new MapperConfiguration(cfg =>
        {
            cfg.CreateMap<IdeaSystem.Domain.Entities.Idea, IdeaDto>().ReverseMap();
        });
        _mapper = config.CreateMapper();

        _ideaService = new IdeaService(_ideaRepositoryMock.Object, _mapper);
    }

    [Fact]
    public async Task GetAllAsync_ShouldReturnAllIdeas()
    {
        // Arrange
        var ideas = new List<IdeaSystem.Domain.Entities.Idea>
{
new IdeaSystem.Domain.Entities.Idea { Id = 1, Title = "Idea 1", Message = "Message 1" },
new IdeaSystem.Domain.Entities.Idea { Id = 2, Title = "Idea 2", Message = "Message 2" }
};
        _ideaRepositoryMock.Setup(repo => repo.GetAllAsync()).ReturnsAsync(ideas);

        // Act
        var result = await _ideaService.GetAllAsync();

        // Assert
        result.Should().HaveCount(2);
        result.Should().ContainSingle(i => i.Title == "Idea 1");
        result.Should().ContainSingle(i => i.Title == "Idea 2");
    }

    [Fact]
    public async Task GetByIdAsync_ShouldReturnIdea()
    {
        // Arrange
        var idea = new IdeaSystem.Domain.Entities.Idea { Id = 1, Title = "Idea 1", Message = "Message 1" };
        _ideaRepositoryMock.Setup(repo => repo.GetByIdAsync(1)).ReturnsAsync(idea);

        // Act
        var result = await _ideaService.GetByIdAsync(1);

        // Assert
        result.Should().NotBeNull();
        result.Title.Should().Be("Idea 1");
    }

    [Fact]
    public async Task AddAsync_ShouldCallRepositoryAdd()
    {
        // Arrange
        var ideaDto = new IdeaDto { Title = "New Idea", Message = "New Message" };

        // Act
        await _ideaService.AddAsync(ideaDto);

        // Assert
        _ideaRepositoryMock.Verify(repo => repo.AddAsync(It.IsAny<IdeaSystem.Domain.Entities.Idea>()), Times.Once);
    }

    [Fact]
    public async Task UpdateAsync_ShouldCallRepositoryUpdate()
    {
        // Arrange
        var ideaDto = new IdeaDto { Id = 1, Title = "Updated Idea", Message = "Updated Message" };

        // Act
        await _ideaService.UpdateAsync(ideaDto);

        // Assert
        _ideaRepositoryMock.Verify(repo => repo.UpdateAsync(It.IsAny<IdeaSystem.Domain.Entities.Idea>()), Times.Once);
    }

    [Fact]
    public async Task DeleteAsync_ShouldCallRepositoryDelete()
    {
        // Act
        await _ideaService.DeleteAsync(1);

        // Assert
        _ideaRepositoryMock.Verify(repo => repo.DeleteAsync(1), Times.Once);
    }
}