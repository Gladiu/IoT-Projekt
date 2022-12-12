using DataTypes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using DesktopInterface.Control;
using System.Collections.ObjectModel;
using DesktopInterface.ViewModel;

namespace DesktopInterface
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private DataGridViewModel model;
        public MainWindow()
        {
            InitializeComponent();
            ApiHelper.InitializeClient();
            
        }

        private async Task LoadTemperatureData()
        {
            var temperature = await SenseHatDataProcessor.LoadTemperatureData();
            temperatureLabel.Content = $"Name: {temperature.name}, Unit: {temperature.defaultUnit}, Value: {temperature.value}";
        }

        private async Task LoadHumidityData()
        {
            var humidity = await SenseHatDataProcessor.LoadHumidityData();
            humidityLabel.Content = $"Name: {humidity.name}, Unit: {humidity.defaultUnit}, Value: {humidity.value}";
        }

        private async Task LoadData()
        {
            var data = await SenseHatDataProcessor.LoadData();
            dataGrid.ItemsSource = data;
        }

        private async void WindowLoaded(object sender, RoutedEventArgs e)
        {
            await LoadTemperatureData();
            await LoadHumidityData();
            await LoadData();
        }

    }
}
