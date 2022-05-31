using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using VehicleCatalog.Application.IRepositories;
using VehicleCatalog.Application.IServices;
using VehicleCatalog.Infrastructure.EF;
using VehicleCatalog.Infrastructure.Repositories;
using VehicleCatalog.Infrastructure.Services;


namespace VehicleCatalog.Infrastructure
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddInfrastructure(this IServiceCollection services)
        {
            var connectionString = @"server=(LocalDb)\MSSQLLocalDB;database=Vehicles;integrated security=True;
                    MultipleActiveResultSets=True;App=EntityFramework;";

            services.AddDbContext<ApplicationContext>(options => options.UseSqlServer(connectionString));

            services.AddScoped(typeof(IGenericRepository<>), typeof(GenericRepository<>));

            return services;
        }

        public static IServiceCollection AddServices(this IServiceCollection services)
        {
            services.AddScoped<IVehiclesService, VehiclesService>();

            return services;
        }
    }
}
