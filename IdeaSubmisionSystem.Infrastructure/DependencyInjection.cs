using IdeaSubmisionSystem.Application.Contracts.Persistence;
using IdeaSubmisionSystem.Infrastructure.Persistence;
using IdeaSubmisionSystem.Infrastructure.Persistence.Repositories;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace IdeaSubmisionSystem.Infrastructure;

public static class DependencyInjection
{
    public static IServiceCollection AddInfrastructureServices(this IServiceCollection services, IConfiguration configuration, IHostEnvironment environment)
    {
        services.AddSingleton(TimeProvider.System);
        AddDbContext(services, configuration);

        return services;
    }

    private static void AddDbContext(IServiceCollection services, IConfiguration configuration)
    {
        var connectionString = configuration.GetConnectionString("DefaultConnection");
        services.AddScoped<IIdeaRepository, IdeaRepository>();

        //TODO: I should implement AuditableEntityInterceptor!
        //services.AddSingleton<ISaveChangesInterceptor, AuditableEntityInterceptor>();

        //TODO
        services.AddDbContextPool<ApplicationDbContext>((sp, options) =>
        {
            //options.UseSqlServer(connectionString);
           // options.AddInterceptors(sp.GetRequiredService<ISaveChangesInterceptor>());
        });

        services.AddScoped<IApplicationDbContext>(provider => provider.GetRequiredService<ApplicationDbContext>());
    }
}
