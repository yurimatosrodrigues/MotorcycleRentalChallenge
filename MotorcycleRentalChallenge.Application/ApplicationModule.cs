using Microsoft.Extensions.DependencyInjection;
using MotorcycleRentalChallenge.Application.Interfaces;
using MotorcycleRentalChallenge.Application.Services;

namespace MotorcycleRentalChallenge.Application
{
    public static class ApplicationModule
    {
        public static IServiceCollection AddApplication(this IServiceCollection services)
        {
            services.AddApplicationServices();
            return services;
        }

        private static IServiceCollection AddApplicationServices(this IServiceCollection services) 
        {
            services.AddScoped<IMotorcycleService, MotorcycleService>();
            return services;
        }
    }
}
