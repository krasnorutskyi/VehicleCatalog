using System;
using vehicleCatalog.CORE;

namespace vehicleCatalog.DAL
{
    public interface IVehicleRepository
    {
        void Create(Vehicle vehicle);
        Vehicle GetById(int id);
        Vehicle Get();
        Vehicle GetAll();
        void Update(Vehicle vehicle);
        void Delete(Vehicle vehicle);
    }
}
