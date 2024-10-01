using IdeaSystem.Application.Contracts;
using IdeaSystem.Application.Services;
using Microsoft.Extensions.DependencyInjection;

namespace IdeaSystem.Application;

public static class DependencyInjection
{
    public static IServiceCollection AddApplicationServices(this IServiceCollection services)
    {
        services.AddScoped<IIdeaService, IdeaService>();
        return services;
    }
}
