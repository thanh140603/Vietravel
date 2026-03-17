using Microsoft.Extensions.DependencyInjection;
using Vietravel.Tours.Application.Abstractions;
using Vietravel.Tours.Application.Services;

namespace Vietravel.Tours.Application;

public static class DependencyInjection
{
    public static IServiceCollection AddApplication(this IServiceCollection services)
    {
        services.AddScoped<ITourService, TourService>();
        services.AddScoped<IAuthService, AuthService>();
        return services;
    }
}

