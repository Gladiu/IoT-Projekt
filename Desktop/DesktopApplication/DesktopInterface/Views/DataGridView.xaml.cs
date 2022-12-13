using Caliburn.Micro;
using DataTypes;
using DesktopInterface.Control;
using DesktopInterface.ViewModels;
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
using System.Windows.Shapes;

namespace DesktopInterface.Views
{
    /// <summary>
    /// Interaction logic for DataGridView.xaml
    /// </summary>
    public partial class DataGridView : Window
    {
        private DataGridViewModel dataGridViewModel = new DataGridViewModel();

        public DataGridView()
        {
            InitializeComponent();
            ApiHelper.InitializeClient();
            DataContext = dataGridViewModel;
        }

        private async Task LoadData()
        {
            var data = await SenseHatDataProcessor.LoadData();
            var dataStructs = new BindableCollection<DataStruct>(data);
            if (dataStructs != null) 
            {
                dataGridViewModel.DataGrid = dataStructs;
                DataContext = dataGridViewModel;
            }
        }

        private async void Button_Click(object sender, RoutedEventArgs e)
        {
            await LoadData();
        }
    }
}
