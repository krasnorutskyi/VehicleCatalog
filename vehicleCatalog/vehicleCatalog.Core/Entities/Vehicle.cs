using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VehicleCatalog.Core.Entities
{
    public class Vehicle : EntityBase
    {
        public string VinCode { get; set; }
        public string Model { get; set; }
        public string VehicleType { get; set; }
        public string Color { get; set; }
        public DateTime ProductionDate { get; set; }
        public string RegistrationNumber { get; set; }      
        public DateTime LastService { get; set; }
        public string OwnersName { get; set; }
    }
}
