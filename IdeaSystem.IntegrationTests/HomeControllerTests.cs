using FluentAssertions;
using Microsoft.AspNetCore.Mvc.Testing;

namespace Idea.IntegrationTests;

public class HomeControllerTests : IClassFixture<WebApplicationFactory<Program>>
{
    private readonly HttpClient _client;

    public HomeControllerTests(WebApplicationFactory<Program> factory)
    {
        _client = factory.CreateClient();
    }

    [Fact]
    public async Task Get_Index_ReturnsSuccess()
    {
        // Act
        var response = await _client.GetAsync("/");

        // Assert
        response.EnsureSuccessStatusCode();
        var content = await response.Content.ReadAsStringAsync();
        content.Should().Contain("Welcome");
    }

    [Fact]
    public async Task Get_IdeaGrid_ReturnsSuccess()
    {
        // Act
        var response = await _client.GetAsync("/Home/IdeaGrid");

        // Assert
        response.EnsureSuccessStatusCode();
        var content = await response.Content.ReadAsStringAsync();
        content.Should().Contain("Ideas");
    }
}
