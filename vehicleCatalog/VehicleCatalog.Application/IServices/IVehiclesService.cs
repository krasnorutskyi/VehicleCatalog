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

        Task AddAsync(Vehicle vehicle);
        Task UpdateAsync(Vehicle vehicle);
        Task DeleteAsync(Vehicle vehicle);
        Task UpdateRangeAsync(IEnumerable<Vehicle> vehicles);

        Task<Vehicle> GetAsync(int id);

        void GenerateIvitationPdf(Vehicle vehicle, string path);

    }
}
