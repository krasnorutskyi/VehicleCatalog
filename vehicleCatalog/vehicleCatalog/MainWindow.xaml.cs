using Microsoft.Win32;
using System;
using System.Threading.Tasks;
using System.Windows;
using System.Collections.Generic;
using System.Windows.Controls;
using VehicleCatalog.Application.IServices;
using VehicleCatalog.Application.Paging;
using VehicleCatalog.Core.Entities;
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
            this._vehicles = await this._vehiclesService.GetVehiclesPageAsync(_pageParameters, vehicleTypeBox.Text, modelBox.Text, colorBox.Text, vinCodeBox.Text, regNumberBox.Text, ownersBox.Text, prodYearBox.Text, lastServiceBox.Text);
            this.vehicles.ItemsSource = this._vehicles;
            this.pagesInfo.Content = $"{this._vehicles.PageIndex} of {this._vehicles.TotalCount}";
            
        }

        private async void SaveChangesClick(object sender, RoutedEventArgs e)
        {
            try
            {
               foreach(var vehicle in this._vehicles)
               {
                    if(String.IsNullOrEmpty(vehicle.VinCode)|| String.IsNullOrEmpty(vehicle.OwnersName)
                        || String.IsNullOrEmpty(vehicle.RegistrationNumber) || String.IsNullOrEmpty(vehicle.Color)
                        || String.IsNullOrEmpty(vehicle.Model) || String.IsNullOrEmpty(vehicle.VehicleType)
                        || String.IsNullOrEmpty(vehicle.LastService.ToString()) || String.IsNullOrEmpty(vehicle.ProductionDate.ToString()))
                    {
                        throw new Exception();
                    }
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
            var searchString = vehicleTypeBox.Text + modelBox.Text + colorBox.Text + vinCodeBox.Text + regNumberBox.Text + ownersBox.Text + prodYearBox.Text + lastServiceBox.Text;
            if (searchString != string.Empty)
            {
                await this.Search(1);
            }
            else
            {
                await this.SetPage(1);
            }
        }

        private async void NextPageButtonClick(object sender, RoutedEventArgs e)
        {
            if (this._vehicles.PageIndex < this._vehicles.TotalCount)
            {
                var searchString = vehicleTypeBox.Text + modelBox.Text + colorBox.Text + vinCodeBox.Text + regNumberBox.Text + ownersBox.Text + prodYearBox.Text + lastServiceBox.Text;
                if (searchString == string.Empty)
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
                var searchString = vehicleTypeBox.Text + modelBox.Text + colorBox.Text + vinCodeBox.Text + regNumberBox.Text + ownersBox.Text + prodYearBox.Text + lastServiceBox.Text;
                if (searchString == string.Empty)
                {

                    await this.SetPage(this._vehicles.PageIndex - 1);
                }
                else
                {
                    await this.Search(this._vehicles.PageIndex - 1);
                }
            }
        }

        private async void SaveSearchResults(object sender, RoutedEventArgs e)
        {
            if (this.vehicles.Items.Count != 0)
            {
                var saveFile = new SaveFileDialog();
                saveFile.Filter = "PDF (*.pdf)|*.pdf";
                var pageParams = new PageParameters();
                var searchedVehicles = new List<Vehicle>();
                for(int i = 1; i <= this._vehicles.TotalCount; i++)
                {
                    pageParams.PageIndex = i;
                    searchedVehicles.AddRange(await this._vehiclesService.GetVehiclesPageAsync(_pageParameters, vehicleTypeBox.Text, modelBox.Text, colorBox.Text, vinCodeBox.Text, regNumberBox.Text, ownersBox.Text, prodYearBox.Text, lastServiceBox.Text));
                }
                if (saveFile.ShowDialog() == true)
                {
                    var path = Path.GetFullPath(saveFile.FileName);
                    this._vehiclesService.SaveSearchResults(searchedVehicles, path);
                }
            }
        }

        private void GenerateInvitationPDF(object sender, RoutedEventArgs e)
        {
            var selectedVehicles = this.vehicles.SelectedItems;
            if (selectedVehicles != null)
            {
                var vehicles = new List<Vehicle>();
                foreach (var vehicle in selectedVehicles)
                {
                    vehicles.Add((Vehicle)vehicle);
                }

                var saveFile = new SaveFileDialog();
                saveFile.Filter = "PDF (*.pdf)|*.pdf";

                if (saveFile.ShowDialog() == true)
                {
                    var path = Path.GetFullPath(saveFile.FileName);
                    this._vehiclesService.GenerateIvitationPdf(vehicles, path);
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
