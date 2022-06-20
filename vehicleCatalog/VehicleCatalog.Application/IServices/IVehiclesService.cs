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

        Task<PagedList<Vehicle>> GetVehiclesPageAsync(PageParameters pageParameters, string vehicleType, string model, string color,
            string vinCode, string regNumber, string ownersName, string prodYear, string lastService);

        Task AddAsync(Vehicle vehicle);
        Task UpdateAsync(Vehicle vehicle);
        Task DeleteAsync(Vehicle vehicle);
        Task UpdateRangeAsync(IEnumerable<Vehicle> vehicles);

        Task<Vehicle> GetAsync(int id);

        void GenerateIvitationPdf(List<Vehicle> vehicles, string path);

        void SaveSearchResults(List<Vehicle> vehicles, string path);
    }
}
