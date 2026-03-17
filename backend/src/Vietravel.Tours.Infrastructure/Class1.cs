using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.EntityFrameworkCore;
using Vietravel.Tours.Application.Abstractions;
using Vietravel.Tours.Infrastructure.Persistence;

namespace Vietravel.Tours.Infrastructure;

public static class DependencyInjection
{
    public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
    {
        var cs = configuration.GetConnectionString("Default");
        if (string.IsNullOrWhiteSpace(cs))
        {
            throw new InvalidOperationException("Missing connection string 'ConnectionStrings:Default'.");
        }

        services.AddDbContextPool<ToursDbContext>(options =>
        {
            options.UseNpgsql(cs, npgsql =>
            {
                npgsql.CommandTimeout(15);
            });
        });

        services.AddScoped<ITourRepository, PostgresTourRepository>();
        services.AddScoped<IUserRepository, UserRepository>();

        return services;
    }
}
