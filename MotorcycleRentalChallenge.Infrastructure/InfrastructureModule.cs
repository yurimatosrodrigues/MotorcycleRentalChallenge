using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using MotorcycleRentalChallenge.Core.Interfaces.Repositories;
using MotorcycleRentalChallenge.Core.Interfaces.Storage;
using MotorcycleRentalChallenge.Infrastructure.Data;
using MotorcycleRentalChallenge.Infrastructure.Data.Repositories;
using MotorcycleRentalChallenge.Infrastructure.Messaging;
using MotorcycleRentalChallenge.Infrastructure.Storage;

namespace MotorcycleRentalChallenge.Infrastructure
{
    public static class InfrastructureModule
    {
        public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddPostgreSQL(configuration);
            services.AddRepositories();

            return services;
        }

        private static IServiceCollection AddPostgreSQL(this IServiceCollection services, IConfiguration configuration)
        {            
            services.AddDbContext<AppDbContext>(options =>
            options.UseNpgsql(configuration.GetConnectionString("DefaultConnection")));

            AppContext.SetSwitch("Npgsql.EnableLegacyTimestampBehavior", false);            

            return services;
        }

        private static IServiceCollection AddRepositories(this IServiceCollection services)
        {
            services.AddScoped<IDeliveryDriverRepository, DeliveryDriverRepository>();
            services.AddScoped<IMotorcycleRepository, MotorcycleRepository>();
            services.AddScoped<IRentalRepository, RentalRepository>();
            services.AddScoped<IFileStorageService, LocalStorageService>();
            services.AddScoped<IRentalPlanRepository, RentalPlanRepository>();
            services.AddScoped<IMessageBusService, RabbitMqService>();

            return services;
        }
    }
}
