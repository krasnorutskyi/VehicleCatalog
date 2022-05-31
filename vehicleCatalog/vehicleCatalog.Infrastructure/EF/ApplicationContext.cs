using Microsoft.EntityFrameworkCore;
using VehicleCatalog.Core.Entities;

namespace VehicleCatalog.Infrastructure.EF
{
    public class ApplicationContext : DbContext
    {
        public ApplicationContext()
        {

        }

        public ApplicationContext(DbContextOptions<ApplicationContext> options) : base(options)
        {

        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            var connectionString = @"server=(LocalDb)\MSSQLLocalDB;database=Vehicles;integrated security=True;
                    MultipleActiveResultSets=True;App=EntityFramework";
            optionsBuilder.UseSqlServer(connectionString);
        }

        public DbSet<Vehicle> Vehicles { get; set; }
    }
}
