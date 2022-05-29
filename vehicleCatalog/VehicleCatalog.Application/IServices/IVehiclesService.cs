using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VehicleCatalog.Application.Paging;
using VehicleCatalog.Core.Entities;


namespace VehicleCatalog.Application.IServices
{
    public interface IVehiclesService
    {
        Task<PagedList<Vehicle>> GetVehiclesPageAsync(PageParameters pageParameters);

        Task<PagedList<Vehicle>> GetVehiclesPageAsync(PageParameters pageParameters, string filter);

        void AddAsync(Vehicle vehicle);
        void UpdateAsync(Vehicle vehicle);
        void DeleteAsync(Vehicle vehicle);

        Task<Vehicle> GetAsync(int id);

        void GenerateIvitationPdf(Vehicle vehicle, string path);

    }
}
