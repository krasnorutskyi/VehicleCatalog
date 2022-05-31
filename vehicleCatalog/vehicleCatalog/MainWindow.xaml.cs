using Microsoft.Win32;
using System;
using System.Linq;
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

            var context = new ApplicationContext();
            DbInitializer.Initialize(context);
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

        private void Remove(object sender, RoutedEventArgs e)
        {
            var vehicleId = Convert.ToInt32((sender as Button)?.Tag);
            var vehicle = this._vehicles.FirstOrDefault(v => v.Id == vehicleId);
            if (vehicle != null)
                this._vehiclesService.DeleteAsync(vehicle);
            RefreshGrid();

        }


        private void RefreshGrid()
        {
            this.vehicles.Items.Refresh();
        }
    }
}
