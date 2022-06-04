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
                new Vehicle{Model = "Honda", VinCode = "3123406537", VehicleType = "Motorcycle", Color = "Black", ProductionDate = Convert.ToDateTime("05.05.21"), OwnersName = "Petro", RegistrationNumber = "AX0230IT", LastService = DateTime.Now},
                new Vehicle{Model = "BMW", VinCode = "3337931667", VehicleType = "Car", Color = "Grey", ProductionDate = Convert.ToDateTime("05.05.21"), OwnersName = "Vasyl", RegistrationNumber = "AX1299CX", LastService = DateTime.Now},
                new Vehicle{Model = "Mercedes-Benz", VinCode = "3210402281", VehicleType = "Bus", Color = "Blue", ProductionDate = Convert.ToDateTime("05.05.21"), OwnersName = "Katerina", RegistrationNumber = "AA6593AI", LastService = DateTime.Now},
                new Vehicle{Model = "Volkswagen", VinCode = "2217664282", VehicleType = "Car", Color = "White", ProductionDate = Convert.ToDateTime("05.05.21"), OwnersName = "Ivan", RegistrationNumber = "AX0908ZA", LastService = DateTime.Now},
                new Vehicle{Model = "Volkswagen", VinCode = "9168893883", VehicleType = "Car", Color = "Green", ProductionDate = Convert.ToDateTime("05.05.21"), OwnersName = "Vitalii", RegistrationNumber = "AI9989KA", LastService = DateTime.Now},
                new Vehicle{Model = "Land Rover", VinCode = "5381191779", VehicleType = "Car", Color = "Red", ProductionDate = Convert.ToDateTime("05.05.21"), OwnersName = "Serhii", RegistrationNumber = "AX0233IT", LastService = DateTime.Now},
                new Vehicle{Model = "BMW", VinCode = "2558134614", VehicleType = "Motorcycle", Color = "Yellow", ProductionDate = Convert.ToDateTime("05.05.21"), OwnersName = "Mihailo", RegistrationNumber = "AP3925GT", LastService = DateTime.Now},
                new Vehicle{Model = "Suzuki", VinCode = "9237649388", VehicleType = "Motorcycle", Color = "Purple", ProductionDate = Convert.ToDateTime("05.05.21"), OwnersName = "Petro", RegistrationNumber = "LF4592KA", LastService = DateTime.Now},
                new Vehicle{Model = "Mazda", VinCode = "4743860252", VehicleType = "Car", Color = "Brown", ProductionDate = Convert.ToDateTime("05.05.21"), OwnersName = "Pavlo", RegistrationNumber = "KD9056JD", LastService = DateTime.Now},
                new Vehicle{Model = "Suzuki", VinCode = "3805820025", VehicleType = "Car", Color = "Green", ProductionDate = Convert.ToDateTime("05.05.21"), OwnersName = "Iryna", RegistrationNumber = "DK9345SD", LastService = DateTime.Now},
                new Vehicle{Model = "Audi", VinCode = "7921876897", VehicleType = "Car", Color = "Black", ProductionDate = Convert.ToDateTime("05.05.21"), OwnersName = "Serhii", RegistrationNumber = "FD2670SH", LastService = DateTime.Now},
                new Vehicle{Model = "Audi", VinCode = "1620072654", VehicleType = "Car", Color = "Black", ProductionDate = Convert.ToDateTime("05.05.21"), OwnersName = "Andrii", RegistrationNumber = "KS3495JG", LastService = DateTime.Now},
                new Vehicle{Model = "Ford", VinCode = "7518657780", VehicleType = "Bus", Color = "White", ProductionDate = Convert.ToDateTime("05.05.21"), OwnersName = "Ivan", RegistrationNumber = "JS6240KB", LastService = DateTime.Now},
                new Vehicle{Model = "BMW", VinCode = "1797864639", VehicleType = "Car", Color = "White", ProductionDate = Convert.ToDateTime("05.05.21"), OwnersName = "Vadym", RegistrationNumber = "GS2453GD", LastService = DateTime.Now},
                new Vehicle{Model = "Volkswagen", VinCode = "4501812345", VehicleType = "Car", Color = "Green", ProductionDate = Convert.ToDateTime("05.05.21"), OwnersName = "Vasyl", RegistrationNumber = "DS3940KF", LastService = DateTime.Now},
                new Vehicle{Model = "Mazda", VinCode = "8633263728", VehicleType = "Car", Color = "Red", ProductionDate = Convert.ToDateTime("05.05.21"), OwnersName = "Mihailo", RegistrationNumber = "SO2093NK", LastService = DateTime.Now},
                new Vehicle{Model = "Nissan", VinCode = "3967499422", VehicleType = "Bus", Color = "Red", ProductionDate = Convert.ToDateTime("05.05.21"), OwnersName = "Dmytro", RegistrationNumber = "KD3045KS", LastService = DateTime.Now},
                new Vehicle{Model = "Nissan", VinCode = "1959429570", VehicleType = "Car", Color = "Black", ProductionDate = Convert.ToDateTime("05.05.21"), OwnersName = "Serhii", RegistrationNumber = "FL9584SD", LastService = DateTime.Now},
                new Vehicle{Model = "BMW", VinCode = "5506831917", VehicleType = "Motorcycle", Color = "Black", ProductionDate = Convert.ToDateTime("05.05.21"), OwnersName = "Vitalii", RegistrationNumber = "SK0345GL", LastService = DateTime.Now},
                new Vehicle{Model = "Land Rover", VinCode = "3282462086", VehicleType = "Car", Color = "Blue", ProductionDate = Convert.ToDateTime("05.05.21"), OwnersName = "Katerina", RegistrationNumber = "SD4073DF", LastService = DateTime.Now},
                new Vehicle{Model = "Ford", VinCode = "3170422066", VehicleType = "Car", Color = "Blue", ProductionDate = Convert.ToDateTime("05.05.21"), OwnersName = "Ostap", RegistrationNumber = "JG0392KD", LastService = DateTime.Now},
                new Vehicle{Model = "Opel", VinCode = "1355340623", VehicleType = "Car", Color = "Blue", ProductionDate = Convert.ToDateTime("05.05.21"), OwnersName = "Maksym", RegistrationNumber = "KO0943FG", LastService = DateTime.Now},
                new Vehicle{Model = "BMW", VinCode = "1171934104", VehicleType = "Motorcycle", Color = "Red", ProductionDate = Convert.ToDateTime("05.05.21"), OwnersName = "Maksym", RegistrationNumber = "LD4389JF", LastService = DateTime.Now},
                new Vehicle{Model = "Porsche", VinCode = "7843042395", VehicleType = "Car", Color = "Green", ProductionDate = Convert.ToDateTime("05.05.21"), OwnersName = "Ivan", RegistrationNumber = "GF4959JG", LastService = DateTime.Now},
                new Vehicle{Model = "Mazda", VinCode = "4255497508", VehicleType = "Car", Color = "Grey", ProductionDate = Convert.ToDateTime("05.05.21"), OwnersName = "Andrii", RegistrationNumber = "DF5430DF", LastService = DateTime.Now},
                new Vehicle{Model = "Ford", VinCode = "8938374697", VehicleType = "Car", Color = "Grey", ProductionDate = Convert.ToDateTime("05.05.21"), OwnersName = "Serhii", RegistrationNumber = "AD2341SG", LastService = DateTime.Now},
                new Vehicle{Model = "Mercedes-Benz", VinCode = "9578979322", VehicleType = "Car", Color = "Black", ProductionDate = Convert.ToDateTime("05.05.21"), OwnersName = "Taras", RegistrationNumber = "OS6892GD", LastService = DateTime.Now},
                new Vehicle{Model = "Ducati", VinCode = "2788544341", VehicleType = "Motorcycle", Color = "White", ProductionDate = Convert.ToDateTime("05.05.21"), OwnersName = "Oleksandr", RegistrationNumber = "KO2109SD", LastService = DateTime.Now},
                new Vehicle{Model = "KTM", VinCode = "6432991772", VehicleType = "Motorcycle", Color = "White", ProductionDate = Convert.ToDateTime("05.05.21"), OwnersName = "Dmytro", RegistrationNumber = "PG09158IJ", LastService = DateTime.Now},
                new Vehicle{Model = "Ducati", VinCode = "9364409096", VehicleType = "Motorcycle", Color = "Black", ProductionDate = Convert.ToDateTime("05.05.21"), OwnersName = "Ivan",  RegistrationNumber = "IJ2974JB", LastService = DateTime.Now},
                new Vehicle{Model = "BMW", VinCode = "2134824965", VehicleType = "Motorcycle", Color = "Grey", ProductionDate = Convert.ToDateTime("05.05.21"), OwnersName = "Viktor", RegistrationNumber = "AJ0134JG", LastService = DateTime.Now},
                new Vehicle{Model = "Seat", VinCode = "6044338506", VehicleType = "Car", Color = "Brown", ProductionDate = Convert.ToDateTime("05.05.21"), OwnersName = "Vitalii", RegistrationNumber = "JS4182SJ", LastService = DateTime.Now},
                new Vehicle{Model = "Audi", VinCode = "1611381063", VehicleType = "Car", Color = "Black", ProductionDate = Convert.ToDateTime("05.05.21"), OwnersName = "Ivan", RegistrationNumber = "SF1482KD", LastService = DateTime.Now},
            };

            context.Vehicles.AddRange(vehicles);
            context.SaveChanges();
        }

        
    }
}
