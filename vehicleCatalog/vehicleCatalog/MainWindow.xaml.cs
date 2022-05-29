using System;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Shapes;
using VehicleCatalog.Application.IServices;
using VehicleCatalog.Application.Paging;
using VehicleCatalog.Core.Entities;
using VehicleCatalog.Infrastructure.EF;
using VehicleCatalog.Infrastructure.DataInitializer;
using Microsoft.Win32;
using System.IO;
using Path = System.IO.Path;
namespace vehicleCatalog
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private readonly IVehiclesService _vehiclesService;
        private PageParameters _pageParameters = new();
        private PagedList<Vehicle> _vehicles = new();
        private Vehicle _selectedvehicle = new();
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
            this._vehicles = await this._vehiclesService.GetVehiclesPageAsync(this._pageParameters);
            this.Vehicles.ItemsSource = this._vehicles;
            Paging.Content = $"{this._vehicles.PageIndex} of {this._vehicles.TotalCount}";
        }

        private async Task Search(int pageNumber)
        {
            this._pageParameters.PageIndex = pageNumber;
            this._vehicles = await this._vehiclesService.GetVehiclesPageAsync(this._pageParameters, SearchBox.Text);
            this.Vehicles.ItemsSource = this._vehicles;
            Paging.Content = $"{this._vehicles.PageIndex} of {this._vehicles.TotalCount}";
        }

        private async void NextButtonClick(object sender, RoutedEventArgs e)
        {
            if (this._vehicles.PageIndex < this._vehicles.TotalCount)
            {
                if (string.IsNullOrEmpty(SearchBox.Text))
                {
                    await this.SetPage(this._vehicles.PageIndex + 1);
                }
                else
                {
                    await this.Search(this._vehicles.PageIndex + 1);
                }
            }
        }

        private async void PrevButtonClick(object sender, RoutedEventArgs e)
        {
            if (this._vehicles.PageIndex > 1)
            {
                if (string.IsNullOrEmpty(SearchBox.Text))
                {
                    await this.SetPage(this._vehicles.PageIndex - 1);
                }
                else
                {
                    await this.Search(this._vehicles.PageIndex - 1);
                }
            }
        }

        private async void GenerateInvitation(object sender, RoutedEventArgs e)
        {
            if (_selectedvehicle != null)
            {
                var saveDialog = new SaveFileDialog();
                saveDialog.Filter = "PDF (*.pdf)|*.pdf";
                if (saveDialog.ShowDialog() == true)
                {
                    var path = Path.GetFullPath(saveDialog.FileName);
                    this._vehiclesService.GenerateIvitationPdf(this._selectedvehicle, path);
                }
            }

        }

    }
}
