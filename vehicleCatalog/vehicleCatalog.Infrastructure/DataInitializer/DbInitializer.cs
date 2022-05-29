using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VehicleCatalog.Core.Entities;
using VehicleCatalog.Infrastructure.EF;

namespace VehicleCatalog.Infrastructure.DataInitializer
{
    public class DbInitializer
    {
        public static void Initialize(ApplicationContext context)
        {
            context.Database.EnsureDeleted();
            context.Database.EnsureCreated();

            var vehicles = new List<Vehicle>()
            {
                new Vehicle{Model = "ferrari", VinCode = "12345678", VehicleType = "Motorcycle", Color = "black", LastService = Convert.ToDateTime("05.05.21"), OwnersName = "petya", RegistrationNumber = "AX0230IT", ProductionDate = DateTime.Now}

            };

            context.Vehicles.AddRange(vehicles);
            context.SaveChanges();
        }

        
    }
}
