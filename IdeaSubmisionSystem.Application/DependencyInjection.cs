using IdeaSubmisionSystem.Application.Contracts;
using IdeaSubmisionSystem.Application.Services;
using Microsoft.Extensions.DependencyInjection;

namespace IdeaSubmisionSystem.Application;

public static class DependencyInjection
{
    public static IServiceCollection AddApplicationServices(this IServiceCollection services)
    {
        services.AddScoped<IIdeaService, IdeaService>();
        return services;
    }
}
