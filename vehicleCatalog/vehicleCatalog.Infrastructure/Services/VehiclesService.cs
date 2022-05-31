using System;
using System.Threading.Tasks;
using VehicleCatalog.Application.IServices;
using VehicleCatalog.Application.Paging;
using VehicleCatalog.Core.Entities;
using VehicleCatalog.Application.IRepositories;
using System.IO;
using iTextSharp.text.pdf;
using iTextSharp.text;

namespace VehicleCatalog.Infrastructure.Services
{
    public class VehiclesService : IVehiclesService
    {
        private readonly IGenericRepository<Vehicle> _vehicleRepository;

        public VehiclesService(IGenericRepository<Vehicle> vehicleRepository)
        {
           this._vehicleRepository = vehicleRepository;
        }

        public async Task AddAsync(Vehicle vehicle)
        {
            await this._vehicleRepository.AddAsync(vehicle); 
        }

        public async Task DeleteAsync(Vehicle vehicle)
        {
            await this._vehicleRepository.DeleteAsync(vehicle);
        }

        public void GenerateIvitationPdf(Vehicle vehicle, string path)
        {
            var invitation = new Document();
            var rnd = new Random();
            var hours = rnd.Next(8, 18);
            var minutes = rnd.Next(0,60);
            var time = new TimeOnly(hours, minutes);
            using (var writer = PdfWriter.GetInstance(invitation, new FileStream(path, FileMode.Create)))
            {
                invitation.Open();

                var helvetica = new Font(Font.FontFamily.HELVETICA, 12);
                var helveticaBase = helvetica.GetCalculatedBaseFont(false);
                writer.DirectContent.BeginText();
                writer.DirectContent.SetFontAndSize(helveticaBase, 12f);
                writer.DirectContent.ShowTextAligned(Element.ALIGN_LEFT, $"Hello {vehicle.OwnersName}! \n You need to visit our service" +
                    $" as your last visit was {vehicle.LastService} and as you need to hold it every year,\n we assign " +
                    $"next date to check and service your vehicle: {vehicle.VehicleType}, {vehicle.Model}, {vehicle.Color}, Vin-Code: {vehicle.VinCode}.\n" +
                    $"Our next meeting is assigned to {vehicle.LastService.AddYears(1).Date} at {time}." +
                    $"We are looking forward to you!", 35, 766, 0);
                writer.DirectContent.EndText();

                invitation.Close();
                writer.Close();

            }
            
        }

        public async Task<Vehicle> GetAsync(int id)
        {
            return await this._vehicleRepository.GetOneAsync(id);
        }

        public async Task<PagedList<Vehicle>> GetVehiclesPageAsync(PageParameters pageParameters)
        {
           return await this._vehicleRepository.GetPageAsync(pageParameters);
        }

        public async Task<PagedList<Vehicle>> GetVehiclesPageAsync(PageParameters pageParameters, string filter)
        {
            return await this._vehicleRepository.GetPageAsync(pageParameters, v =>v.OwnersName.StartsWith(filter) || v.VinCode.StartsWith(filter) || v.Color.StartsWith(filter) ||
            v.LastService.Equals(filter) || v.Model.StartsWith(filter) || v.RegistrationNumber.StartsWith(filter) || v.VehicleType.StartsWith(filter));
        }

        public async Task UpdateAsync(Vehicle vehicle)
        {
            await this._vehicleRepository.UpdateAsync(vehicle);
        }
    }
}
