using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using vehicleCatalog.CORE;
using System.IO;

namespace vehicleCatalog.DAL
{
    public class VehicleRepository : IVehicleRepository
    {
        private readonly string _path;
        public VehicleRepository(string path)
        {
            this._path = path;
            if (!File.Exists(this._path))
            {
                using (File.Create(this._path)) { }
            }
        }
        public void Create(Vehicle vehicle)
        {
            throw new NotImplementedException();
        }

        public void Delete(Vehicle vehicle)
        {
            throw new NotImplementedException();
        }

        public Vehicle Get()
        {
            throw new NotImplementedException();
        }

        public Vehicle GetAll()
        {
            throw new NotImplementedException();
        }

        public Vehicle GetById(int id)
        {
            throw new NotImplementedException();
        }

        public void Update(Vehicle vehicle)
        {
            throw new NotImplementedException();
        }
    }
}
