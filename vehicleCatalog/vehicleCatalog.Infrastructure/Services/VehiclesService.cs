using System;
using System.Threading.Tasks;
using VehicleCatalog.Application.IServices;
using VehicleCatalog.Application.Paging;
using VehicleCatalog.Core.Entities;
using VehicleCatalog.Application.IRepositories;
using System.Collections.Generic;
using System.Linq;
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

        public void GenerateIvitationPdf(List<Vehicle> vehicles, string path)
        {
            PdfWriter writer = new PdfWriter(path);
            PdfDocument pdf = new PdfDocument(writer);
            Document document = new Document(pdf);
            foreach (var vehicle in vehicles)
            {
                var rnd = new Random();
                var hours = rnd.Next(8, 18);
                var minutes = rnd.Next(0, 60);
                var time = new TimeOnly(hours, minutes);


                Paragraph header = new Paragraph("Invitation")
                   .SetTextAlignment(TextAlignment.CENTER)
                   .SetFontSize(20);
                LineSeparator ls = new LineSeparator(new SolidLine());
                Text text = new Text($"Hello {vehicle.OwnersName}! \n You need to visit service" +
                        $" as your last visit was {GetDate(vehicle.LastService)} and you need to hold it anually, we assign " +
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
                if (vehicle != vehicles.Last())
                {
                    document.Add(new AreaBreak());
                }
            }
            document.Close();
        }

        public void SaveSearchResults(List<Vehicle> vehicles, string path)
        {
            PdfWriter writer = new PdfWriter(path);
            PdfDocument pdf = new PdfDocument(writer);
            Document document = new Document(pdf);
            Paragraph header = new Paragraph("Search Results")
               .SetTextAlignment(TextAlignment.CENTER)
               .SetFontSize(20);
            LineSeparator ls = new LineSeparator(new SolidLine());
            float[] pointColumnWidths = { 20F, 60F, 80F, 40F, 60F, 40F, 50F, 50F, 50F };
            Table table = new Table(pointColumnWidths);
            table.AddCell(new Cell().SetTextAlignment(TextAlignment.CENTER).Add(new Paragraph("Id")));
            table.AddCell(new Cell().SetTextAlignment(TextAlignment.CENTER).Add(new Paragraph("Vehicle Type")));
            table.AddCell(new Cell().SetTextAlignment(TextAlignment.CENTER).Add(new Paragraph("Model")));
            table.AddCell(new Cell().SetTextAlignment(TextAlignment.CENTER).Add(new Paragraph("Color")));
            table.AddCell(new Cell().SetTextAlignment(TextAlignment.CENTER).Add(new Paragraph("Vin code")));
            table.AddCell(new Cell().SetTextAlignment(TextAlignment.CENTER).Add(new Paragraph("Registration Number")));
            table.AddCell(new Cell().SetTextAlignment(TextAlignment.CENTER).Add(new Paragraph("Owner")));
            table.AddCell(new Cell().SetTextAlignment(TextAlignment.CENTER).Add(new Paragraph("Production date")));
            table.AddCell(new Cell().SetTextAlignment(TextAlignment.CENTER).Add(new Paragraph("Last Service")));

            foreach (var v in vehicles)
            {
                table.AddCell(new Cell().SetTextAlignment(TextAlignment.CENTER).Add(new Paragraph(v.Id.ToString())));
                table.AddCell(new Cell().SetTextAlignment(TextAlignment.CENTER).Add(new Paragraph(v.VehicleType)));
                table.AddCell(new Cell().SetTextAlignment(TextAlignment.CENTER).Add(new Paragraph(v.Model)));
                table.AddCell(new Cell().SetTextAlignment(TextAlignment.CENTER).Add(new Paragraph(v.Color)));
                table.AddCell(new Cell().SetTextAlignment(TextAlignment.CENTER).Add(new Paragraph(v.VinCode)));
                table.AddCell(new Cell().SetTextAlignment(TextAlignment.CENTER).Add(new Paragraph(v.RegistrationNumber)));
                table.AddCell(new Cell().SetTextAlignment(TextAlignment.CENTER).Add(new Paragraph(v.OwnersName)));
                table.AddCell(new Cell().SetTextAlignment(TextAlignment.CENTER).Add(new Paragraph(this.GetDate(v.ProductionDate))));
                table.AddCell(new Cell().SetTextAlignment(TextAlignment.CENTER).Add(new Paragraph(v.LastService.ToString())));
            }

            document.Add(ls);
            document.Add(header);
            document.Add(ls);
            document.Add(new Paragraph(""));
            document.Add(table);
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

        public async Task<PagedList<Vehicle>> GetVehiclesPageAsync(PageParameters pageParameters, string vehicleType, string model, string color,
            string vinCode, string regNumber, string ownersName, string prodYear, string lastService)
        {
            var intProdYear = 1;
            var lastServiceYear = 1;
            int.TryParse(prodYear, out intProdYear);
            int.TryParse(lastService, out lastServiceYear);
            return await this._vehicleRepository.GetPageAsync(pageParameters, v => v.VehicleType.Contains(vehicleType)
            && v.Model.Contains(model)
            && v.Color.Contains(color)
            && v.VinCode.StartsWith(vinCode)
            && v.RegistrationNumber.Contains(regNumber)
            && v.OwnersName.Contains(ownersName)
            && v.ProductionDate.Year >= intProdYear
            && v.LastService.Year >= lastServiceYear);
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
