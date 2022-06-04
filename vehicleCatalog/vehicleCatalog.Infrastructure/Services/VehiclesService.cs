using System;
using System.Threading.Tasks;
using VehicleCatalog.Application.IServices;
using VehicleCatalog.Application.Paging;
using VehicleCatalog.Core.Entities;
using VehicleCatalog.Application.IRepositories;
using System.Collections.Generic;
using System.IO;
using iText;
using iText.Kernel.Pdf;
using iText.Layout;
using iText.Layout.Element;
using iText.Layout.Properties;
using iText.Kernel.Pdf.Canvas.Draw;

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
            
            var rnd = new Random();
            var hours = rnd.Next(8, 18);
            var minutes = rnd.Next(0,60);
            var time = new TimeOnly(hours, minutes);

            PdfWriter writer = new PdfWriter(path);
            PdfDocument pdf = new PdfDocument(writer);
            Document document = new Document(pdf);
            Paragraph header = new Paragraph("Invitation")
               .SetTextAlignment(TextAlignment.CENTER)
               .SetFontSize(20);
            LineSeparator ls = new LineSeparator(new SolidLine());
            Text text = new Text($"Hello {vehicle.OwnersName}! \n You need to visit our service" +
                    $" as your last visit was {GetDate(vehicle.LastService)} and as you need to hold it every year, we assign " +
                    $"next date to check and service your {vehicle.VehicleType.ToLower()}: {vehicle.Color} {vehicle.Model}, Vin-Code: {vehicle.VinCode}. " +
                    $"Our next meeting is assigned to {GetDate(vehicle.LastService.AddYears(1))} at {time}." +
                    $"We are looking forward to you!");
            Paragraph main = new Paragraph(text)
                .SetTextAlignment(TextAlignment.LEFT)
                .SetFontSize(16);

            document.Add(ls);
            document.Add(header);
            document.Add(ls);
            document.Add(main);
            document.Close();

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

        public async Task UpdateRangeAsync(IEnumerable<Vehicle> vehicles)
        {
            await this._vehicleRepository.UpdateRangeAsync(vehicles);
        }

        private string GetDate(DateTime dateTime)
        {
            string[] date = dateTime.ToString().Split(" ");
            return date[0]; 
        }
    }
}
