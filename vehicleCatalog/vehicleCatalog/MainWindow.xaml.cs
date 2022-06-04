using Microsoft.Win32;
using System;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using VehicleCatalog.Application.IServices;
using VehicleCatalog.Application.Paging;
using VehicleCatalog.Core.Entities;
using VehicleCatalog.Infrastructure.DataInitializer;
using VehicleCatalog.Infrastructure.EF;
using Path = System.IO.Path;

namespace vehicleCatalog
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private readonly IVehiclesService _vehiclesService;
        private PagedList<Vehicle> _vehicles = new();
        private PageParameters _pageParameters = new();
        
        public MainWindow(IVehiclesService vehiclesService)
        {
            this._vehiclesService = vehiclesService;

            //var context = new ApplicationContext();
            //DbInitializer.Initialize(context);
            InitializeComponent();
            new Action(async () => await this.SetPage(1))();
        }

        private async Task SetPage(int pageNumber)
        {
            this._pageParameters.PageIndex = pageNumber;
            this._vehicles = await this._vehiclesService.GetVehiclesPageAsync(_pageParameters);
            this.vehicles.ItemsSource = this._vehicles;
            this.pagesInfo.Content = $"{this._vehicles.PageIndex} of {this._vehicles.TotalCount}";
        }

        private async Task Search(int pageNumber)
        {
            this._pageParameters.PageIndex = pageNumber;
            this._vehicles = await this._vehiclesService.GetVehiclesPageAsync(_pageParameters, searchBox.Text);
            this.vehicles.ItemsSource = this._vehicles;
            this.pagesInfo.Content = $"{this._vehicles.PageIndex} of {this._vehicles.TotalCount}";
        }

        private async void SaveChangesClick(object sender, RoutedEventArgs e)
        {
            try
            {
                var selectedVehicle = (Vehicle?)this.vehicles.SelectedItem;
                if (string.IsNullOrEmpty(selectedVehicle.VehicleType) || string.IsNullOrEmpty(selectedVehicle.OwnersName)
                    || string.IsNullOrEmpty(selectedVehicle.ProductionDate.ToString()) || string.IsNullOrEmpty(selectedVehicle.RegistrationNumber)
                    || string.IsNullOrEmpty(selectedVehicle.VinCode) || string.IsNullOrEmpty(selectedVehicle.Color) || string.IsNullOrEmpty(selectedVehicle.Model)
                    || string.IsNullOrEmpty(selectedVehicle.LastService.ToString()))
                {
                    throw new Exception();
                }                
               await this._vehiclesService.UpdateRangeAsync(_vehicles);
                
            }
            catch
            {
                MessageBox.Show("Fill all cells with information!", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private async void SearchButtonClick(object sender, RoutedEventArgs e)
        {
            if (searchBox.Text != string.Empty)
            {
                await this.Search(1);
            }
        }

        private async void NextPageButtonClick(object sender, RoutedEventArgs e)
        {
            if (this._vehicles.PageIndex < this._vehicles.TotalCount)
            {
                if (searchBox.Text == string.Empty)
                {

                    await this.SetPage(this._vehicles.PageIndex + 1);
                }
                else
                {
                    await this.Search(this._vehicles.PageIndex + 1);
                }
            }
        }

        private async void PrevPageButtonClick(object sender, RoutedEventArgs e)
        {
            if (this._vehicles.PageIndex > 1)
            {
                if (searchBox.Text == string.Empty)
                {

                    await this.SetPage(this._vehicles.PageIndex - 1);
                }
                else
                {
                    await this.Search(this._vehicles.PageIndex - 1);
                }
            }
        }

        private void GenerateInvitationPDF(object sender, RoutedEventArgs e)
        {
            var selectedVehicle = this.vehicles.SelectedItem as Vehicle;
            if (selectedVehicle != null)
            {
                var saveFile = new SaveFileDialog();
                saveFile.Filter = "PDF (*.pdf)|*.pdf";

                if (saveFile.ShowDialog() == true)
                {
                    var path = Path.GetFullPath(saveFile.FileName);
                    this._vehiclesService.GenerateIvitationPdf(selectedVehicle, path);
                }
            }

        }

        private async void Remove(object sender, RoutedEventArgs e)
        {
            var vehicleId = Convert.ToInt32((sender as Button)?.Tag);
            var vehicle = await this._vehiclesService.GetAsync(vehicleId);
            if (vehicle != null)
            {
                await this._vehiclesService.DeleteAsync(vehicle);
            }
            await this.SetPage(this._vehicles.PageIndex);
        }
    }
}
